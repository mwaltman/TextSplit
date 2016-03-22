using System;
using System.Collections.Generic;
using System.IO;

namespace TextSplit
{
    public class LogFile
    {
        private string logfilePath;
        private float storageDays;
        private Queue<LogFileEntry> logfileQueue;
        public string delimiter = "";

        public LogFile(string logfilePath, float storageDays) {
            this.logfilePath = logfilePath;
            this.storageDays = storageDays;
            // Reads the logfile at logfilePath and stores it into the logfileQueue
            logfileQueue = new Queue<LogFileEntry>();
            ReadFromFile(logfilePath);
        }

        public void AddEntry(string message) {
            // Removes all elements at the beginning of the queue that have been in the logfile for more than storageDays days
            RemoveOldEntries();
            // Adds a new LogFileEntry at the end
            logfileQueue.Enqueue(new LogFileEntry(message));
            // Prints the current values of logfileEntries to the file logfileName, overwriting the current logfile in the process
            string entries = "";
            foreach (LogFileEntry entry in logfileQueue) {
                entries += entry.GetMessage() + Environment.NewLine;
            }
            File.WriteAllText(logfilePath, entries);
        }

        private void ReadFromFile(string logfilePath) {
            try {
                string[] entries = File.ReadAllLines(logfilePath);
                for (int i = 0; i < entries.Length; i++) {
                    AddEntryFromFile(entries[i]);
                }
            } catch (IOException) { } // If no logfile exists yet: do nothing
        }

        private void RemoveOldEntries() {
            try {
                while ((logfileQueue.Peek()).entryDate <= DateTime.Now.AddDays(-storageDays)) {
                    logfileQueue.Dequeue();
                }
            } catch (InvalidOperationException) { } // If the queue is empty
        }

        private void AddEntryFromFile(string fullMessage) {
            string delimiter = " : ";
            string message1 = fullMessage.Remove(fullMessage.IndexOf(delimiter));
            string message2 = fullMessage.Substring(fullMessage.IndexOf(delimiter) + delimiter.Length);
            DateTime date = DateTime.ParseExact(message1, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            logfileQueue.Enqueue(new LogFileEntry(message2, date));
        }

        private class LogFileEntry
        {
            public DateTime entryDate;
            private string entryMessage;

            public LogFileEntry(string entryMessage, DateTime entryDate) {
                this.entryMessage = entryMessage;
                this.entryDate = entryDate;
            }

            public LogFileEntry(string entryMessage) {
                this.entryMessage = entryMessage;
                entryDate = DateTime.Now;
            }

            public string GetMessage() {
                return entryDate.ToString("dd-MM-yyyy HH:mm:ss") + " : " + @entryMessage;
            }
        }
    }
}

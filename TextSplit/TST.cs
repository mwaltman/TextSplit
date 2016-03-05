using System;
using System.Collections;
using System.Drawing;
using System.Runtime.Serialization;

namespace TextSplit
{
    // The object used for storing the .tst file
    [Serializable()]
    public class TextSplitText : ISerializable
    {
        public ArrayList TextList { get; set; } // The texts on each slide
        public Font TextFont { get; set; } // The font of the text (name, size, style)
        public Color[] Colors { get; set; } // { font color, background color }
        public int[] Size { get; set; } // Size of the inner window { width, height }
        public int[] Margins { get; set; } // Space between inner window and textbox { Left, Right, Up, Down }
        public string SyncTxtPath { get; set; } // The path to the synced txt file, otherwise it is null
        public string SyncDelimiterText { get; set; } // The delimiter text if syncing isenabled, otherwise it is null
        public bool SyncAutoSave { get; set; } // Whether auto save on sync is enabled

        public TextSplitText() {
        }

        public TextSplitText(TextSplitText other) {
            TextList = other.TextList;
            TextFont = other.TextFont;
            Colors = other.Colors;
            Size = other.Size;
            Margins = other.Margins;
            SyncTxtPath = other.SyncTxtPath;
            SyncDelimiterText = other.SyncDelimiterText;
            SyncAutoSave = other.SyncAutoSave;
        }

        public TextSplitText(SerializationInfo info, StreamingContext ctxt) {
            // v1.1 Attributes
            TextList =              (ArrayList)info.GetValue(   "List",                 typeof(ArrayList));
            TextFont =              (Font)info.GetValue(        "Font",                 typeof(Font));
            Colors =                (Color[])info.GetValue(     "Colors",               typeof(Color[]));
            Size =                  (int[])info.GetValue(       "Size",                 typeof(int[]));

            // v1.4 Attributes
            try {
                Margins =           (int[])info.GetValue(       "Margins",              typeof(int[]));
            } catch (Exception) { Margins = new int[] { 5, 5, 5, 5 }; }

            // v2.0 Attributes
            try {
                SyncTxtPath =       (string)info.GetValue(      "SyncTxtPath",          typeof(string));
            } catch (Exception) { SyncTxtPath = null; }
            try {
                SyncDelimiterText = (string)info.GetValue(      "SyncDelimiterText",    typeof(string));
            } catch (Exception) { SyncDelimiterText = null; }
            try {
                SyncAutoSave =      (bool)info.GetValue(        "SyncAutoSave",         typeof(bool));
            } catch (Exception) { SyncAutoSave = false; }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) {
            info.AddValue("List",               TextList);
            info.AddValue("Font",               TextFont);
            info.AddValue("Colors",             Colors);
            info.AddValue("Size",               Size);
            info.AddValue("Margins",            Margins);
            info.AddValue("SyncTxtPath",        SyncTxtPath);
            info.AddValue("SyncDelimiterText",  SyncDelimiterText);
            info.AddValue("SyncAutoSave",       SyncAutoSave);
        }

        public void SetEmpty() {
            TextList = new ArrayList();
            TextList.Add("Type your text in this window.");
            Size = new int[] { 284, 262 };
            Margins = new int[] { 5, 5, 5, 5 };
            Globals.Themes["Standard"].Apply(this, false);
            SyncTxtPath = null;
            SyncDelimiterText = "----------";
            SyncAutoSave = false;
        }
    }
}

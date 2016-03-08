using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TextSplit
{
    // Class used for serializing and de-serializing between .tst files and TextSplitText objects
    public class Serializer
    {
        public Serializer() {
        }

        public void SerializeObject(string filename, TextSplitText objectToSerialize) {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public TextSplitText DeSerializeObject(string filename) {
            TextSplitText objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (TextSplitText)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}

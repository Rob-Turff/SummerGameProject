using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace SummerGameProject.Src.Common.Utilities
{
    public static class SerializationHandler
    {
        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] bytes)
        {
            var memStream = new MemoryStream(bytes);
            var bf = new BinaryFormatter();
            var obj = bf.Deserialize(memStream);
            return obj;
        }
    }
}

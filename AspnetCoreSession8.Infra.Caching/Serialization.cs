using Newtonsoft.Json;
using System;
using System.IO;

namespace AspnetCoreSession8.Infra.Caching
{
    internal static class Serialization
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            //BinaryFormatter binaryFormatter = new BinaryFormatter();
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //binaryFormatter.Serialize(memoryStream, obj);
            //return memoryStream.ToArray();
            return System.Text.Encoding.UTF8.GetBytes( JsonConvert.SerializeObject(obj));
                //BinaryWriter br = new BinaryWriter(memoryStream);
                //return br.ToByteArray();
            //}
        }
        public static T FromByteArray<T>(this byte[] byteArray) where T : class
        {
            if (byteArray == null)
            {
                return default(T);
            }
            //BinaryFormatter binaryFormatter = new BinaryFormatter();

            //using (MemoryStream memoryStream = new MemoryStream(byteArray))
            //{
                string s = System.Text.Encoding.UTF8.GetString(byteArray);
                return JsonConvert.DeserializeObject<T>(s);
                //BinaryReader br = new BinaryReader(memoryStream);
                //br.
                //return binaryFormatter.Deserialize(memoryStream) as T;
            //}
        }

    }
}

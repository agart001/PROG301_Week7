using Newtonsoft.Json;
using PROG301_Week7.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PROG301_Week7
{
    public static class UtilSerializer
    {
        public static T XmlDeserialize<T>(this string toDeserialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader textReader = new StringReader(toDeserialize))
            {
                T? cast = (T)xmlSerializer.Deserialize(textReader)!;
                if (cast == null) { throw new Exception("Deserialize Null"); }
                return cast;
            }
        }
        public static string XmlSerialize<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
        public static T JsonDeserialize<T>(this string toDeserialize)
        {
            T? cast = JsonConvert.DeserializeObject<T>(toDeserialize);
            if (cast == null) { throw new Exception("Json Deserialize Null"); }
            return cast;
        }

        public static string JsonSerialize<T>(this T toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize);
        }

        public static void SaveFile(string dir, string filename, string extension, string contents)
        {
            string cur = Directory.GetCurrentDirectory();
            string file = $"{filename}.{extension}";
            string path = Path.Combine(cur, dir, file);
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(contents);
                }
            }
        }

        public static SerializedFile GetFile(string dir, string filename, string extension) 
        {
            string cur = Directory.GetCurrentDirectory();
            string file = $"{filename}.{extension}";
            string path = Path.Combine(cur, dir, file);
            string contents;

            if(File.Exists(path))
            {
                contents = File.ReadAllText(path);
            }
            else { throw new Exception("Serialized File NULL"); }

            return new SerializedFile(filename, extension, contents);
        }
    }
}

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Util
{
    public class Serializer
    {
        public static string toXMLString(object what, Type T)
        {
            var serializer = new XmlSerializer(T);
            var sb = new StringBuilder();
            TextWriter writer = new StringWriter(sb);
            serializer.Serialize(writer, what);            
            writer.Close();
            return sb.ToString();
        }

        public static object fromXMLString(string xmlString, Type T)
        {
            var serializer = new XmlSerializer(T);
            object result;

            TextReader reader = new StringReader(xmlString);
            
            result = serializer.Deserialize(reader);

            return result;
        }

        public static void SaveXMLString(string where, object what, Type T)
        {
            UnityEngine.PlayerPrefs.SetString(where,toXMLString(what,T));
        }

        public static object LoadXMLString(string where, Type T)
        {
            string st = UnityEngine.PlayerPrefs.GetString(where);
            UnityEngine.Debug.Log("loaded " + where + ":" + st);
            return fromXMLString(st,T);
        }


        public static void saveXMLFile(string path, object what, Type T)
        {
            var serializer = new XmlSerializer(T);

            var stream = new FileStream(path, FileMode.Create);

            serializer.Serialize(stream, what);

            stream.Close();
        }

        public static object loadXMLFile(string path, object what, Type T)
        {
            var serializer = new XmlSerializer(T);

            var stream = new FileStream(path, FileMode.Open);

            var container = serializer.Deserialize(stream);

            stream.Close();

            return container;
        }
    }
}
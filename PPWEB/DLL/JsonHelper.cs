using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;

namespace PPWEB.DLL
{
    /// <summary>
    ///JsonHelper 的摘要说明
    /// </summary>
    public static class JsonHelper
    {
        //转换为Json格式输出
        public static string ToJson(this object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            Stream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            stream.Position = 0;
            StreamReader streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}
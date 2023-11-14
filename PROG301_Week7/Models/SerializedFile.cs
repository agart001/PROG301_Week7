using PROG301_Week7.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG301_Week7.Models
{
    public class SerializedFile : IFile
    {
        public string? Name {  get; set; }

        public string? Extension { get; set; }

        public string? Contents { get; set; }

        public Type? SerializeType { get; set; }

        public SerializedFile() { }

        public SerializedFile(string name, string extension, string contents, Type? serializeType)
        { Name = name; Extension = extension; Contents = contents; SerializeType = serializeType; }

        public SerializedFile(string name, Type? serializeType) 
        { Name = name; SerializeType = serializeType; }

        public SerializedFile(string name, string extension, Type? serializeType)
        { Name = name; Extension = extension; SerializeType = serializeType; }

        public SerializedFile(string name, string extension, string contents) 
        { Name = name; Extension = extension; Contents = contents; }

        public void SetContents(string contents) => Contents = contents;

        public void SetExtension(string extension) => Extension = extension;
    }
}

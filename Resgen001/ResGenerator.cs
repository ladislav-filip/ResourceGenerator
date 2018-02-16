using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Resources.Tools;

namespace Resgen001
{
    public class ResGenerator
    {
        public string ResxFileName { get; }

        public string DesignerFileName
        {
            get
            {
                var ext = Path.GetExtension(ResxFileName);
                var result = ResxFileName.Replace(ext, ".Designer.cs");
                return result;
            }
        }

        public string NameSpace { get; set; }

        public string BaseName { get; set; }

        public string ResourceNameSpace { get; set; }

        private readonly IDictionary<string, string> m_dictionary;
        private readonly string m_fileCsv;

        public ResGenerator(string resxFileName)
        {
            ResxFileName = resxFileName;
        }

        public ResGenerator(string resxFileName, IDictionary<string, string> dictionary) : this(resxFileName)
        {
            m_dictionary = dictionary;
        }

        public ResGenerator(string resxFileName, string fileCsv) : this(resxFileName)
        {
            m_fileCsv = fileCsv;
        }

        public void Start()
        {
            CreateResXFile();

            var sw = new StreamWriter(DesignerFileName);
            var provider = new CSharpCodeProvider();
            var code = StronglyTypedResourceBuilder.Create(
                ResxFileName, // resxFile
                BaseName, // baseName
                NameSpace, // generatedCodeNamespace
                ResourceNameSpace, // resourcesNamespace
                provider, // codeProvider
                false, // internalClass
                out string[] errors);

            if (errors.Length > 0)
                foreach (var error in errors)
                    Console.WriteLine(error);

            provider.GenerateCodeFromCompileUnit(code, sw, new CodeGeneratorOptions());
            sw.Close();
        }

        private void CreateResXFile()
        {
            using (var rw = new ResXResourceWriter(ResxFileName))
            {
                foreach (var d in m_dictionary)
                {
                    rw.AddResource(d.Key, d.Value);
                }
                rw.Generate();
                rw.Close();
            }
        }
    }
}
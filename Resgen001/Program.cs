using System;
using System.Collections.Generic;
using System.IO;

namespace Resgen001
{
    /// <summary>
    /// Vygenerované soubory se automaticky přepíšou do tohoto projektu.
    /// Je tedy možné po dalším spuštění přistupovat přímo na resource, přidané při generování.
    /// </summary>
    public class Example
    {
        public static void Main()
        {
            Console.WriteLine(Fullsys.FIS.Core.Resources.Common.Entity.Ptpvz.Car);

            var dict = new Dictionary<string, string>()
            {
                { "Car", "Auto" },
                { "Plane", "Letadlo" }
            };

            var dstDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine(dstDir);
            dstDir = dstDir.Substring(0, dstDir.IndexOf("bin"));
            var resxFile = Path.Combine(dstDir, "Ptpvz.resx");
            Console.WriteLine(resxFile);

            var generator = new ResGenerator(resxFile, dict);
            generator.NameSpace = "Fullsys.FIS.Core.Resources.Common.Entity";
            generator.BaseName = "Ptpvz";
            generator.ResourceNameSpace = "Resgen001";
            Console.WriteLine(generator.DesignerFileName);
            generator.Start();

            Console.WriteLine("Finnish.");
            Console.ReadKey();
        }
    }
}

using Mono.Options;
using System;
using System.Collections.Generic;
using System.IO;
using XmlConfigMinifier.UIs.XmlConfigMinifier;

namespace XmlConfigMinifier.UIs.ConsoleApp
{
    class Program
    {


        static void Main(string[] args)
        {
            int verbosity = 0;
            var includeWhitespaces = true;
            var indentation = 1;
            bool showHelp = false;
            bool removeComments = true;
            List<string> fileNames = new List<string>();
            int repeat = 1;

            var p = new OptionSet() {
                { "file|f=", "Path to Xml config file/s to minify. For multiple files use double quotes and separate with commas.",
                  (string v) => fileNames.AddRange (v.Split(',')) },
                { "whitespace|w", "Remove whitespaces.",
                  v => includeWhitespaces = !(v!=null) },
                { "indentation|i=", "config indentation. Default == 1.",
                  (int v) => indentation = v },
                { "v|verbose", "increase debug message verbosity",
                    v => { if (v != null) verbosity=1; } },
                { "h|help",  "show this message and exit", 
                  v => showHelp = v != null },
                { "c|comments",  "remove comments (true|false). Default == true.", 
                  v => removeComments = v != null },
            };

            try
            {
                var extra = p.Parse(args);
                Debug(verbosity, "We've received some extra parameters : {0}", string.Concat(extra));
            }
            catch (OptionException e)
            {
                Console.Write("That didn't work: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try -help' for more information.");
                return;
            }

            if (showHelp)
            {
                ShowHelp(p);
                return;
            }

            foreach (string fileName in fileNames)
            {
                for (int i = 0; i < repeat; ++i)
                {
                    Console.WriteLine("Starting to minify {0}", fileName);
                    var xmlString = string.Empty;
                    try
                    {
                        xmlString = File.ReadAllText(fileName);
                        var xmlStringMinified = XmlConfigMinifierCore.Minify(xmlString, indentation, removeComments, includeWhitespaces);
                        File.WriteAllText(fileName, xmlStringMinified);
                        Console.WriteLine("Minification finished successfully");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Cannot minify  because of an exception : {0}", e.Message);
                    }
                }
            }
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("This tool is used to minify web.config file.");
            Console.WriteLine("Use options to achieve custom indention, remove whitespaces, comments etc.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

        static void Debug(int verbosity, string format, params object[] args)
        {
            if (verbosity > 0)
            {
                Console.Write("# ");
                Console.WriteLine(format, args);
            }
        }
    }
}

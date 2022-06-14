using System.Xml;
using System;
using System.IO;
using System.Diagnostics;

namespace ZenProgramming.DotNetGetVersion.Cli
{
    /// <summary>
    /// Main class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">Arguments</param>
        static void Main(string[] args)
        {
            //Se non ho un argomento, usage e uscita
            if (args.Length != 1) 
            {
                //Usage
                Console.WriteLine("Warning! Invalid dotnet project file name");
                Console.WriteLine();
                Console.WriteLine("Usage   : dotnet-get-version [path-to-csproj-file-name]");
                Console.WriteLine();
                Console.WriteLine("Example : dotnet-get-version ./SomeFolder/Sample.csproj");
                Environment.Exit(1);
                return;
            }

            //Nome del file in ingresso
            string fileName = args[0];
            Debug.WriteLine($"Working with file '{fileName}'...");

            //Se il file non esiste, esco
            if (!File.Exists(fileName))
            {
                //Segnalo l'errore ed esco
                Console.WriteLine($"File '{fileName}' does non exists on file system. Exiting...");
                Environment.Exit(1);
                return;
            }

            //Leggo il contenuto XML del file
            string xmlContent = File.ReadAllText(fileName);

            //Caricamento del documento XML
            Debug.WriteLine($"Loading XML content...");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);

            //Se il primo figlio del documento non è "Project", esco
            if (doc.FirstChild == null || doc.FirstChild.Name != "Project") 
            {
                //Segnalo l'errore ed esco
                Console.WriteLine($"File '{fileName}' is invalid C# Project file. Exiting...");
                Environment.Exit(1);
                return;
            }

            //Nodo con le proprietà del progetto
            Debug.WriteLine($"Found 'Project' XML tag...");

            //List of nodes with "PropertyGroup"
            var propertyGroups = CsprojUtils.FilterPropertyGroupNodes(doc.FirstChild.ChildNodes);

            //If no nodes, exit
            if (propertyGroups.Count == 0)
            {
                //Segnalo l'errore ed esco
                Console.WriteLine($"C# Project file does not have any 'PropertyGroup' tag. Exiting...");
                Environment.Exit(1);
                return;
            }

            //Node with version
            XmlNode versionNode = null;

            //Iterate every possibile node
            foreach (var current in propertyGroups) 
            {
                //Check if currentgroup has "Version"
                versionNode = CsprojUtils.GetFirstVersionNode(current.ChildNodes);
                if (versionNode != null)
                    break;
            }

            //Se non ho trovato il nodo, esco
            if (versionNode == null)
            {
                //Segnalo l'errore ed esco
                Console.WriteLine($"C# Project file does not have 'Version' tag. Exiting...");
                Environment.Exit(1);
                return;
            }

            //Ritorno il valore del tag
            Debug.WriteLine($"Found 'Version' with value '{versionNode.InnerText}'!");
            Console.WriteLine(versionNode.InnerText);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TestAssignment1._1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            string tableTitles = "";
            string item = "";
            string line = "";
            int a = 0;
            int b = 0;
            int c = 0;

            Console.WriteLine(@"  __  ____  __ _       _           ____ ______     __");
            Console.WriteLine(@"  \ \/ /  \/  | |     | |_ ___    / ___/ ___\ \   / /");
            Console.WriteLine(@"   \  /| |\/| | |     | __/ _ \  | |   \___ \\ \ / / ");
            Console.WriteLine(@"   /  \| |  | | |___  | || (_) | | |___ ___) |\ V /  ");
            Console.WriteLine(@"  /_/\_\_|  |_|_____|  \__\___/   \____|____/  \_/   ");
            Console.WriteLine();
            Console.WriteLine();
            //ASCII selcome graphic

            Console.WriteLine("Type xml file path and press Enter.");
            String filepath = Console.ReadLine();
            //filepath to xml file entered in console

            XmlDocument xdoc = new XmlDocument();
            //creating xmldocument

            System.Threading.Thread.Sleep(50);
            try
            {
                xdoc.Load(filepath);
                //loading from xml file
            }

            catch
            {
                Console.WriteLine(".xml file not found! Press Enter to exit program.");
                Console.ReadLine();
                System.Environment.Exit(1);
                //If file was not found
            }

            string exfilepath = filepath.Replace(".xml", ".csv");
            //replacing extension for saving later

            string xmlString = xdoc.InnerXml;
            //parsing into string

            Console.WriteLine();
            Console.WriteLine("Successfuly loaded xml file.");
            Console.WriteLine();

            var csv = new StringBuilder();
            
            
            List<string> colTitles = new List<string> { };
            //list for storing column titles

            XmlNodeList elemList = xdoc.GetElementsByTagName("column");
            //storing node data into list

            for (int i = 0; i < elemList.Count; i++)
            {
                
                item = (elemList[i].Attributes["name"].Value + ", ");
                //geting node attributes (column names)
               
                colTitles.Add(item);
                //adding column name into list
            }

            IEnumerable<string> distinctColTitles = colTitles.Distinct();
            //distinctining unique column names

            foreach (string title in distinctColTitles)
            {
                a++;
                tableTitles = (tableTitles + title);
                //storing column names in a string
            }

            tableTitles = tableTitles.Remove(tableTitles.Length - 2);
            //removing comma in column names string

            Console.WriteLine("Created column names.");
            csv.Append(tableTitles);
            //adding column names to csv

            csv.Append(Environment.NewLine);
            Console.WriteLine();
            Console.WriteLine("Writing data");
            
            for (int i = 0; i < elemList.Count; i++)
            {
                if (b < a-1)
                {
                        line = line + ("\"" + elemList[i].InnerText + "\", ");
                        b++;
                }
                else
                {
                    line = line + ("\"" + elemList[i].InnerText + "\"");
                    string newLine = string.Format("{0}{1}", line, Environment.NewLine);
                    Console.Write(".");
                    csv.Append(newLine);
                    b = 0;
                    line = "";
                }
            }
            Console.WriteLine("Done!");
            File.WriteAllText(exfilepath, csv.ToString());
            System.Threading.Thread.Sleep(200);
            Console.WriteLine();
            Console.WriteLine("File saved at: " + exfilepath);
            System.Threading.Thread.Sleep(100);
            Console.WriteLine();
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
}


       


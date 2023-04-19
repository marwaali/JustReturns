using System.Collections.Generic;
using System.IO;

namespace Task.Utlities
{
    public class FileHundler
    {
        public static void AppendCSVData(string data, string path)
        {
            if(File.Exists(path))
            {
                File.AppendAllText(path, data);
            }

        }

        //public static void WriteDataToNotePade(string path, string upc, string description, List<string> highlights, List<string> specs )
        public static void WriteDataToNotePade(string path, string upc, List<string> specs )
        {
            //TextWriter desc = new StreamWriter(path + "\\Desc.txt");
            //desc.WriteLine(description);
            //desc.Close();
            TextWriter spec = new StreamWriter(path + "\\specs.txt");
            foreach(string s in specs)
            {
                spec.WriteLine(s);
            }
            spec.Close();
            //TextWriter high = new StreamWriter(path + "\\highlights.txt");
            //foreach (string h in highlights)
            //{
             //   high.WriteLine(h);
            //}
            // high.Close();
        }

    }
}

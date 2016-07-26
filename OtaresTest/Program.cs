using Otares;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OtaresTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Defines the various file paths
            string output = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + "songs.txt";
            string inputDir = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + Path.DirectorySeparatorChar + "_Serato_";
            string dbFile = inputDir + Path.DirectorySeparatorChar + "database V2";

            //Reads all the text from the database file
            string content = File.ReadAllText(dbFile);

            //Creates the new database
            OtaresDb db = new OtaresDb(content);

            //Outputs the new database to a file on the desktop
            string superString = "";
            foreach (var s in db.Songs)
                superString += s.ToJSON() + Environment.NewLine;
            File.WriteAllText(output, superString);
        }
    }
}

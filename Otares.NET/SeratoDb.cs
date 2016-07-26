using System;
using System.Collections.Generic;
using System.IO;

namespace Otares
{
    public class OtaresDb
    {
        //CONSTRUCTORS

        public OtaresDb() : this(null) { }

        public OtaresDb(string dbContent)
        {
            initDatabase(dbContent);
        }
        
        
        //PUBLIC PROPERTIES
        
        public List<SongModel> Songs { get; } = new List<SongModel>();

        public List<CrateModel> Crates { get; } = new List<CrateModel>();


        //PRIVATE METHODS

        private void initDatabase(string content)
        {
            //Splits the content by entries
            var entries = content.Split(new string[] { "otrk" }, StringSplitOptions.None);

            //Builds a SongModel from each entry in the database
            for (int i = 1; i < entries.Length; i++)
                Songs.Add(new SongModel(entries[i]));

            //TODO: Add support for crates
        }
    }
}

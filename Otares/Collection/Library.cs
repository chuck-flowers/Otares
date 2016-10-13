using System;
using System.Collections.Generic;
using System.IO;

namespace Otares.Collection
{
    public class Library
    {
        //CONSTRUCTORS

        public Library() : this(null) { }

        public Library(string dbContent)
        {
            initDatabase(dbContent);
        }
        
        
        //PUBLIC PROPERTIES
        
        public List<Song> Songs { get; } = new List<Song>();

        public List<Crate> Crates { get; } = new List<Crate>();


        //PRIVATE METHODS

        private void initDatabase(string content)
        {
            //Splits the content by entries
            var entries = content.Split(new string[] { "otrk" }, StringSplitOptions.None);

            //Builds a SongModel from each entry in the database
            for (int i = 1; i < entries.Length; i++)
                Songs.Add(Song.Create(entries[i]));

            //TODO: Add support for crates
        }
    }
}

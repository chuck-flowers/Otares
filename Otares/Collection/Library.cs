using System;
using System.Collections.Generic;

namespace Otares.Collection
{
    public class Library
    {
        /// <summary>
        /// Default constructor for the library
        /// </summary>
        public Library() : this(null) { }

        /// <summary>
        /// Constructor that takes the contents of the databaseV2 file as its parameter
        /// </summary>
        /// <param name="dbContent">The string content of the databaseV2 file</param>
        public Library(string dbContent)
        {
            initDatabase(dbContent);
        }
        
        
        /// <summary>
        /// The collection of songs within the library
        /// </summary>
        public List<Song> Songs { get; } = new List<Song>();

        /// <summary>
        /// The collection of crates contained within the library
        /// </summary>
        public List<Crate> Crates { get; } = new List<Crate>();


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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Otares
{
    public class CrateModel
    {
        private const string CRATE_FIELD = "osrt";
        
        //CONSTRUCTOR

        public CrateModel(string name, string content, OtaresDb context)
        {
            Name = name;

            //Normalizes the input
            int newStart = content.IndexOf(CRATE_FIELD);
            content = content.Substring(newStart + CRATE_FIELD.Length);

            //Breaks the input into two strings
            int trackStart = content.IndexOf("otrk");

            if (trackStart == -1)
                return;

            var columnString = content.Remove(trackStart);
            var trackString = content.Substring(trackStart);

            //Parse the columns
            parseColumns();

            //Parse the members of the crate
            parseTracks(trackString, context);
        }


        //PUBLIC METHODS

        public override string ToString() => Name;


        //PUBLIC PROPERTIES

        public string Name { get; set; }

        public List<SongModel> Songs { get; } = new List<SongModel>();


        //PRIVATE METHODS

        private void parseTracks(string content, OtaresDb context)
        {
            //Gets each individual track
            IEnumerable<string> tracks = content.Split(new string[] { "otrk" }, StringSplitOptions.RemoveEmptyEntries);

            //Removes the first 12 characters from each string
            tracks = tracks.Select(str => str.Substring(12));

            //Eliminates the blank characters between each UTF-8 string
            tracks = tracks.Select(str =>
            {
                var newStr = "";
                for (int i = 0; i < str.Length; i++)
                    if (i % 2 == 1) newStr += str[i];
                return newStr;
            });

            //Adds each of the specified files to the crate
            foreach(var path in tracks)
                Songs.Add(context.Songs.Find(s => s.Path == path));
        }

        private void parseColumns()
        {

        }
    }
}

using System;
using System.Collections.Generic;

namespace Otares.Collection
{
    public class Song
    {
        //FIELD LABEL CONSTANTS
        private const string TYPE_FIELD = "ttyp";
        private const string PATH_FIELD = "pfil";
        private const string SONG_FIELD = "tsng";
        private const string ARTIST_FIELD = "tart";
        private const string ALBUM_FIELD = "talb";
        private const string GENRE_FIELD = "tgen";
        private const string LENGTH_FIELD = "tlen";
        private const string SIZE_FIELD = "tsiz";
        private const string BITRATE_FIELD = "tbit";
        private const string SAMPLE_RATE_FIELD = "tsmp";
        private const string BPM_FIELD = "tbpm";
        private const string COMMENT_FIELD = "tcom";
        private const string GROUP_FIELD = "tgrp";
        private const string tlbl = "tlbl";
        private const string COMPOSER_FIELD = "tcmp";
        private const string YEAR_FIELD = "ttyr";
        private const string tadd = "tadd"; //Date Added?
        private const string KEY_FIELD = "tkey";
        private const string uadd = "uadd"; //Date Added?
        private const string utkn = "utkn"; //Token?
        private const string ulbl = "ulbl"; //Label?
        private const string utme = "utme"; //Track Time?
        private const string udsc = "udsc";
        private const string sbav = "sbav"; //...Audio Volume?
        private const string bhrt = "bhrt";
        private const string MISSING_FIELD = "bmis";
        private const string PLAYED_FIELD = "bply";
        private const string blop = "blop";
        private const string bitu = "bitu";
        private const string bovc = "bovc";
        private const string bcrt = "bcrt"; //Corrupted?
        private const string biro = "biro";
        private const string bwlb = "bwlb"; //White Label?
        private const string bwll = "bwll";
        private const string buns = "buns";
        private const string bbgl = "bbgl";
        private const string bkrk = "bkrk";

        static string[] labels = new string[]
        {
            TYPE_FIELD,
            PATH_FIELD,
            SONG_FIELD,
            ARTIST_FIELD,
            ALBUM_FIELD,
            GENRE_FIELD,
            LENGTH_FIELD,
            SIZE_FIELD,
            BITRATE_FIELD,
            SAMPLE_RATE_FIELD,
            BPM_FIELD,
            COMMENT_FIELD,
            GROUP_FIELD,
            tlbl,
            COMPOSER_FIELD,
            YEAR_FIELD,
            tadd,
            KEY_FIELD,
            uadd,
            utkn,
            ulbl,
            utme,
            udsc,
            sbav,
            bhrt,
            MISSING_FIELD,
            PLAYED_FIELD,
            blop,
            bitu,
            bovc,
            bcrt,
            biro,
            bwlb,
            bwll,
            buns,
            bbgl,
            bkrk
        };

        //STATIC METHODS
        public static Song Create(string dbString)
        {
            Song toRet = new Song();

            for (int farthestReached = 0; farthestReached < dbString.Length;)
            {
                int startOfString = -1;
                int endOfString = -1;
                
                //Gets the start index
                foreach(var l in labels)
                {
                    startOfString = dbString.IndexOf(l, farthestReached);

                    if (startOfString >= 0)
                    {
                        farthestReached = startOfString + l.Length;
                        break;
                    }
                }

                //If a field was not found, there are no more fields
                if (startOfString == -1)
                    break;

                //Gets the end index
                foreach(var l in labels)
                {
                    endOfString = dbString.IndexOf(l, farthestReached);

                    if (startOfString >= 0)
                        break;
                }

                //If no next field was found, this is the last field
                if (endOfString == -1)
                    endOfString = dbString.Length;

                //Updates the farthest reached
                farthestReached = endOfString;

                //Isolates the chunk of string to process and cuts out the field and value
                string fieldValueString = dbString.Substring(startOfString, endOfString - startOfString);
                string fieldLabel = fieldValueString.Remove(4);
                string rawString = fieldValueString.Substring(fieldLabel.Length);

                //Performs different actions based off the type of field
                switch(fieldLabel[0])
                {
                    case 'p':
                    case 't':
                        string stringValue = "";
                        foreach(char c in rawString)
                        {
                            if (c == 0)
                                continue;
                            stringValue += c;
                        }

                        assignStringField(toRet, fieldLabel, stringValue);
                        break;
                }
            }

            return toRet;
        }

        private static void assignStringField(Song s, string f, string v)
        {
            switch (f)
            {
                case TYPE_FIELD:
                    s.Type = v;
                    break;
                case PATH_FIELD:
                    s.Path = v;
                    break;
                case SONG_FIELD:
                    s.Title = v;
                    break;
                case ARTIST_FIELD:
                    s.Artist = v;
                    break;
                case ALBUM_FIELD:
                    s.Album = v;
                    break;
                case GENRE_FIELD:
                    s.Genre = v;
                    break;
                case LENGTH_FIELD:
                    s.Length = v;
                    break;
                case SIZE_FIELD:
                    s.Size = v;
                    break;
                case BITRATE_FIELD:
                    s.Bitrate = v;
                    break;
                case SAMPLE_RATE_FIELD:
                    s.SampleRate = v;
                    break;
                case BPM_FIELD:
                    s.Bpm = v;
                    break;
                case COMMENT_FIELD:
                    s.Comment = v;
                    break;
                case GROUP_FIELD:
                    s.Group = v;
                    break;
                case tlbl:
                    break;
                case COMPOSER_FIELD:
                    s.Composer = v;
                    break;
                case YEAR_FIELD:
                    s.Year = v;
                    break;
                case tadd:
                    break;
                case KEY_FIELD:
                    s.Key = v;
                    break;
            }
        }
        

        //CONSTRUCTOR
        private Song() { }


        //PUBLIC PROPERTIES
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Length { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public string Bitrate { get; set; }
        public string SampleRate { get; set; }
        public string Bpm { get; set; }
        public string Comment { get; set; }
        public string Group { get; set; }
        public string Composer { get; set; }
        public string Year { get; set; }
        public string Key { get; set; }
        public bool IsMissing { get; set; }
        public bool IsPlayed { get; set; }
        
        
        //PUBLIC METHODS

        public override string ToString()
        {
            const int fieldWidth = 25;

            var songString = $"{(Title.Length > fieldWidth ? Title.Remove(fieldWidth - 3) + "..." : Title)}";
            var artistString = $"{(Artist.Length > fieldWidth ? Artist.Remove(fieldWidth - 3) + "..." : Artist)}";
            var albumString = $"{(Album.Length > fieldWidth ? Album.Remove(fieldWidth - 3) + "..." : Album)}";

            return $"{songString,-fieldWidth} {artistString,-fieldWidth} {albumString,-fieldWidth}";
        }
    }
}
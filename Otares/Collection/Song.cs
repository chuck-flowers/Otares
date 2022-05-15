

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

        private static string[] labels = new string[]
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

        /// <summary>
        /// Creates a song object from a string from the databaseV2 file
        /// </summary>
        /// <param name="dbString">The databaseV2 string representation of the song</param>
        /// <returns>The song object created</returns>
        public static Song Create(string dbString)
        {
            Song toRet = new Song();

            for (int farthestReached = 0; farthestReached < dbString.Length;)
            {
                //Gets the start index
                int startOfLabel = -1;
				string? fieldLabel = null;
                foreach(var l in labels)
                {
                    startOfLabel = dbString.IndexOf(l, farthestReached);
                    if (startOfLabel >= 0)
                    {
						fieldLabel = dbString.Substring(startOfLabel, l.Length);
                        farthestReached = startOfLabel + l.Length;
                        break;
                    }
                }

                //If a field was not found, there are no more fields
                if (fieldLabel == null)
				{
					break;
				}

                //Gets the end index
				string? fieldValue = null;
				int endOfValue = labels
					.Select(l => dbString.IndexOf(l, farthestReached))
					.Where(x => x != -1)
					.OrderBy(x => x)
					.FirstOrDefault();

				// If no next field was found, this value goes to the end of the string
				if (endOfValue == 0)
				{
					endOfValue = dbString.Length;
				}

				// Extracts the field value from the DB string
				fieldValue = dbString.Substring(farthestReached, endOfValue - farthestReached);

                //Updates the farthest reached
                farthestReached = endOfValue;

                //Performs different actions based off the type of field
                switch(fieldLabel[0])
                {
                    case 'p':
                    case 't':
                        string stringValue = "";

                        foreach(char c in fieldValue)
                        {
                            if (c == 0)
							{
								continue;
							}

                            stringValue += c;
                        }

                        assignStringField(toRet, fieldLabel, stringValue);
                        break;
                }
            }

            return toRet;
        }

        /// <summary>
        /// Assigns a string value to a string field with the given databaseV2 label
        /// </summary>
        /// <param name="s">The song to perform the property update on</param>
        /// <param name="f">The databaseV2 field representation for the property to update</param>
        /// <param name="v">The value to set the field to</param>
        private static void assignStringField(Song s, string f, string v)
        {
            switch (f)
            {
                case TYPE_FIELD:
                    s.Type = v;
                    break;
                case PATH_FIELD:
                    s.PathString = v;
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
        /// <summary>
        /// The title of the song
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The artist who performs the song
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// The album the song appears on
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// The genre of the song
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// The length of the song
        /// </summary>
        public string Length { get; set; }

        /// <summary>
        /// The size of the song
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// The file type of the song
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The string representation of the path to the song
        /// </summary>
        public string PathString { get; set; }

        /// <summary>
        /// The bitrate of the song
        /// </summary>
        public string Bitrate { get; set; }

        /// <summary>
        /// The sample rate of the song
        /// </summary>
        public string SampleRate { get; set; }

        /// <summary>
        /// The beats per minute of the song
        /// </summary>
        public string Bpm { get; set; }

        /// <summary>
        /// The contents of the comment field
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The contents of the group field
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// The composer of the song
        /// </summary>
        public string Composer { get; set; }

        /// <summary>
        /// The year that the song was released
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// The key the song is in
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Whether or not the song has been marked as missing
        /// </summary>
        public bool IsMissing { get; set; }

        /// <summary>
        /// Whether or not the song has been marked as played
        /// </summary>
        public bool IsPlayed { get; set; }

        
        //PUBLIC METHODS

        /// <summary>
        /// Returns the JSON representation of the song
        /// </summary>
        /// <returns>The JSON string that represents the song</returns>
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

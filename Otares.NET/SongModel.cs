using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otares
{
    public class SongModel
    {
        //ENUMERATIONS

        private enum FieldType
        {
            TEXT_FIELD,
            UNSIGNED_FIELD,
            BOOLEAN_FIELD,
            NO_TYPE
        }
        

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


        //LABEL CATEGORIZATIONS

        private string[] labels = new string[]
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

        private string[] textLabels = new string[]
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
            KEY_FIELD
        };


        //CONSTRUCTOR

        public SongModel(string str)
        {
            dbString = str;

            for (int farthestReached = 0; farthestReached < str.Length;)
            {
                //Gets the start index
                int startOfString = getStartIndex(farthestReached);

                //If a field was not found, there are no more fields
                if (startOfString == -1)
                    break;

                //Gets the end index
                int endOfString = getEndIndex(startOfString);

                //If no next field was found, this is the last field
                if (endOfString == -1)
                    endOfString = str.Length;

                //Updates the farthest reached
                farthestReached = endOfString;

                //Cuts the string from the constructor string
                string fieldValueString = str.Substring(startOfString, endOfString - startOfString);

                //Records the value of this label and the raw string to process
                string fieldLabel = fieldValueString.Remove(4);

                //Removes the label from the string
                string rawString = fieldValueString.Substring(fieldLabel.Length);

                //Performs different actions based off the type of field
                switch (getFieldType(fieldLabel))
                {
                    //Text Field
                    case FieldType.TEXT_FIELD:
                        stringFields[fieldLabel] = stringCleaner(rawString);
                        break;
                    //Unsigned field
                    case FieldType.UNSIGNED_FIELD:
                        unsignedFields[fieldLabel] = unsignedMaker(rawString);
                        break;
                    //Boolean field
                    case FieldType.BOOLEAN_FIELD:
                        booleanFields[fieldLabel] = boolMaker(rawString);
                        break;
                }

            }

        }


        //PUBLIC PROPERTIES

        public string Song
        {
            get { try { return stringFields[SONG_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Artist
        {
            get { try { return stringFields[ARTIST_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Album
        {
            get { try { return stringFields[ALBUM_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Genre
        {
            get { try { return stringFields[GENRE_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Length
        {
            get { try { return stringFields[LENGTH_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Size
        {
            get { try { return stringFields[SIZE_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Type
        {
            get { try { return stringFields[TYPE_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Path
        {
            get { try { return stringFields[PATH_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Bitrate
        {
            get { try { return stringFields[BITRATE_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string SampleRate
        {
            get { try { return stringFields[SAMPLE_RATE_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Bpm
        {
            get { try { return stringFields[BPM_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Comment
        {
            get { try { return stringFields[COMMENT_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Group
        {
            get{ try { return stringFields[GROUP_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }
        
        public string Composer
        {
            get { try { return stringFields[COMPOSER_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Year
        {
            get { try { return stringFields[YEAR_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public string Key
        {
            get { try { return stringFields[KEY_FIELD]; } catch (KeyNotFoundException) { return ""; } }
        }

        public bool IsMissing
        {
            get { try { return booleanFields[MISSING_FIELD]; } catch (KeyNotFoundException) { return false; } }
        }

        public bool IsPlayed
        {
            get { try { return booleanFields[PLAYED_FIELD]; } catch (KeyNotFoundException) { return false; } }
        }
        
        
        //PUBLIC METHODS

        public override string ToString()
        {
            const int fieldWidth = 25;

            var songString = $"{(Song.Length > fieldWidth ? Song.Remove(fieldWidth - 3) + "..." : Song)}";
            var artistString = $"{(Artist.Length > fieldWidth ? Artist.Remove(fieldWidth - 3) + "..." : Artist)}";
            var albumString = $"{(Album.Length > fieldWidth ? Album.Remove(fieldWidth - 3) + "..." : Album)}";

            return $"{songString,-fieldWidth} {artistString,-fieldWidth} {albumString,-fieldWidth}";
        }

        public string ToJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }


        //PRIVATE MEMBERS

        private readonly Dictionary<string, string> stringFields = new Dictionary<string, string>();

        private readonly Dictionary<string, uint> unsignedFields = new Dictionary<string, uint>();

        private readonly Dictionary<string, bool> booleanFields = new Dictionary<string, bool>();

        private string dbString;

        
        //PRIVATE METHODS

        private int getStartIndex(int farthestReached)
        {
            int startOfString = -1;

            foreach (var label in labels)
            {
                startOfString = dbString.IndexOf(label, farthestReached);

                if (startOfString != -1)
                    break;
            }

            return startOfString;
        }

        private int getEndIndex(int startOfString)
        {
            int endOfString = -1;
            
            //Finds the end of the field value
            foreach (var label in labels)
            {
                endOfString = dbString.IndexOf(label, startOfString + 4);

                if (endOfString != -1)
                    break;
            }

            return endOfString;
        }

        private FieldType getFieldType(string str)
        {
            if (textLabels.Contains(str))
                return FieldType.TEXT_FIELD;

            return FieldType.NO_TYPE;
        }

        private string stringCleaner(string str)
        {
            string tempValue = str.Substring(4);
            string toRet = "";

            for (int j = 0; j < tempValue.Length; j++)
                if (j % 2 == 1) toRet += tempValue[j];

            return toRet;
        }

        private uint unsignedMaker(string str)
        {
            return 0;
        }

        private bool boolMaker(string str)
        {
            string tempValue = str.Substring(4);

            if (tempValue.Length > 1)
                throw new Exception("String is not a single byte as expected");

            short s = (short)tempValue[0];

            return s != 0;
        }
    }
}

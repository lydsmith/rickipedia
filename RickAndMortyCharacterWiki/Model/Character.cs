using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RickAndMortyCharacterWiki.Model
{
    public class Character
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string species { get; set; }
        public string type { get; set; }
        public string gender { get; set; }
        public Location origin { get; set; }
        public Location location { get; set; }
        public string image { get; set; }
        public List<string> episode { get; set; }
        public string url { get; set; }
        public DateTime created { get; set; }
    }

    public class CharactersResponse
    {
        public ResponseInfo info { get; set; }
        public ObservableCollection<Character> results { get; set; }
    }
    public class ResponseInfo
    {
        public int count { get; set; }
        public int pages { get; set; }
        public string next { get; set; }
        public string prev { get; set; }
    }
}

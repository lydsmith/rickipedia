using System;
using System.Collections.Generic;
using System.Text;

namespace RickAndMortyCharacterWiki.Model
{
    public class Episode
    {
        public int id { get; set; }
        public string name { get; set; }
        public string air_date { get; set; }
        public string episode { get; set; }
        public IEnumerable<string> characters { get; set; }
        public string url { get; set; }
        public string created { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCinema.Models
{
    public class DiscoverMovie
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("title")]
        public String Title { get; set; }
        private String _PosterPath;
        [JsonProperty("poster_path")]
        public String PosterPath
        {
            get { return this._PosterPath; }
            set { this._PosterPath = ImagesPath + value; }
        }
        public static String ImagesPath { get; set; }
        public DiscoverMovie()
        {
            ImagesPath = "https://image.tmdb.org/t/p/w300/";
        }
    }
}
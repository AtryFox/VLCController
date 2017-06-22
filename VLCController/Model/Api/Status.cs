using System.Collections.Generic;
using Newtonsoft.Json;

namespace DerAtrox.VLCController.Model.Api
{
    public class Meta
    {
        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "artwork_url")]
        public string ArtworkUrl { get; set; }

        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        [JsonProperty(PropertyName = "album")]
        public string Album { get; set; }

        [JsonProperty(PropertyName = "publisher")]
        public string Publisher { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "track_number")]
        public string TrackNumber { get; set; }

        [JsonProperty(PropertyName = "filename")]
        public string Filename { get; set; }

        [JsonProperty(PropertyName = "artist")]
        public string Artist { get; set; }
    }

    public class Category
    {
        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }
    }

    public class Information
    {
        [JsonProperty(PropertyName = "chapter")]
        public int Chapter { get; set; }

        [JsonProperty(PropertyName = "chapters")]
        public List<object> Chapters { get; set; }

        [JsonProperty(PropertyName = "title")]
        public int Ttitle { get; set; }

        [JsonProperty(PropertyName = "category")]
        public Category Category { get; set; }

        [JsonProperty(PropertyName = "titles")]
        public List<object> Titles { get; set; }
    }

    public class Status
    {
        [JsonProperty(PropertyName = "fullscreen")]
        public int Fullscreen { get; set; }

        [JsonProperty(PropertyName = "audiodelay")]
        public int AudioDelay { get; set; }

        [JsonProperty(PropertyName = "apiversion")]
        public int ApiVersion { get; set; }

        [JsonProperty(PropertyName = "currentplid")]
        public int CurrentPlId { get; set; }

        [JsonProperty(PropertyName = "time")]
        public int Time { get; set; }

        [JsonProperty(PropertyName = "volume")]
        public int Volume { get; set; }

        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }

        [JsonProperty(PropertyName = "random")]
        public bool Random { get; set; }

        [JsonProperty(PropertyName = "rate")]
        public int Rate { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "loop")]
        public bool Loop { get; set; }

        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "position")]
        public double Position { get; set; }

        [JsonProperty(PropertyName = "information")]
        public Information Information { get; set; }

        [JsonProperty(PropertyName = "repeat")]
        public bool Repeat { get; set; }

        [JsonProperty(PropertyName = "subtitledelay")]
        public int SubtitleDelay { get; set; }

        [JsonProperty(PropertyName = "equalizer")]
        public List<object> Equalizer { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace API.Models
{
    public class Book : object
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public Specifications specifications { get; set; }
    }

    public class Specifications
    {
        [JsonPropertyName("Originally published")]
        public string Originallypublished { get; set; }
        public string Author { get; set; }

        [JsonPropertyName("Page count")]
        public int Pagecount { get; set; }
        public object Illustrator { get; set; }
        public object Genres { get; set; }
    }
}
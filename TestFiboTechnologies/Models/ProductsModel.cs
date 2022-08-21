namespace TestFiboTechnologies.Models
{
    using System;
    using Newtonsoft.Json;

    public partial class ProductsModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("rating")]
        public Rating Rating { get; set; }
    }

    public partial class Rating
    {
        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }
    }

}

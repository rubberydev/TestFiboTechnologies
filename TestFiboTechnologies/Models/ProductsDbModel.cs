using System;
using SQLite;

namespace TestFiboTechnologies.Models
{
    public partial class ProductsDbModel
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Uri Image { get; set; }
        public Rating Rating { get; set; }
    }
}

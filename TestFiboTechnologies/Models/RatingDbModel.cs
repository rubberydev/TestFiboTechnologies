using System;
using SQLite;

namespace TestFiboTechnologies.Models
{
    public class RatingDbModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double Rate { get; set; }

        public long Count { get; set; }

        [Indexed]
        public long IdProduct { get; set; }
    }
}

using System;
using SQLite;

namespace TestFiboTechnologies.Models
{
    public class Animals
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string KindOfCorral { get; set; }
        public int CantOfAnimals { get; set; }
        [Indexed]
        public int CharlesId { get; set; }
    }
}

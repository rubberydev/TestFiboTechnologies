using System;
using SQLite;

namespace TestFiboTechnologies.Models
{
    public class Charles
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public int MaximunByCorral { get; set; }
    }
}

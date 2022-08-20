using System;
using SQLite;

namespace TestFiboTechnologies.Models
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }
}

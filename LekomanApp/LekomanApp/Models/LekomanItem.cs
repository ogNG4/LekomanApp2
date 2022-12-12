using SQLite;
using System;


namespace LekomanApp.Models
{
    public class LekomanItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Lek { get; set; }

        public string Dawka { get; set; }

        public DateTime Data { get; set; }

        public TimeSpan Godzina { get; set; }








    }
}

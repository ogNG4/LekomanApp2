using SQLite;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;


namespace LekomanApp.Models
{
    public class LekomanItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public Guid UserId { get; set; }


        public string Lek { get; set; }

        public string Dawka { get; set; }

        public DateTime Data { get; set; }

        public TimeSpan Godzina { get; set; }

        
        public bool Zrobione { get; set; }


       


    }

}

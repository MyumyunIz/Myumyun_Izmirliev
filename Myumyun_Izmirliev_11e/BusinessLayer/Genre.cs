using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Genre
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        public List<Game> Games { get; set; }
        public List<User> Users { get; set; }
        public Genre(string name)
        {
            Name = name;
            Games = new List<Game>();
            Users = new List<User>();
        }
        private Genre()
        {
            Games = new List<Game>();
            Users = new List<User>();
        }
    }
}

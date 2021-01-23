using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HadankyScaffolded.Models
{
    public class Hadanky
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Otázka")]
        public string Otazka { get; set; }
        [Required]
        [DisplayName("Odpověď")]
        public string Odpoved { get; set; }

        public Hadanky()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Models
{
    [Table("Pokemons")]
    public class Pokemon
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PokemonID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }       
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public double Speed { get; set; }

        [Required]
        public string Type { get; set; }

        [ForeignKey(nameof(Trainer))]
        public int TrainerID { get; set; }

        [NotMapped]
        public virtual Trainer Trainer { get; set; }

        
    }
}

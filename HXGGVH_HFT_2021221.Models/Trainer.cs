using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Models
{
    [Table("Trainers")]
    public class Trainer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainerID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }        
        public int? Wins { get; set; }

        [Required]
        public int Level { get; set; }

        [ForeignKey(nameof(Region))]
        public int RegionID { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Region Region { get; set; }
        [NotMapped]
        public virtual ICollection<Pokemon> Pokemons { get; set; }

        public Trainer()
        {
            Pokemons = new HashSet<Pokemon>();
        }       
    }
}

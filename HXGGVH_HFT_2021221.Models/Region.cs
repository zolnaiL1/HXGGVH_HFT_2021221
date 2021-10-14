using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Models
{
    [Table("Regions")]
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegionID { get; set; }

        [Required]
        [MaxLength (100)]
        public string Name { get; set; }

        [NotMapped]
        public virtual ICollection<Trainer> Trainers { get; set; }

        public Region()
        {
            Trainers = new HashSet<Trainer>();
        }
    }
}

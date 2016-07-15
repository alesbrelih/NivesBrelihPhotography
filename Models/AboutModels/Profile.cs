using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.AboutModels
{
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(40)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(100)]
        public string ProfilePicture { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        [Required]
        public string About { get; set; }
    }
}

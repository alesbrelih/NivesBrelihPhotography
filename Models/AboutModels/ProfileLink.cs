using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.AboutModels
{
    public class ProfileLink
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileLinkId { get; set; }

        [Required]
        [StringLength(20)]
        public string LinkName { get; set; }

        [Required]
        [StringLength(100)]
        public string LinkImgUrl { get; set; }

        [Required]
        [StringLength(100)]
        public string LinkUrl { get; set; }

        [Required]
        [StringLength(100)]
        public string LinkDescription { get; set; }
    }
}

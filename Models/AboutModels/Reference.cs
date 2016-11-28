using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.AboutModels
{
    public class Reference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReferenceId { get; set; }

        [Required]
        [StringLength(50)]
        public string ReferenceTitle { get; set; }


        public string ReferenceDescription { get; set; }

        //all photos
        public virtual ICollection<ReferencePhoto> Photos { get; set; }


    }
}

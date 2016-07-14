using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivesBrelihPhotography.Models.PhotoModels;

namespace NivesBrelihPhotography.Models.AboutModels
{
    public class ReferencePhoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReferencePhotoId { get; set; }

        [Required]
        [ForeignKey("Reference")]
        public int ReferenceId { get; set; }

        public virtual Reference Reference { get; set; }

        [Required]
        [ForeignKey("Photo")]
        public int PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

    }
}

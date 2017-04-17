using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.AboutModels
{
    public class WorkingWith
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkingWithId { get; set; }

        //main title
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        //working with description
        public string Description { get; set; }

        //external pagelink and title / not necessary
        public string Link { get; set; }

        public string LinkText { get; set; }

        //how high its located
        public int Importance { get; set; }

        //photo to display alongside link!
        //not required
        public string PhotoUrl { get; set; }
    }
}

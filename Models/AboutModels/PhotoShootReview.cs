using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.AboutModels
{
    public class PhotoShootReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoShootReviewId { get; set; }

        [StringLength(30)]
        public string ReviewerName { get; set; }

        public string Review { get; set; }
    }
}

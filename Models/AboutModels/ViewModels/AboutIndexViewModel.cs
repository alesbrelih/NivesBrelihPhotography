using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.AboutModels.ViewModels
{
    public class AboutIndexViewModel
    {

        public ProfileView Profile { get; set; }

        public ICollection<ProfileLinkView> SocialLinks { get; set; }

        //public ICollection<ReferenceView> References { get; set; }

        public ICollection<PhotoShotReviewView> PhotoShootReviews { get; set; } 

    }
}

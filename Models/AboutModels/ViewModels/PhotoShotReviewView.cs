using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.AboutModels.ViewModels
{
    public class PhotoShotReviewView
    {
        public int PhotoShotReviewId { get; set; }

        public string ReviewerName { get; set; }

        public string Review { get; set; }

        public string ReviewIndexPage
        {
            get
            {
                if (Review.Length <= 100)
                {
                    return Review;
                }
                else
                {
                    return Review.Substring(0, 97) + "...";
                }
            }
        }

    }
}

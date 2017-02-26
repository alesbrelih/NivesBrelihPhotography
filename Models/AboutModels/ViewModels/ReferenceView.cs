using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivesBrelihPhotography.Models.PhotoModels.ViewModels;

namespace NivesBrelihPhotography.Models.AboutModels.ViewModels
{
    public class ReferenceView
    {
        public int ReferenceId { get; set; }

        public string Title { get; set; }

        public PhotoView LeadPhoto { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.AboutModels.ViewModels.Admin_ViewModels
{
    public class AdminAboutProfileLinkUpdate
    {
        public int ProfileLinkId { get; set; }

        public string LinkUrl { get; set; }

        public string LinkDescription { get; set; }

        public bool ShownOnProfile { get; set; }
    }
}

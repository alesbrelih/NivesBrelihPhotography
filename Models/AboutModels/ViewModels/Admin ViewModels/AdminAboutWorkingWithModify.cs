using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.AboutModels.ViewModels.Admin_ViewModels
{
    public class AdminAboutWorkingWithModify
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string LinkText { get; set; }

        public int Importance { get; set; }

        public string PhotoUrl { get; set; }
    }
}

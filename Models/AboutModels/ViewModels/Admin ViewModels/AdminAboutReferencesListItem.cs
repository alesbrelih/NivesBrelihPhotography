using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.AboutModels.ViewModels.Admin_ViewModels
{
    public class AdminAboutReferencesListItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public AdminAboutReferencesListItem()
        {
            
        }

        public AdminAboutReferencesListItem(Reference reference):this()
        {
            Id = reference.ReferenceId;
            Title = reference.ReferenceTitle;
            Description = reference.ReferenceDescription;
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivesBrelihPhotography.Models.BlogModels.ViewModels.Admin_ViewModels
{
    public class AdminBlogsIndexVm
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string DateString { get; set; }

    }
}

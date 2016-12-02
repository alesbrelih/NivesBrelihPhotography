using System;
using System.Collections.Generic;


namespace NivesBrelihPhotography.Models.BlogModels.ViewModels
{
    public class BlogView
    {

        public string BlogTitle { get; set; }

        public DateTime BlogDate { get; set; }  //blog date

        public string BlogDateString => BlogDate.ToShortDateString(); //returnsstring

        public string BlogDateMonthAsString
        {
            get
            {
                var month = "";
                switch (BlogDate.Month)
                {
                    case (1):
                        month = "January";
                        break;
                    case (2):
                        month = "February";
                        break;
                    case (3):
                        month = "March";
                        break;
                    case (4):
                        month = "April";
                        break;
                    case (5):
                        month = "May";
                        break;
                    case (6):
                        month = "June";
                        break;
                    case (7):
                        month = "July";
                        break;
                    case (8):
                        month = "August";
                        break;
                    case (9):
                        month = "September";
                        break;
                    case (10):
                        month = "October";
                        break;
                    case (11):
                        month = "November";
                        break;
                    case (12):
                        month = "December";
                        break;

                }
                return month + " " + BlogDate.Year;
            }
        } //blog date with month as string

        public ICollection<BlogCategory> Categories { get; set; }

    }
}

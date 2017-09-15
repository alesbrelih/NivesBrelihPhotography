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
                        month = "Januar";
                        break;
                    case (2):
                        month = "Februar";
                        break;
                    case (3):
                        month = "Marec";
                        break;
                    case (4):
                        month = "April";
                        break;
                    case (5):
                        month = "Maj";
                        break;
                    case (6):
                        month = "Junij";
                        break;
                    case (7):
                        month = "Julij";
                        break;
                    case (8):
                        month = "Avgust";
                        break;
                    case (9):
                        month = "September";
                        break;
                    case (10):
                        month = "Oktober";
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

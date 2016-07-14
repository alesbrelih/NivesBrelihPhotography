using System.Collections.Generic;


namespace NivesBrelihPhotography.Models.BlogModels.ViewModels.Index
{
    //inherits BlogId, BlogTitle, BlogDate, BlogDateString, BlogDateMonthAsString, Categories from BlogView
    public class BlogIndexView:BlogView
    {

        #region props

        public int BlogId { get; set; }  //blog id 

        public string BlogCoverPhoto { get; set; }  //url to cover photo

        public string Description { get; set; }  //description



        #endregion


    }
}

using NivesBrelihPhotography.Models.PhotoModels.ViewModels;

namespace NivesBrelihPhotography.Models.BlogModels.ViewModels.Detail
{

    //inherits BlogTitle, BlogDate, BlogDateString, BlogDateMonthAsString, Categories from BlogView
    public class BlogDetailsView:BlogView
    {

        #region props
        //blog content
        public string BlogContent { get; set; }

        public bool AlbumLink { get; set; }

        public PhotoAlbumView Album { get; set; }
        #endregion

        #region constructor

        //constructor
        public BlogDetailsView(Blog blog)
        {
            BlogTitle = blog.BlogTitle;
            BlogDate = blog.BlogDate;
            BlogContent = blog.Content;
            Categories = blog.Categories;
            AlbumLink = blog.AlbumLink;

            //if album link exists then create new object
            if (AlbumLink)
            {
                Album = new PhotoAlbumView();
            }

        }

        #endregion

    }
}

﻿using System.Web;
using System.Web.Optimization;

namespace NivesBrelihPhotography
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/page-index-scripts").Include(
                "~/Scripts/imagesloaded.pkgd.min.js",
                "~/Scripts/masonry.pkgd.min.js",
                "~/Scripts/EnlargePhoto_Lib/enlarge-photo-scripts.js",
                "~/Scripts/photosIndex.js"
                ));
            bundles.Add(new StyleBundle("~/Content/page-index-styles").Include(
                "~/Content/ImgDescriptions_Styles/photo-cover-descriptions.css",
                "~/Content/EnlargePhoto_Lib_Styles/enlarge-photo-styles.css",
                "~/Content/photos-index.css"
                ));

            //enlarge photo lib bundles
            bundles.Add(new StyleBundle("~/Content/EnlargePhotoStyles").Include(
                "~/Content/EnlargePhoto_Lib_Styles/enlarge-photo-styles.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/EnlargePhotoScript").Include(
                "~/Scripts/EnlargePhoto_Lib/enlarge-photo-scripts.js"));

            //albums index bundles
            bundles.Add(new StyleBundle("~/Content/albums-index-styles").Include(
                "~/Content/ImgDescriptions_Styles/photo-cover-descriptions.css",
                "~/Content/ImgDescriptions_Styles/album-cover-descriptions.css",
                "~/Content/EnlargePhoto_Lib_Styles/enlarge-photo-styles.css",
                "~/Content/AlbumPhotos_Section/album-photos.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/albums-index-scripts").Include(
                "~/Scripts/EnlargePhoto_Lib/enlarge-photo-scripts.js",
                "~/Scripts/masonry.pkgd.min.js",
                "~/Scripts/imagesloaded.pkgd.min.js",
                "~/Scripts/AlbumPhotos_Section/album-section.js"
                ));

            //blog index bundles
            bundles.Add(new StyleBundle("~/Content/blog-index-styles").Include(
                "~/Content/Blog_Section/blog-index.css"));
            bundles.Add(new ScriptBundle("~/bundles/blog-index-scripts").Include(
                "~/Scripts/Blog_Section/blog-index.js"));

            //blog view bundles
            bundles.Add(new StyleBundle("~/Content/blog-view-styles").Include(
                "~/Content/Blog_Section/blog-view.css"));
            bundles.Add(new StyleBundle("~/bundles/blog-view-scripts").Include(
                "~/Scripts/Blog_Section/blog-view.js"));

            //album photos reindirected from blogs
            bundles.Add(new StyleBundle("~/Content/album-photos-from-blog-styles").Include(
                "~/Content/ImgDescriptions_Styles/photo-cover-descriptions.css",
                "~/Content/AlbumPhotos_Section/album-photos-from-blog.css",
                "~/Content/EnlargePhoto_Lib_Styles/enlarge-photo-styles.css"));

            bundles.Add(new ScriptBundle("~/bundles/album-photos-from-blog-scripts").Include(
                "~/Scripts/masonry.pkgd.min.js",
                "~/Scripts/imagesloaded.pkgd.min.js",
                "~/Scripts/ImgDescription_Scripts/cover-descriptions-scripts.js",
                "~/Scripts/EnlargePhoto_Lib/enlarge-photo-scripts.js",
                "~/Scripts/AlbumPhotos_Section/album-photos-from-blog.js"
                ));

        }
    }
}

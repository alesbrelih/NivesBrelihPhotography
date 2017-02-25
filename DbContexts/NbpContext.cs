using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using NivesBrelihPhotography.Models.AboutModels;
using NivesBrelihPhotography.Models.BlogModels;
using NivesBrelihPhotography.Models.CategoryModels;
using NivesBrelihPhotography.Models.CommentModels;
using NivesBrelihPhotography.Models.PhotoModels;

namespace NivesBrelihPhotography.DbContexts
{
    public class NbpContext:DbContext
    {
        public NbpContext():base("hostingEu")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NbpContext>());
            //Database.SetInitializer<NbpContext>(new DropCreateDatabaseAlways<NbpContext>());
        }

        #region tables

        //category table in db
        public DbSet<Category> Categories { get; set; }

        //photo table in db
        public DbSet<Photo> Photos { get; set; }

        //photocategory table in db
        public DbSet<PhotoCategory> PhotoCategories { get; set; }

        //comments table in db
        public DbSet<Comment> Comments { get; set; }

        //photo albums
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }

        public DbSet<AlbumCover> AlbumCovers { get; set; }

        //blogs
        public DbSet<Blog> Blogs { get; set; }

        //blog categories
        public DbSet<BlogCategory> BlogCategories { get; set; }

        //profile db
        public DbSet<Profile> Profile { get; set; }

        //profile links db
        public DbSet<ProfileLink> ProfileLinks { get; set; }

        //references db
        public DbSet<Reference> References { get; set; }

        //reference photos
        public DbSet<ReferencePhoto> ReferencePhotos { get; set; }

        //photoshoot reviews
        public DbSet<PhotoShootReview> PhotoShootReviews { get; set; }

        #endregion

        #region onModelCreatig
        //remove pluralization of tables
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        #endregion

    }
}
using System.Collections.Generic;
using System.Net;
using NivesBrelihPhotography.DbContexts;
using NivesBrelihPhotography.Models.AboutModels;
using NivesBrelihPhotography.Models.BlogModels;
using NivesBrelihPhotography.Models.CategoryModels;
using NivesBrelihPhotography.Models.PhotoModels;

namespace NivesBrelihPhotography.Migrations.NbpContextMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NivesBrelihPhotography.DbContexts.NbpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\NbpContextMigrations";
        }

        protected override void Seed(NivesBrelihPhotography.DbContexts.NbpContext context)
        {
            const string photo1 = "/Images/Photos/Thumbnail_Test/nbPic1.jpg";
            const string photo2 = "/Images/Photos/Thumbnail_Test/nbPic2.jpg";
            const string photo3 = "/Images/Photos/Thumbnail_Test/nbPic3.jpg";
            const string photo4 = "/Images/Photos/Thumbnail_Test/nbPic4.jpg";
            const string photo5 = "/Images/Photos/Thumbnail_Test/nbPic5.jpg";
            const string photo6 = "/Images/Photos/Thumbnail_Test/nbPic6.jpg";
            const string photo7 = "/Images/Photos/Thumbnail_Test/nbPic7.jpg";



            #region albums seed


            var albumDesc =
                "Bacon ipsum dolor amet ham flank short ribs pork belly andouille, tongue kielbasa corned beef. " +
                "Bresaola short ribs pork belly frankfurter hamburger, leberkas meatloaf chicken t-bone. Andouille" +
                " chicken shankle leberkas picanha. Salami doner swine meatloaf.Alcatra pork belly kevin picanha brisket" +
                " jowl bresaola flank leberkas chicken pork chop pork ball tip. Prosciutto meatloaf salami, alcatra shank" +
                " pork chop andouille meatball cupim beef ribs jerky bresaola kielbasa tri-tip. Pork belly jowl andouille" +
                " meatloaf short ribs. Tongue chicken chuck pork belly, pork picanha bresaola doner prosciutto tri-tip" +
                " andouille ham hock. Ground round jowl shankle meatloaf bresaola pork chop chuck short loin corned beef" +
                " pork turkey turducken. Tongue turducken jerky pastrami, pancetta capicola short loin filet mignon biltong." +
                "Ball tip turducken pastrami sirloin ribeye prosciutto short loin tenderloin picanha landjaeger tri-tip bresaola" +
                " pork chop filet mignon. Short ribs landjaeger sirloin ball tip, shankle prosciutto flank pork belly. Turducken" +
                " andouille beef ribs brisket picanha. Shoulder pig jerky prosciutto, kevin tri-tip ham tongue meatloaf shankle" +
                " bacon cow landjaeger. Ball tip turkey cupim picanha fatback landjaeger pork. Tail kevin shoulder short loin.";


            new List<PhotoAlbum>()
            {
                new PhotoAlbum() {PhotoAlbumId = 1, AlbumDate = DateTime.Now.AddDays(20),AlbumName = "Svedska",AlbumDescription = albumDesc },
                new PhotoAlbum() {PhotoAlbumId = 2, AlbumDate = DateTime.Now.AddDays(30),AlbumName = "Poroka Mrjan in Marija",AlbumDescription = albumDesc},
                new PhotoAlbum() {PhotoAlbumId = 3, AlbumDate = DateTime.Now.AddDays(55),AlbumName = "Dinozavri",AlbumDescription =albumDesc},
                new PhotoAlbum() {PhotoAlbumId = 4, AlbumDate = DateTime.Now.AddDays(98),AlbumName = "Diploma Marije Terezije Cetrte",AlbumDescription = albumDesc},

                new PhotoAlbum() {PhotoAlbumId = 5, AlbumDate = DateTime.Now.AddDays(120),AlbumName = "Svedska",AlbumDescription = albumDesc },
                new PhotoAlbum() {PhotoAlbumId = 6, AlbumDate = DateTime.Now.AddDays(130),AlbumName = "Poroka Mrjan in Marija",AlbumDescription = albumDesc},
                new PhotoAlbum() {PhotoAlbumId = 7, AlbumDate = DateTime.Now.AddDays(155),AlbumName = "Dinozavri",AlbumDescription =albumDesc},
                new PhotoAlbum() {PhotoAlbumId = 8, AlbumDate = DateTime.Now.AddDays(198),AlbumName = "Diploma Marije Terezije Cetrte",AlbumDescription = albumDesc},

                new PhotoAlbum() {PhotoAlbumId = 9, AlbumDate = DateTime.Now.AddDays(220),AlbumName = "Svedska",AlbumDescription = albumDesc },
                new PhotoAlbum() {PhotoAlbumId = 10, AlbumDate = DateTime.Now.AddDays(330),AlbumName = "Poroka Mrjan in Marija",AlbumDescription = albumDesc},
                new PhotoAlbum() {PhotoAlbumId = 11, AlbumDate = DateTime.Now.AddDays(255),AlbumName = "Dinozavri",AlbumDescription =albumDesc},
                new PhotoAlbum() {PhotoAlbumId = 12, AlbumDate = DateTime.Now.AddDays(298),AlbumName = "Diploma Marije Terezije Cetrte",AlbumDescription = albumDesc},

                new PhotoAlbum() {PhotoAlbumId = 13, AlbumDate = DateTime.Now.AddDays(320),AlbumName = "Svedska",AlbumDescription = albumDesc },
                new PhotoAlbum() {PhotoAlbumId = 14, AlbumDate = DateTime.Now.AddDays(330),AlbumName = "Poroka Mrjan in Marija",AlbumDescription = albumDesc},
                new PhotoAlbum() {PhotoAlbumId = 15, AlbumDate = DateTime.Now.AddDays(355),AlbumName = "Dinozavri",AlbumDescription =albumDesc},
                new PhotoAlbum() {PhotoAlbumId = 16, AlbumDate = DateTime.Now.AddDays(398),AlbumName = "Diploma Marije Terezije Cetrte",AlbumDescription = albumDesc},

                new PhotoAlbum() {PhotoAlbumId = 17, AlbumDate = DateTime.Now.AddDays(420),AlbumName = "Svedska",AlbumDescription = albumDesc },
                new PhotoAlbum() {PhotoAlbumId = 18, AlbumDate = DateTime.Now.AddDays(430),AlbumName = "Poroka Mrjan in Marija",AlbumDescription = albumDesc},
                new PhotoAlbum() {PhotoAlbumId = 19, AlbumDate = DateTime.Now.AddDays(455),AlbumName = "Dinozavri",AlbumDescription =albumDesc},
                new PhotoAlbum() {PhotoAlbumId = 20, AlbumDate = DateTime.Now.AddDays(498),AlbumName = "Diploma Marije Terezije Cetrte",AlbumDescription = albumDesc}
            }.ForEach(p => context.PhotoAlbums.AddOrUpdate(p));
            //add default categories



            #endregion

            #region category seed

            new List<Category>()
            {
                new Category() { CategoryId = 1, CategoryTitle = "Weddings"},
                new Category() { CategoryId = 2, CategoryTitle = "Animals"},
                new Category() { CategoryId = 3, CategoryTitle = "Nature"}
            }.ForEach(p => context.Categories.AddOrUpdate(p));

            #endregion

            #region photo seed


            new List<Photo>()
            {
                new Photo() {PhotoId = 1, CommentsEnabled = true,PhotoTitle = "First Photo",PhotoText = "Super Photo First",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic1.jpg", Uploaded = DateTime.Now,IsOnFrontPage = true,PhotoAlbumId = 1,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 2, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic2.jpg", Uploaded = DateTime.Now.AddDays(1),IsOnFrontPage = true,PhotoAlbumId = 1},
                new Photo() {PhotoId = 3, CommentsEnabled = true,PhotoTitle = "Third Photo",PhotoText = "Super Photo Third",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic3.jpg", Uploaded = DateTime.Now.AddDays(2),IsOnFrontPage = true,PhotoAlbumId = 1},
                new Photo() {PhotoId = 4, CommentsEnabled = true,PhotoTitle = "Fourth Photo",PhotoText = "Super Photo Forth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic4.jpg", Uploaded = DateTime.Now.AddDays(3),IsOnFrontPage = true,PhotoAlbumId = 2,IsPhotoAlbumCover = false},
                new Photo() {PhotoId = 5, CommentsEnabled = true,PhotoTitle = "Fifth Photo",PhotoText = "Super Photo Fifth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic5.jpg", Uploaded = DateTime.Now.AddDays(4),IsOnFrontPage = true,PhotoAlbumId = 2},
                new Photo() {PhotoId = 6, CommentsEnabled = true,PhotoTitle = "Sixth Photo",PhotoText = "Super Photo Sixth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic6.jpg", Uploaded = DateTime.Now.AddDays(15),IsOnFrontPage = true,PhotoAlbumId = 2},
                new Photo() {PhotoId = 7, CommentsEnabled = true,PhotoTitle = "Seventh Photo",PhotoText = "Super Photo Seventh",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic7.jpg", Uploaded = DateTime.Now.AddDays(7),IsOnFrontPage = true,PhotoAlbumId = 3},
                new Photo() {PhotoId = 8, CommentsEnabled = true,PhotoTitle = "First Photo",PhotoText = "Super Photo First",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic1.jpg", Uploaded = DateTime.Now.AddDays(19),IsOnFrontPage = true,PhotoAlbumId = 3},
                new Photo() {PhotoId = 9, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic2.jpg", Uploaded = DateTime.Now.AddDays(22),IsOnFrontPage = true,PhotoAlbumId = 3,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 10, CommentsEnabled = true,PhotoTitle = "Third Photo",PhotoText = "Super Photo Third",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic3.jpg", Uploaded = DateTime.Now.AddDays(442),IsOnFrontPage = true,PhotoAlbumId = 4},
                new Photo() {PhotoId = 11, CommentsEnabled = true,PhotoTitle = "Fourth Photo",PhotoText = "Super Photo Forth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic4.jpg", Uploaded = DateTime.Now.AddDays(213),IsOnFrontPage = true,PhotoAlbumId = 4,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 12, CommentsEnabled = true,PhotoTitle = "Fifth Photo",PhotoText = "Super Photo Fifth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic5.jpg", Uploaded = DateTime.Now.AddDays(45),IsOnFrontPage = true,PhotoAlbumId = 20,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 13, CommentsEnabled = true,PhotoTitle = "Sixth Photo",PhotoText = "Super Photo Sixth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic6.jpg", Uploaded = DateTime.Now.AddDays(66),IsOnFrontPage = true,PhotoAlbumId = 5,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 14, CommentsEnabled = true,PhotoTitle = "Seventh Photo",PhotoText = "Super Photo Seventh",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic7.jpg", Uploaded = DateTime.Now.AddDays(87),IsOnFrontPage = true,PhotoAlbumId = 6,IsPhotoAlbumCover = true},
                 new Photo() {PhotoId = 15, CommentsEnabled = true,PhotoTitle = "First Photo",PhotoText = "Super Photo First",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic1.jpg", Uploaded = DateTime.Now,IsOnFrontPage = true,PhotoAlbumId = 1,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 16, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic2.jpg", Uploaded = DateTime.Now.AddDays(1),IsOnFrontPage = true,PhotoAlbumId = 7,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 17, CommentsEnabled = true,PhotoTitle = "Third Photo",PhotoText = "Super Photo Third",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic3.jpg", Uploaded = DateTime.Now.AddDays(2),IsOnFrontPage = true,PhotoAlbumId = 8,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 18, CommentsEnabled = true,PhotoTitle = "Fourth Photo",PhotoText = "Super Photo Forth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic4.jpg", Uploaded = DateTime.Now.AddDays(3),IsOnFrontPage = true,PhotoAlbumId = 9,IsPhotoAlbumCover = false},
                new Photo() {PhotoId = 19, CommentsEnabled = true,PhotoTitle = "Fifth Photo",PhotoText = "Super Photo Fifth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic5.jpg", Uploaded = DateTime.Now.AddDays(4),IsOnFrontPage = true,PhotoAlbumId = 10,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 20, CommentsEnabled = true,PhotoTitle = "Sixth Photo",PhotoText = "Super Photo Sixth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic6.jpg", Uploaded = DateTime.Now.AddDays(15),IsOnFrontPage = true,PhotoAlbumId = 11,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 21, CommentsEnabled = true,PhotoTitle = "Seventh Photo",PhotoText = "Super Photo Seventh",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic7.jpg", Uploaded = DateTime.Now.AddDays(7),IsOnFrontPage = true,PhotoAlbumId = 12,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 22, CommentsEnabled = true,PhotoTitle = "First Photo",PhotoText = "Super Photo First",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic1.jpg", Uploaded = DateTime.Now.AddDays(19),IsOnFrontPage = true,PhotoAlbumId = 13,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 23, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic2.jpg", Uploaded = DateTime.Now.AddDays(22),IsOnFrontPage = true,PhotoAlbumId = 14,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 24, CommentsEnabled = true,PhotoTitle = "Third Photo",PhotoText = "Super Photo Third",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic3.jpg", Uploaded = DateTime.Now.AddDays(442),IsOnFrontPage = true,PhotoAlbumId = 15,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 25, CommentsEnabled = true,PhotoTitle = "Fourth Photo",PhotoText = "Super Photo Forth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic4.jpg", Uploaded = DateTime.Now.AddDays(213),IsOnFrontPage = true,PhotoAlbumId = 16,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 26, CommentsEnabled = true,PhotoTitle = "Fifth Photo",PhotoText = "Super Photo Fifth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic5.jpg", Uploaded = DateTime.Now.AddDays(45),IsOnFrontPage = true,PhotoAlbumId = 17,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 27, CommentsEnabled = true,PhotoTitle = "Sixth Photo",PhotoText = "Super Photo Sixth",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic6.jpg", Uploaded = DateTime.Now.AddDays(66),IsOnFrontPage = true,PhotoAlbumId = 18,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 28, CommentsEnabled = true,PhotoTitle = "Seventh Photo",PhotoText = "Super Photo Seventh",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic7.jpg", Uploaded = DateTime.Now.AddDays(87),IsOnFrontPage = true,PhotoAlbumId = 19,IsPhotoAlbumCover = true},
                new Photo() {PhotoId = 29, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = "/Images/Photos/Thumbnail_Test/nbPic2.jpg", Uploaded = DateTime.Now.AddDays(1),IsOnFrontPage = true,PhotoAlbumId = 1},

                new Photo() {PhotoId = 39, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo1, Uploaded = DateTime.Now.AddDays(771),PhotoAlbumId = 1},
                new Photo() {PhotoId = 30, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo2, Uploaded = DateTime.Now.AddDays(12),PhotoAlbumId = 1},
                new Photo() {PhotoId = 31, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo3, Uploaded = DateTime.Now.AddDays(31),PhotoAlbumId = 1},
                new Photo() {PhotoId = 32, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo4, Uploaded = DateTime.Now.AddDays(41),PhotoAlbumId = 1},
                new Photo() {PhotoId = 33, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo5, Uploaded = DateTime.Now.AddDays(51),PhotoAlbumId = 1},
                new Photo() {PhotoId = 34, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo6, Uploaded = DateTime.Now.AddDays(16),PhotoAlbumId = 1},
                new Photo() {PhotoId = 35, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo7, Uploaded = DateTime.Now.AddDays(17),PhotoAlbumId = 1},
                new Photo() {PhotoId = 36, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo7, Uploaded = DateTime.Now.AddDays(1),PhotoAlbumId = 1},
                new Photo() {PhotoId = 37, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo1, Uploaded = DateTime.Now.AddDays(17),PhotoAlbumId = 1},
                new Photo() {PhotoId = 38, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo2, Uploaded = DateTime.Now.AddDays(188),PhotoAlbumId = 1},
                new Photo() {PhotoId = 40, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo5, Uploaded = DateTime.Now.AddDays(51),PhotoAlbumId = 1},
                new Photo() {PhotoId = 41, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo6, Uploaded = DateTime.Now.AddDays(16),PhotoAlbumId = 1},
                new Photo() {PhotoId = 42, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo7, Uploaded = DateTime.Now.AddDays(17),PhotoAlbumId = 1},
                new Photo() {PhotoId = 43, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo7, Uploaded = DateTime.Now.AddDays(1),PhotoAlbumId = 1},
                new Photo() {PhotoId = 44, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo1, Uploaded = DateTime.Now.AddDays(17),PhotoAlbumId = 1},
                new Photo() {PhotoId = 45, CommentsEnabled = true,PhotoTitle = "Second Photo",PhotoText = "Super Photo Second",PhotoUrl = photo2, Uploaded = DateTime.Now.AddDays(188),PhotoAlbumId = 1},



            }.ForEach(p => context.Photos.AddOrUpdate(p));


            #endregion

            #region PhotoCategory seed


            new List<PhotoCategory>()
            {
                new PhotoCategory() {PhotoCategoryId = 1,CategoryId = 1,PhotoId = 1},
                new PhotoCategory() {PhotoCategoryId = 2,CategoryId = 2,PhotoId = 2},
                new PhotoCategory() {PhotoCategoryId = 3,CategoryId = 3,PhotoId = 3},
                new PhotoCategory() {PhotoCategoryId = 4,CategoryId = 2,PhotoId = 4},
                new PhotoCategory() {PhotoCategoryId = 5,CategoryId = 3,PhotoId = 5},
                new PhotoCategory() {PhotoCategoryId = 6,CategoryId = 1,PhotoId = 6},
                new PhotoCategory() {PhotoCategoryId = 7,CategoryId = 2,PhotoId = 7},
                new PhotoCategory() {PhotoCategoryId = 8,CategoryId = 1,PhotoId = 8},
                new PhotoCategory() {PhotoCategoryId = 9,CategoryId = 2,PhotoId = 9},
                new PhotoCategory() {PhotoCategoryId = 10,CategoryId = 3,PhotoId = 10},
                new PhotoCategory() {PhotoCategoryId = 11,CategoryId = 2,PhotoId = 12},
                new PhotoCategory() {PhotoCategoryId = 12,CategoryId = 3,PhotoId = 11},
                new PhotoCategory() {PhotoCategoryId = 13,CategoryId = 1,PhotoId = 13},
                new PhotoCategory() {PhotoCategoryId = 14,CategoryId = 2,PhotoId = 14},

            }.ForEach(p => context.PhotoCategories.AddOrUpdate(p));

            #endregion

            #region blogs seed

            var blogDesc =
                "A wonderful serenity has taken possession of my entire soul, like these sweet mornings of spring which I enjoy with my whole heart. I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a si";
            var blogContent = WebUtility.HtmlEncode("<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.Praesent sed pretium quam. Nam aliquam tempor est sed hendrerit."
                              +
                              "Nulla facilisi. Morbi sed eleifend arcu. Donec lobortis non nibh eu mattis. Mauris sit amet consectetur lacus, sed suscipit felis." +
                              "Ut ullamcorper dui mauris, at sagittis ligula congue ac. Sed et tortor in nulla sodales cursus ut in metus." +

                              "Ut finibus turpis in porta pellentesque. Donec fringilla augue ac pulvinar iaculis. Pellentesque habitant morbi tristique senectus et netus et malesuada" +
                              "fames ac turpis egestas.Cras rhoncus mi erat, ac dapibus velit pellentesque eget. Integer eget volutpat lorem, in facilisis magna. Morbi eu" +
                              "pretium metus. Vivamus justo massa, dapibus id lacinia ac, volutpat at lectus. Donec sodales risus arcu, vitae maximus velit volutpat in." +
                              "Cras hendrerit pretium ex, non tristique mi tristique a." +

                              "Praesent dolor justo, volutpat at mattis a, hendrerit eget odio. Nam placerat elit ac dui sollicitudin, eu convallis lacus fringilla.Morbi" +
                              "sodales erat vitae ex eleifend, eu dapibus neque molestie.Praesent pretium tincidunt nisl, eu efficitur mi vulputate sed. Etiam id augue" +
                              "ac risus tempor accumsan et id neque. Donec neque risus, semper a molestie sit amet, consequat et lacus.Nam vitae mi lacus. Suspendisse" +
                              "ut erat non neque sollicitudin auctor.Ut magna elit, interdum sit amet rutrum ac, dapibus vitae turpis.Donec posuere, lectus vitae mattis" +
                              "vestibulum, ante lorem volutpat massa, eget tempus mauris arcu non enim.Nullam ullamcorper dignissim eros sit amet mollis." +

                              "</ p >" +

                              "< img src = '/Images/Photos/Thumbnail_Test/nbPic7.jpg' class='img-responsive' />" +


                              "<p>" +
                              "Etiam vulputate magna neque, vel egestas quam lobortis sit amet.Sed tempus tellus vitae felis dapibus, non mattis neque pretium.Morbi vitae consequat diam.Nulla facilisi.Mauris pretium lectus eget neque mollis mattis.Curabitur aliquam hendrerit ullamcorper. Morbi vitae augue ultrices, aliquet eros vel, vehicula ligula." +


                              "Cras tincidunt bibendum interdum. Sed lacinia eu urna quis volutpat. Suspendisse potenti. Aenean fringilla turpis vel odio scelerisque ullamcorper.Duis vel purus nec elit dignissim feugiat.Nullam ac fringilla est. Pellentesque sed quam mattis quam placerat tempor quis vitae mi. Phasellus pharetra semper ipsum, scelerisque sodales mauris commodo ac.Cras volutpat faucibus augue ac vehicula. Phasellus in iaculis eros, sed vestibulum lacus.Curabitur a condimentum diam, ac finibus ligula." +
                              "Suspendisse potenti. In sit amet purus ullamcorper, gravida urna quis, commodo justo. In aliquam ullamcorper ante, et ultrices felis pulvinar viverra.Nullam porta ultricies risus eu finibus." +


                              "Duis tristique ipsum a venenatis maximus. Proin laoreet nunc vel dignissim feugiat. Ut accumsan maximus velit. Praesent lorem sapien, accumsan sed purus et, tempor interdum odio.Nullam nec ante ligula. Fusce convallis luctus hendrerit. Proin sed interdum nibh. Integer viverra dignissim risus vel aliquet. Vestibulum sed ultrices felis. Sed eget ante fringilla, tristique nunc id, aliquam tellus." +
                              "</p>" +

                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic3.jpg' class='img-responsive' />" +


                              "<p>" +
                              "Donec consequat, turpis eu egestas venenatis, quam ex tincidunt nisl, in sodales lorem felis vel orci.Integer maximus, est vitae porta viverra, justo ex ullamcorper nibh, in semper ex massa vehicula felis.Phasellus tortor sapien, pharetra ut pellentesque eu, gravida a urna.Pellentesque non mi eget neque volutpat tincidunt. Curabitur pellentesque quam sit amet justo egestas scelerisque. Nunc a nibh accumsan, faucibus turpis et, malesuada risus. Nunc eget est pellentesque, bibendum velit at, imperdiet orci. Vestibulum cursus felis a nisi interdum bibendum.Integer ornare ligula quis scelerisque blandit." +


                              "Morbi nec mollis sem, a tristique ligula.Sed commodo lectus et odio vestibulum, sed malesuada dolor ornare. Mauris ultricies pretium consectetur. Phasellus ornare mattis tristique. Morbi volutpat sed sem quis feugiat. Maecenas sodales, quam at gravida viverra, ante eros bibendum mi, sit amet ultrices ipsum metus nec dolor.Etiam commodo nunc lacus, vitae gravida nunc consequat non.Ut vehicula sem quis ultrices suscipit. Fusce sagittis, dolor sagittis feugiat dictum, orci risus aliquet lacus, sit amet consequat urna erat vitae diam.Ut a volutpat urna, sed mattis tellus.Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec at sapien tortor.Pellentesque aliquam elit vel urna pulvinar rhoncus." +


                              "Mauris non lectus eleifend, viverra ligula nec, laoreet tellus. Vestibulum vitae dictum nunc. Duis ac tristique metus, accumsan dignissim tortor.Integer posuere sed lacus id sodales. Morbi accumsan auctor lacus, sit amet malesuada quam dictum a. Maecenas sit amet lectus nec ante semper placerat eget nec massa.Maecenas lectus est, posuere vitae efficitur semper, volutpat at ex.Vestibulum eget mattis justo. Praesent pretium sem rutrum lectus rutrum, sit amet laoreet felis faucibus.Cras eget diam diam." +
                              "</p>" +
                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic3.jpg' class='img-responsive'/>" +

                              "<p>" +
                              "Morbi convallis libero vel mi consectetur egestas.Etiam facilisis elementum lacinia.In consequat diam massa, porttitor feugiat justo pretium ut.Cras sed convallis ipsum. Vestibulum eget tellus quis diam dictum aliquet sit amet at nulla.Vivamus efficitur sem vitae quam congue malesuada.Nunc egestas purus eget nisi laoreet tristique.Praesent nec leo eget dolor vestibulum finibus." +

                              "Aenean commodo quam accumsan faucibus efficitur. Vestibulum non turpis sagittis, consequat dolor nec, porta leo. Quisque dapibus et dolor at iaculis. Curabitur at odio eu mauris faucibus rhoncus sit amet sit amet urna. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Sed tempus est eu venenatis lacinia. Maecenas pharetra semper tempor. Nam malesuada, ipsum non dapibus luctus, quam risus aliquam nulla, volutpat dignissim metus eros ac eros. Sed pulvinar dolor condimentum lacus pharetra, nec mollis nibh consectetur. Integer aliquam, risus sed commodo tristique, urna nisi interdum lacus, a porttitor odio tellus porta enim. Nullam aliquet tortor nec elementum maximus. Maecenas sollicitudin, elit ut commodo pellentesque, justo ex bibendum nunc, a scelerisque libero turpis ac nulla. Etiam feugiat, lorem et elementum vestibulum, sem magna lacinia ligula, porttitor mattis ipsum justo nec sem. Nam vel hendrerit neque." +


                              "Quisque nec neque risus. Suspendisse a ornare massa. Nulla eget augue ut felis ultrices eleifend.Nam ultrices enim sed viverra venenatis. Curabitur pulvinar, mauris et iaculis feugiat, erat augue commodo justo, vel posuere odio diam a tortor. Nunc non turpis mi. Nunc quis tellus orci. Aliquam sed tellus a magna hendrerit pellentesque eget suscipit tellus. Etiam semper, ligula fermentum sollicitudin placerat, turpis nisi maximus turpis, volutpat dapibus eros felis in turpis.Fusce vestibulum, dui in finibus ultricies, odio dolor tristique nisl, et pharetra purus nisi et tellus. Etiam quis vehicula lacus. Nam faucibus leo nec risus rhoncus, sed gravida neque fermentum. Curabitur nisi massa, mollis vitae consectetur vel, elementum quis justo.Nam suscipit rutrum urna, at interdum ligula sagittis in. Proin nisl sapien, egestas molestie turpis eu, tincidunt interdum augue.Sed ac nisi at lectus efficitur commodo eget sit amet libero." +

                              "Pellentesque vestibulum enim in faucibus finibus. Etiam vehicula ac tellus ac rhoncus. Fusce varius sem ac nisl luctus, porta laoreet lorem cursus. Maecenas hendrerit massa eu lacinia posuere. Praesent nec magna sit amet enim ornare pulvinar et sed enim.Morbi efficitur elit in gravida efficitur. Fusce ac augue nec lectus ullamcorper commodo.Integer sed dolor sed lorem varius tristique.Cras consectetur mattis ex ac gravida. Vivamus ac libero nisi. Aenean hendrerit arcu venenatis, consectetur sem nec, tempor urna. Fusce egestas, ante sed auctor congue, nibh nibh fringilla ipsum, id elementum sem augue ut magna. Donec interdum fringilla enim aliquam scelerisque. Nullam nec placerat tellus." +
                              "</p>" +
                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic2.jpg' class='img-responsive' />" +

                              "<p>" +
                              "Sed cursus nisl a auctor lobortis.Sed euismod ex eu dapibus dictum.Integer efficitur magna felis, eget gravida dui consequat venenatis.Suspendisse maximus eget ante non luctus. Cras porttitor nisi et fringilla iaculis. Donec eu ante id lacus ullamcorper maximus.Praesent fringilla orci erat, a congue massa aliquam a.Aliquam velit nibh, lacinia non tortor nec, scelerisque gravida mauris.Donec et elit sed felis vulputate aliquet ut vel sem. In est tellus, scelerisque sit amet pretium in, eleifend sed felis.Sed consequat malesuada dapibus. Lorem ipsum dolor sit amet, consectetur adipiscing elit.Nullam nec velit in ex posuere ornare lobortis eget quam. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas." +

                              "Morbi convallis risus sit amet nisl placerat venenatis. Donec interdum at elit interdum faucibus. In mauris tortor, pharetra non pulvinar eget, faucibus id nibh.Ut leo libero, ullamcorper ac turpis id, laoreet pellentesque libero.Praesent ut velit vitae purus viverra facilisis.Pellentesque gravida augue vel elit lacinia auctor.Nam iaculis suscipit aliquam. Maecenas lobortis facilisis gravida. Vestibulum suscipit luctus rutrum. Sed nisi augue, placerat eu pharetra nec, efficitur ut dolor.Ut ut erat euismod, faucibus felis in, fringilla nibh. Quisque sed orci condimentum, hendrerit quam quis, ullamcorper felis. Praesent volutpat ipsum quis dui finibus auctor.Phasellus imperdiet est sit amet eros aliquet varius. Maecenas rutrum blandit magna nec pellentesque." +

                              "Phasellus dapibus, turpis eget ornare vulputate, lacus odio consectetur felis, nec condimentum risus orci a elit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Curabitur interdum convallis ante sed eleifend.Quisque et lorem ac turpis vehicula tristique. Vestibulum finibus est id feugiat eleifend. Nam sit amet ultrices urna.Nunc tempor augue id odio lacinia, et sagittis mi ullamcorper. Praesent turpis augue, elementum a interdum et, cursus vel ex.Maecenas egestas, neque sit amet vulputate sagittis, magna libero varius lacus, et vestibulum ligula dolor non nulla. Integer accumsan quam turpis, sit amet varius libero interdum quis. Aliquam vitae sem nisi. Proin ut diam nibh. Nam dapibus nisl sagittis iaculis porttitor. Morbi at magna congue, bibendum magna sit amet, viverra lacus. Etiam eu lobortis ipsum." +
                              "</p>" +
                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic1.jpg' class='img-responsive' />" +
                              "<p>" +
                              "Sed vel eros gravida, vehicula nulla id, varius nunc.Etiam leo lorem, bibendum quis vehicula id, scelerisque ut ante.Sed in libero vehicula, placerat justo id, efficitur nibh. Nam viverra erat arcu. Fusce at diam vel massa efficitur mollis.Proin nec mattis ipsum, nec hendrerit mauris.Aenean molestie commodo sapien, nec dapibus ex iaculis et.Mauris enim risus, scelerisque ut facilisis a, finibus in mi.Duis tempus, metus nec laoreet vulputate, quam erat sollicitudin purus, vel finibus tellus eros et nisl. Ut vulputate dolor quis nisl lacinia, eget finibus elit blandit. Etiam in dolor augue. Donec sit amet vestibulum urna." +

                              "In hac habitasse platea dictumst.Etiam gravida varius arcu. Duis laoreet pulvinar facilisis. Aenean ut sollicitudin ex. Nulla nec justo sed ex tempus dapibus.Donec et enim libero. Morbi tincidunt ante non eros fermentum ullamcorper.Curabitur enim elit, venenatis et pretium sit amet, iaculis eget metus.Ut tincidunt volutpat venenatis. Proin consequat commodo enim tempor consectetur. Nunc pretium metus nec eros vestibulum, consectetur bibendum massa euismod. Cras sit amet magna volutpat ligula imperdiet ultricies quis eu ipsum.Sed felis enim, suscipit sit amet convallis facilisis, pulvinar molestie arcu.Nunc molestie lorem et eros tristique pretium.Nulla accumsan quis nibh ut aliquet." +


                              "Donec ut lectus risus. Donec porta nisi et ipsum venenatis aliquam.Aliquam quam neque, fermentum quis convallis quis, porta sed libero.Pellentesque semper lacus id ipsum suscipit egestas.Vestibulum ut arcu lorem. Phasellus in cursus ante. Integer euismod est a ipsum pretium, non convallis metus tempus. Proin vitae massa nibh. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Nullam interdum urna sit amet erat vulputate luctus. Donec aliquet facilisis nisl sed dictum. Aliquam commodo sem et justo consequat iaculis.Praesent at hendrerit massa, quis porta nulla.Mauris ullamcorper faucibus neque eu vehicula. Sed luctus enim in porttitor tempor. In hendrerit sem lorem, nec porttitor velit sodales vitae." +

                              "Pellentesque maximus at dolor in malesuada.Nunc a sapien lectus. Cras porttitor lobortis quam, in euismod quam tristique ut. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Curabitur scelerisque mi placerat nulla faucibus feugiat.Sed consectetur vehicula nisi a pharetra. Cras est metus, facilisis accumsan neque et, varius pellentesque leo.Aliquam feugiat mauris eu molestie iaculis. Curabitur ante massa, aliquet eu elementum nec, congue ut risus." +
                              "</p>" +

                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic7.jpg' class='img-responsive' />" +

                              "<p>" +
                              "Nulla quis fringilla orci.Nam condimentum ante id mauris gravida, nec finibus eros sollicitudin. Vestibulum facilisis ipsum ipsum, et dignissim nibh aliquet et.Nulla ligula lorem, accumsan vel iaculis et, egestas eget nibh.Ut lobortis nisl est, a gravida sapien feugiat ac.Phasellus eleifend mattis vulputate. Nullam congue semper porttitor. Cras ut viverra eros, at posuere lorem.Duis bibendum vehicula lorem vel gravida. Aliquam erat volutpat.Integer consectetur magna vel porttitor interdum. Sed ornare non nisi sed finibus. Sed condimentum neque ac sapien mattis, vitae congue mauris porttitor. Morbi consequat et justo sit amet consectetur.Quisque sed turpis odio." +

                              "Nulla ut ligula porta, mollis est sit amet, sollicitudin mi. Duis interdum risus molestie dui bibendum, quis fermentum velit interdum. Mauris maximus sapien ut sollicitudin pellentesque. Vivamus ac varius risus. Vivamus semper molestie nibh ut auctor. Vestibulum pretium rhoncus ex vel sodales. Fusce aliquet egestas libero non lobortis. In hac habitasse platea dictumst.Curabitur laoreet tellus vitae suscipit tincidunt. Aliquam convallis varius nibh quis dapibus." +

                              "Fusce fringilla justo id neque pharetra, ut mollis lectus pulvinar. Nullam aliquam ornare enim id pulvinar. Maecenas aliquam sollicitudin neque. Sed id tellus porta felis malesuada gravida sit amet non odio.Donec ante sapien, feugiat vel sagittis vitae, varius non tortor.Nulla congue, turpis a tincidunt hendrerit, ipsum tellus imperdiet dolor, vitae volutpat ligula arcu in tellus.Etiam iaculis nec enim nec gravida. Vivamus porta congue sapien ac finibus. Proin erat mauris, egestas sit amet molestie nec, porttitor nec velit.Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.Sed vel lobortis leo. Praesent in varius sem, at accumsan justo.Integer porttitor, felis sit amet interdum commodo, tellus turpis rutrum leo, sed ornare lacus sem et dolor." +

                              "Vivamus ultrices purus eros, nec lobortis purus dapibus sed.Vestibulum ac elit metus. Etiam sit amet nisl condimentum nibh elementum rutrum ut ac ipsum.Nunc massa massa, pellentesque et hendrerit et, egestas ut dolor.Cras tempus, neque eget lobortis lobortis, nibh magna tempor quam, ullamcorper cursus orci ex eget arcu. Morbi cursus euismod mollis. Nullam molestie feugiat finibus. Aenean mi nunc, egestas eu mi vel, faucibus sodales massa.Donec scelerisque libero id nulla malesuada, ac ullamcorper velit tincidunt. Nam et risus finibus, tempus elit eget, iaculis purus. Duis placerat urna nec tortor tempus, eget ornare nisl consectetur. Fusce lacinia urna sit amet dui congue molestie. Suspendisse potenti. Sed aliquet elementum ligula vel egestas. Sed vel bibendum nisi. Integer placerat semper leo, non commodo enim porttitor nec." +

                              "Donec rutrum, ligula sit amet accumsan interdum, velit dolor facilisis orci, vel facilisis tortor nisl non lectus. Cras dignissim ligula neque, ac placerat libero porttitor eu.Cras porta posuere fringilla. Praesent et eros vitae nunc bibendum tincidunt.Integer congue turpis eu eros efficitur, non mollis nunc bibendum. Sed ut molestie lacus. Pellentesque ac laoreet sapien. Vivamus quis augue eros. Sed sollicitudin ullamcorper nulla quis eleifend. Mauris sit amet lectus ac tortor pulvinar feugiat. Duis molestie justo ipsum, vitae feugiat ligula scelerisque vel.Pellentesque dignissim ornare mi, dignissim accumsan purus faucibus quis.Ut nec lectus a tortor suscipit ornare eget pellentesque tortor. Ut iaculis metus rutrum nisi imperdiet, sed tincidunt orci malesuada." +

                              "Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Praesent ipsum dui, vulputate quis magna sed, pellentesque pretium nulla.Ut ante dui, dictum feugiat mi quis, tincidunt congue elit.Integer viverra nisl ex, vel aliquet urna porta a.In imperdiet mi sit amet risus fringilla laoreet. Quisque magna mauris, posuere eu aliquet quis, hendrerit quis lacus.Nullam ac purus sit amet tellus semper sollicitudin. Sed convallis molestie nisl bibendum auctor. Praesent vel purus nec erat egestas consectetur quis nec ex. Nunc a mi eu ante fermentum consequat sed sed augue." +
                              "</p>" +
                              "<div class='row two-photo-group'>" +
                              "<div class='col-md-6'>" +
                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic5.jpg' class='img-responsive' />" +

                              "</div>" +
                              "<div class='col-md-6'>" +

                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic4.jpg' class='img-responsive'/>" +

                              "</div>" +
                              "</div>" +


                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic3.jpg' class='img-responsive' />" +


                              "<p>" +
                              "Mauris eleifend tempus vehicula.Vestibulum feugiat arcu quis est posuere, vel egestas enim imperdiet. Integer et vulputate tellus. Aenean quis maximus eros, a placerat sapien.Quisque pretium ultricies felis, vel ultricies sem imperdiet a.Morbi ut lacus vel odio blandit volutpat nec sed libero. Morbi tristique ipsum in dolor porta, in tincidunt nunc congue.Donec ullamcorper fermentum volutpat. Fusce sollicitudin ante felis, a consequat dui pharetra id.Fusce et fermentum velit." +

                              "Donec iaculis, erat tincidunt iaculis tempor, velit sapien sodales velit, nec finibus nulla sapien eget felis. Phasellus lobortis justo et neque pulvinar gravida.Nunc a placerat lectus. Fusce lacinia fringilla metus, ut rhoncus justo finibus volutpat.Maecenas sodales dapibus neque, id lacinia est auctor id.Mauris lacus erat, vulputate sed dictum a, scelerisque in velit.Curabitur rhoncus leo ut eros sollicitudin, sed ultrices justo blandit. Donec finibus enim ante, id consectetur neque eleifend vitae.Pellentesque et ex elementum, imperdiet diam ut, egestas augue. Sed dignissim hendrerit commodo. Sed efficitur sem fringilla neque imperdiet aliquam.Nulla in tristique urna. Etiam vel euismod orci. Quisque tempus vehicula massa." +

                              "Sed eget mattis quam, eget interdum eros.Nam tortor quam, finibus nec leo ut, porttitor sollicitudin erat.Nunc blandit volutpat nulla, et viverra diam aliquam ut.Vivamus at elit ac sem eleifend cursus.Aenean lacus urna, malesuada sed diam nec, tristique mattis velit.Donec a dignissim magna. In in finibus odio, sodales rutrum arcu.Proin accumsan, massa eu posuere pretium, augue arcu euismod turpis, volutpat finibus sem metus sit amet urna.Integer a mi condimentum, ornare nisl ut, sagittis velit. Nullam fermentum non enim et posuere. Vivamus placerat enim quis tellus gravida porta.Vivamus venenatis metus nisl, eget iaculis augue facilisis sed.Fusce dapibus nec risus pellentesque malesuada. Donec eu dapibus elit, in pellentesque lacus. Quisque tempor eros et magna porta molestie quis sagittis augue. Vestibulum felis est, aliquet vitae consectetur ac, ornare a mauris." +

                              "Vivamus gravida sapien eleifend commodo egestas. Etiam euismod velit purus. Etiam ut ex vel ligula tempor eleifend quis non enim. Fusce id nisl in tellus dictum porta in elementum nisi. Fusce ac nisi massa. Mauris lectus purus, suscipit ut mollis scelerisque, efficitur sit amet eros. Ut nec pretium ante, et condimentum quam.Donec sollicitudin id mauris ut consequat. Praesent convallis, libero quis auctor finibus, turpis mi dignissim nulla, sit amet lacinia ipsum lacus a purus.Donec a fringilla ipsum, in vestibulum velit. Donec mollis convallis leo quis ultrices. Pellentesque sed ligula ut sem congue commodo.In vestibulum orci ornare magna aliquet facilisis.Quisque faucibus nec lacus non auctor. Vivamus commodo placerat justo mattis tincidunt." +

                              "Etiam elit eros, facilisis ut ultricies molestie, fringilla eu sapien.Mauris imperdiet iaculis aliquet. Sed faucibus sapien id molestie aliquet. Mauris viverra nunc erat, suscipit gravida mi eleifend at.Sed magna lorem, egestas ac ante eu, commodo efficitur turpis.Sed non tincidunt metus, varius finibus nulla.Nunc sed ipsum ut dui dapibus gravida.Cras ullamcorper rutrum tristique. Pellentesque aliquam nulla felis, nec ultrices massa fringilla in. Aenean efficitur tristique velit non accumsan. Morbi vitae magna bibendum, molestie justo sed, luctus diam. Sed euismod finibus justo, id porta arcu hendrerit sit amet. Aenean vestibulum tellus nec nibh ultrices, id imperdiet ipsum lobortis." +
                              "</p>" +
                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic2.jpg' class='img-responsive' />" +
                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic6.jpg' class='img-responsive' />" +

                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic1.jpg' class='img-responsive' />" +

                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic1.jpg' class='img-responsive'/>" +
                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic2.jpg' class='img-responsive' />" +
                              "<p>" +
                              "Vestibulum eget urna a sapien viverra interdum.Nullam aliquet justo quis arcu tempor tempus.Vivamus et odio sit amet ligula euismod iaculis non eu metus.Mauris quis orci porttitor, tempor eros a, convallis lorem.Nullam at lacinia mauris.Quisque ac faucibus diam, at auctor turpis.Integer ex nisl, vulputate ac finibus id, tempus ornare leo.In pellentesque rutrum lectus in placerat.Etiam eleifend mauris sed interdum gravida. Etiam pulvinar non felis eu vulputate. Aenean a purus vitae urna bibendum vehicula.Quisque fermentum commodo eros vel consequat. Morbi pellentesque tellus ut massa pulvinar, quis lacinia felis ultricies. Quisque feugiat quam at feugiat lacinia. Integer at porttitor dui." +


                              "Etiam dolor nisi, rhoncus ac est et, ultrices accumsan ligula.Proin sed volutpat justo. Maecenas commodo varius libero, nec rhoncus diam dictum a.Integer tempor lacus non elit dapibus consequat.Integer egestas rutrum nulla, ut scelerisque turpis laoreet sed.Cras imperdiet feugiat urna ut consequat. Aliquam erat volutpat.Pellentesque a nulla euismod, scelerisque felis et, commodo tellus. Maecenas ac suscipit leo, nec convallis ex.Maecenas a rutrum dui. Nulla neque arcu, pulvinar at tempor non, vulputate at purus.Curabitur rutrum vitae magna sed hendrerit. Praesent laoreet aliquet odio, et tempor purus porttitor nec.Fusce non est a odio vulputate iaculis ut ut erat." +


                              "Suspendisse vehicula varius ullamcorper. Suspendisse imperdiet, lacus sed porttitor porta, leo velit pellentesque sapien, et placerat sapien elit at sem. Nullam sit amet dolor a odio consequat blandit. Pellentesque neque nibh, porta ut rhoncus a, maximus ut velit.Nam scelerisque bibendum justo vel tincidunt. Ut maximus, nunc a laoreet aliquam, eros leo tincidunt dolor, eu mattis massa turpis at justo. Sed faucibus interdum justo, nec maximus dui congue nec.Duis neque enim, blandit nec massa ut, aliquam ultricies lectus.Mauris dignissim nisi non diam blandit, a egestas ante bibendum." +


                              "Nam aliquet metus vitae rutrum interdum. Donec sed finibus massa, non laoreet leo.Donec erat nulla, faucibus ut odio gravida, tincidunt lacinia nunc.Suspendisse et tellus est. Etiam vitae laoreet mauris. Fusce ac dolor sed nisi efficitur ornare.Vivamus ullamcorper orci felis, vel sollicitudin neque auctor ut." +
                              "</p>" +
                              "<img src = '/Images/Photos/Thumbnail_Test/nbPic2.jpg' class='img-responsive' />" +

                              "<p>" +
                              "Nulla ultricies enim vitae molestie molestie.Quisque porta nunc ac dui egestas bibendum. Nulla sagittis malesuada vehicula. Praesent efficitur eleifend leo molestie sagittis. Morbi mattis pellentesque nunc, sit amet ornare lacus convallis a. Aliquam congue libero a erat suscipit volutpat.Sed posuere, sapien vel feugiat dapibus, magna massa pharetra metus, nec porttitor justo magna ac nulla. Nulla facilisi. Suspendisse et turpis metus. Aliquam dapibus eget nisi id tristique. Duis at ex in sem lacinia ornare.Maecenas tempus, mi sit amet dignissim hendrerit, sem est dignissim enim, vel commodo ante sem a ante. Aenean tincidunt vitae elit quis volutpat. Sed ac dui quam. Ut eleifend feugiat tellus, eu fermentum justo accumsan quis." +

                              "Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Phasellus libero odio, dignissim vitae tempor a, accumsan id nunc.Mauris non velit ac massa lacinia ultrices.Suspendisse potenti. Sed id augue tempus, rhoncus tellus ac, euismod urna. Nullam imperdiet nisi nec dapibus efficitur. Sed dolor nulla, bibendum et justo ac, congue dignissim ligula.Aliquam sit amet tempus sapien, eget interdum metus.Maecenas vitae massa ac ligula vestibulum bibendum malesuada et erat. Phasellus ornare et metus vitae gravida. Nulla facilisi." +

                              "Morbi non tempor nibh. Nullam imperdiet tellus non quam consectetur, sed lobortis orci finibus. Phasellus ipsum est, sodales in nibh sed, ornare pharetra erat.Curabitur non diam eu massa tincidunt tempor.Nam aliquet leo sit amet tellus pulvinar, vitae fermentum ante tincidunt. Duis elementum efficitur pharetra. Ut pretium enim nibh, eget tristique diam accumsan posuere." +

                              "Cras diam erat, rhoncus a sodales nec, sagittis quis eros.Nam dictum condimentum vulputate. Vestibulum elit mauris, efficitur et est sit amet, sodales pretium nulla.Nam eu facilisis est. Duis porttitor elit erat, laoreet dignissim urna lobortis accumsan.Mauris sed tincidunt nisl. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Fusce eget lacus accumsan orci auctor sodales.In id rutrum elit." +

                              "Integer porta, metus in tincidunt rutrum, mi odio rhoncus quam, ac congue lorem orci lacinia purus. Aliquam erat volutpat.Nullam commodo ultrices augue, et efficitur quam eleifend eget.Quisque gravida velit neque, vel ultricies tortor faucibus in. Etiam dictum tortor a eros tempus luctus.In at tincidunt nulla. Cras pretium augue sit amet elit vestibulum, ut venenatis urna gravida. Donec a ipsum condimentum, consequat nisi ut, porttitor quam. Maecenas vel nunc porta, dictum libero ac, commodo augue. Morbi ornare mauris sit amet ultrices scelerisque.Cras dignissim massa vitae libero dictum, ullamcorper maximus ligula interdum. Fusce sed nisi lacus. Curabitur a condimentum leo, non placerat nunc.Vivamus quis scelerisque turpis. Nam a massa faucibus, porttitor quam nec, rhoncus lacus. Quisque sit amet faucibus dolor." +
                              "</p>");

            var blogContent2 = WebUtility.HtmlEncode("<p>Jesus</p>");

            new List<Blog>()
            {

                new Blog() {BlogId = 1,BlogTitle = "Prvi Super Duper Blog",BlogDescription = blogDesc, BlogDate = DateTime.Now,Content = blogContent,CoverPhotoId = 1,AlbumLink = true,AlbumId=1},
                new Blog() {BlogId = 2,BlogTitle = "Drugi Super Duper Blog",BlogDescription = blogDesc, BlogDate = DateTime.Now.AddDays(100),Content = blogContent,CoverPhotoId = 2,AlbumLink = true,AlbumId=2},
                new Blog() {BlogId = 3,BlogTitle = "Tretji Super Duper Blog",BlogDescription = blogDesc, BlogDate = DateTime.Now.AddDays(200),Content = blogContent,CoverPhotoId = 3,AlbumLink = true,AlbumId=3},
                new Blog() {BlogId = 4,BlogTitle = "CEtrti Super Duper Blog",BlogDescription = blogDesc, BlogDate = DateTime.Now.AddDays(300),Content = blogContent,CoverPhotoId = 4,AlbumLink = true,AlbumId=4}

            }.ForEach(x => context.Blogs.AddOrUpdate(x));

            #endregion

            #region about controller seed

            //profile seed
            const string profileAbout = "Hi! I am Nives & I come from Slovenia." +
                                        "Photography is my passion & my way of seeing the world." +
                                        "I like to capture spontaneous moments in combination with good light." +
                                        "I enjoy taking photos of newborn babies, children, couples, weddings, nature, animales & landscapes, ...";

            context.Profile.AddOrUpdate(new Profile() { ProfileId = 1, Name = "Nives", Lastname = "Brelih", About = profileAbout,ProfilePicture = "/Images/Profile_Img/profile_pic.jpg" });

            //profileLink
            const string twitter = "https://twitter.com/_NBPhotography";
            const string facebook = "https://www.facebook.com/nivesbrelihphotography/";
            const string youtube = "https://www.youtube.com/channel/UCQiZ4iENcziMa5OaTOJGDPQ";
            const string linkedIn = "https://www.linkedin.com/in/nives-brelih-58613916";
            const string pinterest = "https://www.pinterest.com/nivesb/";

            const string twitterIcon = "/Images/Social_Media_Icons/twitter-logo.png";
            const string facebookIcon = "/Images/Social_Media_Icons/facebook-logo.png";
            const string youtubeIcon = "/Images/Social_Media_Icons/youtube-play-button.png";
            const string linkedIcon = "/Images/Social_Media_Icons/linkedin-logo.png";
            const string pinterestIcon = "/Images/Social_Media_Icons/pinterest-logo.png";


            new List<ProfileLink>()
            {
                new ProfileLink() {ProfileLinkId = 1,LinkName = "Twitter",LinkDescription = "Check my twits",LinkUrl = twitter,LinkImgUrl = twitterIcon},
                new ProfileLink() {ProfileLinkId = 2,LinkName = "Facebook",LinkDescription = "Follow me on facebook",LinkUrl = facebook,LinkImgUrl = facebookIcon},
                new ProfileLink() {ProfileLinkId = 3,LinkName = "Youtube",LinkDescription = "Check my videos",LinkUrl = youtube,LinkImgUrl = youtubeIcon},
                new ProfileLink() {ProfileLinkId = 4,LinkName = "LinkedIn",LinkDescription = "See my linkedIn",LinkUrl = linkedIn,LinkImgUrl = linkedIcon},
                new ProfileLink() {ProfileLinkId = 5,LinkName = "Pinterest",LinkDescription = "Check my pinterest",LinkUrl = pinterest,LinkImgUrl = pinterestIcon}
            }.ForEach(x=>context.ProfileLinks.AddOrUpdate(x));

            const string refDesc =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vitae augue sagittis, porta dui quis, euismod arcu. Praesent tristique tempor finibus. Aenean accumsan placerat diam vel lobortis. Aliquam eu arcu ut lacus malesuada venenatis. Donec orci mauris, porttitor et tellus et, pulvinar dictum leo. Vestibulum faucibus accumsan leo non interdum. Aenean hendrerit justo condimentum, faucibus justo nec, tempus lectus. Etiam gravida ex sit amet aliquet tincidunt. Vestibulum nec massa lectus. In a scelerisque purus. Nulla ac est at metus semper semper. Nulla pellentesque laoreet lectus, in pharetra ante suscipit elementum. In finibus lectus non ullamcorper luctus. Nullam lacinia bibendum aliquet.";
            new List<Reference>()
            {
                new Reference() {ReferenceId = 1,ReferenceTitle="Plavanje Klub Tivoli",ReferenceDescription = refDesc},
                new Reference() {ReferenceId = 2,ReferenceTitle="Poroka Marija",ReferenceDescription = refDesc},
                new Reference() {ReferenceId = 3,ReferenceTitle="Poroka Marjan",ReferenceDescription = refDesc},
                new Reference() {ReferenceId = 4,ReferenceTitle="Poroka Yipikaje",ReferenceDescription = refDesc},
                new Reference() {ReferenceId = 5,ReferenceTitle="Turnir v sahu",ReferenceDescription = refDesc},
                new Reference() {ReferenceId = 6,ReferenceTitle="Turnir v Nogometu",ReferenceDescription = refDesc},
                new Reference() {ReferenceId = 7,ReferenceTitle="Slikanje Nike",ReferenceDescription = refDesc},
                new Reference() {ReferenceId = 8,ReferenceTitle="Ples Ob Ljubljanici",ReferenceDescription = refDesc}
           }.ForEach(x=>context.References.AddOrUpdate(x));

            new List<ReferencePhoto>()
            {
                new ReferencePhoto() {ReferencePhotoId = 1,PhotoId = 1,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 2,PhotoId = 2,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 3,PhotoId = 3,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 4,PhotoId = 4,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 5,PhotoId = 5,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 6,PhotoId = 6,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 7,PhotoId = 7,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 8,PhotoId = 1,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 9,PhotoId = 2,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 10,PhotoId = 3,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 11,PhotoId = 4,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 12,PhotoId = 5,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 13,PhotoId = 6,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 14,PhotoId = 7,ReferenceId = 1},
                new ReferencePhoto() {ReferencePhotoId = 15,PhotoId = 1,ReferenceId = 2},
                new ReferencePhoto() {ReferencePhotoId = 16,PhotoId = 2,ReferenceId = 2},
                new ReferencePhoto() {ReferencePhotoId = 17,PhotoId = 3,ReferenceId = 2},
                new ReferencePhoto() {ReferencePhotoId = 18,PhotoId = 4,ReferenceId = 2},
                new ReferencePhoto() {ReferencePhotoId = 19,PhotoId = 1,ReferenceId = 3},
                new ReferencePhoto() {ReferencePhotoId = 20,PhotoId = 2,ReferenceId = 4},
                new ReferencePhoto() {ReferencePhotoId = 21,PhotoId = 3,ReferenceId = 5},
                new ReferencePhoto() {ReferencePhotoId = 22,PhotoId = 4,ReferenceId = 6},
                new ReferencePhoto() {ReferencePhotoId = 23,PhotoId = 5,ReferenceId = 6}



            }.ForEach(x=>context.ReferencePhotos.AddOrUpdate(x));

            #endregion
            #region blog categories seed

            new List<BlogCategory>()
            {
                new BlogCategory() {BlogCategoryId = 1,BlogId = 1,CategoryId = 1 },
                new BlogCategory() {BlogCategoryId = 2,BlogId = 1,CategoryId = 2 },
                new BlogCategory() {BlogCategoryId = 3,BlogId = 1,CategoryId = 3 },
                new BlogCategory() {BlogCategoryId = 4,BlogId = 2,CategoryId = 1 },
                new BlogCategory() {BlogCategoryId = 5,BlogId = 2,CategoryId = 2 },
                new BlogCategory() {BlogCategoryId = 6,BlogId = 3,CategoryId = 2 },
                new BlogCategory() {BlogCategoryId = 7,BlogId = 3,CategoryId = 3 },
                new BlogCategory() {BlogCategoryId = 8,BlogId = 4,CategoryId = 1 }

            }.ForEach(x => context.BlogCategories.AddOrUpdate(x));

            #endregion


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}

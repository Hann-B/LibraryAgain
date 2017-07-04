namespace LibraryAgain.Migrations
{
    using LibraryAgain.DataContext;
    using LibraryAgain.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LibraryAgain.DataContext.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LibraryContext context)
        {
            var books = new List<Book>
            {
                new Book{ Title="Sanibel Flats", Author="Randy Wayne White", YearPublished=1990, Genre="Mystery"},
                new Book{ Title="Skinny Dip", Author="Randy Wayne White", YearPublished=2005, Genre="Fiction"},
                new Book{ Title="Gone", Author="Randy Wayne White", YearPublished=2012, Genre="Mystery"},
                new Book{ Title="Deep", Author="James Nestor", YearPublished=2014, Genre="Science"},
                new Book{ Title="Swimsuit", Author="James Patterson", YearPublished=2008, Genre="Mystery"}
            };
            books.ForEach(book => context.Books.AddOrUpdate(b => new {b.Title},book));
            context.SaveChanges();
        }
    }
}

using LibraryAgain.DataContext;
using LibraryAgain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAgain.Services
{
    /// <summary>
    /// Book Services
    /// </summary>
    public class BookServices
    {
        private LibraryContext db = new LibraryContext();

        /// <summary>
        /// Get all books
        /// </summary>
        /// <remarks>
        /// Get a list of all library books
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        public IEnumerable<Book> GetAllBooks()
        {
            return new LibraryContext().Books.ToList();
        }

        /// <summary>
        /// Check out a book
        /// </summary>
        /// <remarks>
        /// Check a book out of the library
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        internal void Out(Book book)
        {
            book.IsCheckedOut = true;
            book.LastCheckedOutDate = DateTime.Now;
            db.SaveChanges();
        }

        /// <summary>
        /// Return a book
        /// </summary>
        /// <remarks>
        /// Return a book to the library
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        internal void In(Book book)
        {
            book.IsCheckedOut = false;
            db.SaveChanges();
        }
    }
}
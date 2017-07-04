using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LibraryAgain.DataContext;
using LibraryAgain.Models;
using LibraryAgain.Services;

namespace LibraryAgain.Controllers
{
    /// <summary>
    /// Books in the library currently
    /// </summary>
    public class InLibraryController : ApiController
    {
        private LibraryContext db = new LibraryContext();

        /// <summary>
        /// Get all books
        /// </summary>
        /// <remarks>
        /// Get a list of all library books checked in
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        // GET: api/InLibrary
        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return new BookServices().GetAllBooks()
                .Where(w => w.IsCheckedOut == false);
        }

        /// <summary>
        /// Check out a book
        /// </summary>
        /// <remarks>
        /// Checkout a book
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        //CHECK-OUT
        [HttpPut]
        public IHttpActionResult PutBook(int id, Book book)
        {
            book = db.Books.Find(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            var service = new BookServices();

            if (!book.IsCheckedOut)
            {
                service.Out(book);
                db.Entry(book).Property(p => p.IsCheckedOut).IsModified = true;
                db.SaveChanges();
                return Ok(new { message = "You have checked Out a Book", book.Title, book.Author, book.NeatDueBackDate });
            }

            if (book.IsCheckedOut)
            {
                return Ok(new { message = "This book is already Checked Out", book.NeatDueBackDate});
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}
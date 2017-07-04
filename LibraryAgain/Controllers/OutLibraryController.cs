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
    /// Books Checked Out of Library
    /// </summary>
    public class OutLibraryController : ApiController
    {
        private LibraryContext db = new LibraryContext();

        /// <summary>
        /// Get all books checked out
        /// </summary>
        /// <remarks>
        /// Get a list of all library books that are checked out
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        // GET: api/OutLibrary
        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return new BookServices().GetAllBooks()
                .Where(w => w.IsCheckedOut == true);
        }

        /// <summary>
        /// Book Return
        /// </summary>
        /// <remarks>
        /// Return a checked out book to the library
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        //CHECK-IN
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
            if (book.IsCheckedOut)
            {
                service.In(book);
                db.Entry(book).Property(p => p.IsCheckedOut).IsModified = true;
                db.SaveChanges();
                return Ok(new { message = "You have checked In a Book", book.Title, book.Author });
            }
            if (!book.IsCheckedOut)
            {
                return Ok(new { message = "This book is already Checked In"});
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
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
    /// Book controller to create, read, update, and edit books
    /// </summary>
    public class BooksController : ApiController
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
        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return new BookServices().GetAllBooks();
        }

        /// <summary>
        /// Get a book
        /// </summary>
        /// <remarks>
        /// Get a book by book id
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        // GET: api/Books/id
        [HttpGet]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        /// <summary>
        /// Edit a book
        /// </summary>
        /// <remarks>
        /// Edit a book
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        // PUT: api/Books/5
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

            db.Entry(book).State = EntityState.Modified;

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

        /// <summary>
        /// Create a book
        /// </summary>
        /// <remarks>
        /// Add new a book to the library
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        // POST: api/Books
        [HttpPost]
        public IHttpActionResult PostBook([FromBody]Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        /// <remarks>
        /// Delete a book from the library
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        // DELETE: api/Books/5
        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}
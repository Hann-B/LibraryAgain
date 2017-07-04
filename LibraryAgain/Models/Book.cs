using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAgain.Models
{
    /// <summary>
    /// Book
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Book Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title of the book
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Author of the book
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Year the book was published
        /// </summary>
        public int YearPublished { get; set; }
        /// <summary>
        /// Genre of the book
        /// </summary>
        public string Genre { get; set; }
        /// <summary>
        /// Is the book checked out?
        /// </summary>
        public bool IsCheckedOut { get; set; }
        /// <summary>
        /// The last check out date of the book
        /// </summary>
        public DateTime? LastCheckedOutDate
        {
            get
            {
                return this.lastCheckedOutDate.HasValue
                       ? this.lastCheckedOutDate.Value
                       : DateTime.Now;
            }
            set { this.lastCheckedOutDate = value; }
        }
        private DateTime? lastCheckedOutDate { get; set; }
        /// <summary>
        /// Taylored date of last date the book was checked out
        /// </summary>
        public string NeatLastCheckedOutDate
        {
            get
            {
                if (LastCheckedOutDate.HasValue)
                {
                    return ((DateTime)this.LastCheckedOutDate).ToShortDateString();

                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Due back date of the book
        /// </summary>
          public DateTime? DueBackDate
        {
            get { return IsCheckedOut==true
                    ?this.LastCheckedOutDate.Value.AddDays(10)
                    :(DateTime?)null; }         
        }
        /// <summary>
        /// Taylored date of the due back date of the book
        /// </summary>
        public string NeatDueBackDate
        {
            get
            {
                if (DueBackDate.HasValue)
                {
                    return ((DateTime)this.DueBackDate).ToShortDateString();

                }
                else
                {
                    return null;
                }
            }
        }
    }
}
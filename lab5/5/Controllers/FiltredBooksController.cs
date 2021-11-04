using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5.Data;
using _5.Models;
using _5.Infrastructure;
using _5.Infrastructure.Filters;
using _5.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace _5.Controllers
{
    public class FiltredBooksController : Controller
    {
        private readonly PublishingLabContext _context;
        private FilterBookViewModel _book = new FilterBookViewModel 
        { 
            AuthorName = ""
        };

        public FiltredBooksController(PublishingLabContext context)
        {
            _context = context;
        }

        [SetToSession("SortState")]
        public IActionResult Index(SortState sortOrder)
        {
            var sessionBook = HttpContext.Session.Get("Book");
            var sessionSortState = HttpContext.Session.Get("SortState");
            if (sessionBook != null)
                _book = Transformations.DictionaryToObject<FilterBookViewModel>(sessionBook);
            IQueryable<Book> publishingLabContext = _context.Books;
            publishingLabContext = Sort_Search(publishingLabContext, sortOrder, _book.BookName ?? "");

            BooksViewModel books = new BooksViewModel
            {
                Books = publishingLabContext,
                SortViewModel = new SortViewModel(sortOrder),
                FilterBookViewModel = _book
            };
            return View(books);
        }

        [HttpPost]
        [SetToSession("Book")]
        public IActionResult Index(FilterBookViewModel book)
        {
            var sessionSortState = HttpContext.Session.Get("SortState");
            var sortBook = new SortState();
            if (sessionSortState.Count > 0)
                sortBook = (SortState)Enum.Parse(typeof(SortState), sessionSortState["sortBook"]);
            IQueryable<Book> publishingLabContext = _context.Books;
            publishingLabContext = Sort_Search(publishingLabContext, sortBook, book.AuthorName ?? "");

            BooksViewModel books = new BooksViewModel
            {
                Books = publishingLabContext,
                SortViewModel = new SortViewModel(sortBook),
                FilterBookViewModel = book
            };
            return View(books);
        }

        private IQueryable<Book> Sort_Search(IQueryable<Book> books, SortState sortBook, string searchAuthorName)
        {
            switch (sortBook)
            {
                case SortState.AuthorNameAsc:
                    books = books.OrderBy(s => s.Author.Fio);
                    break;
                case SortState.AuthornameDesc:
                    books = books.OrderByDescending(s => s.Author.Fio);
                    break;
            }
            books = books.Include(o => o.Author).Where
                (o => o.Author.Fio.Contains(searchAuthorName ?? ""));
            return books;
        }
    }
}

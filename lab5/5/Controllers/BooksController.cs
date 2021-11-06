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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _5.Controllers
{
    public class BooksController : Controller
    {
        private readonly PublishingLabContext _context;
        private readonly int pageSize = 15;  

        public BooksController(PublishingLabContext context)
        {
            _context = context;
        }

        [SetToSession("Operation")]
        public IActionResult Index(FilterBookViewModel book, SortState sortOrder, int page = 1)
        {
            if (book.AuthorName == null & book.BookName == null)
            {
                var sessionBook = HttpContext.Session.Get("Book");
                if (sessionBook != null)
                    book = Transformations.DictionaryToObject<FilterBookViewModel>(sessionBook);
            }


            IQueryable<Book> publishingLabContext = _context.Books;
            publishingLabContext = Sort_Search(publishingLabContext, sortOrder, book.AuthorName ?? "", book.BookName ?? "");

            var count = publishingLabContext.Count();
            publishingLabContext = publishingLabContext.Skip((page - 1) * pageSize).Take(pageSize);

            BooksViewModel books = new BooksViewModel
            {
                Books = publishingLabContext,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterBookViewModel = book
            };
            return View(books);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(o => o.Author)
                .Include(o => o.Orders)
                .SingleOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorName");
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,BookName,Total,ExitDate,BaseCost,FinishCost,Salary,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName", book.BookId);
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName", book.BookId);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,BookName,Total,ExitDate,BaseCost,FinishCost,Salary,AuthorId")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName", book.BookId);
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operation = await _context.Books
                .Include(o => o.Author)
                .Include(o => o.Orders)
                .SingleOrDefaultAsync(m => m.BookId == id);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(m => m.BookId == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
        private IQueryable<Book> Sort_Search(IQueryable<Book> books, SortState sortOrder, string searchBookName, string searchAuthorName)
        {
            switch (sortOrder)
            {
                case SortState.AuthorNameAsc:
                    books = books.OrderBy(s => s.Author.Fio);
                    break;
                case SortState.AuthornameDesc:
                    books = books.OrderByDescending(s => s.Author.Fio);
                    break;
            }
            books = books.Include(o => o.Author)
                .Where(o => o.Author.Fio.Contains(searchAuthorName ?? ""));

            return books;
        }
    }
}

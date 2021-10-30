using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5.Data;
using _5.Models;
using _5.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace _5.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly int pageSize = 15;
        private readonly PublishingLabContext _context;

        public AuthorsController(PublishingLabContext context)
        {
            _context = context;
        }
        public IActionResult Index(string AuthorName, SortState sortOrder, int page = 1)
        {
            IQueryable<Author> publishingContext = _context.Authors;
            publishingContext = Sort_Search(publishingContext, sortOrder, AuthorName ?? "");
            var count = publishingContext.Count();
            publishingContext = publishingContext.Skip((page - 1) * pageSize).Take(pageSize);
            AuthorsViewModel authors = new AuthorsViewModel
            {
                Authors = publishingContext,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                AuthorName = AuthorName
            };
            return View(authors);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .SingleOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,AuthorName")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.SingleOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,AuthorName")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorId))
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
            return View(author);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .SingleOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.SingleOrDefaultAsync(m => m.AuthorId == id);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
        private IQueryable<Author> Sort_Search(IQueryable<Author> authors, SortState sortOrder, string AuthorName)
        {
            switch (sortOrder)
            {
                case SortState.AuthorNameAsc:
                    authors = authors.OrderBy(s => s.Fio);
                    break;
                case SortState.AuthornameDesc:
                    authors = authors.OrderByDescending(s => s.Fio);
                    break;
            }
            authors = authors.Where(o => o.Fio.Contains(AuthorName ?? ""));
            return authors;
        }
    }
}

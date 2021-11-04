using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5.Data;
using _5.Models;
using _5.Infrastructure;
using _5.ViewModels;
using _5.Infrastructure.Filters;
using Microsoft.EntityFrameworkCore;

namespace _5.Controllers
{
    public class FiltredContractsController : Controller
    {
        public readonly PublishingLabContext _context;
        private FilterContractViewModel _contract = new FilterContractViewModel
        {
            AuthorName = ""
        };

        public FiltredContractsController(PublishingLabContext context)
        {
            _context = context;
        }

        [SetToSession("SortState")]
        public IActionResult Index(SortState sortOrder)
        {
            var sessionContract = HttpContext.Session.Get("Contract");
            var sessionSortState = HttpContext.Session.Get("SortState");
            if (sessionContract != null)
                _contract = Transformations.DictionaryToObject<FilterContractViewModel>(sessionContract);
            if ((sessionSortState != null))
                if ((sessionSortState.Count > 0) & (sortOrder == SortState.No)) sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState["sortOrder"]);
            IQueryable<Contract> publisingLabContext = _context.Contracts;
            publisingLabContext = Sort_Search(publisingLabContext, sortOrder, _contract.AuthorName ?? "");
            ContractsViewModel contracts = new ContractsViewModel
            {
                Contracts = publisingLabContext,
                SortViewModel = new SortViewModel(sortOrder),
                FilterContractViewModel = _contract
            };
            return View(contracts);
        }

        [HttpPost]
        [SetToSession("Book")]
        public IActionResult Index(FilterContractViewModel contract)
        {
            var sessionSortState = HttpContext.Session.Get("SortState");
            var sortOrder = new SortState();
            if (sessionSortState.Count > 0)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState["sortOrder"]);

            IQueryable<Contract> publishingLabContext = _context.Contracts;
            publishingLabContext = Sort_Search(publishingLabContext, sortOrder, contract.AuthorName ?? "");

            ContractsViewModel contracts = new ContractsViewModel
            {
                Contracts = publishingLabContext,
                SortViewModel = new SortViewModel(sortOrder),
                FilterContractViewModel = contract
            };
            return View(contracts);
        }

        private IQueryable<Contract> Sort_Search(IQueryable<Contract> contracts, SortState sortOrder, string searchAuthorName)
        {
            switch (sortOrder)
            {
                case SortState.AuthorNameAsc:
                    contracts = contracts.OrderBy(s => s.Author.Fio);
                    break;
                case SortState.AuthornameDesc:
                    contracts = contracts.OrderByDescending(s => s.Author.Fio);
                    break;
            }
            contracts = contracts.Include(o => o.Author)
                .Where(o => o.Author.Fio.Contains(searchAuthorName ?? ""));
            return contracts;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;
        private readonly SellerService _sellerService;

        public SalesRecordsController(SalesRecordService salesRecordService, SellerService sellerService)
        {
            _salesRecordService = salesRecordService;
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var salesRecordList = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(salesRecordList);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var salesRecordList = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(salesRecordList);
        }

        //Get Request
        public async Task<IActionResult> Create()
        {
            var sellerList = await _sellerService.FindAllAsync();
            var viewModel = new SaleRecordFormViewModel { Sellers = sellerList };
            return View(viewModel);
        }

        //Post Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesRecord saleRecord)
        {
            if (!ModelState.IsValid)
            {
                var sellerList = await _sellerService.FindAllAsync();
                var viewModel = new SaleRecordFormViewModel { Sellers = sellerList };
                return View(viewModel);
            }
            await _salesRecordService.InsertAsync(saleRecord);
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using System.Diagnostics;
using SalesWebMVC.Services.Exceptions;

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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var saleRecord = await _salesRecordService.FindByIdAsync(id.Value);
            if (saleRecord == null)
            {
                RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(saleRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _salesRecordService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException)
            {
                return RedirectToAction(nameof(Error), new { message = "Cannot delete this sale" });
            }
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _salesRecordService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var sellers = await _sellerService.FindAllAsync();
            var viewModel = new SaleRecordFormViewModel { SaleRecord = obj, Sellers = sellers};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SalesRecord saleRecord)
        {
            if (!ModelState.IsValid)
            {
                var sellerList = await _sellerService.FindAllAsync();
                var viewModel = new SaleRecordFormViewModel { Sellers = sellerList };
                return View(viewModel);
            }
            if (id != saleRecord.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _salesRecordService.UpdateAsync(saleRecord);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsManager_UI_Web.Data;
using AccountsManager_UI_Web.Data.DTOs;
using AccountsManager_UI_Web.Models;
using AccountsManager_UI_Web.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountsManager_UI_Web.Controllers
{
    public class CreditController : Controller
    {
        private readonly IGenericRepository<Credit> _CreditRepository;
        private readonly IMapper _mapper;

        public CreditController(IGenericRepository<Credit> CreditRepository, IMapper mapper)
        {
            CreditRepository.ResourcePath = "api/Credit";
            _CreditRepository = CreditRepository;
            _mapper = mapper;
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<ViewResult> Edit(int Id)
        {
            var Credit = await _CreditRepository.GetByIdAsync(Id);

            if (Credit == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Credit not found!");
            }

            var CreditModel = _mapper.Map<CreditEditModel>(Credit);
            ViewBag.FormAspAction = "Edit";

            return View(CreditModel);
        }

        [HttpPost("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Edit(int Id, [FromForm]CreditEditModel model)
        {
            var Credit = await _CreditRepository.GetByIdAsync(Id);

            if (Credit == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Credit not found!");
            }

            _mapper.Map(model, Credit);

            await _CreditRepository.UpdateAsync(Credit);

            return RedirectToAction("Index");
        }

        [HttpGet("[controller]/Create/{invoiceId}")]
        public ViewResult Create(int invoiceId)
        {
            var CreditModel = new CreditEditModel();
            ViewBag.FormAspAction = "Create";

            CreditModel.Id = 0;
            CreditModel.CreditDate = DateTime.Now;
            CreditModel.InvoiceId = invoiceId;

            return View("Create", CreditModel);
        }

        [HttpPost("[controller]/Create/{InvoiceId}")]
        public async Task<IActionResult> Create([FromForm]CreditEditModel model)
        {
            var Credit = new Credit();
            _mapper.Map(model, Credit);

            _CreditRepository.ResourcePath = $"api/invoice/{Credit.InvoiceId}/credit";

            await _CreditRepository.StoreNewAsync(Credit);

            return RedirectToAction("Dash","Invoice", new { Id = Credit.InvoiceId},"credit");
        }
    }
}

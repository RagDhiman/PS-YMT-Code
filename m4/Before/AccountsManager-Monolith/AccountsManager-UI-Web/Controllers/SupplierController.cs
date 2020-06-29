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

namespace AccountsManager_UI_Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IGenericRepository<Supplier> _SupplierRepository;
        private readonly IMapper _mapper;

        public SupplierController(IGenericRepository<Supplier> SupplierRepository, IMapper mapper)
        {
            SupplierRepository.ResourcePath = "api/Supplier";
            _SupplierRepository = SupplierRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var Suppliers = await _SupplierRepository.GetAllAsync();

            var SupplierModels = _mapper.Map<IEnumerable<SupplierIndexModel>>(Suppliers);

            return View(SupplierModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<ViewResult> Edit(int Id)
        {
            var Supplier = await _SupplierRepository.GetByIdAsync(Id);

            if (Supplier == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Supplier not found!");
            }

            var SupplierModel = _mapper.Map<SupplierEditModel>(Supplier);
            ViewBag.FormAspAction = "Edit";

            return View(SupplierModel);
        }

        [HttpPost("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Edit(int Id, [FromForm]SupplierEditModel model)
        {
            var Supplier = await _SupplierRepository.GetByIdAsync(Id);

            if (Supplier == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Supplier not found!");
            }

            _mapper.Map(model, Supplier);

            await _SupplierRepository.UpdateAsync(Supplier);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Create()
        {
            var SupplierModel = new SupplierEditModel();
            ViewBag.FormAspAction = "Create";

            return View("Edit", SupplierModel);
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> Create([FromForm]SupplierEditModel model)
        {
            var Supplier = new Supplier();
            _mapper.Map(model, Supplier);

            await _SupplierRepository.StoreNewAsync(Supplier);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var Supplier = await _SupplierRepository.GetByIdAsync(Id);

            if (Supplier == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Supplier not found!");
            }

            await _SupplierRepository.DeleteAsync(Supplier);

            return RedirectToAction("Index");
        }
    }
}

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
    public class ExpenseController : Controller
    {
        private readonly IGenericRepository<Expense> _ExpenseRepository;
        private readonly IMapper _mapper;

        public ExpenseController(IGenericRepository<Expense> ExpenseRepository, IMapper mapper)
        {
            ExpenseRepository.ResourcePath = "api/Expense";
            _ExpenseRepository = ExpenseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var Expenses = await _ExpenseRepository.GetAllAsync();

            var ExpenseModels = _mapper.Map<IEnumerable<ExpenseIndexModel>>(Expenses);

            return View(ExpenseModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<ViewResult> Edit(int Id)
        {
            var Expense = await _ExpenseRepository.GetByIdAsync(Id);

            if (Expense == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Expense not found!");
            }

            var ExpenseModel = _mapper.Map<ExpenseEditModel>(Expense);
            ViewBag.FormAspAction = "Edit";

            return View(ExpenseModel);
        }

        [HttpPost("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Edit(int Id, [FromForm]ExpenseEditModel model)
        {
            var Expense = await _ExpenseRepository.GetByIdAsync(Id);

            if (Expense == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Expense not found!");
            }

            _mapper.Map(model, Expense);

            await _ExpenseRepository.UpdateAsync(Expense);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Create()
        {
            var ExpenseModel = new ExpenseEditModel();
            ViewBag.FormAspAction = "Create";

            return View("Edit", ExpenseModel);
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> Create([FromForm]ExpenseEditModel model)
        {
            var Expense = new Expense();
            _mapper.Map(model, Expense);

            await _ExpenseRepository.StoreNewAsync(Expense);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var Expense = await _ExpenseRepository.GetByIdAsync(Id);

            if (Expense == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Expense not found!");
            }

            await _ExpenseRepository.DeleteAsync(Expense);

            return RedirectToAction("Index");
        }
    }
}

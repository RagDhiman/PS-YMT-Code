using AccountsManager_UI_Web.Data;
using AccountsManager_UI_Web.Data.DTOs;
using AccountsManager_UI_Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesManager_UI_Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IGenericRepository<Employee> _EmployeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IGenericRepository<Employee> EmployeeRepository,
            IMapper mapper)
        {
            _EmployeeRepository = EmployeeRepository;
            _EmployeeRepository.ResourcePath = "api/Employee";
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> Index(int Id)
        {
            var employees = await _EmployeeRepository.GetAllAsync();
            var model = _mapper.Map<IEnumerable<EmployeeIndexModel>>(employees);

            return View(model);
        }
    }
}
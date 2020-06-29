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
    public class AccountController : Controller
    {
        private readonly IGenericRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public AccountController(IGenericRepository<Account> accountRepository,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _accountRepository.ResourcePath = "api/account";
            _mapper = mapper;
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var Account = await _accountRepository.GetByIdAsync(Id);
            var model = _mapper.Map<AccountDetailModel>(Account);

            return View(model);
        }
    }
}
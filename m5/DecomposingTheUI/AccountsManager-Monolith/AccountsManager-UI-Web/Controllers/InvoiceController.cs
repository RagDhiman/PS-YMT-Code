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
    public class InvoiceController : Controller
    {
        private readonly IGenericRepository<Invoice> _invoiceRepository;

        private readonly IGenericRepository<CreditNote> _creditNoteRepository;
        private readonly IGenericRepository<DelayedCharge> _delayedChargeRepository;
        private readonly IGenericRepository<Estimate> _estimateRepository;
        private readonly IGenericRepository<Payment> _paymentRepository;
        private readonly IGenericRepository<SalesReceipt> _salesReceiptRepository;
        private readonly IGenericRepository<InvoiceLine> _invoiceLineRepository;
        private readonly IGenericRepository<Credit> _creditRepository;
        private readonly IMapper _mapper;

        public InvoiceController(IGenericRepository<Invoice> InvoiceRepository,
                    IGenericRepository<CreditNote> CreditNoteRepository,
                    IGenericRepository<Credit> CreditRepository,
                    IGenericRepository<DelayedCharge> DelayedChargeRepository,
                    IGenericRepository<Estimate> EstimateRepository,
                    IGenericRepository<Payment> PaymentRepository,
                    IGenericRepository<SalesReceipt> SalesReceiptRepository,
                    IGenericRepository<InvoiceLine> InvoiceLineRepository,
        IMapper mapper)
        {
            InvoiceRepository.ResourcePath = "api/Invoice";
            _invoiceRepository = InvoiceRepository;
            _creditNoteRepository = CreditNoteRepository;
            _delayedChargeRepository = DelayedChargeRepository;
            _estimateRepository = EstimateRepository;
            _paymentRepository = PaymentRepository;
            _salesReceiptRepository = SalesReceiptRepository;
            _invoiceLineRepository = InvoiceLineRepository;
            _creditRepository = CreditRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var Invoices = await _invoiceRepository.GetAllAsync();

            var InvoiceModels = _mapper.Map<IEnumerable<InvoiceIndexModel>>(Invoices);

            return View(InvoiceModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<ViewResult> Edit(int Id)
        {
            var Invoice = await _invoiceRepository.GetByIdAsync(Id);

            if (Invoice == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Invoice not found!");
            }

            var InvoiceModel = _mapper.Map<InvoiceEditModel>(Invoice);
            ViewBag.FormAspAction = "Edit";

            return View(InvoiceModel);
        }

        [HttpPost("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Edit(int Id, [FromForm]InvoiceEditModel model)
        {
            var Invoice = await _invoiceRepository.GetByIdAsync(Id);

            if (Invoice == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Invoice not found!");
            }

            _mapper.Map(model, Invoice);

            await _invoiceRepository.UpdateAsync(Invoice);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Create()
        {
            var InvoiceModel = new InvoiceEditModel();
            ViewBag.FormAspAction = "Create";

            return View("Edit", InvoiceModel);
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> Create([FromForm]InvoiceEditModel model)
        {
            var Invoice = new Invoice();
            _mapper.Map(model, Invoice);

            await _invoiceRepository.StoreNewAsync(Invoice);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var Invoice = await _invoiceRepository.GetByIdAsync(Id);

            if (Invoice == null)
            {
                Response.StatusCode = NotFound().StatusCode;
                return View("Invoice not found!");
            }

            await _invoiceRepository.DeleteAsync(Invoice);

            return RedirectToAction("Index");
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Dash(int Id)
        {
            //Invoice
            var invoice = await _invoiceRepository.GetByIdAsync(Id);
            var model = _mapper.Map<InvoiceDetailModel>(invoice);
            SetupChildRepositories(Id);
            InvoiceDashViewModel invoiceViewModel = new InvoiceDashViewModel();
            invoiceViewModel.Invoice = model;

            //Credit Notes
            var creditNotes = await _creditNoteRepository.GetAllAsync();
            invoiceViewModel.CreditNotes = _mapper.Map<CreditNoteIndexModel[]>(creditNotes);

            //Credit
            var credits = await _creditRepository.GetAllAsync();
            var creditListViewModel = new CreditListViewModel();
            creditListViewModel.InvoiceId = Id;
            creditListViewModel.Credits = _mapper.Map<CreditIndexModel[]>(credits);
            invoiceViewModel.CreditListViewModel = creditListViewModel;

            //Delayed Charge
            var delayedCharges = await _delayedChargeRepository.GetAllAsync();
            invoiceViewModel.DelayedCharge = _mapper.Map<DelayedChargeIndexModel[]>(delayedCharges);

            //Estimate
            var estimates = await _estimateRepository.GetAllAsync();
            invoiceViewModel.Estimates = _mapper.Map<EstimateIndexModel[]>(estimates);

            //Invoice Line
            var invoiceLines = await _invoiceLineRepository.GetAllAsync();
            invoiceViewModel.InvoiceLines = _mapper.Map<InvoiceLineIndexModel[]>(invoiceLines);

            //Payment
            var payments = await _paymentRepository.GetAllAsync();
            invoiceViewModel.Payments = _mapper.Map<PaymentIndexModel[]>(payments);

            //SalesReceipt
            var salesReceipt = await _salesReceiptRepository.GetAllAsync();
            invoiceViewModel.SalesReceipts = _mapper.Map<SalesReceiptIndexModel[]>(salesReceipt);

            return View(invoiceViewModel);
        }

        private void SetupChildRepositories(int forInvoiceId)
        {
            _creditNoteRepository.ResourcePath = $"api/invoice/{forInvoiceId}/creditnote";
            _creditRepository.ResourcePath = $"api/invoice/{forInvoiceId}/credit";
            _delayedChargeRepository.ResourcePath = $"api/invoice/{forInvoiceId}/delayedCharge";
            _estimateRepository.ResourcePath = $"api/invoice/{forInvoiceId}/estimate";
            _paymentRepository.ResourcePath = $"api/invoice/{forInvoiceId}/payment";
            _salesReceiptRepository.ResourcePath = $"api/invoice/{forInvoiceId}/salesReceipt";
            _invoiceLineRepository.ResourcePath = $"api/invoice/{forInvoiceId}/invoiceLine";
        }
    }
}

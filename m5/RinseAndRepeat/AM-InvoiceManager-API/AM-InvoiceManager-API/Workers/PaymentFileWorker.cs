using AM_CustomerManager_Data_EFC.Repositories;
using AM_InvoiceManager_Core;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace AccountsManager_API.Workers
{
    public class PaymentFileWorker : BackgroundService
    {
        private readonly ILogger<PaymentFileWorker> _logger;
        private readonly string _paymentFilePath;
        private readonly string _paymentFileFilter;
        private PaymentRepository _paymentRepository;

        public PaymentFileWorker(ILogger<PaymentFileWorker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _paymentFilePath = configuration.GetValue<string>("PaymentFilePath");
            _paymentFileFilter = configuration.GetValue<string>("PaymentFileFilter");

            _paymentRepository = new PaymentRepository(configuration.GetConnectionString("InvoiceManagerConnection"));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested && !string.IsNullOrEmpty(_paymentFilePath))
            {
                FindFilesToProcess();
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void FindFilesToProcess()
        {
            try
            {
                var filesToProcess = Directory.GetFiles(_paymentFilePath, _paymentFileFilter).ToList();
                if (filesToProcess.Count > 0)
                {
                    foreach (string fileToProcess in filesToProcess)
                    {
                        ProcessPaymentFile(fileToProcess);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in find payment files to process: {e.Message}");
            }
        }

        private void MoveFileToProcessed(string fileToProcess)
        {
            File.Move(fileToProcess, $"{_paymentFilePath}\\Processed\\{System.DateTime.Now.Ticks.ToString()}.txt");
        }

        private async void ProcessPaymentFile(string fileToProcess)
        {
            TextReader reader = null;
            CsvReader csvReader = null;
            IEnumerable<Payment> paymentRecords;

            try
            {
                reader = new StreamReader(fileToProcess);
                csvReader = new CsvReader(reader);
                paymentRecords = csvReader.GetRecords<Payment>();

                var paymentsList = paymentRecords.ToList<Payment>();



                _logger.LogInformation($"Payments to process {paymentsList.Count()}");

                foreach (Payment newPayment in paymentsList)
                {
                    await _paymentRepository.StoreNewPaymentAsync(newPayment.InvoiceId, newPayment);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in Process Payment File: {e.Message}");
                reader?.Close();
                csvReader?.Dispose();
                paymentRecords = null;

                MoveFileToErrored(fileToProcess);
            }
            finally
            {
                reader?.Close();
                csvReader?.Dispose();
                paymentRecords = null;
                MoveFileToProcessed(fileToProcess);

            }
        }

        private void MoveFileToErrored(string fileToProcess)
        {
            File.Move(fileToProcess, $"{_paymentFilePath}\\Errored\\{System.DateTime.Now.Ticks.ToString()}.txt");
        }
    }
}

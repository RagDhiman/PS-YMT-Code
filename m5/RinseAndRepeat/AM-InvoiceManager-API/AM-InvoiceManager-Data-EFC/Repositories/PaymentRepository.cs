using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class PaymentRepository : DapperBaseRepository, IPaymentRepository
    {
        internal const string SQL =
            @"SELECT
                    B.Id,
                    B.InvoiceId,
                    B.CustomerId,
                    B.PaymentDate,
                    B.PaymentMethod,
                    B.Memo,
                    B.AmountReceived
                FROM vwIMPayments B";

        public PaymentRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeletePayment(Payment Payment)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMPaymentDelete", new { Payment.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Payment[]> GetAllPaymentesAsync(int InvoiceId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Payments = await con.QueryAsync<Payment>(sql: $"{SQL} where B.InvoiceId = @InvoiceId", new { InvoiceId }, commandType: CommandType.Text);

                return Payments.AsList().ToArray();
            }
        }

        public async Task<Payment> GetPaymentAsync(int? InvoiceId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Payment = await con.QueryAsync<Payment>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Payment.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewPaymentAsync(int InvoiceId, Payment Payment)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Payment>(sql: "spIMPaymentInsert",
                    new
                    {
                        Payment.InvoiceId,
                        Payment.CustomerId,
                        Payment.PaymentDate,
                        Payment.PaymentMethod,
                        Payment.Memo,
                        Payment.AmountReceived
                    },
                    commandType: CommandType.StoredProcedure);

                Payment.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdatePaymentAsync(int InvoiceId, Payment Payment)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMPaymentUpdate",
                    new
                    {
                        Payment.Id,
                        Payment.InvoiceId,
                        Payment.CustomerId,
                        Payment.PaymentDate,
                        Payment.PaymentMethod,
                        Payment.Memo,
                        Payment.AmountReceived
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}

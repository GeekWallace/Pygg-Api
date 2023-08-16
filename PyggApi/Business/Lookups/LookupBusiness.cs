using Microsoft.EntityFrameworkCore;
using PyggApi.Interfaces.Lookups;

namespace PyggApi.Business.Lookups
{
    public class LookupBusiness : ILookups
    {
        private readonly ApiDbContext _context;

        public LookupBusiness(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<List<Day>> GetAllDays()
        {
            var days = await _context.Days.ToListAsync();
            return days;
        }

        public async Task<List<Frequency>> GetAllFrequencies()
        {
            var frequencies = await _context.Frequencies.ToListAsync();
            return frequencies;
        }

        public async Task<List<PaymentType>> GetPaymentTypes()
        {
            var paymentTypes = await _context.PaymentTypes
                .Where(a => a.PaymentTypeId != "PYT02")
                .ToListAsync();
            return paymentTypes;
        }

        public async Task<List<PaymentType>> GetFinePaymentTypes()
        {
            var paymentTypes = await _context.PaymentTypes
                .Where(a => a.PaymentTypeId != "PYT01" && a.PaymentTypeId != "PYT03")
                .ToListAsync();
            return paymentTypes;
        }
        public async Task<List<MemberRole>> GetAllMemberRoles()
        {
            var memberRoles = await _context.MemberRoles.ToListAsync();
            return memberRoles;
        }

        public async Task<List<TransactionType>> GetAllTransactionTypes()
        {
            var transactionTypes = await _context.TransactionTypes.ToListAsync();
            return transactionTypes;
        }

    }
}

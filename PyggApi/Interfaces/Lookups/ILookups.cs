namespace PyggApi.Interfaces.Lookups
{
    public interface ILookups
    {
        Task<List<Day>> GetAllDays();
        Task<List<Frequency>> GetAllFrequencies();
        Task<List<TransactionType>> GetAllTransactionTypes();
        Task<List<MemberRole>> GetAllMemberRoles();
        Task<List<PaymentType>> GetPaymentTypes();
        Task<List<PaymentType>> GetFinePaymentTypes();
    }
}

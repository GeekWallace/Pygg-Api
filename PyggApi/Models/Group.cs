namespace PyggApi.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int? GroupMaxMembers { get; set; }
        public decimal? GroupMaxAmount { get; set; }
        public DateTime? GroupStartDate { get; set; }
        public DateTime? GroupEndDate { get; set; }
        public decimal? GroupPyggBalance { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? CurrentBalance { get; set; }
        public decimal? ExpensesAmount { get; set; }
        public DateTime GroupCreatedOn { get; set; }
        public string GroupCreatedBy { get; set; }
        public bool IsGroupClosed { get; set; }
        public string GroupComments { get; set; }

    }
}

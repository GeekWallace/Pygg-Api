namespace PyggApi.Models
{
    public class Octopus
    {
        public int InstallmentGroupId { get; set; }
        public int InstallmentMemberId { get; set; }
        public int InstallmentUserId { get; set; }
        public string Name { get; set; }
        public int MemberRobinNumber { get; set; }
        public int InstallmentSeries { get; set; }
        public DateTime MemberDueDate { get; set; }
    }
}

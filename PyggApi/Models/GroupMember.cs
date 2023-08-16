namespace PyggApi.Models
{
    public class GroupMember
    {
        public int GroupId { get; set; }
        public int GroupMemberId { get; set; }
        public int GroupMemberUserId { get; set; }
        public DateTime? GroupMemberJoinDate { get; set; }
        public DateTime? GroupMemberExitDate { get; set; }
        public int? GroupMemberRoleId { get; set; }
        public string GroupMemberUserRoleId { get; set; }
        public bool? IsActive { get; set; }
        public string GroupMemberComments { get; set; }
        public DateTime? GroupMemberCreatedOn { get; set; }
        public string GroupMemberCreatedBy { get; set; }
     
    }
}

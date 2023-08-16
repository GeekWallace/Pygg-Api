namespace PyggApi.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string MemberUserId { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberMiddleName { get; set; }
        public string MemberLastName { get; set; }
   
        public string Email { get; set; }
        public string MemberCountryCode { get; set; }
        public string MemberPhoneNumber { get; set; }
        public string MemberComments { get; set; }
        public DateTime? MemberJoinDate { get; set; }
        public int? MemberRatingStars { get; set; }
        public bool? MemberIsActive { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string Gender { get; set; }

    }
}

namespace UserMicroservice.Models
{
    public class FollowedUser
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int FollowedUserID { get; set; }
    }
}

using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Data.Entities
{
    public class PasswordResetCode
    {
        public int Id { get; set; }
        public string HashedCode { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public bool IsRevoked { get; set; }
    }
}
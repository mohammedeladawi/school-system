using SchoolProject.Domain.Entities.Identities;

namespace SchoolProject.Domain.Entities
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
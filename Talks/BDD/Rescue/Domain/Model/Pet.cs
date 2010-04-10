using System.Web.Security;

namespace Domain.Model
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MembershipUser Publisher { get; set; }
        public Genders Gender { get; set; }
        public Animal Animal { get; set; }
        public int Age { get; set; }
        public Status Status { get; set; }
        public string Picture { get; set; }
    }
}
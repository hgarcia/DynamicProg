using Domain.Model;
using NHibernate;

namespace Domain.Repsitories
{
    public class PetRepository : Repository<Pet>
    {
        public PetRepository(ISession session) : base(session)
        {
        }
    }
}

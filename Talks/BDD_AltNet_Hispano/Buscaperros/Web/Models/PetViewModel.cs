using System.Collections.Generic;
using System.Web.Mvc;

namespace Domain.Model
{
    public class PetViewModel : Pet
    {
        public IEnumerable<SelectListItem> Breeds { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}

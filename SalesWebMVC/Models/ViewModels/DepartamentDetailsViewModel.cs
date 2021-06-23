using System.Collections.Generic;

namespace SalesWebMVC.Models.ViewModels
{
    public class DepartamentDetailsViewModel
    {
        public Departament Departament { get; set; }
        public ICollection<Seller> Sellers { get; set; }
    }
}

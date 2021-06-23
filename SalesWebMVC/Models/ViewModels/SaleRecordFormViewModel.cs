using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Models.ViewModels
{
    public class SaleRecordFormViewModel
    {
        public SalesRecord SaleRecord { get; set; }
        public SalesStatus Status { get; set; }
        public ICollection<Seller> Sellers { get; set; }
    }
}

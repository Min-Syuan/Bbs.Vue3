using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Base;
using Yi.Framework.Model.ERP.Entitys;

namespace Yi.Framework.DtoModel.ERP.Purchase
{
    public class PurchaseGetListOutput: EntityDto<long>
    {
        public string Code { get; set; } = string.Empty;
        public DateTime NeedTime { get; set; }
        public string Buyer { get; set; } = string.Empty;
        public long TotalMoney { get; set; }
        public long PaidMoney { get; set; }

        public string SupplierName { get; set; } = string.Empty;
        public PurchaseStateEnum PurchaseState { get; set; }

     
    }
}

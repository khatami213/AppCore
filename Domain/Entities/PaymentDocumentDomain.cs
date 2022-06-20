using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PaymentDocumentDomain
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string IP { get; set; }
        public long Amount { get; set; }
        public long UserId { get; set; }
        public string Cardno { get; set; }

    }
}

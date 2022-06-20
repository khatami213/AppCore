using Domain.DTO.Charge;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPaymentDocumentDomain : IGenericDomain<PaymentDocumentDomain>
    {
        Task<bool> InsertPayDoc(ShaparakPaymentDTO paymentDTO);
    }
}

using Handmade.Application.Contracts;
using Handmade.Context;
using Handmade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Infrastructure
{
    public class PaymentRepository(HandmadeContext context) : GenericRepository<Payment, int>(context), IPaymentRepository
    {
    }
}

using Handmade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Contracts
{
    public interface IPaymentMethodRepository : IGenericRepository<PaymentMethod, int>
    {
        //Task<PaymentMethod> FindAsync(Expression<Func<PaymentMethod, bool>> predicate);

    }
}

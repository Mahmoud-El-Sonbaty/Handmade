using Handmade.Application.Contracts;
using Handmade.Context;
using Handmade.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Infrastructure
{
    public class PaymentMethodRepository(HandmadeContext context) : GenericRepository<PaymentMethod, int>(context), IPaymentMethodRepository
    {

        //private readonly DbSet<PaymentMethod> PaymentMethods;
        //public PaymentMethodRepository(HandmadeContext context):base(context)
        //{
        //    PaymentMethods = context.Set<PaymentMethod>();
        //}
        //public async Task<PaymentMethod> FindAsync(Expression<Func<PaymentMethod, bool>> predicate)
        //{
        //    return await PaymentMethods.FirstOrDefaultAsync(predicate);
        //}
    }
}

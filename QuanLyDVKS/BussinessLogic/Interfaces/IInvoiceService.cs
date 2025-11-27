using System;
using System.Collections.Generic;

namespace BussinessLogic.Interfaces
{
    public interface IInvoiceService
    {
        decimal CalculateTotal(decimal roomPrice, IEnumerable<Tuple<decimal,int>> servicePriceQty, decimal discountPercent);
    }
}

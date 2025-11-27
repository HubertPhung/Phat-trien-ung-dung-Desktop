using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.Interfaces;

namespace BussinessLogic.Services
{
    public class InvoiceService : IInvoiceService
    {
        public decimal CalculateTotal(decimal roomPrice, IEnumerable<Tuple<decimal, int>> servicePriceQty, decimal discountPercent)
        {
            if (discountPercent < 0) discountPercent = 0;
            if (discountPercent > 100) discountPercent = 100;

            decimal services = 0m;
            if (servicePriceQty != null)
            {
                services = servicePriceQty.Sum(x => x.Item1 * x.Item2);
            }

            var subtotal = roomPrice + services;
            var discount = subtotal * (discountPercent / 100m);
            return Math.Max(0, subtotal - discount);
        }
    }
}

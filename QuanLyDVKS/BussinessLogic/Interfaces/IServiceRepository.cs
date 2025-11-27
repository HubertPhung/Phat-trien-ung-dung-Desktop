using System.Collections.Generic;
using BussinessLogic.Models;

namespace BussinessLogic.Interfaces
{
    public interface IServiceRepository
    {
        IEnumerable<ServiceItem> GetByCategory(string category);
        ServiceItem GetByCode(string code);
    }
}

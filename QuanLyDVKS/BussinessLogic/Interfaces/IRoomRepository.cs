using System.Collections.Generic;
using BussinessLogic.Models;

namespace BussinessLogic.Interfaces
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAll();
        Room GetByNumber(string number);
        void Update(Room room);
    }
}

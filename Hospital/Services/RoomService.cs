using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class RoomService
    {
        IRoomRepository roomRepository;
        public RoomService()
        {
            roomRepository = new RoomFileRepository();
        }
        public Room GetById(string id)
        {
            return roomRepository.GetByID(id);
        }
    }
}

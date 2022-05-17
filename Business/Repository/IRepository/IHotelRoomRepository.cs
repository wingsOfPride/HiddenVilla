﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {

        public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> GetHotelRoom(int roomId);
        public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRoom();
        public Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0);

        public Task<int> DeleteHotelRoom(int roomId);
    }
}

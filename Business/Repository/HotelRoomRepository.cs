using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {

            HotelRoom hotelroom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            hotelroom.CreatedDate = DateTime.Now;
            hotelroom.CreatedBy = "";
            var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelroom);

            await _db.SaveChangesAsync();

            return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(roomId);
            if (roomDetails != null)
            {
                var allimages = await _db.HotelRoomsImage.Where(x => x.RoomId == roomId).ToListAsync();
                foreach(var image in allimages)
                {
                    if (File.Exists(image.RoomImageUrl))
                    {
                        File.Delete(image.RoomImageUrl);
                    }
                }
                _db.HotelRoomsImage.RemoveRange(allimages);
                _db.HotelRooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRoom()
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs = _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(_db.HotelRooms);

                return hotelRoomDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId)
        {
            try
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _db.HotelRooms.Include(x => x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));

                return hotelRoom;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //if unique returns null else returns the room obj
        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomid = 0)
        {
            try
            {
                if(roomid == 0)
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                 await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));

                    return hotelRoom;
                }
                else
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
              await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                 && x.Id != roomid));

                    return hotelRoom;
                }
             
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if(roomId == hotelRoomDTO.Id)
                {
                    //valid
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);

                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.HotelRooms.Update(room);

                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);


                }
                else
                {
                    //invalid

                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}

using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelAmenityRepository : IHotelAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelAmenityRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<HotelAmenityDTO> CreateAmenity(HotelAmenityDTO amenityDTO)
        {

            HotelAmenity amenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(amenityDTO);

            var addedAmenity = await _db.HotelAmenities.AddAsync(amenity);

            await _db.SaveChangesAsync();

            return _mapper.Map<HotelAmenity, HotelAmenityDTO>(addedAmenity.Entity);
        }


        public async Task<int> DeleteAmenity(int amenityId)
        {
            var amenityDetails = await _db.HotelAmenities.FindAsync(amenityId);
            if (amenityDetails != null)
            {
               _db.HotelAmenities.Remove(amenityDetails);
              return await _db.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetAllAmenity()
        {
        
            try
            {
                IEnumerable<HotelAmenityDTO> hotelAmenityDTO = _mapper.Map<IEnumerable<HotelAmenity>, IEnumerable<HotelAmenityDTO>>(_db.HotelAmenities);
                
                return hotelAmenityDTO;
            }
            catch
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> GetAmenity(int amenityId)
        {
            var data = _mapper.Map<HotelAmenity, HotelAmenityDTO>(await _db.HotelAmenities.FindAsync(amenityId));

            return data;
           
        }

        public async Task<HotelAmenityDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
                {
                    HotelAmenityDTO hotelAmenity = _mapper.Map<HotelAmenity, HotelAmenityDTO>(
                 await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));

                    return hotelAmenity;
                }
                else
                {
                    HotelAmenityDTO hotelAmenity = _mapper.Map<HotelAmenity, HotelAmenityDTO>(
              await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != roomId));

                    return hotelAmenity;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> UpdateAmenity(int amenityId, HotelAmenityDTO amenityDTO)
        {
            try
            {
                if (amenityId == amenityDTO.Id)
                {
                    //valid
                    HotelAmenity hotelAmenity = await _db.HotelAmenities.FindAsync(amenityId);
                    HotelAmenity amenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(amenityDTO, hotelAmenity);

         
                    var updatedAmenity = _db.HotelAmenities.Update(amenity);

                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelAmenity, HotelAmenityDTO>(updatedAmenity.Entity);


                }
                else
                {
                    //invalid

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

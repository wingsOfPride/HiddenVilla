using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Data;
using Models;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>();
            CreateMap<HotelRoom, HotelRoomDTO>();

            //you can use create map per DTO and model or add a reverseMap

            CreateMap<HotelRoomImage, HotelRoomImageDTO>().ReverseMap();
            CreateMap<HotelAmenity, HotelAmenityDTO>().ReverseMap();
           
        }
    }
}

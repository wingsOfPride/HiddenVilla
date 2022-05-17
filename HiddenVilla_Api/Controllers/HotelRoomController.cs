using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    public class HotelRoomController : Controller
    {
        private readonly IHotelRoomRepository _hotelRepository;
        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRepository = hotelRoomRepository;
        }

        [HttpGet]
        
       public async Task<IActionResult> GethotelRooms()
        {
            var allRooms = await _hotelRepository.GetAllHotelRoom();

            return Ok(allRooms);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetHotelRoom(int? roomId)
        {
            if(roomId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Room Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            else
            {
                var roomDetails = await _hotelRepository.GetHotelRoom(roomId.Value);
                
                if(roomDetails == null)
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "Invalid Room Id",
                        StatusCode = StatusCodes.Status404NotFound
                    });
                }

                return Ok(roomDetails);
            
            }
        }
    }
}

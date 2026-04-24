using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailOrdering.API.DTOs.Order;
using RetailOrdering.API.Helpers;
using RetailOrdering.API.Interfaces;
using RetailOrdering.API.Models;
using System.Security.Claims;

namespace RetailOrdering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orders;
        public OrderController(IOrderService orders)
        {
            _orders = orders;
        }
        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);



        // The User Place an Order
        [HttpPost("place")]
        public async Task<IActionResult> Place(OrderRequestDto dto)
        {
            return Ok(ApiResponse<OrderConfirmationDto>.Ok(await _orders.PlaceOrderAsync(UserId, dto)));
        }


        // The User Get All the Orders 
        [HttpGet("my")]
        public async Task<IActionResult> MyOrders()
        {
            return Ok(ApiResponse<List<OrderDto>>.Ok(await _orders.GetUserOrdersAsync(UserId)));
        }



        //Get all the orders to show for an admin
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> All()
        {
            return Ok(ApiResponse<List<OrderDto>>.Ok(await _orders.GetAllOrdersAsync()));
        }



        //The Admin update the order status
        [HttpPatch("{id}/status"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Status(int id, [FromBody] OrderStatus status)
        {
            await _orders.UpdateStatusAsync(id, status);
            return Ok(ApiResponse<string>.Ok("Status updated"));
        }
    }
}

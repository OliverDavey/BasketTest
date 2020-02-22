using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using BasketTest.SDK.Models;

namespace BasketTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly IStockItemRepository stockItemRepository;
        private readonly IMapper mapper;
        private readonly ILogger<BasketController> logger;

        public BasketController(
            IBasketRepository basketRepository,
            IStockItemRepository stockItemRepository,
            IMapper mapper,
            ILogger<BasketController> logger)
        {
            this.basketRepository = basketRepository;
            this.stockItemRepository = stockItemRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var basket = await this.basketRepository.Get(id);
            if (basket == null)
            {
                return NotFound();
            }

            var result = mapper.Map<GetBasketResponse>(basket);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBasketRequest request)
        {
            foreach (var item in request.Items)
            {
                var stockCheck = await this.stockItemRepository.CheckStock(item.ItemId, item.Quantity);
                if (!stockCheck.Success)
                {
                    return BadRequest(stockCheck.Message);
                }
            }

            return Ok();
        }
    }
}

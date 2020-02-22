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
using BasketTest.Services;
using BasketTest.Services.Models;

namespace BasketTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService basketService;
        private readonly IPricingService pricingService;
        private readonly IBasketValidationService basketValidationService;
        private readonly IMapper mapper;
        private readonly ILogger<BasketController> logger;

        public BasketController(
            IBasketService basketService,
            IPricingService pricingService,
            IBasketValidationService basketValidationService,
            IMapper mapper,
            ILogger<BasketController> logger)
        {
            this.basketService = basketService;
            this.pricingService = pricingService;
            this.basketValidationService = basketValidationService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet("/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var basket = await this.basketService.GetBasket(id);
            if (basket == null)
            {
                return NotFound();
            }

            var result = this.mapper.Map<RetrieveBasketResponse>(basket);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBasketRequest request)
        {
            // validation accepts a direct SDK model which it shouldn't do
            var validation = await this.basketValidationService.ValidateCreateBasket(request);

            if (!validation.Success)
            {
                return BadRequest(validation.Message);
            }

            var unpricedItems = this.mapper.Map<IList<UnpricedBasketItemModel>>(request.Items);
            var pricedItems = await this.pricingService.PriceItems(unpricedItems);

            var model = this.mapper.Map<CreateBasketModel>(request);
            model.Items = pricedItems;

            var result = await this.basketService.CreateBasket(model);

            // Most likely going to need to fetch this info immediately afterwards anyway
            var basket = await this.basketService.GetBasket(result.BasketId);
            return Ok(basket);
        }
    }
}

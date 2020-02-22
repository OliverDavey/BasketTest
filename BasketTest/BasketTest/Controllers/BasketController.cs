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
using BasketTest.Models;

namespace BasketTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly IBasketValidationService basketValidationService;
        private readonly IMapper mapper;
        private readonly ILogger<BasketController> logger;

        public BasketController(
            IBasketRepository basketRepository,
            IBasketValidationService basketValidationService,
            IMapper mapper,
            ILogger<BasketController> logger)
        {
            this.basketRepository = basketRepository;
            this.basketValidationService = basketValidationService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var basket = await this.basketRepository.Get(id);
            if (basket == null)
            {
                return NotFound();
            }

            var result = this.mapper.Map<GetBasketResponse>(basket);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBasketRequest request)
        {
            var validation = await this.basketValidationService.ValidateCreateBasket(request);

            if (!validation.Success)
            {
                return BadRequest(validation.Message);
            }

            // Should fetch the prices from the repository, really
            var model = this.mapper.Map<CreateBasketModel>(request);
            var result = await this.basketRepository.Create(model);

            return Ok(result);
        }
    }
}

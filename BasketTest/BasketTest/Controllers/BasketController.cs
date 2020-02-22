using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketTest.Data;
using BasketTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasketTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly ILogger<BasketController> logger;

        public BasketController(
            IBasketRepository basketRepository,
            ILogger<BasketController> logger)
        {
            this.basketRepository = basketRepository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<Basket> Get([FromRoute] string id)
        {
            var basket = await this.basketRepository.Get(id);

            return basket;
        }
    }
}

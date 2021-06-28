﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peaky.Models.Interfaces;
using Peaky.Infra.PgSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Peaky.Models;

namespace Peaky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorsesController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IHorseRepository repository) {

            /*HorseRepository hr1 = new HorseRepository();

            await hr1.TestQuery();*/

            List<Horse> horses = await repository.GetAll();

            return Ok(horses);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IHorseRepository repository)
        {

            var result = await repository.GetOneById(id);

            return Ok("Hello");

        }

    }
}

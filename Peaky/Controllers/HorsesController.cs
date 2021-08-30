﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peaky.Models.Interfaces;
using Peaky.Infra.PgSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Peaky.Models;
using Peaky.Models.DTOs;
using FluentValidation;
using FluentValidation.Results;
using Peaky.Infra.Mongo;

namespace Peaky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorsesController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IHorseService horseService) {

            /*HorseRepository hr1 = new HorseRepository();

            await hr1.TestQuery();*/

            //TODO: REFACTOR TO SERVICE

            List<Horse> horses = await horseService.ReadAll();

            return Ok(horses);

        }

        [HttpGet("mongo")]
        public async Task<IActionResult> GetAllMongo([FromServices] IMongoRepository<Horse> mongoRepository)
        {



            //var conn = new SampleConnection();

            List<Horse> horses = await mongoRepository.GetAll();

            //Horse h1 = new Horse();

            //h1.name = "Test Horse from API";
            //h1.age = 11;

            //conn.InsertOne(h1);

            return Ok(horses);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IHorseService horseService)
        {

            var result = await horseService.ReadById(id);

            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateHorseDTO dto, [FromServices] IHorseService horseService, [FromServices] IValidator<CreateHorseDTO> validator) {

            ValidationResult validationResults = validator.Validate(dto);

            if (!validationResults.IsValid) {

                return BadRequest(validationResults.Errors);

            }

            Horse horse = await horseService.Create(dto);

            if (horse != null) {

                return Ok(horse);

            }

            return BadRequest("Unable to register horse");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {

            return Ok("NOT IMPLEMENTED");

        }

    }
}

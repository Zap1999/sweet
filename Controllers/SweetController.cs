﻿using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.ApiModels;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/sweet")]
    [ApiController]
    public class SweetController : ControllerBase
    {
        private readonly SweetService _service;


        public SweetController()
        {
            _service = new SweetService(new SweetLifeDbContext());
        }

        [HttpGet]
        public IEnumerable<Sweet> Get()
        {
            return _service.GetSweet();
        }

        [HttpGet("full")]
        public IEnumerable<SweetFullDto> GetFullAll()
        {
            return _service.GetFullAll();
        }

        [HttpGet("{categoryId}")]
        public IEnumerable<Sweet> GetSweetsByCategoryId([FromRoute] int categoryId)
        {
            return _service.GetSweetsByCategoryId(categoryId);
        }

        [HttpPost]
        public void Post([FromBody] SweetPostDto sweetPostDto)
        {
            _service.Save(sweetPostDto.Sweet, sweetPostDto.SweetIngredients);
        }

        [HttpDelete("{sweetId}")]
        public void DeleteSweet([FromRoute] long sweetId)
        {
            _service.Delete(sweetId);
        }
    }
}

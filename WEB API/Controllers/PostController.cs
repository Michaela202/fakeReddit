﻿using Application.Logic;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Controllers;
 [ApiController]
    [Route("[controller]")]
public class PostController: ControllerBase
{
        private readonly IPostLogic postLogic;

        public PostController(IPostLogic postLogic)
        {
            this.postLogic = postLogic;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreateAsync([FromBody] PostCreationDto dto)
        {
            try
            {
                Post created = await postLogic.CreateAsync(dto);
                return Created($"/post/{created.Id}", created);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAsync(
            [FromQuery] int? userId, [FromQuery] string? titleContains, [FromQuery] string? userName)
        {
            try
            {
                SearchPostParametersDto parametersDto = new( userId, titleContains,userName);
                var posts = await postLogic.GetAsync(parametersDto);
                return Ok(posts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> GetAsync(int id)
        {
            try
            {
                PostBasicDto result = await postLogic.GetByIdAsync(id);
                return Ok(result);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }

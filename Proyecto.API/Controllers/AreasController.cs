﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoBL.DTOs;
using ProyectoBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly BdParcialContext context;
        private readonly IMapper mapper;

        public AreasController(BdParcialContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var areas = context.Areas.ToList();
            return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = areas, Message = "Se retorno la información con exíto" });
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var area = context.Areas.Find(id);
            if (area == null)
                return NotFound(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = area, Message = "Se retorno la información con exíto" });
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Post(AreaDTO areaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new RespuestaDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });
                areaDTO.IdArea = null;
                areaDTO.DArea = areaDTO.DArea.ToString().Trim();
                var area = context.Areas.Add(mapper.Map<Area>(areaDTO)).Entity;
                context.SaveChanges();
                areaDTO.IdArea = area.IdArea;

                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = areaDTO, Message = "Se guardo la información exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new RespuestaDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, AreaDTO areaDTO)
        {
            if (areaDTO.IdArea == null || areaDTO.IdArea <= 0)
            {
                return BadRequest(new RespuestaDTO { Code = (int)HttpStatusCode.BadRequest, Message = "El campo IdArea es incorrecto" });
            }
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new RespuestaDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var area = context.Areas.Find(id);
                if (area == null)
                    return NotFound(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                areaDTO.DArea = areaDTO.DArea.ToString().Trim();
                context.Entry(area).State = EntityState.Detached;
                context.Areas.Update(mapper.Map<Area>(areaDTO));
                context.SaveChanges();

                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = areaDTO });
            }
            catch (Exception ex)
            {
                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var area = context.Areas.Find(id);
                if (area == null)
                    return NotFound(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });
                context.Areas.Remove(area);
                context.SaveChanges();
                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Message = "Se ha eliminado el registro con exito." });
            }
            catch (Exception ex)
            {
                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
    }
}

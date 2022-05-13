using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoBL.DTOs;
using ProyectoBL.Models;
using System;
using System.Linq;
using System.Net;

namespace Proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncargadosController : ControllerBase
    {
        private readonly BdParcialContext context;
        private readonly IMapper mapper;

        public EncargadosController(BdParcialContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var encargados = context.Encargados.ToList();
            return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = encargados, Message = "Se retorno la información con exíto" });
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var encargado = context.Encargados.Find(id);
            if (encargado == null)
                return NotFound(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = encargado, Message = "Se retorno la información con exíto" });
        }
        [HttpPost]
        public IActionResult Post(EncargadoDTO encargadoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new RespuestaDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });
                encargadoDTO.IdEncargado = null;
                encargadoDTO.NombreEncargado = encargadoDTO.NombreEncargado.ToString().Trim();
                var encargado = context.Encargados.Add(mapper.Map<Encargado>(encargadoDTO)).Entity;
                context.SaveChanges();
                encargadoDTO.IdEncargado = encargado.IdEncargado;

                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = encargadoDTO, Message = "Se guardo la información exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new RespuestaDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, EncargadoDTO encargadoDTO)
        {
            if (encargadoDTO.IdEncargado == null || encargadoDTO.IdEncargado <= 0)
            {
                return BadRequest(new RespuestaDTO { Code = (int)HttpStatusCode.BadRequest, Message = "El campo IdEncargado es incorrecto" });
            }
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new RespuestaDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var encargado = context.Encargados.Find(id);
                if (encargado == null)
                    return NotFound(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                encargadoDTO.NombreEncargado = encargadoDTO.NombreEncargado.ToString().Trim();
                context.Entry(encargado).State = EntityState.Detached;
                context.Encargados.Update(mapper.Map<Encargado>(encargadoDTO));
                context.SaveChanges();

                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = encargadoDTO, Message = "Se actualizo el registro con exíto" });
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
                var encargado = context.Encargados.Find(id);
                if (encargado == null)
                    return NotFound(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });
                context.Encargados.Remove(encargado);
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

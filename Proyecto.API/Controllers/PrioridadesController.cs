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
    public class PrioridadesController : ControllerBase
    {
        private readonly BdParcialContext context;
        private readonly IMapper mapper;

        public PrioridadesController(BdParcialContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var prioridades = context.Prioridades.ToList();
            return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = prioridades, Message = "Se retorno la información con exíto" });
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var prioridad = context.Prioridades.Find(id);
            if (prioridad == null)
                return NotFound(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = prioridad, Message = "Se retorno la información con exíto" });
        }
        [HttpPost]
        public IActionResult Post(PrioridadDTO prioridadDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new RespuestaDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });
                prioridadDTO.IdPrioridad = null;
                prioridadDTO.DPrioridad = prioridadDTO.DPrioridad.ToString().Trim();
                var prioridad = context.Prioridades.Add(mapper.Map<Prioridad>(prioridadDTO)).Entity;
                context.SaveChanges();
                prioridadDTO.IdPrioridad = prioridad.IdPrioridad;

                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = prioridadDTO, Message = "Se guardo la información exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new RespuestaDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, PrioridadDTO prioridadDTO)
        {
            if (prioridadDTO.IdPrioridad == null || prioridadDTO.IdPrioridad <= 0)
            {
                return BadRequest(new RespuestaDTO { Code = (int)HttpStatusCode.BadRequest, Message = "El campo IdPrioridad es incorrecto" });
            }
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new RespuestaDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var prioridad = context.Prioridades.Find(id);
                if (prioridad == null)
                    return NotFound(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                prioridadDTO.DPrioridad = prioridadDTO.DPrioridad.ToString().Trim();
                context.Entry(prioridad).State = EntityState.Detached;
                context.Prioridades.Update(mapper.Map<Prioridad>(prioridadDTO));
                context.SaveChanges();

                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = prioridadDTO, Message = "Se actualizo el registro con exíto" });
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
                var prioridad = context.Prioridades.Find(id);
                if (prioridad == null)
                    return NotFound(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });
                context.Prioridades.Remove(prioridad);
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

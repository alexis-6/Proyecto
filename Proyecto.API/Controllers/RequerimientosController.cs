using AutoMapper;
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
    public class RequerimientosController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly BdParcialContext context;

        public RequerimientosController(BdParcialContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        /// <summary>
        /// Obtiene una lista de requerimientos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var requerimientos = context.Requerimientos.Include("NombreEncargado").Include("DPrioridad").Include("DArea").ToList();
            var requerimientosDTO = requerimientos.Select(x => mapper.Map<RequerimientoOutputDTO>(x)).OrderByDescending(x => x.IdRequerimiento);
            return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data =  requerimientosDTO, Message = "Se retorno la información con exíto"});
        }
        /// <summary>
        /// Obtiene un requerimiento por su id.
        /// </summary>
        /// <param name="id">Id del requerimiento</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")] //  api/Requerimientos/GetById/1
        public IActionResult GetById(int id)
        {
            var requerimiento = context.Requerimientos.Find(id);
            if (requerimiento == null)
                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var requerimientoDTO = mapper.Map<RequerimientoOutputDTO>(requerimiento);
            return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = requerimientoDTO });
        }
        /// <summary>
        /// Crea un requerimiento.
        /// </summary>
        /// <param name="requerimientoDTO">Objeto del requerimiento</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(RequerimientoDTO requerimientoDTO)
        {
            try
            {
                if (requerimientoDTO.IdRequerimiento != 0)
                {
                    return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "El Id del requerimiento debe ser igual a cero" });
                }
                else if (requerimientoDTO.IdArea <= 0 || requerimientoDTO.IdArea > 10)
                {
                    return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "El Id del area no existe" });
                }
                else if (requerimientoDTO.IdEncargado <= 0 || requerimientoDTO.IdEncargado > 6)
                {
                    return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "El Id del encargado no existe" });
                }
                else if (requerimientoDTO.IdPrioridad <= 0 || requerimientoDTO.IdPrioridad > 3)
                {
                    return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "El Id de la prioridad no existe" });
                }
                requerimientoDTO.FechaSolicitud = System.DateTime.Now; //fecha en que el usuario monta el requerimiento
                requerimientoDTO.FechaDesarrollo = requerimientoDTO.FechaSolicitud.AddDays(requerimientoDTO.DiasDesarrollo); // fecha de desarrollo  = fecha_solic + dias_desarrollo
                var _diasDesarrollo = requerimientoDTO.DiasDesarrollo / 2;
                requerimientoDTO.FechaPrueba = requerimientoDTO.FechaSolicitud.AddDays(_diasDesarrollo); // fecha_solic + el numero divido de los dias de desarrollo
                
                if (!ModelState.IsValid)
                    return Ok(new RespuestaDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var requerimiento = context.Requerimientos.Add(mapper.Map<Requerimiento>(requerimientoDTO)).Entity;
                context.SaveChanges();
                requerimientoDTO.IdRequerimiento = requerimiento.IdRequerimiento;

                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = requerimientoDTO });
            }
            catch (Exception ex)
            {
                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
        /// <summary>
        /// Edita un objeto del requerimiento
        /// </summary>
        /// <param name="id">Id del requrimiento</param>
        /// <param name="requerimientoDTO">Objeto del requerimiento</param>
        /// <returns></returns>
        [HttpPut("{id}")] //    api/Requermientos/1
        public IActionResult Edit(int id, RequerimientoDTO requerimientoDTO)
        {
            try
            {
                if (requerimientoDTO.IdRequerimiento <= 0)
                {
                    return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "Debe agregar el Id del requerimiento en la petición" });
                }
                else if (requerimientoDTO.IdArea <= 0 || requerimientoDTO.IdArea > 10)
                {
                    return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "El Id del area no existe" });
                }
                else if (requerimientoDTO.IdEncargado <= 0 || requerimientoDTO.IdEncargado > 6)
                {
                    return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "El Id del encargado no existe" });
                }
                else if (requerimientoDTO.IdPrioridad <= 0 || requerimientoDTO.IdPrioridad > 3)
                {
                    return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "El Id de la prioridad no existe" });
                }

                if (!ModelState.IsValid)
                    return Ok(new RespuestaDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var requerimiento = context.Requerimientos.Find(id);
                if (requerimiento == null)
                    return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                requerimientoDTO.FechaSolicitud = System.DateTime.Now; //fecha en que el usuario monta el requerimiento
                requerimientoDTO.FechaDesarrollo = requerimientoDTO.FechaSolicitud.AddDays(requerimientoDTO.DiasDesarrollo); // fecha de desarrollo  = fecha_solic + dias_desarrollo
                var _diasDesarrollo = requerimientoDTO.DiasDesarrollo / 2;
                requerimientoDTO.FechaPrueba = requerimientoDTO.FechaSolicitud.AddDays(_diasDesarrollo); // fecha_solic + el numero divido de los dias de desarrollo

                
                context.Entry(requerimiento).State = EntityState.Detached;
                context.Requerimientos.Update(mapper.Map<Requerimiento>(requerimientoDTO));
                context.SaveChanges();

                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.OK, Data = requerimientoDTO });
            }
            catch (Exception ex)
            {
                return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
        /// <summary>
        /// Elimina un objeto del requerimiento
        /// </summary>
        /// <param name="id">Id del requerimiento</param>
        /// <returns></returns>
        [HttpDelete("{id}")] //    api/Requerimientos/5
        public IActionResult Delete(int id)
        {
            try
            {
                var requerimiento = context.Requerimientos.Find(id);
                if (requerimiento == null)
                    return Ok(new RespuestaDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });
                context.Requerimientos.Remove(requerimiento);
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

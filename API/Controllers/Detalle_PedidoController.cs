using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class Detalle_PedidoController: BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Detalle_PedidoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Detalle_Pedido>>> Get()
        {
            var entidades = await _unitOfWork.Detalle_Pedidos.GetAllAsync();
            return _mapper.Map<List<Detalle_Pedido>>(entidades);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Detalle_PedidoDto>> Get(int id)
        {
            var entidad = await _unitOfWork.Detalle_Pedidos.GetByIdAsync(id);
            if(entidad == null)
            {
                return NotFound();
            }
            return _mapper.Map<Detalle_PedidoDto>(entidad);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Detalle_Pedido>> Post(Detalle_PedidoDto Detalle_PedidoDto)
        {
            var entidad = _mapper.Map<Detalle_Pedido>(Detalle_PedidoDto);
            this._unitOfWork.Detalle_Pedidos.Add(entidad);
            await _unitOfWork.SaveAsync();
            if(entidad == null)
            {
                return BadRequest();
            }
            Detalle_PedidoDto.Id = entidad.Id;
            return CreatedAtAction(nameof(Post), new {id = Detalle_PedidoDto.Id}, Detalle_PedidoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Detalle_PedidoDto>> Put(int id, [FromBody] Detalle_PedidoDto Detalle_PedidoDto)
        {
            if(Detalle_PedidoDto == null)
            {
                return NotFound();
            }
            var entidades = _mapper.Map<Detalle_Pedido>(Detalle_PedidoDto);
            _unitOfWork.Detalle_Pedidos.Update(entidades);
            await _unitOfWork.SaveAsync();
            return Detalle_PedidoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var entidad = await _unitOfWork.Detalle_Pedidos.GetByIdAsync(id);
            if(entidad == null)
            {
                return NotFound();
            }
            _unitOfWork.Detalle_Pedidos.Delete(entidad);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }

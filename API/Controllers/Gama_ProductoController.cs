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
public class Gama_ProductoController: BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Gama_ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Gama_Producto>>> Get()
        {
            var entidades = await _unitOfWork.Gama_Productos.GetAllAsync();
            return _mapper.Map<List<Gama_Producto>>(entidades);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Gama_ProductoDto>> Get(int id)
        {
            var entidad = await _unitOfWork.Gama_Productos.GetByIdAsync(id);
            if(entidad == null)
            {
                return NotFound();
            }
            return _mapper.Map<Gama_ProductoDto>(entidad);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Gama_Producto>> Post(Gama_ProductoDto Gama_ProductoDto)
        {
            var entidad = _mapper.Map<Gama_Producto>(Gama_ProductoDto);
            this._unitOfWork.Gama_Productos.Add(entidad);
            await _unitOfWork.SaveAsync();
            if(entidad == null)
            {
                return BadRequest();
            }
            Gama_ProductoDto.Id = entidad.Id;
            return CreatedAtAction(nameof(Post), new {id = Gama_ProductoDto.Id}, Gama_ProductoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Gama_ProductoDto>> Put(int id, [FromBody] Gama_ProductoDto Gama_ProductoDto)
        {
            if(Gama_ProductoDto == null)
            {
                return NotFound();
            }
            var entidades = _mapper.Map<Gama_Producto>(Gama_ProductoDto);
            _unitOfWork.Gama_Productos.Update(entidades);
            await _unitOfWork.SaveAsync();
            return Gama_ProductoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var entidad = await _unitOfWork.Gama_Productos.GetByIdAsync(id);
            if(entidad == null)
            {
                return NotFound();
            }
            _unitOfWork.Gama_Productos.Delete(entidad);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }

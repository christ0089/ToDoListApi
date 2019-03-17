using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TareaApi.Interfaces;
using TareaApi.Models;
using System.Diagnostics;

namespace TareaApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITareaRepository _toDoRepository;

        public TodoController(ITareaRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
        // Request : GET regresa All items
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_toDoRepository.All);
        }
        // Request : GET regresa un item por id
        [HttpGet("{id}")]
        public IActionResult Item(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
            }
            bool itemExist = _toDoRepository.DoesItemExist(id);
            if (!itemExist)
            {
                return BadRequest(ErrorCode.RecordNotFound.ToString());
            }
            return Ok(_toDoRepository.Find(id));

        }

        // Request : POST agrega items a la lista
        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
                }
                bool itemExists = _toDoRepository.DoesItemExist(item.ID);
                if (itemExists)
                {
                    return StatusCode(StatusCodes.Status409Conflict, ErrorCode.TodoItemIDInUse.ToString());
                }
                _toDoRepository.Insert(item);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
            }
            return Ok(item);
        }
        // Request : PUT actualiza un item ya existente
        [HttpPut]
        public IActionResult Edit([FromBody] TodoItem item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
                }
                var existingItem = _toDoRepository.DoesItemExist(item.ID);
                if (!existingItem)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _toDoRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
            }
            return NoContent();
        }
        // Request : DELETE borra un item ya existente
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var item = _toDoRepository.Find(id);
                if (item == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _toDoRepository.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotDeleteItem.ToString());
            }
            return NoContent();
        }
    }
    // Codigos de error dependiendo de la respuesta
    public enum ErrorCode
    {
        TodoItemNameAndNotesRequired,
        TodoItemIDInUse,
        RecordNotFound,
        CouldNotCreateItem,
        CouldNotUpdateItem,
        CouldNotDeleteItem,
    }

}
using System.Collections.Generic;
using System.Linq;
using TareaApi.Interfaces;
using TareaApi.Models;
using System.Diagnostics;
namespace TareaApi.Services
{
    public class ToDoRepository : ITareaRepository
    {
        private readonly TodoContext _context;

        public ToDoRepository(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        public IEnumerable<TodoItem> All
        {
            get { return _context.TodoItems.ToList(); }
        }
        // Dectecta que los items existan en la Base de Datos
        public bool DoesItemExist(string id)
        {
            return _context.TodoItems.Any(item => item.ID == id);
        }
        // Busca los Items por id a la Base de Datos
        public TodoItem Find(string id)
        {
            return _context.TodoItems.FirstOrDefault(item => item.ID == id);
        }
        // Agrega los Items a la Base de Datos
        public async void Insert(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
        }
        // Actualiza un Items por ID a la Base de Datos
        public async void Update(TodoItem item)
        {
            _context.TodoItems.Update(item);
            await _context.SaveChangesAsync();
        }
        // Actualiza un Items por ID a la Base de Datos
        public async void Delete(string id)
        {
            _context.TodoItems.Remove(this.Find(id));
            await _context.SaveChangesAsync();
        }

    }
}
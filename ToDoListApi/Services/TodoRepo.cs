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

        public bool DoesItemExist(string id)
        {
            return _context.TodoItems.Any(item => item.ID == id);
        }

        public TodoItem Find(string id)
        {
            return _context.TodoItems.FirstOrDefault(item => item.ID == id);
        }

        public async void Insert(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async void Update(TodoItem item)
        {

                Debug.WriteLine("Hello World");
                _context.TodoItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async void Delete(string id)
        {
            _context.TodoItems.Remove(this.Find(id));
            await _context.SaveChangesAsync();
        }

    }
}
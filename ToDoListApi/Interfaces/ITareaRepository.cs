using System;
using System.Collections.Generic;
using TareaApi.Models;
namespace TareaApi.Interfaces
{
    public interface ITareaRepository
    {
        // Checar que el item exista en la base de datos por id.
        bool DoesItemExist(string id);
        // Obtener All items.
        IEnumerable<TodoItem> All { get; }
        // Buscar items por id y regresar el item.
        TodoItem Find(string id);
        // Agregar item a la base de datos.
        void Insert(TodoItem item);
        // Actualizar la base de datos.
        void Update(TodoItem item);
        // Borrar item de la base de datos.
        void Delete(string id);
    }
}

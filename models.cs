
using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace muehan.func
{
    public class Todo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class TodoCreateModel
    {
        public string TaskDescription { get; set; }
    }

    public class ToDoUpdateModel
    {
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class TodoTableEntity : TableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
    }

    public static class Mappings
    {
        public static TodoTableEntity ToTableEntity(this Todo todo)
        {
            var entity = new TodoTableEntity{
                PartitionKey = "TODO",
                RowKey = todo.Id,
                CreatedAt = todo.CreatedAt,
                IsCompleted = todo.IsCompleted,
                TaskDescription = todo.TaskDescription
            };

            return entity;
        }

        public static Todo ToTodo(this TodoTableEntity entity)
        {
            var todo = new Todo{
                Id = entity.RowKey,
                CreatedAt = entity.CreatedAt,
                IsCompleted = entity.IsCompleted,
                TaskDescription = entity.TaskDescription
            };

            return todo;
        }
    }
}
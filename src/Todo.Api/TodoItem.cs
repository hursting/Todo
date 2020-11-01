using System;

namespace Todo.Api
{
    public class TodoItem
    {

        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
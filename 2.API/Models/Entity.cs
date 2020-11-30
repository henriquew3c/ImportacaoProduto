using System;

namespace _2.API.Models
{
    public class Entity : IHaveId
    {
        public Guid Id { get; set; }
    }
}
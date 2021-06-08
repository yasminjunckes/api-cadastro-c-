using System;

namespace Domain
{
    public class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Entity(Guid id)
        {
            Id = id;
        }

    }
}
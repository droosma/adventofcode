using System;

namespace Day06
{
    public class Entity
    {
        public string Id { get; }

        private Entity(string id)
        {
            Id = id;
        }

        public static implicit operator string(Entity entity) => entity.Id;
        public static implicit operator Entity(string input) => From(input);
        public static Entity From(string input) => new Entity(input.Trim());
        public override bool Equals(object obj)
        {
            if (obj is string id)
                return Id == id;
            if (obj is Entity entity)
                return Id == entity.Id;

            return false;
        }

        public override string ToString() => Id;

        public override int GetHashCode() => HashCode.Combine(Id);
    }
}

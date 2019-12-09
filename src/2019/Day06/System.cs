using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    public class System
    {
        public Entity Center { get; }
        private readonly IList<System> _satellites;
        public IEnumerable<System> Satellites => _satellites;

        private System(Entity center, IList<System> satellites)
        {
            Center = center;
            _satellites = satellites;
        }

        public System With(System entity)
        {
            _satellites.Add(entity);
            return this;
        }

        public int Trace(Entity entity, int depth = 0)
        {
            if (Center.Equals(entity))
                return depth;

            depth++;

            foreach (var satelliteSystem in _satellites)
            {
                var threadDepth = satelliteSystem.Trace(entity, depth);
                if (threadDepth >= depth)
                    return threadDepth;
            }

            return 0;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var satellite in _satellites)
            {
                builder.AppendLine($"{Center} => {satellite.Center}");
            }

            return builder.ToString();
        }

        public static System Initalize(Entity center)
            => new System(center, new List<System>());

        public static System Construct(Entity center, IEnumerable<Orbit> orbits)
        {
            return Construct(center);

            System Construct(Entity center, System localSystem = null)
            {
                if (localSystem == null)
                    localSystem = Initalize(center);

                Parallel.ForEach(orbits.Where(o => o.Center.Equals(center))
                                       .Distinct(), os =>
                                       {
                                           localSystem.With(Construct(os.Satellite));
                                       });

                return localSystem;
            }
        }
    }
}

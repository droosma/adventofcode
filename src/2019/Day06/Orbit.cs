namespace Day06
{
    public class Orbit
    {
        private const char seperator = ')';

        public Entity Center { get; }
        public Entity Satellite { get; }

        public Orbit(Entity source,
                     Entity satellite)
        {
            Center = source;
            Satellite = satellite;
        }

        public override string ToString()
            => $"{Center} => {Satellite}";

        public static Orbit From(string orbit)
            => From(orbit.Split(seperator));
        public static Orbit From(string[] input)
            => new Orbit(input[0], input[1]);
    }
}

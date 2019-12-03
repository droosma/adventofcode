using System;

namespace Day_01
{
    public static class FuelCalculator
    {
        public static decimal ByMass(decimal mass)
            => Math.Floor(mass / 3) - 2;

        public static decimal ByMassWithFuel(decimal mass)
        {
            var fuelRequirement = ByMass(mass);
            var sum = fuelRequirement;
            do
            {
                fuelRequirement = ByMass(fuelRequirement);
                if (fuelRequirement > 0)
                    sum += fuelRequirement;
            }
            while (fuelRequirement > 0);

            return sum;
        }
    }
}
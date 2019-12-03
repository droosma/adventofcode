using System;

namespace Day_01
{
    public static class FuelCalculator
    {
        public static decimal CalculateFuelRequirement(decimal mass)
            => Math.Floor(mass / 3) - 2;
    }
}
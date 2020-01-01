using System;

using AutoFixture.Kernel;

namespace SimpleCards.Engine
{
    public class PilePositionBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var t = request as Type;
            if (typeof(PilePosition).Equals(t))
            {
                var allValues = new[]
                {
                    PilePosition.Bottom,
                    PilePosition.Middle,
                    PilePosition.Top,
                };
                var rnd = new System.Random();
                return allValues[rnd.Next(0, allValues.Length)];
            }

            return new NoSpecimen();
        }
    }
}
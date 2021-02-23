using System.Collections.Generic;
using System.Linq;

namespace Cogworks.Umbraco.Essentials.Utilities
{
    /// <summary>
    /// Abstract class created to help us compare value objects based on its content.
    ///
    /// Resources used:
    ///     - https://docs.microsoft.com/pl-pl/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects
    ///     - https://enterprisecraftsmanship.com/2017/08/28/value-object-a-better-implementation
    /// </summary>
    public abstract class ValueObject
    {
        private const int HashCodeSeed = 1;
        private const int HashCodeMultiplier = 23;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
            => GetEqualityComponents()
                .Aggregate(HashCodeSeed, (current, obj) =>
                {
                    unchecked
                    {
                        return (current * HashCodeMultiplier) + (obj?.GetHashCode() ?? 0);
                    }
                });

        protected abstract IEnumerable<object> GetEqualityComponents();

        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left is null || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject a, ValueObject b)
            => !(a == b);
    }
}
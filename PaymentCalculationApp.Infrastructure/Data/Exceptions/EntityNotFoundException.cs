using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Infrastructure.Data.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityType { get; }
        public object EntityId { get; }

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public EntityNotFoundException(string entityType, object entityId)
            : base($"Entity of type '{entityType}' with ID '{entityId}' not found.")
        {
            EntityType = entityType;
            EntityId = entityId;
        }
    }
}

using CoreX.Domain.Properties;
using System.Globalization;

namespace CoreX.Domain
{
    public class EntityWasNotFoundException : LogicalException
    {
        public EntityWasNotFoundException()
            : base(string.Format(CultureInfo.CurrentCulture,
                Resource.EntityWasNotFound_ValidationError, Resource.Entiy))
        {
        }

        public EntityWasNotFoundException(string entityName)
            : base(string.Format(CultureInfo.CurrentCulture,
                Resource.EntityWasNotFound_ValidationError, entityName))
        {
        }
    }
}
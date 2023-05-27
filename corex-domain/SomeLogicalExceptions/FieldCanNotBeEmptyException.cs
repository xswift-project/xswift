using CoreX.Domain.Properties;
using System.Globalization;

namespace CoreX.Domain
{
    public class FieldCanNotBeEmptyException : LogicalException
    {
        public FieldCanNotBeEmptyException()
            : base(string.Format(CultureInfo.CurrentCulture,
        Resource.EntityWasNotFound_ValidationError, Resource.Entiy))
        {
        }

        public FieldCanNotBeEmptyException(string entityName)
            : base(string.Format(CultureInfo.CurrentCulture,
                Resource.FieldCanNotBeEmpty_ValidationError, entityName))
        {
        }
    }
}
using System.Globalization;

namespace XSwift.Domain
{
    /// <summary>
    /// Represents an invariant issue that occurs when attempting to create an entity with uniqueness conditions
    /// that already exist.
    /// </summary>
    public class AnEntityWithTheseUniquenessConditionsHasAlreadyBeenExisted : LogicalIssue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnEntityWithTheseUniquenessConditionsHasAlreadyBeenExisted"/> class
        /// with the specified entity name and description.
        /// </summary>
        /// <param name="entityName">The name of the entity with uniqueness conditions that already exist.</param>
        /// <param name="description">A custom description for the issue (optional).</param>
        public AnEntityWithTheseUniquenessConditionsHasAlreadyBeenExisted(
            string entityName = "", string description = "") :
            base (outerDescription: description,
                innerDescription: string.Format(CultureInfo.CurrentCulture,
                "An {0} entity with these uniqueness conditions has already been existed.",
                entityName))
        {
        }
    }
}
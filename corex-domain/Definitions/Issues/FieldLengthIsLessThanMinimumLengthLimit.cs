﻿using CoreX.Domain.Properties;
using System.Globalization;

namespace CoreX.Domain
{
    public class FieldLengthIsLessThanMinimumLengthLimit : ValidationIssue
    {
        public FieldLengthIsLessThanMinimumLengthLimit(int minLength, string fieldName = "", string description = "")
        {
            Provide<FieldLengthIsLessThanMinimumLengthLimit>(
                outerDescription: description,
                innerDescription: string.Format(CultureInfo.CurrentCulture,
                Resource.Validation_Issue_FieldLengthIsLessThanMinimumLengthLimit, fieldName, minLength));
        }
    }
}

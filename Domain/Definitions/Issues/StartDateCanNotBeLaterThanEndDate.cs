﻿using XSwift.Properties;
using System.Globalization;

namespace XSwift.Domain
{
    public class StartDateCanNotBeLaterThanEndDate : ValidationIssue
    {
        public StartDateCanNotBeLaterThanEndDate(
            string entityName = "", string description = "") :
            base (outerDescription: description,
                innerDescription: string.Format(CultureInfo.CurrentCulture,
                Resource.Validation_Issue_StartDateCanNotBeLaterThanEndDate, entityName))
        {
        }
    }
}

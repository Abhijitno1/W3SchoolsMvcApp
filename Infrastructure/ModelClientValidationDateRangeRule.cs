using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace W3SchoolsMvcApp.Infrastructure
{
    public class ModelClientValidationDateRangeRule: ModelClientValidationRule
    {
        public ModelClientValidationDateRangeRule(string errorMessage, DateTime minValue, DateTime maxValue)
        {
            this.ErrorMessage = errorMessage;
            this.ValidationType = "pickerDateRange";
            this.ValidationParameters.Add("min", minValue.ToString("yyyy/mm/dd"));
            this.ValidationParameters.Add("max", maxValue.ToString("yyyy/mm/dd"));
        }
    }
}
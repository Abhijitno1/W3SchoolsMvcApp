using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace W3SchoolsMvcApp.Infrastructure
{
    public class DateRangeAttribute: ValidationAttribute
    {
        const string DATE_FORMAT = "yyyy/MM/dd"; //date format to use when setting attribute on a model definition
        const string ERROR_MESSAGE = "{0} must be between {1:d} and {2:d}";

        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }

        //Ctor
        public DateRangeAttribute(string minDate, string maxDate)
        {
            MinDate = ParseDate(minDate);
            MaxDate = ParseDate(maxDate);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.InvariantCulture, ERROR_MESSAGE, name, this.MinDate, this.MaxDate);
        }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime)) return true;
            var dateValue = (DateTime)value;
            return (dateValue >= MinDate && dateValue <= MaxDate);
        }

        private DateTime ParseDate(string date)
        {
            return DateTime.ParseExact(date, DATE_FORMAT, CultureInfo.InvariantCulture);
        }
    }
}
$(function () {
    $.validator.addMethod('pickerDateRange', function (value, element, param) {
        if (!value) return true; //For empty value we have separate requiredfieldvalidation. this validation does not return false for empty value
        var dateValue = $.datepicker.parseDate('mm/dd/yyyy', value);
        return (param.min <= dateValue && dateValue <= param.max);
    });

    $.validator.unobtrusive.adapters.add('pickerDateRange', function (options) {
        options.rules['pickerDateRange'] = {
            min: $.datepicker.parseDate('mm/dd/yyyy', options.params.min),
            max: $.datepicker.parseDate('mm/dd/yyyy', options.params.max)
        };
        if (options.message)
            options.messages['pickerDateRange'] = options.message;
    });
} (jQuery));
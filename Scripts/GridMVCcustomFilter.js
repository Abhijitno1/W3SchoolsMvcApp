function ReleaseDateGridFilter() {
    this.getAssociatedTypes = function () {
        return ["customReleaseDateRangeFilter"];
    }

    this.onShow = function () {
        //Nothing special
    }

    this.showClearFilterButton = function () {
        return true;
    }

    this.onRender = function (container, lang, typeName, values, cb, data) {
        this.cb = cb;
        this.container = container;
        this.lang = lang;
        this.fromValue = new Date(), this.toValue = new Date();
        if (values.length > 0) {
            this.fromValue = values[0].filterValue;
            this.toValue = values[1].filterValue;
        }
        this.renderWidget();
        this.setDefaultValues();
        this.registerEvents();
    }

    this.renderWidget = function () {
        var html = '<legend>Filter Date Range</legend> \
            <div class="form-group"> \
            <label>From Date</label> \
            <input type="text" id="dtFromDate" name="dtFromDate" class="grid-filter-input-form-control date"></input> '
            html += '</div> \
            <div class="form-group"><label>To Date</label> \
            <input type="text" id="dtToDate" name="dtToDate" class="grid-filter-input-form-control date"></input></div> \
            <div class="grid-filter-buttons"> \
            <button id="btnDateRangeFilter" class="btn btn-primary grid-apply">Apply Filter</button></div>';
        this.container.append(html);
    }

    this.setDefaultValues = function () {
        var dtFromDate = this.container.find("#dtFromDate");
        var dtToDate = this.container.find("#dtToDate");

        //Workaround for datepicker not getting bound via class selector
        dtFromDate.datepicker();
        dtToDate.datepicker();

        dtFromDate.datepicker("setDate", this.fromValue);
        dtToDate.datepicker("setDate", this.toValue);
    }

    this.registerEvents = function () {
        var context = this;
        var filterBtn = this.container.find("#btnDateRangeFilter");
        filterBtn.click(function () {
            this.fromValue = $.datepicker.parseDate('mm/dd/yy', context.container.find("#dtFromDate").val());
            this.toValue = $.datepicker.parseDate('mm/dd/yy', context.container.find("#dtToDate").val());
            var values = [
                { filterType: 7, filterValue: this.fromValue }, //GreaterThanOrEqualTo filter
                {filterType: 8, filterValue: this.toValue}    //LessThanOrEqualTo filter
            ];
            context.cb(values);
        });
    }
}
var createMovieWindow, updateMovieWindow, displayMovieWindow;

$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $("#createMovieWindow, #updateMovieWindow", "#displayMovieWindow").hide();
    createMovieWindow = defineMovieEditorPopup("Create", OnSaveofCreateWindow);
    updateMovieWindow = defineMovieEditorPopup("Update", OnSaveofUpdateWindow);
    displayMovieWindow = defineMovieEditorPopup("Display", function () { });
    pageGrids.moviesGrid.addFilterWidget(new ReleaseDateGridFilter());
});

function defineMovieEditorPopup(windowMode, updateFunction) {
    var domElm = windowMode == "Create" ? "#createMovieWindow" : (windowMode == "Update" ? "#updateMovieWindow" : "#displayMovieWindow");
    return $(domElm).dialog({
        title: windowMode + " Movie",
        autoOpen: false,
        closeOnEscape: false,
        resizable: true,
        width: 400,
        show: "puff",
        hide: "explode",
        buttons: {
            update: updateFunction,
            cancel: function () {
                $(domElm).dialog("close");
            }
        },
        open: function () {
            if (windowMode == "Display") {
                //Hide the update button if the popup is shown in display mode
                $(this).dialog().next().find(".ui-dialog-buttonset").children("button:first-child").hide();
                //$(this).dialog().prev().find(".ui-dialog-titlebar-close").hide();
            }
        }
    });
}

function OnSaveofCreateWindow() {
    var newMovie = {
        Title: $("#Title").val(),
        Director: $("#Director").val(),
        ReleaseDate: $("#ReleaseDate").val()
    };
    if (!newMovie.Title) {
        alert("Title is mandatory");
        return;
    }
    $.ajax({
        url: "/Movie/Create",
        type: "POST",
        data: JSON.stringify(newMovie),
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.Success == true) {
                alert(response.Message);
                createMovieWindow.dialog("close");
                $.get("/Movie/List", function (response) {
                    $("#moviesList").html(response);
                });
            }
            else {
                alert(response.Message);
            }
        },
        error: function (response) {
            alert("Unhandled error occurred at Serverside");
        }
    });
}

function OnSaveofUpdateWindow() {
    var movie = {
        Title: $("#Title").val(),
        Director: $("#Director").val(),
        ReleaseDate: $("#ReleaseDate").val(),
        ID: $("#ID").val()
    };
    if (!movie.Title) {
        alert("Title is mandatory");
        return;
    }
    $.ajax({
        url: "/Movie/Edit",
        type: "POST",
        data: JSON.stringify(movie),
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.success !== undefined) {
                if (response.Success == true) {
                    alert(response.Message);
                    updateMovieWindow.dialog("close");
                    $("#moviesList").load("/Movie/List");
                }
                else {
                    alert(response.Message);
                }
            }
            else {
                $("#updateMovieWindow").html(response);
            }
        },
        error: function (response) {
            alert("Unhandled error occurred at Serverside");
        }
    });
}

function ShowAddMovieWindow() {
    $.ajax({
        url: "/Movie/Create",
        type: "GET",
        success: function (response) {
            $("#createMovieWindow").html(response);
            createMovieWindow.dialog("open");
        },
        error: function (response) {
            $("#createMovieWindow").html(response.responseText);
            createMovieWindow.dialog("open");
        }
    });
}

function ShowEditMovieWindow(movieID) {
    $.ajax({
        url: "/Movie/Edit/" + movieID,
        type: "GET",
        success: function (response) {
            $("#updateMovieWindow").html(response);
            updateMovieWindow.dialog("open");
        },
        error: function (response) {
            $("#updateMovieWindow").html(response.responseText);
            updateMovieWindow.dialog("open");
        }
    });
}

function ShowDisplayMovieWindow(movieID) {
    $.ajax({
        url: "/Movie/Details/" + movieID,
        type: "GET",
        success: function (response) {
            $("#displayMovieWindow").html(response);
            displayMovieWindow.dialog("open");
        },
        error: function (response) {
            $("#displayMovieWindow").html(response.responseText);
            displayMovieWindow.dialog("open");
        }
    });
}

function ConfirmMovieDelete(movieID) {
    if (confirm("Are you sure you want to delete this movie?")) {
        $.ajax({
            url: "/Movie/Delete",
            type: "GET",
            data: { id: movieID },
            success: function (response) {
                if (response.Success == true) {
                    $.get("/Movie/List", function (response) {
                        $("#moviesList").html(response);
                    });
                }
                else {
                    alert(response.Message);
                }
            },
            error: function (response) {
                alert("There was a server error while deleting");
            }
        });
    }
}

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
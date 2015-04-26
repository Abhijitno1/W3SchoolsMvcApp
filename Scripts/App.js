var createMovieWindow, updateMovieWindow;

$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $("#createMovieWindow, #updateMovieWindow").hide();
    createMovieWindow = defineMovieEditorPopup("Create", OnSaveofCreateWindow);
    updateMovieWindow = defineMovieEditorPopup("Update", OnSaveofUpdateWindow);
    //GridMvc.addFilterWidget(new ReleaseDateGridFilter());
});

function defineMovieEditorPopup(windowMode, updateFunction) {
    var domElm = windowMode == "Create" ? "#createMovieWindow" : "#updateMovieWindow";
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
        ID: $("#ID").val(),
        Title: $("#Title").val(),
        Director: $("#Director").val(),
        ReleaseDate: $("#ReleaseDate").val()
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
            if (response.Success == true) {
                alert(response.Message);
                updateMovieWindow.dialog("close");
                $("#moviesList").load("/Movie/List");
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
    this.getAssociatedFilterTypes = function () {
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
        this.fromValue = values[0], this.toValue = value[1];
        this.renderWidget();
        this.registerEvents();
    }

    this.renderWidget = function () {
        var html = '<h3>Filter Date Range<h3> \
            <fieldset> \
            <div class="editor-label">From Date<div> \
            <div class="editor-field"><input type="text" id="dtFromDate" name="dtFromDate"></input></div> \
            <div class="editor-label">To Date<div> \
            <div class="editor-field"><input type="text" id="dtToDate" name="dtToDate"></input></div> \
            </fieldset> \
            <p><button class="btn btnPrimary customFilter">Apply Filter</button></p>';
        this.container.append(html);
    }

    this.registerEvents = function () {
        var filterBtn = this.container.find(".customfilter");
        filterBtn.live('click', function () {
            var values = new Array(2);
            values[0] = this.container.find("#dtFromDate").val();
            values[1] = this.container.find("#dtToDate").val();
            cb(values);
        });
    }
}
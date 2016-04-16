var createMovieWindow, updateMovieWindow, displayMovieWindow;

$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $("#createMovieWindow, #updateMovieWindow", "#displayMovieWindow").hide();
    createMovieWindow = defineMovieEditorPopup("Create", OnSaveofCreateWindow);
    updateMovieWindow = defineMovieEditorPopup("Update", OnSaveofUpdateWindow);
    displayMovieWindow = defineMovieEditorPopup("Display", function () { });
    pageGrids.moviesGrid.addFilterWidget(new ReleaseDateGridFilter());

    $("#frmCreateMovie").validate({
        //showErrors: function (errorMap, errorList) {
        //},
        rules: {
            Title: {
                required: true
                ,maxLength: 5
            }
        },
        messages: {
            Title: {
                required: "Title is required"
                ,range: "Title must be less than or equal to 5 chars"
            }
        }
        ,submitHandler: function (form) {
            $(form).dialog("close");
        }
    });

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

function OnSaveofCreateWindowTest() {
    var formObject = $("#frmCreateMovie");
    alert(formObject.valid());
    formObject[0].action = "/Movie/Create";
    formObject[0].method = "POST";
    formObject.submit();
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
            if (response.Success !== undefined) {
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


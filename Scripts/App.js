﻿var createMovieWindow, updateMovieWindow;

$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $("#createMovieWindow, #updateMovieWindow").hide();
    createMovieWindow = defineMovieEditorPopup("Create", OnSaveofCreateWindow);
    updateMovieWindow = defineMovieEditorPopup("Update", OnSaveofUpdateWindow);
});

function defineMovieEditorPopup(windowMode, updateFunction) {
    var domElm = windowMode == "Create" ? "#createMovieWindow" : "#updateMovieWindow";
    return $(domElm).dialog({
        title: windowMode + " Movie",
        autoOpen: false,
        closeOnEscape: false,
        resizable: true,
        width: 500,
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

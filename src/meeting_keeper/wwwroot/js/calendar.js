function eventHTML() {
    var html = "";

    html += '<div id="eventForm">';
    html += '   <input type="text" class="form-control" id="eID-input" style="display: none;" />';

    html += '<div class="col-md-6"> <div class="form-group">';
    html += '       <label for="eStart-input" class="control-label">Start time</label>';
    html += '   <div class="input-group date" id="eStart">';
    html += '       <span class="input-group-addon"> <span class="glyphicon glyphicon-calendar"></span> </span>';
    html += '       <input type="text" class="form-control" id="eStart-input" />';
    html += '   </div>';
    html += '</div> </div>';

    html += '<div class="col-md-6"> <div class="form-group">';
    html += '       <label for="eEnd-input" class="control-label">End time</label>';
    html += '   <div class="input-group date" id="eEnd">';
    html += '       <span class="input-group-addon"> <span class="glyphicon glyphicon-calendar"></span> </span>';
    html += '       <input type="text" class="form-control" id="eEnd-input" />';
    html += '   </div>';
    html += '</div> </div>';

    html += '<div class="col-md-12">';
    html += '   <div class="form-group">';
    html += '       <div class="input-group" id="eTitle">';
    html += '           <label for="eTitle-input" class="control-label">Title</label>';
    html += '           <input type="text" class="form-control" id="eTitle-input" />';
    html += '       </div>';
    html += '   </div>';
    html += '</div>';

    html += '<div class="col-md-12">';
    html += '   <div class="form-group">';
    html += '       <div class="input-group" id="eDay">';
    html += '           <label for="eDay-input" class="control-label"> All day event <input type="checkbox" class="form-control" id="eDay-input" /> </label>';
    html += '       </div>';
    html += '   </div>';
    html += '</div>';

    var colors = [
        {v: "87", c: "#FF4500", n: "orangered"}, {v: "106", c: "#A0522D", n: "sienna"}, {v: "47", c: "#CD5C5C", n: "indianred"}, {v: "17", c: "#008B8B", n: "darkcyan"},
        {v: "18", c: "#B8860B", n: "darkgoldenrod"}, {v: "68", c: "#32CD32", n: "limegreen"}, {v: "42", c: "#FFD700", n: "gold"}, {v: "77", c: "#48D1CC", n: "mediumturquoise"}, {v: "107", c: "#87CEEB", n: "skyblue"},
        {v: "46", c: "#FF69B4", n: "hotpink"}, {v: "47", c: "#CD5C5C", n: "indianred"}, {v: "64", c: "#87CEFA", n: "lightskyblue"}, {v: "13", c: "#6495ED", n: "cornflowerblue"}, {v: "15", c: "#DC143C", n: "crimson"},
        {v: "24", c: "#FF8C00", n: "darkorange"}, {v: "78", c: "#C71585", n: "mediumvioletred"}, {v: "123", c: "#000000", n: "black"}
    ];

    html += '<div class="col-md-12">';
    html += '   <div class="form-group">';
    html += '       <div class="input-group" id="eColor">';
    html += '           <label for="eColor-input" class="control-label">Color</label>';
    html += '           <select id="eColor-input">';
    for (var i = 0; i < colors.length; i++) {
        html += '<option value="' + colors[i].v + '" data-color="' + colors[i].c + '">' + colors[i].n + '</option>';
    }
    html += '           </select>';
    html += '       </div>';
    html += '   </div>';
    html += '</div>';

    html += '<div class="col-md-12">';
    html += '   <input type="button" id="eDelete-input" />';
    html += '</div>';

    html += '</div>';

    return html;
}

function formatTime(t) {
    function round(n) {
        n = parseInt(n);
        return n > 10 ? n : "0" + n;
    }
    return round(t.getDate()) + "." + round(parseInt(t.getMonth()) + 1) + "." + t.getFullYear() + " " + round(t.getHours()) + ":" + round(t.getMinutes());
}

function reformatTime(t) {
    var d = t.split(" ")[0].split(".");
    return new Date(d[2] + "/" + d[1] + "/" + d[0] + " " + t.split(" ")[1]).toISOString();
}

function addEvent(start, end) {

    swal({
        title: "Add event",
                
        confrmButtonText: "Add",
        cancelButtonText: "Cancel",

        text: eventHTML(),

        showCancelButton: true,
        html: true,
        type: "input"

    }, function (isConfirm) {

        if (isConfirm !== false) {

            var form = $(".sweet-alert #eventForm");

            var eventData = {
                title: form.find("#eTitle-input").val(),
                start: form.find("#eStart-input").val() ? new Date(reformatTime(form.find("#eStart-input").val())).getTime() : 0,
                end: form.find("#eEnd-input").val() ? new Date(reformatTime(form.find("#eEnd-input").val())).getTime() : 0,
                allDay: $("#eDay-input").prop('checked'),
                color: form.find("#eColor-input option[value=" + form.find("#eColor-input").val() + "]").data("color")
            };

            $.ajax({
                url: "Calendar/createEvent",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "GET",
                cache: false,
                data: { eventJson: JSON.stringify(eventData) }
            }).done(function (eventData) {

                eventData.start = new Date(eventData.start).toISOString();
                eventData.end = new Date(eventData.end).toISOString();

                $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true

            });
        }

    });

    $('#calendar').fullCalendar('unselect');

    //initialize datetime picker on swal and colorpicker
    $(".sweet-alert").find("fieldset").hide();
    $(".sweet-alert").find("#eDelete-input").hide();
    $('#eStart').datetimepicker({ format: "DD.MM.YYYY HH:mm", minDate: new Date() });
    $('#eEnd').datetimepicker({ format: "DD.MM.YYYY HH:mm",  useCurrent: false });
    $("#eStart").on("dp.change", function (e) { $('#eEnd').data("DateTimePicker").minDate(e.date); });
    $("#eEnd").on("dp.change", function (e) { $('#eStart').data("DateTimePicker").maxDate(e.date); });
    $('#eColor-input').colorselector();
}

function editEvent(event, element) {

    swal({
        title: "Edit event",

        confrmButtonText: "Edit",
        cancelButtonText: "Cancel",

        text: eventHTML(),

        showCancelButton: true,
        html: true,
        type: "input"

    }, function (isConfirm, e, a) {

        if (isConfirm !== false) {

            var form = $(".sweet-alert #eventForm");

            var eventData = {
                id: parseInt(form.find("#eID-input").val()),
                title: form.find("#eTitle-input").val(),
                start: form.find("#eStart-input").val() ? new Date(reformatTime(form.find("#eStart-input").val())).getTime() : 0,
                end: form.find("#eEnd-input").val() ? new Date(reformatTime(form.find("#eEnd-input").val())).getTime() : 0,
                allDay: $("#eDay-input").prop('checked'),
                color: form.find("#eColor-input option[value=" + form.find("#eColor-input").val() + "]").data("color")
            };

            $.ajax({
                url: "Calendar/editEvent",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "GET",
                cache: false,
                data: { eventJson: JSON.stringify(eventData) }
            });

            event.title = eventData.title;
            event.start = new Date(eventData.start).toISOString();
            event.end = new Date(eventData.end).toISOString();
            event.allDay = eventData.allDay;
            event.color = eventData.color;

            $('#calendar').fullCalendar('updateEvent', event);

        }

    });

    $('#calendar').fullCalendar('unselect');

    //initialize datetime picker on swal and colorpicker
    $(".sweet-alert").find("fieldset").hide();
    $('#eStart').datetimepicker({ format: "DD.MM.YYYY HH:mm" });
    $('#eEnd').datetimepicker({ format: "DD.MM.YYYY HH:mm", useCurrent: false });
    $("#eStart").on("dp.change", function (e) { $('#eEnd').data("DateTimePicker").minDate(e.date); });
    $("#eEnd").on("dp.change", function (e) { $('#eStart').data("DateTimePicker").maxDate(e.date); });
    $('#eColor-input').colorselector();

    //set event values
    $("#eID-input").val(event.id);
    $('#eColor-input').colorselector('setColor', event.color);
    $('#eStart-input').val(formatTime(event.start._d));
    $('#eEnd-input').val( event.end ? formatTime(event.end._d) : "");
    $("#eTitle-input").val(event.title);
    $("#eDay-input").prop('checked', event.allDay);

    $(".sweet-alert").find("#eDelete-input").val("Delete").click(removeEvent);

}

function removeEvent() {

    var form = $(".sweet-alert #eventForm");

    var eventData = {
        id: parseInt(form.find("#eID-input").val()),
        title: form.find("#eTitle-input").val(),
        start: form.find("#eStart-input").val() ? new Date(reformatTime(form.find("#eStart-input").val())).getTime() : 0,
        end: form.find("#eEnd-input").val() ? new Date(reformatTime(form.find("#eEnd-input").val())).getTime() : 0,
        allDay: $("#eDay-input").prop('checked'),
        color: form.find("#eColor-input option[value=" + form.find("#eColor-input").val() + "]").data("color")
    };

    $.ajax({
        url: "Calendar/removeEvent",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "GET",
        cache: false,
        data: { eventJson: JSON.stringify(eventData) }
    }).done( function (eventID) {

        $('#calendar').fullCalendar('removeEvents', [eventID]);
        $('.sweet-alert button.cancel').click();

    });

}

function settings() {
    
    $.ajax({
        url: "Calendar/getSettings",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "GET",
        cache: false,
        data: {}
    }).done(function (settings) {

        var html = '<div id="settings">';

        html += '<input type="text" id="hiddenBug" style="display: none;">';

        html += '<div class="col-md-12">';
        html += '   <div class="form-group">';
        html += '       <div class="input-group" id="sSaturday">';
        html += '           <label for="sSaturday-input" class="control-label"> Show Saturday <input type="checkbox" class="form-control" id="sSaturday-input" /> </label>';
        html += '       </div>';
        html += '   </div>';
        html += '</div>';
        html += '<div class="col-md-12">';
        html += '   <div class="form-group">';
        html += '       <div class="input-group" id="sSunday">';
        html += '           <label for="sSunday-input" class="control-label"> Show Sunday <input type="checkbox" class="form-control" id="sSunday-input" /> </label>';
        html += '       </div>';
        html += '   </div>';
        html += '</div>';

        html += '</div>';

        swal({
            title: "Calendar settings",
            confrmButtonText: "Save",
            cancelButtonText: "Cancel",
            text: html,
            showCancelButton: true,
            html: true,
            type: "input"
        }, function (isConfirm) {

            if (isConfirm !== false) {
                var settings = {
                    showSaturday: $(".sweet-alert").find("#sSaturday-input").prop("checked"),
                    showSunday: $(".sweet-alert").find("#sSunday-input").prop("checked")
                };

                $.ajax({
                    url: "Calendar/saveSettings",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "GET",
                    cache: false,
                    data: { settingsJSON: JSON.stringify(settings) }
                }).done(function (data) {

                    console.log(data);
                    //TODO update calendar, possible?

                });
            }

        });

        $('#calendar').fullCalendar('unselect');

        //initialize js
        $(".sweet-alert").find("fieldset").hide();

        //settings hidden days
        if (settings.hiddenDays.indexOf(0) === -1) { $(".sweet-alert").find("#sSunday-input").prop("checked", true); }
        if (settings.hiddenDays.indexOf(6) === -1) { $(".sweet-alert").find("#sSaturday-input").prop("checked", true); }

    });

}

$(document).ready(function () {

    $.ajax({
        url: "Calendar/initCalendar",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "GET",
        cache: false,
        data: {}
    }).done(function (data) {

        for (var i = 0; i < data.events.length; i++) {
            data.events[i].start = new Date(data.events[i].start).toISOString();
            data.events[i].end = new Date(data.events[i].end).toISOString();
        }

        $('#calendar').fullCalendar({

            timeFormat: "HH:mm",
            firstDay: data.settings.firstDay,
            hiddenDays: data.settings.hiddenDays, //6, 0 are saturday and sunday

            editable: true,
            eventLimit: true,
            selectable: true,
            selectHelper: true,

            select: addEvent,
            eventClick: editEvent,

            customButtons: {
                settings: {
                    text: 'Settings',
                    //icon: ' fa fa-cog',
                    click: settings
                },
            },
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay settings'
            },

            events: data.events

        });


    });


    


    //$.ajax({
    //    url: $("#calendarController").data("request-url"),
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    type: "GET",
    //    cache: false,
    //    data: { data: "123" }
    //}).done(function (data) {
    //    console.log(data);
    //});



});
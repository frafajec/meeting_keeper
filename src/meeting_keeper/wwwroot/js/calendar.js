/*
 * Calendar file
*/

function eventHTML() {
    var html = "";

    html += '<div id="eventForm">';

    html += '<div class="col-md-6"> <div class="form-group">';
    html += '   <div class="input-group date" id="eStart">';
    html += '       <span class="input-group-addon"> <span class="glyphicon glyphicon-calendar"></span> </span>';
    html += '       <label for="eStart-input" class="control-label">Start time</label>';
    html += '       <input type="text" class="form-control" id="eStart-input" />';
    html += '   </div>';
    html += '</div> </div>';

    html += '<div class="col-md-6"> <div class="form-group">';
    html += '   <div class="input-group date" id="eEnd">';
    html += '       <span class="input-group-addon"> <span class="glyphicon glyphicon-calendar"></span> </span>';
    html += '       <label for="eEnd-input" class="control-label">End time</label>';
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

        if (isConfirm) {

            var form = $(".sweet-alert #eventForm");

            var eventData = {
                title: form.find("#eTitle-input").val(),
                start: form.find("#eStart-input").val() ? reformatTime(form.find("#eStart-input").val()) : "",
                end: form.find("#eEnd-input").val() ? reformatTime(form.find("#eEnd-input").val()) : "",
                allDay: $("#eDay-input").prop('checked'),
                color: form.find("#eColor-input option[value=" + form.find("#eColor-input").val() + "]").data("color")
            };

            $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
        }
        else {
            swal("Cancelled", "Your imaginary file is safe :)", "error");
        }

    });

    $('#calendar').fullCalendar('unselect');

    //initialize datetime picker on swal and colorpicker
    $(".sweet-alert").find("fieldset").hide();
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

    }, function (isConfirm) {

        if (isConfirm) {

            var form = $(".sweet-alert #eventForm");

            var eventData = {
                title: form.find("#eTitle-input").val(),
                start: form.find("#eStart-input").val() ? reformatTime(form.find("#eStart-input").val()) : "",
                end: form.find("#eEnd-input").val() ? reformatTime(form.find("#eEnd-input").val()) : "",
                allDay: $("#eDay-input").prop('checked'),
                color: form.find("#eColor-input option[value=" + form.find("#eColor-input").val() + "]").data("color")
            };

            event.title = eventData.title;
            event.start = eventData.start;
            event.end = eventData.end;
            event.allDay = eventData.allDay;
            event.color = eventData.color;

            $('#calendar').fullCalendar('updateEvent', event);
        }
        else {
            swal("Cancelled", "Your imaginary file is safe :)", "error");
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
    $('#eColor-input').colorselector('setColor', event.color);
    $('#eStart-input').val(formatTime(event.start._d));
    $('#eEnd-input').val( event.end ? formatTime(event.end._d) : "");
    $("#eTitle-input").val(event.title);
    $("#eDay-input").prop('checked', event.allDay);

}

$(document).ready(function () {

    $('#calendar').fullCalendar({
        //TODO: get user preferences!

        timeFormat: "HH:mm",
        firstDay: 1,
        hiddenDays: [], //6, 0 are saturday and sunday

        editable: true,
        eventLimit: true,
        selectable: true,
        selectHelper: true,

        select: addEvent,
        eventClick: editEvent,

        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        }
    });

    

});
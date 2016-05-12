/*
 * Calendar file
*/

$(document).ready(function () {

    $('#calendar').fullCalendar({
        // put your options and callbacks here
        dayClick: function () {
            console.log('a day has been clicked!');
        },

        editable: true,
        eventLimit: true,
        selectable: true,
        selectHelper: true,

        select: function (start, end) {
            var title = prompt('Event Title:');
            var eventData;
            if (title) {
                eventData = {
                    title: title,
                    start: start,
                    end: end
                };
                $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
            }
            $('#calendar').fullCalendar('unselect');
        },

        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        }
    })

});
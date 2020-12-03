$(window).on('load', (event) => {
    $("#service-picker").val("");
    $("#date-picker").prop('hidden', true);
    $("#submit-appointment").prop("disabled", true);
})

/*
** available times picker events
*/

$(document).on('change', "#AvailableTimes-picker", (e) => {
    if ($("#AvailableTimes-picker").val() != "") {
        $("#submit-appointment").prop("disabled", false);
        var lst = [];
        lst = $("#AvailableTimes-picker").val().split('-');
        for (let i = 0; i < lst.length; i++) {
            lst[i] = lst[i].trim();
        }
        $("#Appointment_Start").val(lst[0]);
        $("#Appointment_End").val(lst[1]);
    }
    else if ($("#AvailableTimes-picker").val() == "") {
        $("#submit-appointment").prop("disabled", true);
        $("#Appointment_Start").val("");
        $("#Appointment_End").val("");
    }
})

/*
** date picker events 
*/

$(document).on('change', "#date-picker", (e) => {
    /* reset available times */
    $("#possibleDates").val("");
    $("#submit-appointment").prop("disabled", true);
    $("#Appointment_Start").val("");
    $("#Appointment_End").val("");
    /* collect ajax data */
    var srv_id = $("#service-picker").val();
    var agent = $('input[name="agent-picker"]:checked').val();
    var d = new Date($("#date-picker").val());
    var output = get_current_date(d);
    /* bring possible dates */
    $.ajax({
        url: `/AjaxCall/AvailableTimes?day=${output}&srvId=${srv_id}&agent=${agent}`,
        success: (result) => {
            $("#Appointment_Date").val(d.toISOString());
            $("#possibleDates").html(result);
        }
    })
})

/*
** Agent picker events
*/

$(document).on('click', "#agent-picker", (e) => {
    /* reset available times */
    $("#submit-appointment").prop("disabled", true);
    $("#Appointment_Start").val("");
    $("#Appointment_End").val("");
    $("#possibleDates").empty();
    /* reset date picker */
    $("#date-picker").val("");
    $("#date-picker").prop('hidden', false);
    $("#Appointment_Date").val("");
    /* reset the agent */
    $("#Appointment_AgentNumber").val($('input[name="agent-picker"]:checked').val());
})

/*
** on click event of service-picker of appointment
*/

$(document).on('change', "#service-picker", (e) => {
    /* reset available times */
    $("#submit-appointment").prop("disabled", true);
    $("#Appointment_Start").val("");
    $("#Appointment_End").val("");
    $("#possibleDates").empty();
    /* reset date picker */
    $("#date-picker").val("");
    $("#date-picker").prop('hidden', true);
    $("#Appointment_Date").val("");
    /* reset agent picker */
    $("#SelectedAgents").empty("");
    $("#Appointment_AgentNumber").val("");
    /* reset service picker */
    var srv_id = $("#service-picker").val();
    $("#Appointment_ServiceId").val(srv_id);
    /* bring the possible agents */
    $.ajax({
        url: `/AjaxCall/GetAgents?srvId=${srv_id}`,
        success: (result) => {
            $("#SelectedAgents").html(result);
        }
    })
})

function get_current_date(d) {
    var res = d.getFullYear() + '-' +
        ((d.getMonth() + 1) < 10 ? '0' : '') + (d.getMonth() + 1) + '-' +
        (d.getDate() < 10 ? '0' : '') + d.getDate();
    return res;
}
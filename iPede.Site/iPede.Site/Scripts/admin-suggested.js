var admin_suggested_add_action = 'Add';
var last_add_request = null;
$(function () {
    $(".add-item-button").click(function () {
        var btn = $(this);
        //Deletes the row where the button is located, so that the button doesn't get clicked again.
        var row = btn.closest("tr");
        row.remove();

        var table = $("#admin-suggested #list table");
        table.append('<tr><td><img src="../../Content/images/busy.gif"></img></td></tr>');

        //Track this request
        last_add_request = $.post(admin_suggested_add_action, { id: btn.data("item-id") }, function (data, status, jqXHR) {
            //Only replace the table content if it's the last POST callback being called
            if (jqXHR == last_add_request) {
                $("#admin-suggested #list").html(data);
                //Inform to our var that this was the last request
                last_add_request = null;
            }

        });

    });
});
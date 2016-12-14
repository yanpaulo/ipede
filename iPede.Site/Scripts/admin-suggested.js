var admin_suggested =
{
    last_request: null,
    add_data:
        {
            action: 'Add',
            button: '.add-item-button',
            div: '#admin-suggested #list',
            table: '#admin-suggested #list table',
            callback: setupRemoveAction
        },
    remove_data:
       {
           action: 'Remove',
           button: '.remove-item-button',
           div: '#admin-suggested #available',
           table: '#admin-suggested #available table',
           callback: setupAddAction

       }
};

function setupAddAction() {
    setupAction(admin_suggested.add_data);
}

function setupRemoveAction() {
    setupAction(admin_suggested.remove_data);
}

function setupAction(data) {
    $(data.button).click(function () {
        var btn = $(this);
        //Deletes the row where the button is located, so that the button doesn't get clicked again.
        var row = btn.closest("tr");
        row.remove();

        var table = $(data.table);
        table.append('<tr><td><img src="../../Content/images/busy.gif"></img></td></tr>');

        //Track this request
        admin_suggested.last_request = $.post(data.action, { id: btn.data("item-id") }, function (post_data, status, jqXHR) {
            //Only replace the table content if it's the last POST callback being called
            if (jqXHR == admin_suggested.last_request) {
                $(data.div).html(post_data);
                //Inform to our var that this was the last request
                admin_suggested.last_request = null;
                data.callback();
            }

        });

    });

}

$(function () {
    setupAddAction();
    setupRemoveAction();
});
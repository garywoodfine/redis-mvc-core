// Write your Javascript code.

$(document).ready(function (e) {
    $("#vote-group p > button").each(function (index, item) {
        $(item).click(function () {
            var vote = $(item).attr("value");
            $.ajax({
                url: "/Home/Vote",
                datatype: "text",
                data: { value: vote
        },
                type: "POST",
                success: function (data) {
                    $('#testarea').html("All OK");
                },
                error: function () {
                    $("#testarea").html("ERROR");
                }
            });
        });
       
    });
});

$(function () {
    $("#search-friend-btn").click(function () {
        var searchTermA = $("input[name='searchTerm']").val(); // Get search term from textbox
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            type: "POST",
            url: "/Search",
            data: { searchTerm: searchTermA },
            headers: {
                "RequestVerificationToken": token
            },
            dataType: "json",
            success: function (data) {
                if (data) {
                    $('#search-friend-btn').removeClass('btn-warning');
                    $('#search-friend-btn').removeAttr("type").attr("type", "submit");
                    $('#search-friend-btn').val(true);
                    $('#search-friend-btn').addClass('btn-success');
                    $('#search-friend-btn').html('Add Friend ?');
                    $('#warning-message').addClass('visually-hidden');
                } else {
                    $('#search-friend-btn').removeAttr("type").attr("type", "button");
                    $('#search-friend-btn').addClass('btn-warning');
                    $('#search-friend-btn').val(false);
                    $('#search-friend-btn').removeClass('btn-success');
                    $('#search-friend-btn').html('Search For Player');
                    $('#warning-message').removeClass('visually-hidden');
                }
            },
            error: function (xhr, status, error) {
                console.error(status);
                console.error(error);
            }
        });
    });
    $("#friend-search").change(function () {
        $('#search-friend-btn').addClass('btn-warning');
        $('#search-friend-btn').removeAttr("type").attr("type", "button");
        $('#search-friend-btn').val(false);
        $('#search-friend-btn').removeClass('btn-success');
        $('#search-friend-btn').html('Search For Player');
    })
});

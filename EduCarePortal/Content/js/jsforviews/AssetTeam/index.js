$(function () {
    $('.group-header').click(function () {
        $(this).nextUntil('.group-header').toggle();
        $(this).toggleClass("toggle");
    });
    $('#close-partial-modal').on('click', function () {
        $('#myPartialModal').modal('hide');
    });

    $('#addnewmember').on('click', function () {
        var assetID = $(this).attr("data-asset-id");
        var clientID = $(this).attr("data-client-id");
        var entityID = $(this).attr("data-entity-id");
        var containerID = $(this).attr("data-container-id");
        var url_string = "/AssetTeam/AddAssetTeamMember?assetID=" + assetID + "&clientID=" + clientID + "&entityID=" + entityID + "&containerID=" + containerID;
        $("#loaderDivPartial2").show();
        $.ajax({
            url: url_string,
            type: "GET",
            success: function (result) {
                if (result != null) {
                    $("#myPartialModalBodyDiv").html(result);
                    $("#loaderDivPartial2").hide();
                    $("#myPartialModal").modal("show");
                }
            },
            error: function (request, status, error) {
                $("#loaderDivPartial2").hide();
                $('#inlisted-alert').show();
                $('#inlisted-alert').addClass('alert-danger');
                $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not ad new member at this moment!');
                window.setTimeout(function () {
                    $("#inlisted-alert").fadeTo(500, 0).slideUp(500, function () {
                        $(this).removeClass('alert-danger');
                        $(this).css("opacity", "");
                        $(this).hide();
                    });
                }, 2000);
            }
        });
    });
});
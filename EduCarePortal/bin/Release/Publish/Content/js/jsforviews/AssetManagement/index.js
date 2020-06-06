$(function () {
    $('.group-header').click(function () {
        $(this).nextUntil('.group-header').toggle();
        $(this).toggleClass("toggle");
    });
    $('#clientValueFilter').on('change', function () {
        var _client = $(this).val();
        var _entity = $('#entityValueFilter').val();
        $("#loaderDiv").show();
        window.location.href = "/AssetManagement/Index?clientFilter=" + _client + "&entityFilter=" + _entity;
    });
    $('#entityValueFilter').on('change', function () {
        var _client = $('#clientValueFilter').val();
        var _entity = $(this).val();
        $("#loaderDiv").show();
        window.location.href = "/AssetManagement/Index?clientFilter=" + _client + "&entityFilter=" + _entity;
    });

    $('#addnewasset').on('click', function () {
        $("#loaderDiv").show();
        $.ajax({
            async: true,
            url: "/AssetManagement/AddAsset",
            type: "GET",
            success: function (result) {
                if (result != null) {
                    $("#myModalBodyDiv1").html(result);
                    $("#loaderDiv").hide();
                    $("#myModal1").modal("show");
                }
            },
            error: function (request, status, error) {
                $("#loaderDiv").hide();
                $('#inlisted-alert').show();
                $('#inlisted-alert').addClass('alert-danger');
                $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not add new asset at this moment!');
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

    $('.edit-icon').on('click', function () {
        var assetID = $(this).attr("data-asset-id");
        var clientID = $(this).attr("data-client-id");
        $("#loaderDiv").show();
        $.ajax({
            url: "/AssetManagement/EditAsset?assetID=" + assetID + "&clientID=" + clientID,
            type: "GET",
            success: function (result) {
                if (result != null) {
                    $("#myModalBodyDiv2").html(result);
                    $("#loaderDiv").hide();
                    $("#myModal2").modal("show");
                }
            },
            error: function (request, status, error) {
                $("#loaderDiv").hide();
                $('#inlisted-alert').show();
                $('#inlisted-alert').addClass('alert-danger');
                $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not edit the asset at this moment!');
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

    $('.view-team-icon').on('click', function () {
        var assetID = $(this).attr("data-asset-id");
        var clientID = $(this).attr("data-client-id");
        var entityID = $(this).attr("data-entity-id");
        var containerID = $(this).attr("data-container-id");
        $("#loaderDiv").show();
        var url_string = "/AssetTeam/Index?assetID=" + assetID + "&clientID=" + clientID + "&entityID=" + entityID + "&containerID=" + containerID;
        $.ajax({
            url: url_string,
            type: "GET",
            success: function (result) {
                if (result != null) {
                    $("#myModalBodyDiv3").html(result);
                    $("#loaderDiv").hide();
                    $("#myModal3").modal("show");
                }
            },
            error: function (request, status, error) {
                $("#loaderDiv").hide();
                $('#inlisted-alert').show();
                $('#inlisted-alert').addClass('alert-danger');
                $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not view the asset team at this moment!');
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

    $('#close-modal-three').on('click', function () {
        $('#myModal3').modal('hide');
        $("#loaderDiv").show();
        window.location.href = "/AssetManagement/Index?currentClientFilter=" + currentClientFilter + "&currentEntityFilter=" + currentEntityFilter;
    });
});
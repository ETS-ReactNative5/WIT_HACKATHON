$(function () {
    $("#myForm").submit(function (event) {
        event.preventDefault();
        $.validator.unobtrusive.parse($("#myForm"));
        if ($(this).valid()) {
            $("#loaderDivPartial3").show();
            var myformdata = $("#myForm").serialize();
            $.ajax({
                type: "POST",
                url: "/AssetTeam/AddAssetTeamMemberWithModel?assetID=" + _assetID + "&clientID=" + _clientID + "&entityID=" + _entityID + "&containerID=" + _containerID,
                data: myformdata,
                success: function (response) {
                    if (response["Success"]) {
                        $("#loaderDivPartial3").hide();
                        $("#myPartialModal").modal("hide");
                        $('#inlisted-alert').show();
                        $('#inlisted-alert').addClass('alert-success');
                        $('#unlisted-alert-text').html('<strong>Successfully on-boarded team member!</strong>');
                        $("#loaderDivPartial2").show();
                        var url_string = "/AssetTeam/Index?assetID=" + response["AssetID"] + "&clientID=" + response["ClientID"] + "&entityID=" + response["EntityID"] + "&containerID=" + response["ContainerID"];
                        $.ajax({
                            url: url_string,
                            type: "GET",
                            success: function (result) {
                                if (result != null) {
                                    $("#myModalBodyDiv3").html(result);
                                    $("#loaderDivPartial2").hide();
                                    $("#myModal3").modal("show");
                                }
                            },
                            error: function (request, status, error) {
                                $("#loaderDivPartial2").hide();
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
                    }
                    else {
                        $("#loaderDivPartial3").hide();
                        $("#myPartialModal").modal("hide");
                        $('#inlisted-alert').show();
                        $('#inlisted-alert').addClass('alert-danger');
                        $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not on-board new team member at this moment!');
                        window.setTimeout(function () {
                            $("#inlisted-alert").fadeTo(500, 0).slideUp(500, function () {
                                $(this).removeClass('alert-danger');
                                $(this).css("opacity", "");
                                $(this).hide();
                            });
                        }, 2000);
                    }
                },
                error: function (request, status, error) {
                    $("#loaderDivPartial3").hide();
                    $("#myPartialModal").modal("hide");
                    $('#inlisted-alert').show();
                    $('#inlisted-alert').addClass('alert-danger');
                    $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not on-board new team member at this moment!');
                    window.setTimeout(function () {
                        $("#inlisted-alert").fadeTo(500, 0).slideUp(500, function () {
                            $(this).removeClass('alert-danger');
                            $(this).css("opacity", "");
                            $(this).hide();
                        });
                    }, 2000);
                }
            })
        }
        return false;
    });

    $("#ContainerRoleName").on('change', function () {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#Username").html(procemessage).show();
        var _containerRoleID = $('#ContainerRoleName').val();
        $(this).attr("disabled", true);
        $.ajax({
            url: '/AssetTeam/GetUsersByRoles',
            type: "GET",
            dataType: "JSON",
            data: { containerRoleID: _containerRoleID, clientID: _clientID, containerID: _containerID },
            success: function (response) {
                $("#Username").html("");
                $("#Username").append(
                    $('<option></option>').val("").html("-- Select User --"));
                $.each(response, function (index, value) {
                    $("#Username").append(
                        $('<option></option>').val(value.UserID).html(value.Name));
                });
            },
            error: function (request, status, error) {
                $('#ContainerRoleName').attr("disabled", false);
                var procemessage = "<option value='0'> Oops! Not able to get the data.</option>";
                $("#Username").html(procemessage).show();
            }
        });

    });
});
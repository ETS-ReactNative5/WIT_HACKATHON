$(function () {
    $("#myForm").submit(function (event) {
        event.preventDefault();
        $.validator.unobtrusive.parse($("#myForm"));
        if ($(this).valid()) {
            $("#loaderDivPartial").show();
            var myformdata = $("#myForm").serialize();
            $.ajax({
                type: "POST",
                url: "/AssetManagement/UpdateAssetWithModel",
                data: myformdata,
                success: function (response) {
                    if (response["Success"]) {
                        $("#loaderDivPartial").hide();
                        $("#myModal2").modal("hide");
                        $('#inlisted-alert').show();
                        $('#inlisted-alert').addClass('alert-success');
                        $('#unlisted-alert-text').html('<strong>Successfully update asset details!</strong>');
                        $("#loaderDiv").show();
                        window.location.href = "/AssetManagement/Index?clientFilter=" + response["ClientID"];
                    }
                    else {
                        $("#loaderDivPartial").hide();
                        $("#myModal2").modal("hide");
                        $('#inlisted-alert').show();
                        $('#inlisted-alert').addClass('alert-danger');
                        $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not update asset details at this moment!');
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
                    $("#loaderDivPartial").hide();
                    $("#myModal2").modal("hide");
                    $('#inlisted-alert').show();
                    $('#inlisted-alert').addClass('alert-danger');
                    $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not update asset details at this moment!');
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
});
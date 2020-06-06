$(function () {
    $("#myForm").submit(function (event) {
        event.preventDefault();
        $.validator.unobtrusive.parse($("#myForm"));
        if ($(this).valid()) {
            $("#loaderDivPartial").show();
            var myformdata = $("#myForm").serialize();
            $.ajax({
                type: "POST",
                url: "/AssetManagement/AddAssetWithModel",
                data: myformdata,
                success: function (response) {
                    if (response["Success"]) {
                        $("#loaderDivPartial").hide();
                        $("#myModal1").modal("hide");
                        $('#inlisted-alert').show();
                        $('#inlisted-alert').addClass('alert-success');
                        $('#unlisted-alert-text').html('<strong>Successfully on-boarded asset!</strong>');
                        $("#loaderDiv").show();
                        window.location.href = "/AssetManagement/Index?clientFilter=" + response["ClientID"];
                    }
                    else {
                        $("#loaderDivPartial").hide();
                        $("#myModal1").modal("hide");
                        $('#inlisted-alert').show();
                        $('#inlisted-alert').addClass('alert-danger');
                        $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not on-board new asset at this moment!');
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
                    $("#myModal1").modal("hide");
                    $('#inlisted-alert').show();
                    $('#inlisted-alert').addClass('alert-danger');
                    $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not on-board new asset at this moment!');
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

    $("#ClientName").on('change', function () {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#EntityName").html(procemessage).show();
        $("#ContainerName").html(procemessage).show();
        var _clientID = $('#ClientName').val();
        $(this).attr("disabled", true);
        $.ajax({
            url: '/AssetManagement/FillEntity',
            type: "GET",
            dataType: "JSON",
            data: { clientID: _clientID },
            success: function (response) {
                $("#EntityName").html("");
                $("#EntityName").append(
                    $('<option></option>').val("").html("-- Select Entity --"));
                $.each(response, function (index, value) {
                    $("#EntityName").append(
                        $('<option></option>').val(value.ID).html(value.Name));
                });
                $.ajax({
                    url: '/AssetManagement/FillContainer',
                    type: "GET",
                    dataType: "JSON",
                    data: { clientID: _clientID },
                    success: function (response) {
                        console.log(response);
                        $("#ContainerName").html("");
                        $("#ContainerName").append(
                            $('<option></option>').val("").html("-- Select Container --"));
                        $.each(response, function (index, value) {
                            console.log(value);
                            $("#ContainerName").append(
                                $('<option></option>').val(value.ID).html(value.Name));
                        });
                    }
                });
                $('#ClientName').attr("disabled", false);
            },
            error: function (request, status, error) {
                $('#ClientName').attr("disabled", false);
                var procemessage = "<option value='0'> Oops! Not able to get the data.</option>";
                $("#EntityName").html(procemessage).show();
                $("#ContainerName").html(procemessage).show();
            }
        });

    });
});
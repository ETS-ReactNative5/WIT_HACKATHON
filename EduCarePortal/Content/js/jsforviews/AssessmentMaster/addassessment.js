$(function () {
    $("#myForm").submit(function (event) {
        event.preventDefault();
        $.validator.unobtrusive.parse($("#myForm"));
        if ($(this).valid()) {
            $("#loaderDivPartial").show();
            var myformdata = $("#myForm").serialize();
            $.ajax({
                type: "POST",
                url: "/AssessmentMaster/AddAssessmentWithModel",
                data: myformdata,
                success: function (response) {
                    if (response["Success"]) {
                        $("#loaderDivPartial").hide();
                        $("#myModal1").modal("hide");
                        $('#inlisted-alert').show();
                        $('#inlisted-alert').addClass('alert-success');
                        $('#unlisted-alert-text').html('<strong>Successfully created assessment!</strong>');
                        $("#loaderDiv").show();
                        window.location.href = "/AssessmentMaster/Index?clientFilter=" + response["ClientID"];
                    }
                    else {
                        $("#loaderDivPartial").hide();
                        $("#myModal1").modal("hide");
                        $('#inlisted-alert').show();
                        $('#inlisted-alert').addClass('alert-danger');
                        $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not add new assessment at this moment!');
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
                    $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not add new assessment at this moment!');
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
        $("#CustomAssessmentDomainName").html(procemessage).show();
        var _clientID = $('#ClientName').val();
        $(this).attr("disabled", true); 
        $.ajax({
            url: '/AssessmentMaster/FillDomainData',
            type: "GET",
            dataType: "JSON",
            data: { clientID: _clientID },
            success: function (response) {
                $("#CustomAssessmentDomainName").html("");
                $("#CustomAssessmentDomainName").append(
                    $('<option></option>').val("").html("-- Select Assessment Domain --")
                );
                $.each(response, function (index, value) {
                    $("#CustomAssessmentDomainName").append(
                        $('<option></option>').val(value.ID).html(value.Name));
                });
                $('#ClientName').attr("disabled", false); 
            },
            error: function (request, status, error) {
                $('#ClientName').attr("disabled", false); 
                var procemessage = "<option value='0'> Oops! Not able to get the data.</option>";
                $("#CustomAssessmentDomainName").html(procemessage).show();
            }
        });
        
    });
});


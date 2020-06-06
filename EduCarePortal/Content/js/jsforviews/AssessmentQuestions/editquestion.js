$(function () {
    $("#AnswerType").on('change', function () {
        var _answerType = $('#AnswerType').val();
        $('#Options').val('');
        $('#JumpTo').val('');
        if (_answerType != 'SingleOptionBased') {
            $('#Options').val('NA');
            $('#Options').attr("readonly", true);
        }
        else {
            $('#Options').attr("readonly", false);
            $('#Options').attr("placeholder", "Type the options by comma separated withour space.");
            $('#JumpTo').attr("placeholder", "Type the serial number of the question by comma separeted based on option.");
        }
    });


    $("#myForm").submit(function (event) {
        event.preventDefault();
        $.validator.unobtrusive.parse($("#myForm"));
        if ($(this).valid()) {
            $("#loaderDivPartial").show();
            var myformdata = $("#myForm").serialize();
            $.ajax({
                type: "POST",
                url: "/AssessmentQuestions/EditQuestionwithModel",
                data: myformdata,
                success: function (response) {
                    if (response["Success"]) {
                        $("#loaderDivPartial").hide();
                        $("#myModal2").modal("hide");
                        $('#inlisted-alert').show();
                        $('#inlisted-alert').addClass('alert-success');
                        $('#unlisted-alert-text').html('<strong>Successfully updated this assessment question!</strong>');
                        $("#loaderDiv").show();
                        window.location.href = "/AssessmentQuestions/Index?assessmentID=" + assessmentID + "&clientID=" + clientID;
                    }
                    else {
                        $("#loaderDivPartial").hide();
                        $("#myModal2").modal("hide");
                        $('#inlisted-alert').show();
                        $('#inlisted-alert').addClass('alert-danger');
                        $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not updated this assessment question at this moment!');
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
                    $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not updated this assessment question at this moment!');
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
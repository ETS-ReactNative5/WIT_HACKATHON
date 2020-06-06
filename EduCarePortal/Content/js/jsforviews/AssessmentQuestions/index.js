$(function () {
    $('.group-header').click(function () {
        $(this).nextUntil('.group-header').toggle();
        $(this).toggleClass("toggle");
    });

    $('.edit-icon').on('click', function () {
        var assessmentID = $(this).attr("data-assessment-id");
        var clientID = $(this).attr("data-client-id");
        var assessmentQuestionID = $(this).attr("data-assessmentquestion-id");
        $("#loaderDiv").show();
        $.ajax({
            url: "/AssessmentQuestions/EditQuestion?assessmentQuestionID=" + assessmentQuestionID + "&clientID=" + clientID + "&assessmentID=" + assessmentID,
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
                $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not edit the question at this moment!');
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

    $('#backtolist').on('click', function () {
        $("#loaderDiv").show();
        var clientID = $(this).attr("data-client-id");
        var dataurl = "/AssessmentMaster/Index?clientFilter=" + clientID;
        window.location.href = dataurl;
    });
});
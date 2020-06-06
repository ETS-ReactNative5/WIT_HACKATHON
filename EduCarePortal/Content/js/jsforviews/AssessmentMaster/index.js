$(function () {
    $('.group-header').click(function () {
        $(this).nextUntil('.group-header').toggle();
        $(this).toggleClass("toggle");
    });

    
    $('#addnewassessment').on('click', function () {
        $("#loaderDiv").show();
        $.ajax({
            async: true,
            url: "/AssessmentMaster/AddAssessment",
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
                $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not add new assessment at this moment!');
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
        var assessmentID = $(this).attr("data-assessment-id");
        var clientID = $(this).attr("data-client-id");
        $("#loaderDiv").show();
        $.ajax({
            url: "/AssessmentMaster/EditAssessment?assessmentID=" + assessmentID + "&clientID=" + clientID,
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
                $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not edit the assessment at this moment!');
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

    $('.upload-icon').on('click', function () {
        $("#loaderDiv").show();
        var assessmentID = $(this).attr("data-assessment-id");
        var clientID = $(this).attr("data-client-id");
        var dataurl = "/AssessmentMaster/UploadFile?assessmentID=" + assessmentID + "&clientID=" + clientID;
        $.ajax({
            url: dataurl,
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
                $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not upload assessment question at this moment!');
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

    $('.config-icon').on('click', function () {
        $("#loaderDiv").show();
        var assessmentID = $(this).attr("data-assessment-id");
        var clientID = $(this).attr("data-client-id");
        var dataurl = "/AssessmentQuestions/Index?assessmentID=" + assessmentID + "&clientID=" + clientID;
        window.location.href = dataurl;
    });

    $('#clientValueFilter').on('change', function () {
        var _client = $(this).val();
        $("#loaderDiv").show();
        window.location.href = "/AssessmentMaster/Index?clientFilter=" + _client;
    });

});


$(function () {
    $('#txtUploadFile').on('change', function (e) {
        $("#loaderDivPartial").show();
        var files = e.target.files;
        if (files.length > 0) {
            if (files.length < 2) {
                if (window.FormData !== undefined) {
                    var data = new FormData();
                    for (var x = 0; x < files.length; x++) {
                        data.append(files[x].name, files[x]);
                    }
                    $.ajax({
                        type: "POST",
                        url: '/AssessmentMaster/UploadQuestions?clientID=' + _clientID + '&assessmentID=' + _assessmentID,
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (response) {
                            if (response["Success"]) {
                                $("#loaderDivPartial").hide();
                                $("#myModal3").modal("hide");
                                $('#inlisted-alert').show();
                                $('#inlisted-alert').addClass('alert-success');
                                $('#unlisted-alert-text').html('<strong>Successfully uploaded assessment questions!</strong>');
                                window.setTimeout(function () {
                                    $("#inlisted-alert").fadeTo(500, 0).slideUp(500, function () {
                                        $(this).removeClass('alert-success');
                                        $(this).css("opacity", "");
                                        $(this).hide();
                                    });
                                }, 2000);
                            }
                            else {
                                $("#loaderDivPartial").hide();
                                $("#myModal3").modal("hide");
                                $('#inlisted-alert').show();
                                $('#inlisted-alert').addClass('alert-danger');
                                $('#unlisted-alert-text').html(response["Exception"]);
                                window.setTimeout(function () {
                                    $("#inlisted-alert").fadeTo(500, 0).slideUp(500, function () {
                                        $(this).removeClass('alert-danger');
                                        $(this).css("opacity", "");
                                        $(this).hide();
                                    });
                                }, 2000);
                            }
                        },
                        error: function (xhr, status, p3, p4) {
                            var err = "Error " + " " + status + " " + p3 + " " + p4;
                            if (xhr.responseText && xhr.responseText[0] == "{")
                                err = JSON.parse(xhr.responseText).Message;
                            console.log(err);
                            $("#loaderDivPartial").hide();
                            $("#myModal3").modal("hide");
                            $('#inlisted-alert').show();
                            $('#inlisted-alert').addClass('alert-danger');
                            $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not upload assessment questions at this moment!');
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
                    $("#myModal3").modal("hide");
                    $('#inlisted-alert').show();
                    $('#inlisted-alert').addClass('alert-danger');
                    $('#unlisted-alert-text').html('<strong>This browser does not support HTML5 file uploads!</strong>');
                    window.setTimeout(function () {
                        $("#inlisted-alert").fadeTo(500, 0).slideUp(500, function () {
                            $(this).removeClass('alert-danger');
                            $(this).css("opacity", "");
                            $(this).hide();
                        });
                    }, 2000);
                }
            }
            else {
                $("#myModal3").modal("hide");
                $('#inlisted-alert').show();
                $('#inlisted-alert').addClass('alert-danger');
                $('#unlisted-alert-text').html('<strong>Only one file allowed at a time!</strong>');
                window.setTimeout(function () {
                    $("#inlisted-alert").fadeTo(500, 0).slideUp(500, function () {
                        $(this).removeClass('alert-danger');
                        $(this).css("opacity", "");
                        $(this).hide();
                    });
                }, 2000);
            }
        }
        else {
            alert("Not able to read the uploaded file!");
            $("#myModal3").modal("hide");
            $('#inlisted-alert').show();
            $('#inlisted-alert').addClass('alert-danger');
            $('#unlisted-alert-text').html('<strong>Not able to read the uploaded file!</strong>');
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
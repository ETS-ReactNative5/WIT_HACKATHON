

function drawChart() {
    $.ajax({
        type: "GET",
        url: "GetClientAssessmentDomain?clientID=@ViewBag.ClientID",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (dt) {

            var dataArray = [];

            $.each(dt, function (i, item) {
                dataArray.push([item.ID, item.Name, item.Description, item.ParentID, item.ParentName, item.ClientID, item.ClientName, item.ApplicationDomain, item.DomainStandardID, item.DomainStandardName]);
            });

            var data = new google.visualization.DataTable();

            data.addColumn('string', 'Entity');
            data.addColumn('string', 'ReportEntity');
            data.addColumn('string', 'ToolTip');


            for (var i = 0; i < dataArray.length; i++) {

                var Node_ID = dataArray[i][0].toString();
                var Node_Name = dataArray[i][1];
                var Description = dataArray[i][2];
                var ParentID = dataArray[i][3] != null ? dataArray[i][3].toString() : '';

                if (dataArray[i][3] == null) {
                    data.addRows([[{
                        v: Node_ID,
                        f: '<div class="plusIcon-Parent" data-node-id="\'' + Node_ID + '\'" ><i class="fas fa-plus"></i></div>' +
                            '<div class="namepopupLink" data-node-id="\'' + Node_ID + '\'" >' + Node_Name + '</div>' + '<b>' +
                            Description + '</b>' + '<br/><div class="plusIcon-Child" data-node-id="\'' + Node_ID + '\'" ><i class="fas fa-plus"></i></div>'
                        //f: Entitye_Name + '<br/><b>' + Description + '</b>'
                    }, ParentID, Description]]);
                }
                else {
                    data.addRows([[{
                        v: Node_ID,
                        f: '<div class="namepopupLink" data-node-id="\'' + Node_ID + '\'">' + Node_Name + '</div>' + '<b>' +
                            Description + '</b>' + '<br/><div class="plusIcon-Child" onclick="AddChildEntity(\'' + Node_ID + '\')" ><i class="fas fa-plus"></i></div>'
                        //f: Entitye_Name + '<br/><b>' + Description + '</b>'
                    }, ParentID, Description]]);
                }

            }

            $("#loaderDiv").hide();

            var chart = new google.visualization.OrgChart($("#chartOrg")[0]);
            chart.draw(data, { allowHtml: true });

        },
        failure: function (dt) {
            alert(dt);
        },
        error: function (dt) {
            alert(dt);
        }
    });
}

//var AddRootEntity = function (entityID) {
//    $("#loaderDiv").show();
//    var url = "/ClientDomain/AddRootClientAssessmentDomain?elementID=" + entityID + "&clientID=" + _clientID;

//    $("#addRootNodePopUpModelBody").load(url, function () {
//        $("#loaderDiv").hide();
//        $("#addRootNodePopUp").modal("show");

//    })

//}

//var AddChildEntity = function (entityID) {
//    $("#loaderDiv").show();
//    var url = "/ClientDomain/AddChildClientAssessmentDomain?parentElementID=" + entityID + "&clientID=" + _clientID;

//    $("#addNodePopUpModelBody").load(url, function () {
//        $("#loaderDiv").hide();
//        $("#addChildNodePopUp").modal("show");

//    })

//}

//var EditEntity = function (entityID) {
//    $("#loaderDiv").show();
//    var url = "/ClientDomain/EditClientAssessmentDomain?elementID=" + entityID + "&clientID="+ _clientID;

//    $("#editNodePopUpModelBody").load(url, function () {
//        $("#loaderDiv").hide();
//        $("#editNodePopUp").modal("show");

//    })
//}

$(function () {
    $('#clientValueFilter').on('change', function () {
        var _client = $(this).val();
        $("#loaderDiv").show();
        window.location.href = "/AssetManagement/Index?searchString=" + _client;
    });
    $('.plusIcon-Parent').on('click', function () {
        var entityID = $(this).attr("data-node-id");
        $("#loaderDiv").show();
        $.ajax({
            url: "/ClientDomain/AddRootClientAssessmentDomain?elementID=" + entityID + "&clientID=" + _clientID,
            type: "GET",
            success: function (result) {
                if (result != null) {
                    $("#addRootNodePopUpModelBody").html(result);
                    $("#loaderDiv").hide();
                    $("#addRootNodePopUp").modal("show");
                }
            },
            error: function (request, status, error) {
                $("#loaderDiv").hide();
                $('#inlisted-alert').show();
                $('#inlisted-alert').addClass('alert-danger');
                $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not add root node at this moment!');
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
    $('.plusIcon-Child').on('click', function () {
        var entityID = $(this).attr("data-node-id");
        $("#loaderDiv").show();
        $.ajax({
            url: "/ClientDomain/AddChildClientAssessmentDomain?parentElementID=" + entityID + "&clientID=" + _clientID,
            type: "GET",
            success: function (result) {
                if (result != null) {
                    $("#addNodePopUpModelBody").html(result);
                    $("#loaderDiv").hide();
                    $("#addChildNodePopUp").modal("show");
                }
            },
            error: function (request, status, error) {
                $("#loaderDiv").hide();
                $('#inlisted-alert').show();
                $('#inlisted-alert').addClass('alert-danger');
                $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not add child node at this moment!');
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
    $('.namepopupLink').on('click', function () {
        var entityID = $(this).attr("data-node-id");
        $("#loaderDiv").show();
        $.ajax({
            url: "/ClientDomain/EditClientAssessmentDomain?elementID=" + entityID + "&clientID=" + _clientID,
            type: "GET",
            success: function (result) {
                if (result != null) {
                    $("#editNodePopUpModelBody").html(result);
                    $("#loaderDiv").hide();
                    $("#editNodePopUp").modal("show");
                }
            },
            error: function (request, status, error) {
                $("#loaderDiv").hide();
                $('#inlisted-alert').show();
                $('#inlisted-alert').addClass('alert-danger');
                $('#unlisted-alert-text').html('<strong>Something went wrong!</strong> Could not edit the node at this moment!');
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
//globals.workspaceSelect = $("#workspace-select");
//globals.workspaceDefaultOption = $("#workspace-default-option").get(0);
//globals.reportDiv = $("#report-div");
//globals.dashboardDiv = $("#dashboard-div");
//globals.tileDiv = $("#tile-div");
//globals.reportSelect = $("#report-select");
//globals.dashboardSelect = $("#dashboard-select");
//globals.reportWrapper = $(".report-wrapper");
//globals.dashboardWrapper = $(".dashboard-wrapper");
//globals.tileWrapper = $(".tile-wrapper");
//globals.reportDisplayText = $(".report-display-text");
//globals.dashboardDisplayText = $(".dashboard-display-text");
//globals.tileDisplayText = $(".tile-display-text");
//globals.embedButton = $(".embed-button");
//globals.tileSelect = $("#tile-select");
//globals.reportContainer = $("#report-container");
//globals.dashboardContainer = $("#dashboard-container");
//globals.tileContainer = $("#tile-container");
//globals.reportSpinner = $("#report-spinner");
//globals.dashboardSpinner = $("#dashboard-spinner");
//globals.tileSpinner = $("#tile-spinner");

//// Set default state of isPreviousReportRDL flag
//globals.isPreviousReportRDL = false;


window.onload = function () {

    globals.getWorkspaces = function () {
        const componentType = "workspace";
        const componentListEndpoint = `${globals.powerBiApi}/groups`;

        // Populates workspace select list
        populateSelectList(componentType, componentListEndpoint, globals.workspaceSelect);
    }

}


// Populates select list
function populateSelectList(componentType, componentListEndpoint, componentContainer) {

    let componentDisplayName;

    // Set component select list display name depending on embed type
    switch (componentType.toLowerCase()) {
        case "workspace":
        case "report":
            componentDisplayName = "name";
            break;
        case "dashboard":
            componentDisplayName = "displayName";
            break;
        case "tile":
            componentDisplayName = "title";
            break;
        default:
            showError("Invalid Power BI Component");
    }

    // Fetch component list from Power BI
    $.ajax({
        type: "GET",
        url: componentListEndpoint,
        headers: {
            //"Authorization": `Bearer ${loggedInUser.accessToken}`
            "Authorization": `Bearer ${gToken}`
        },
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // Sort dropdown list
            let sortedList = data.value.sort((a, b) => (a[componentDisplayName].toLowerCase() > b[componentDisplayName].toLowerCase()) ? 1 : -1);

            // Populate select list
            for (let i = 0; i < sortedList.length; i++) {
                componentContainer.append(
                    $("<option />")
                        .text(sortedList[i][componentDisplayName])
                        .val(sortedList[i].id)
                );
            }

            if (sortedList.length >= 1) {

                // Enable tile select list
                componentContainer.removeAttr("disabled");
            }
        },
        error: function (err) {
            showError(err);
        }
    });
}

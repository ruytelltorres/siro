// Variables Globales para PBI
const globals = {
    workspaceSelect: null,
    workspaceDefaultOption: null,
    reportDiv: null,
    dashboardDiv: null,
    tileDiv: null,
    reportSelect: null,
    dashboardSelect: null,
    reportWrapper: null,
    dashboardWrapper: null,
    tileWrapper: null,
    reportDisplayText: null,
    dashboardDisplayText: null,
    tileDisplayText: null,
    embedButton: null,
    tileSelect: null,
    reportContainer: null,
    dashboardContainer: null,
    tileContainer: null,
    powerBiHostname: null,
    isPreviousReportRDL: null
}

// Constantes del Sistema
const gConstGeneral = {
    RiogoOperacional: null,
    Evaluacion: null,
    EventoPerdida: null,
    AnalistaRiesgoOperacional: null,
}

// Constantes Tipos de Autoevaluaciones
const gTiposEval = {
    Procesos: null,
    SubContratacionSignificativa: null,
    NuevoProducto: null,
    Areas: null
}

const gRespuestas = {
    exito: undefined,
    advertencia: undefined,
    informacion: undefined,
    error: undefined
}

// Cache logged in user's info
const loggedInUser = {
    accessToken: undefined
};

$(function () {
    gConstGeneral.RiesgoOperacional = 1;
    gConstGeneral.Evaluacion = 2;
    gConstGeneral.EventoPerdida = 3;
    gConstGeneral.AnalistaRiesgoOperacional = "005011";

    gTiposEval.Procesos = 1001;
    gTiposEval.SubContratacionSignificativa = 1002;
    gTiposEval.NuevoProducto = 1003;
    gTiposEval.Areas = 1004;
    
    gRespuestas.informacion = 1;
    gRespuestas.exito = 2;
    gRespuestas.error = 3;
    gRespuestas.advertencia = 4;
    
    globals.workspaceSelect = $("#workspace-select");
    globals.workspaceDefaultOption = $("#workspace-default-option").get(0);
    globals.reportDiv = $("#report-div");
    globals.dashboardDiv = $("#dashboard-div");
    globals.tileDiv = $("#tile-div");
    globals.reportSelect = $("#report-select");
    globals.dashboardSelect = $("#dashboard-select");
    globals.reportWrapper = $(".report-wrapper");
    globals.dashboardWrapper = $(".dashboard-wrapper");
    globals.tileWrapper = $(".tile-wrapper");
    globals.reportDisplayText = $(".report-display-text");
    globals.dashboardDisplayText = $(".dashboard-display-text");
    globals.tileDisplayText = $(".tile-display-text");
    globals.embedButton = $(".embed-button");
    globals.tileSelect = $("#tile-select");
    globals.reportContainer = $("#report-container");
    globals.dashboardContainer = $("#dashboard-container");
    globals.tileContainer = $("#tile-container");
    globals.reportSpinner = $("#report-spinner");
    globals.dashboardSpinner = $("#dashboard-spinner");
    globals.tileSpinner = $("#tile-spinner");
    // Set default state of isPreviousReportRDL flag
    globals.isPreviousReportRDL = false;

});
/* LOADING PAGE REPOSITORY */

import { GlobalDotNetReference } from '../BlazorIntegrationModule.js';

/* GLOBAL DATA STRUCT */

export var wasFirstLaunch;                         // Первая ли это загрузка

/* LOADINGS REPOSITORY */

export function reloadPage() {
    document.location.reload();
}

export function startLoading() {
    $("._cl_page_loader").fadeIn();
}

export function endLoading() {
    if (!wasFirstLaunch)
        setTimeout(function () { $("._cl_application_loader").fadeOut(); }, 100);

    $("._cl_page_loader").fadeOut();

    wasFirstLaunch ||= true;
}

export function endLoadingSpy() {
    $("._cl_page_loader").fadeOut();
}

/* EVENT'S */

window.onpopstate = function (event) {
    endLoadingSpy();
    startLoading();
    var s = location.href;
    console.log(GlobalDotNetReference);
    GlobalDotNetReference.invokeMethodAsync("UpdateRtLayoutBlock", s);
    history.replaceState(null, /* ignored title */ '', s);
};
/* IMPORTS */

import { GlobalDotNetReference } from '../BlazorIntegrationModule.js';

/* NAVIGATION REPOSITORY MODULE */

export function GetCurrentInstanceUrl() { // getUrl before
    return window.location.href;
}

export function SetHash(url, newHash) {
    window.history.pushState(null, null, url + '#' + newHash);
}

/* EVENTS */

$(document).on("click", ".js-dynaimcNavigate", function () {
    var href = $(this).attr("href");
    GlobalDotNetReference.invokeMethodAsync('GoToDynamicHrefUrl', href);
});

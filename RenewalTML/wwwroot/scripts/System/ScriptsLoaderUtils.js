/* VARIABLES */

export var loadedScriptList = [];

/* FUNCTIONS */

export function loadScript(scriptPath) {
    // check list - if already loaded we can ignore
    if (loadedScriptList[scriptPath]) {
        // return 'empty' promise
        return new Promise(function (resolve, reject) {
            resolve();
        });
    }

    return new Promise(function (resolve, reject) {
        // create JS library script element
        var script = document.createElement("script");
        script.src = scriptPath;
        script.type = "text/javascript";
        console.log(scriptPath + " created");

        // flag as loading/loaded
        loadedScriptList[scriptPath] = true;

        // if the script returns okay, return resolve
        script.onload = function () {
            resolve(scriptPath);
        };

        // if it fails, return reject
        script.onerror = function () {
            reject(scriptPath);
        }

        // scripts will load at end of body
        document["body"].appendChild(script);
    });
}
/* ERROR CATCHING */

export function createObjectError(message, type) {
    var errorHandle = new Object();

    errorHandle.message = message;
    errorHandle.type = type;

    return JSON.stringify(errorHandle);
}

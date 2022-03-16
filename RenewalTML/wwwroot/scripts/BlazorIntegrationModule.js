/* BLAZOR INTEGRATION MODULE */

export var GlobalDotNetReference;       // Глобальная ссылка на инстанс blazor
export var isReferenceSet = false;      // Была ли добавлена ссылка на инстанс при запуске

export function SetDotnetReference(pDotNetReference) {
    if (!isReferenceSet) {
        GlobalDotNetReference = pDotNetReference;
        isReferenceSet = true;

        console.info("[Blazor integration module] The instance is sucsesseful set.")
    } else console.warn("[Blazor integration module] The instance alredy set.")
};

/* OTHER FUNCTIONS */

Blazor.defaultReconnectionHandler._reconnectCallback = function (d) {
    document.location.reload();
}
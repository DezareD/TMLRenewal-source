export var autocompleteList = new Array();         // Хранит список autocomplete объектов.

export function GenerateAutoComplete_userlist(id, jsonDataUser, placeHolder, dotNetReference) {
    const autoCompleteJS = new autoComplete({
        selector: id,
        placeHolder: placeHolder,
        data: {
            src: jsonDataUser
        },
        threshold: 3,
        searchEngine: (query, record) => {
            if (record['screenName'].toLowerCase().includes(query.toLowerCase())) {
                return query;
            }
        },
        resultsList: {
            element: (list, data) => {
                const info = document.createElement("p");
                if (data.results.length == 0) {
                    info.innerHTML = `<p style="text-align: center;font-size: 13px;color: #333;padding: 10px;">Не найдено.<p>`;
                }
                list.prepend(info);
            },
            noResults: true,
            maxResults: 5
        },
        resultItem: {
            element: (item, data) => {
                item.style = "display: flex; justify-content:start;gap:10px;";
                item.innerHTML = `<div class="image-autocomplete"><img src="${data.value['imageUrl']}"/></div>
      <div style="display: flex;flex-direction: column;overflow: hidden;"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden;">
        ${data.value['screenName']}
      </span>
<span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden;color: grey;">
        @${data.value['login']}
      </span></div>`;
            },
            highlight: true
        },
        events: {
            input: {
                selection: (event) => {
                    const selection = event.detail.selection.value['login'];
                    autoCompleteJS.input.value = selection;

                    dotNetReference.invokeMethodAsync('UpdateAutoCompleteValue', selection);
                }
            }
        }
    });

    autocompleteList[id] = autoCompleteJS;

    var foo = document.getElementById(id.replace('#', ''));

    var observer = new MutationObserver(function (mutations) {
        var text = $(foo).attr("data-bind-text-value");
        var changer = $(foo).attr("data-bind-text-status-changer");

        //console.log(changer, text);

        if (!(changer.startsWith("userchanged-") && changer == ("userchanged-" + text))) {
            autocompleteList[id].input.value = text;
        }
        //markDownArray[id].value($(foo).attr("data-bind-text-value"));
    });
}

export function GetDataAutocompleteUser(id) {
    return autocompleteList[id].input.value;
}

export function SetDataAutocompleteUser(id, value) {
    return autocompleteList[id].input.value = value;
}
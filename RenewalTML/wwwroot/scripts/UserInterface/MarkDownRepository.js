export var markDownArray = new Array();            // Хранит список всех mardown редакторов.

export function GetDataMarkDownEditor(id, isHtml) {
    if (isHtml) {
        var string = markDownArray[id].markdown(markDownArray[id].value());
        return DOMPurify.sanitize(string, { FORBID_ATTR: ['style', 'class'] })
    }
    else return markDownArray[id].value();
}

export function MarkDownSetData(id, value) {
    markDownArray[id].value(value);
}

export function AddImageToEditor(id, imgUrl) {
    //![](https://)

    var doc = markDownArray[id].codemirror.getDoc();
    var cursor = doc.getCursor();

    var pos = {
        line: cursor.line,
        ch: cursor.ch
    }

    doc.replaceRange("![](" + imgUrl + ")", pos);
}

export function ClearMarkDown(id) {
    markDownArray[id].value('');
}

export function DestroyMarkDown(id) {
    markDownArray[id].toTextArea();
    markDownArray[id] = null;
}

export function MarkDownCreate(id, type, uniqName, readonly, dotNetReference) {


    // BOLD ( **text** ): <strong>
    // HEADER1 ( # TEXT ); <h1>
    // QUOTE ( > TEXT....TEXT ); <blockquote>
    // LIST* ( * text1
    //         * text2 ); <ul> <li> + <ol>
    // URLING ( [text](https://url) ); <a>

    // CODE ( ``` code ``` ); <pre> , <code>

    var toolbar_settings, shortcuts_settings =
    {
        "toggleBlockquote": null,
        "toggleBold": null,
        "cleanBlock": null,
        "toggleHeadingSmaller": null,
        "toggleItalic": null,
        "drawLink": null,
        "toggleUnorderedList": null,
        "togglePreview": "Ctrl-P",
        "toggleCodeBlock": null,
        "drawImage": null,
        "toggleOrderedList": null,
        "toggleHeadingBigger": null,
        "toggleSideBySide": null,
        "toggleFullScreen": null
    },
        allow_tags, autosave_settings;

    /* BUTTTONS */

    var toggleBold = {
        name: "bold",
        action: EasyMDE.toggleBold,
        className: "fa fa-bold",
        title: "Жирный текст",
    };

    var header1 = {
        name: "heading-1",
        className: "fa fa-header fa-heading",
        action: EasyMDE.toggleHeading1,
        title: "Большой заголовок"
    };

    var header2 = {
        name: "heading-2",
        className: "fa fa-header fa-heading",
        action: EasyMDE.toggleHeading2,
        title: "Средний заголовок"
    };

    var header3 = {
        name: "heading-3",
        className: "fa fa-header fa-heading",
        action: EasyMDE.toggleHeading3,
        title: "Маленький заголовок"
    };

    var preview = {
        name: "preview",
        className: "fa fa-eye no-disable preview-text",
        action: EasyMDE.togglePreview,
        title: "Предпросмотр"
    };

    var information = {
        name: "link infoblock",
        action: 'https://github.com/Ionaru/easy-markdown-editor',
        className: "fa fab fa-info-circle",
        title: "Подробнее об этом редакторе",
    };

    var information_left = {
        name: "link",
        action: 'https://github.com/Ionaru/easy-markdown-editor',
        className: "fa fab fa-info-circle",
        title: "Подробнее об этом редакторе",
    };

    var quotes = {
        name: "quote",
        action: EasyMDE.toggleBlockquote,
        className: "fa fa-quote-left",
        title: "Цитата"
    }

    var genericList = {
        name: "unordered-list",
        action: EasyMDE.toggleUnorderedList,
        className: "fa fa-list-ul",
        title: "Список"
    };

    var numericList = {
        name: "ordered-list",
        action: EasyMDE.toggleOrderedList,
        className: "fa fa-list-ol",
        title: "Нумерованный список"
    };

    var link = {
        name: "link",
        action: EasyMDE.drawLink,
        className: "fa fa-link",
        title: "Ссылка",
    }

    var code = {
        name: "code",
        action: EasyMDE.toggleCodeBlock,
        className: "fa fa-code",
        title: "Блок кода",
    }

    var image = {
        name: "image",
        action: (editor) => {
            dotNetReference.invokeMethodAsync('StartUploadImage');
        },
        className: "fas fa-image",
        title: "Загрузить изоображение"
    }

    /* ACTIONS */

    if (readonly == true) {
        toolbar_settings = [information_left];
        allow_tags = [];
    }
    else {

        if (type == "mini") {
            toolbar_settings = [toggleBold, header3, information, preview];
            shortcuts_settings['toggleBold'] = "Ctrl-B";
            allow_tags = ['p', 'strong', 'h3'];

            autosave_settings = {
                enabled: false
            }
        }
        else if (type == "requestMoney") {
            toolbar_settings = [link, information, preview];
            shortcuts_settings['drawLink'] = "Ctrl-I";

            allow_tags = ['p', 'a'];

            autosave_settings = {
                enabled: false
            }

        }
        else if (type == "premium") {
            toolbar_settings = [toggleBold, header1, header2, header3, information, preview, quotes, genericList, numericList, link];

            shortcuts_settings['toggleBold'] = "Ctrl-B";
            shortcuts_settings['toggleBlockquote'] = "Ctrl-'";
            shortcuts_settings['toggleUnorderedList'] = "Ctrl-L";
            shortcuts_settings['drawLink'] = "Ctrl-A";

            allow_tags = ['p', 'strong', 'h3', 'h2', 'h1', 'blockquote', 'li', 'ul', 'ol', 'a'];

            autosave_settings = {
                enabled: true,
                uniqueId: type + '#' + uniqName + '#' + window.location.href, // Сохраняет какого типа данный редактор ( простой, расширенный и т.д), сохраняет уникальное название редактора и на какой странице этот редактор находится
                delay: 1000,
                submit_delay: 5000,
                timeFormat: {
                    locale: 'ru-RU',
                    format: {
                        year: 'numeric',
                        month: 'long',
                        day: '2-digit',
                        hour: '2-digit',
                        minute: '2-digit',
                    },
                }
            }
        }
        else if (type == "ultimate") {
            toolbar_settings = [toggleBold, header1, header2, header3, information, preview, quotes, genericList, numericList, link, code, image];

            shortcuts_settings['toggleBold'] = "Ctrl-B";
            shortcuts_settings['toggleBlockquote'] = "Ctrl-'";
            shortcuts_settings['toggleUnorderedList'] = "Ctrl-L";
            shortcuts_settings['drawLink'] = "Ctrl-I";
            shortcuts_settings['toggleCodeBlock'] = "Ctrl-Q";

            allow_tags = ['p', 'strong', 'h3', 'h2', 'h1', 'blockquote', 'li', 'ul', 'ol', 'a', 'code', 'pre', 'img'];

            autosave_settings = {
                enabled: true,
                uniqueId: type + '#' + uniqName + '#' + window.location.href, // Сохраняет какого типа данный редактор ( простой, расширенный и т.д), сохраняет уникальное название редактора и на какой странице этот редактор находится
                delay: 1000,
                submit_delay: 5000,
                timeFormat: {
                    locale: 'ru-RU',
                    format: {
                        year: 'numeric',
                        month: 'long',
                        day: '2-digit',
                        hour: '2-digit',
                        minute: '2-digit',
                    },
                }
            }
        }
    }

    //['strong', 'h1', 'h2', 'h3', 'blockquote', 'ul', 'li', 'ol', 'a', 'pre', 'code', 'p']

    var settings = {
        element: $(id)[0],
        autoDownloadFontAwesome: false,
        spellChecker: false,
        toolbar: toolbar_settings,
        shortcuts: shortcuts_settings,
        autosave: autosave_settings,
        status: [],
        renderingConfig: {
            singleLineBreaks: true,
            codeSyntaxHighlighting: false,
            sanitizerFunction: (renderedHTML) => {
                return DOMPurify.sanitize(renderedHTML, { ALLOWED_TAGS: allow_tags, FORBID_ATTR: ['style', 'class'] })
            },
        }
    };

    const easyMDE = new EasyMDE(settings);

    markDownArray[id] = easyMDE;

    if (readonly) {
        markDownArray[id].togglePreview();
    }

    var foo = document.getElementById(id.replace('#', ''));

    markDownArray[id].codemirror.on("change", (e, a) => {
        if (a.origin != "setValue") {
            dotNetReference.invokeMethodAsync('UpdateMarkDownValue', markDownArray[id].value());
        }
        else dotNetReference.invokeMethodAsync('UpdateMarkDownStatus', "None");
    });

    var observer = new MutationObserver(function (mutations) {
        var text = $(foo).attr("data-bind-text-value");
        var changer = $(foo).attr("data-bind-text-status-changer");

        //console.log(changer, text);

        if (!(changer.startsWith("userchanged-") && changer == ("userchanged-" + text))) {

            console.log("[INFO] System edit text in state: " + id + ", text: " + text);

            markDownArray[id].value(text);
        }
        //markDownArray[id].value($(foo).attr("data-bind-text-value"));
    });

    observer.observe(foo, {
        attributes: true,
        attributeFilter: ['data-bind-text-value']
    });

    foo.dataset.selectContentVal = 1;
}
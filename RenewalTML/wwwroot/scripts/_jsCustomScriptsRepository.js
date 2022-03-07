/* GLOBAL VARIABLE */

var loaded = [];                            // Хранит все загруженные скрипты.
var GLOBAL = {};                            // Хранит глобальные объекты ( полько только инстансы для blazor ).
var autocompleteList = new Array();         // Хранит список autocomplete объектов.
var graphicsList = new Array();             // Хранит список графиков.
var markDownArray = new Array();            // Хранит список всех mardown редакторов.

/* GLOBAL DATA STRUCT */

var wasFirstLaunch;                         // Первая ли это загрузка

/* BLAZOR.FUNCTIONS */

function CreateTippyElement(element) {
    console.log(element);

    element.forEach((tooltip) => {
        tippy.delegate("body", {
            theme: tooltip.toolTipStyles,
            target: tooltip.elementName,
            content: tooltip.innerHtmlText,
            allowHTML: true,
            interactive: tooltip.isInteractive,
            maxWidth: tooltip.width
        });
    })
}

GLOBAL.DotNetReference = null;
GLOBAL.SetDotnetReference = function (pDotNetReference) {
    GLOBAL.DotNetReference = pDotNetReference;
};


/* DYNAMIC URL */

$(document).on("click", ".js-dynaimcNavigate", function() {
    var href = $(this).attr("href");
    GLOBAL.DotNetReference.invokeMethodAsync('GoToDynamicHrefUrl', href);
});


/* BLAZOR RECONNECTION */

Blazor.defaultReconnectionHandler._reconnectCallback = function(d) {
        document.location.reload(); 
}

/* max-height pagging */

window.MaxHeightLoader = function () {
    setTimeout(function () {
        $(".-js-page-maxheight-relaod").css("max-height", "unset");
    }, 505);
}

/* NOTIFICATION COUNT TITLES */

window.SetNotificationTitleCount = function (count) {
    const pattern = /^\(\d+\)/;

    console.log(document.title, count);

    if (count == 0) {
        document.title = document.title.replace(pattern, "").trimStart();
    }
    else {
        if (pattern.test(document.title)) {
            document.title = document.title.replace(pattern, "(" + count + ")");
        }
        else {
            document.title = "(" + count + ") " + document.title;
        }
    }
}

/* BLAZOR ADVANCE LOADER JS SCRIPTS */

window.loadScript = function (scriptPath) {
    // check list - if already loaded we can ignore
    if (loaded[scriptPath]) {
        console.log(scriptPath + " already loaded");
        // return 'empty' promise
        return new this.Promise(function (resolve, reject) {
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
        loaded[scriptPath] = true;

        // if the script returns okay, return resolve
        script.onload = function () {
            console.log(scriptPath + " loaded ok");
            resolve(scriptPath);
        };

        // if it fails, return reject
        script.onerror = function () {
            console.log(scriptPath + " load failed");
            reject(scriptPath);
        }

        // scripts will load at end of body
        document["body"].appendChild(script);
    });
}

function setTitle(title) {
    document.title = title;
}

/* SYTEMS FADE CHANGER */
window.DesignApplication = {
    blockToggleFade: function (name) {
        $(name).fadeToggle();
    }
}

/* SYSTEM.ERRORCATCHING */

function createObjectError(message, type) {
    var errorHandle = new Object();

    errorHandle.message = message;
    errorHandle.type = type;

    return JSON.stringify(errorHandle);
}

/* CROPPER CONNECTION */

window.CropperConnector = {
    InjectCropperToImage: function (imageId, x, y, width, height) {
        console.log(imageId, x, y, width, height);

        var dataObj;

        if (typeof x != "undefined") { 
            dataObj = {
                width: width,
                height: height,
                x: x,
                y: y
            };
        }

        $(".cropper-container").hide();
        $(imageId).cropper('destroy');


        $(imageId).cropper({
            viewMode: 2,
            data: dataObj,
            aspectRatio: 32 / 35.63, // 1 / 1
            rotatable: false,
            zoomable: false,
            autoCropArea: 1,
            responsive: false,
            restore: false
        });
    },
    GetDataCropper: function (imageId) {
        var cropper = $(imageId).data('cropper');
        return JSON.stringify(cropper.getData());
    }
}

/* LOADINGS REPOSITORY */

window.LoadingRepositry = {
    reloadPage: function () {
        document.location.reload();
    },
    getUrl: function () {
        return window.location.href;
    },
    startLoading: function () {
        $("._cl_page_loader").fadeIn();
    },
    endLoading: function () {
        if (!wasFirstLaunch)
            setTimeout(function () { $("._cl_application_loader").fadeOut(); }, 100);

        $("._cl_page_loader").fadeOut();

        wasFirstLaunch ||= true;
    },
    endLoadingSpy: function () {
        $("._cl_page_loader").fadeOut();
    }
}

window.onpopstate = function (event) {
    window.LoadingRepositry.endLoadingSpy();
    window.LoadingRepositry.startLoading();
    var s = location.href;
    console.log(s);
    GLOBAL.DotNetReference.invokeMethodAsync("UpdateRtLayoutBlock", s);
    history.replaceState(null, /* ignored title */ '', s);
};

/* COKIE */

window.CookiesRepository = {
    addCookie: function (name, value, days) {
        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }

        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    },
    deleteCookie: function (name) {
        document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    },
    getCookie: function (name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0)
                return c.substring(nameEQ.length, c.length);
        }
        return null;
    }
}


/* COLORING BAR */

window.ColoringBar = {
    BlnkText: function (tag, type) {

        var s = $(tag).css("color");

        if (type == "green") {
            s = "green"
        }
        else if (type == "red") {
            s = "red";
        }

        var color = $(tag).css("color");
        $(tag).css({ "transition": ".07s" });
        $(tag).css({ "color": s });

        setTimeout(function () {
            $(tag).css({ "color": color });
            $(tag).css({ "transition": "1s" });
        }, 70)

        setTimeout(function () {
            $(tag).css({ "color": color });
            $(tag).css({ "transition": "1s" });
        }, 1000)
    }
}

/* AUTH APPLICATION */

window.VKAuthorizeFunction = {
    AuthorizeVk: function (instance) {
        try {
            VK.Auth.getLoginStatus(function (response) {
                if (response.session) {
                    // Если пользователь уже был авторизован когда-то, не запускаем popup окно, а сразу авторизируем пользователя
                    // Предположительно пользователь всегда должен быть зарегистрирован уже, но на всякий лучше сделать проверку
                    instance.invokeMethodAsync("VKAuthorizeComplete", JSON.stringify(response));
                } else {
                    VK.Auth.login(function (response) {
                        if (response.session) {
                            // В целом тоже самое, что и просто вывод, но с открытием popup окна.
                            instance.invokeMethodAsync("VKAuthorizeComplete", JSON.stringify(response));

                        } else {

                            instance.invokeMethodAsync("VKAuthorizeComplete", createObjectError('-user-vkAuth-declaim', 'info'));
                            //return JSON.stringify('-declaim-authorize');
                            // Пользователь нажал кнопку Отмена в окне авторизации 
                        }
                    });
                }
            });
        }
        catch (e) {
            instance.invokeMethodAsync("VKAuthorizeComplete", createObjectError('-user-vkAuth-error', 'error'));
        }
    }
}

window.AutoComplete = {
    GenerateAutoComplete_userlist: function (id, jsonDataUser, placeHolder) {
        console.log(jsonDataUser);

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
                    }
                }
            }
        });

        autocompleteList[id] = autoCompleteJS;
    },
    GetDataAutocompleteUser: function (id) {
        return autocompleteList[id].input.value;
    },
    SetDataAutocompleteUser: function (id, value) {
        return autocompleteList[id].input.value = value;
    }
}

/* GRAPHICS AND CHART'S */

window.Graphics = {
    AddSingleData: function (instname, numberofDataSet, obj) {

        var chart = graphicsList[instname];

        if (chart) {

            obj.forEach(model => {
                // вставляем в определенный дата сет информацию
                chart.data.datasets[numberofDataSet].data.push(model.data);

                // создаём новый столбец для нашей вставленный даты в датасеты ранее
                chart.data.labels.push(model.label);
            });

            // обновляем
            chart.update();
        }
    },
    DestroyChart: function (inst) {
        console.log("chart: ", graphicsList[inst]);
        if (graphicsList[inst]) {
            graphicsList[inst].destroy();
            delete graphicsList[inst];
        }
    },
    GenerateChart: function (htmltag, inst, tickratekf) {

        if (tickratekf <= 0)
            tickratekf = 1;

        if (inst == "user-profile-balance") {
            var ctx = document.getElementById(htmltag).getContext('2d');

            var gradient = ctx.createLinearGradient(0, 0, 0, 150);
            gradient.addColorStop(0, 'rgba(240,90,40,.3)');
            gradient.addColorStop(1, 'rgba(240,90,40,0)');

            var myChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: [],
                    datasets: [
                        {
                            data: [],
                            label: '',
                            fill: 'start',
                            backgroundColor: gradient,
                            pointBorderWidth: 0,
                            pointBorderColor: "transparent",
                            pointBackgroundColor: "transparent",
                            pointHoverBackgroundColor: "#e65727",
                            pointHoverBorderColor: "white",
                            pointHoverBorderWidth: 2,
                            pointHoverRadius: 4,
                            borderColor: "#e65727",
                            borderWidth: 5,
                            tension: 0.2
                        }
                    ],

                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        y: {
                            grid: {
                                borderDash: [4, 4],
                                color: "#dde2ec",
                                drawBorder: false
                            },
                            ticks: {
                                stepSize: tickratekf,
                                font: {
                                    family: 'SF UI Display', 
                                    size: 12,
                                    color: "#bdc1c5"
                                },
                                callback: function (value, index) {
                                    return nFormatter(value) + "   ";
                                }
                            }
                        },
                        x: {
                            grid: {
                                display: false
                            },
                            ticks: {
                                callback: function (value, index) {
                                    if (index % 2 == 0) return value
                                    else return ""
                                }
                            }
                        }
                    },
                    plugins: {
                        legend: {
                            display: false
                        },
                        tooltip: {
                            backgroundColor: "white",
                            titleColor: "#333",
                            bodyColor: "#333",
                            displayColors: false,
                            titleFont: 'SF UI Display',
                            titleMarginBottom: 1,
                            titleFont: {
                                weght: "bold"
                            },
                            callbacks: {
                                label: function (context) {
                                    var label = context.parsed.x;
                                    return label;
                                },
                                title: function (context) {
                                    var label = context[0].parsed.y;
                                    return nFormatter(label, 2);
                                }
                            }
                        }
                    }
                }
            });

            graphicsList["user-profile-balance"] =  myChart;
        }
        else {
            console.log("# INFO: График для рендеринга не найден.");
        }
    }
}

window.UrlHelper = {
    SetHash: function (url, newHash) {
        window.history.pushState(null, null, url + '#' + newHash);
    }
}

window.ScrollHelper = {
    ScrollToTop: function () {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }
}


/* Markdowneditor */

window.MarkDown = {
    GetDataMarkDownEditor: function (id, isHtml) {
        if (isHtml) {
            var string = markDownArray[id].markdown(markDownArray[id].value());
            return DOMPurify.sanitize(string, { FORBID_ATTR: ['style', 'class'] })
        }
        else return markDownArray[id].value();
    },
    MarkDownSetData: function (id, value) {
        markDownArray[id].value(value);
    },
    AddImageToEditor: function (id, imgUrl) {
        //![](https://)

        var doc = markDownArray[id].codemirror.getDoc();
        var cursor = doc.getCursor();

        var pos = {
            line: cursor.line,
            ch: cursor.ch
        }

        doc.replaceRange("![](" + imgUrl + ")", pos);
    },
    ClearMarkDown: function (id) {
        markDownArray[id].value('');
    },
    DestroyMarkDown: function (id) {
        markDownArray[id].toTextArea();
        markDownArray[id] = null;
    },
    MarkDownCreate: function (id, type, uniqName, readonly, dotNetReference) {


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
            
            if (a.origin != "setValue")
            {
                console.log("[INFO] User edit state in text: " + id);
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
}

/* OTHER */

$(function () {
    /* INIT APPLICATION */
    VK.init({
        apiId: 7912543
    });
});

function nFormatter(num, tofixed = 0) {
    if (num >= 99999999) {
        return "99M+";
    }

    if (num >= 1000000) {
        return (num / 1000000).toFixed(tofixed).replace(/\.0$/, '') + 'M';
    }
    if (num >= 1000) {
        return (num / 1000).toFixed(tofixed).replace(/\.0$/, '') + 'K';
    }
    return num;
}

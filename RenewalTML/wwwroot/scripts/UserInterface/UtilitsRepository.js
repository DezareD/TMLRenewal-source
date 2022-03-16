/* OTHER UTILS ON UI */

export function MaxHeightLoader() {
    setTimeout(function () {
        $(".-js-page-maxheight-relaod").css("max-height", "unset");
    }, 505);
}

export function BlockToggleFade(name) {
    $(name).fadeToggle();
}

export function ScrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
}

export function BlnkTextOnBar(tag, type) { // BlinkText before
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

export function nFormatter(num, tofixed = 0) {
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

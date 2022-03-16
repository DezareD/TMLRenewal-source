/* NOTIFICATION MODULE */

export function SetNotificationTitleCount(count) {
    const pattern = /^\(\d+\)/;

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
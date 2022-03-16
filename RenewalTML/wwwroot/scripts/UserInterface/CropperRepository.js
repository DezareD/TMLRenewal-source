// TODO: Сделать поддержку для разного ratio в дальнейшем

export function InjectCropperToImage(imageId, x, y, width, height) {
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
}

export function GetDataCropper(imageId) {
    var cropper = $(imageId).data('cropper');
    return JSON.stringify(cropper.getData());
}
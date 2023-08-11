const IMAGE_MIME_REGEX = /^image\/(p?jpeg|gif|png)$/i;

$('div[contenteditable]').on("paste", function (e) {
    var loadImage = function (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = document.createElement('img');
            img.src = e.target.result;

            var range = window.getSelection().getRangeAt(0);
            range.deleteContents();
            range.insertNode(img);
        };
        reader.readAsDataURL(file);

        reader.onloadend = function () {
            var img = new Image;
            img.onload = function () {
                $(elem).append(img);
            };
            img.src = reader.result;

            var range = window.getSelection().getRangeAt(0);
            range.deleteContents();
            range.insertNode(img);
        };
    };
});

document.onpaste = function (e) {
    let IsFirefox = navigator.userAgent.toLowerCase().indexOf('firefox');
    if (IsFirefox > -1) {
        // Normal paste handling on Firefox
    }
    else {
        var items = e.clipboardData.items;

        for (var i = 0; i < items.length; i++) {
            if (IMAGE_MIME_REGEX.test(items[i].type)) {
                loadImage(items[i].getAsFile());
                return;
            }
        }

        // Normal paste handling here on other browsers
    }
}
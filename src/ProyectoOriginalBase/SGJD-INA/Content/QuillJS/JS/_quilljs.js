// Opciones para cada instancia del editor QuillJS
var OpcionesQuillJS = {
    modules: {
        toolbar: [
            //[{ header: [1, 2, false] }],
            ['bold', 'italic', 'underline'],
            [{ 'list': 'ordered' }, { 'list': 'bullet' }],
            [{ 'indent': '-1' }, { 'indent': '+1' }],
            [{ 'align': [] }],
            ['clean'],
            ['image']
        ]
    },
    theme: 'snow'
};

$('div[contenteditable]').on("paste", function (e) {
    e.preventDefault();
    var data = e.originalEvent.clipboardData.items[0].getAsFile();
    var elem = this;
    var fr = new FileReader;

    fr.onloadend = function () {
        var img = new Image;
        img.onload = function () {
            $(elem).append(img);
        };
        img.src = fr.result;
    };

    fr.readAsDataURL(data);
});
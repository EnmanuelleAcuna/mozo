// JQuery Dialog
function CerrarDialog() {
    PopUp.dialog('destroy').remove();
    $('body').css({ overflow: 'auto' });
}

//Cambia el tamaño del dialog abierto, dependiendo del tamaño de la pantalla.
$(window).resize(function () {
    FluidDialog();
});

function FluidDialog() {
    var $visible = $(".ui-dialog:visible");
    // each open dialog
    $visible.each(function () {
        var $this = $(this);
        var dialog = $this.find(".ui-dialog-content").data("ui-dialog");
        // if fluid option == true
        if (dialog.options.fluid) {
            var wWidth = $(window).width();
            // check window width against dialog width
            if (wWidth < (parseInt(dialog.options.maxWidth) + 50)) {
                // keep dialog from filling entire screen
                $this.css("max-width", "90%");
            } else {
                // fix maxWidth bug
                $this.css("max-width", dialog.options.maxWidth + "px");
            }
            //reposition dialog
            dialog.option("position", dialog.options.position);
        }
    });
}

// Parámetros para Dialog/Modal con JQuery.
var ParametrosPopUp = {
    autoOpen: true,
    draggable: false,
    resizable: false,
    fluid: true,
    height: 'auto',
    width: 'auto',
    modal: true,
    closeOnEscape: true,
    close: CerrarDialog
};

function PopUpForm(url) {
    //Mostrar carga de página
    MostrarCargandoPagina();

    var FormDiv = $('<div/>');

    // Cargar la vista especificada
    $.get(url).done(function (response) {
        FormDiv.html(response);
        PopUp = FormDiv.dialog(ParametrosPopUp);
        //$('.ui-dialog').css('z-index', 2000);
        //Ocultar carga de página
        DesaparecerCargandoPagina();
    });
}
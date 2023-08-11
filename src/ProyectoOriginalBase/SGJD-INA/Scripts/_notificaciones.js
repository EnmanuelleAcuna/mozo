// Notificaciones en toast de MaterializeCSS emergentes

// Mensaje con error de la operación o proceso (Rojo)
function MensajeError() {
    M.toast({ html: "Error al procesar su solicitud.", classes: 'toast-error' });
}

function MostrarMensajeError(message) {
    if (message === null || message === "") {
        MensajeError();
    }
    else {
        M.toast({ html: message, classes: 'toast-error' });
        try {
            $("#load").attr('hidden', true);
        }
        catch (error) {
            console.log('Error MensajeError' + error);
        }
    }
}

// Mensaje con éxito de la operación o proceso
function MensajeExito() {
    M.toast({ html: "Se ha procesado su solicitud.", classes: 'toast-exito' });
}

function MostrarMensajeExito(message) {
    if (message === null || message === "") {
        MensajeExito();
    }
    else {
        M.toast({ html: message, classes: 'toast-exito' });
    }
}

function MostrarMensajeExitoConAccion(message) {
    if (message === null) {


        MensajeExito();
    }
    else {
        var toastHTML = '<span>' + message + '</span><button class="toast-action btn btn-small grey lighten-3 black-text " onclick=(location.reload())>Continuar</button>';
        M.toast({ html: toastHTML, classes: 'toast-exito', displayLength: 9999999999 });
    }
}

// Mensaje informativo
function MostrarMensajeInformacion(message) {
    if (message === null || message === "") {
        M.toast({ html: "Se ha procesado la información.", classes: 'toast-informacion' });
    }
    else {
        M.toast({ html: message });
    }
}
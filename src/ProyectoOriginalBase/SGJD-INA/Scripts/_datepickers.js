////////////////////////////////////////////////////////////////////////////////////////////////////
// Funciones relacionadas a las búsquedas por fecha
////////////////////////////////////////////////////////////////////////////////////////////////////

// Parámetros de lenguaje para DatePicker de MaterializeCSS
var parametrosi18n = {
    months: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
    monthsShort: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Set", "Oct", "Nov", "Dic"],
    weekdays: ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"],
    weekdaysShort: ["Dom", "Lun", "Mar", "Mie", "Jue", "Vie", "Sab"],
    weekdaysAbbrev: ["D", "L", "M", "M", "J", "V", "S"],
    cancel: 'Cancelar',
    done: 'Ok'
};

$("#FechaNotificacion").datepicker({
    firstDay: 1,
    format: 'dd-mm-yyyy',
    i18n: parametrosi18n,
    autoClose: true,
    onSelect: function (selected) {
        $("#FechaVencimiento").datepicker({
            minDate: selected,
            firstDay: 1,
            format: 'dd-mm-yyyy',
            i18n: parametrosi18n,
            autoClose: true,
            onClose: function () {
                $("#PlazoDias").val(CalcularPlazoDias($("#FechaNotificacion").val(), $("#FechaVencimiento").val()));
            }
        });
    },
    onClose: function () {
        $("#FechaFin").val("");
    }
});

// para cuando se necesita limitar un dateTimecomo el de sesión
var date = new Date();

$("#fechaSesion").datepicker({
    firstDay: 1,
    format: 'dd-mm-yyyy',
    i18n: parametrosi18n,
    autoClose: true,
    minDate: new Date(date.getFullYear(), date.getMonth(), date.getDate()),
    onSelect: function (selected) {

    },
    onClose: function () {
        $("#fechaFin").val("");
    }
});

$("#btnLimpiar").click(function () {
    $('#btnLimpiar').addClass("display-none");
    $('#btnBuscar').addClass("display-none");
});

// Limpiar búsqueda
function fnLimpiarBusquedaFecha() {
    $('#fechaInicio').val("");
    $('#fechaFin').val("");
    $('#btnLimpiar').addClass("display-none");
    $('#btnBuscar').addClass("display-none");
}

//Valida los campos vacios y habilitar el boton de buscar una vez efectuada una confirmacion al seleccionar el dia habilitar el btnBuscar.
function ControlBtnBuscar() {
    if (!$('#fechaInicio').val() || !$('#fechaFin').val()) {
        return false;
    } else {
        return true;
    }
}

function DesplegarFlechaInicial() {
    $('li.active').find('div.collapsible-header i').removeClass('fa fa-caret-right').addClass('fa fa-caret-down');
}

// TIMEPICKERS
// Parámetros de lenguaje para TimePicker de MaterializeCSS
var parametrosi18nTimePicker = {
    cancel: 'Cancelar',
    done: 'Ok'
};




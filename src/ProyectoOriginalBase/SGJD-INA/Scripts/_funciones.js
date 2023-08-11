////////////////////////////////////////////////////////////////////////////////////////////////////
// jQuery DataTables
////////////////////////////////////////////////////////////////////////////////////////////////////
var parametrosLenguajeDataTable = {
    loadingRecords: "Cargando.",
    search: "Filtrar",
    emptyTable: "No hay datos.",
    errorMessage: "Error"
};

/** Ajustar filtro de DataTables y ocultarlo para que ea mostrado haciendo click en el botón [Filtrar] */
function CambiarPropiedadesFiltro() {
    let DivFiltro = $(".dataTables_filter");
    DivFiltro.removeClass("dataTables_filter");
    DivFiltro.addClass("filtro center");
    DivFiltro.children("label").addClass("display-block m-0-auto max-width-320px fz-1r font-bold blue-text");
    DivFiltro.hide();
}

$("#BtnFiltrar").click(function () {
    if ($(".filtro").is(":visible")) {
        $(".filtro").hide("slideUp");
    } else {
        $(".filtro").show("slideDown");
    }
});

//Function: Enviar información del formulario al servidor utilizando AJAX.
function SubmitForm(form) {
    // Valida que los campos del formulario estén llenos y tengan valores correctos
    $.validator.unobtrusive.parse(form);

    if ($(form).valid()) {
        MostrarMensajeInformacion("Procesando información");

        $.ajax({
            type: "POST",
            url: form.action,
            dataType: 'json',
            data: $(form).serialize(),
            success: function (data) {
                if (data.success) {
                    MostrarMensajeExito(data.message); // Notificar éxito
                    location.reload();
                } else {
                    MostrarMensajeError(data.message); // Notificar error
                }
            },
            error: function () {
                MostrarMensajeError("Error al procesar su solicitud."); // Notificar error
            },
            complete: function () {
            }
        });
    }

    return false;
}

////////////////////////////////////////////////////////////////////////////////////////////////////
// Funciones para parámetros de edición
////////////////////////////////////////////////////////////////////////////////////////////////////
function AgregarEstiloTexto(textoEjemplo, nombreCheckbox) {
    if (nombreCheckbox === "negritaTitulo" || nombreCheckbox === "negritaContenido")
        textoEjemplo.css("font-weight", "bold");
    else if (nombreCheckbox === "cursivaTitulo" || nombreCheckbox === "cursivaContenido")
        textoEjemplo.css("font-style", "italic");
    else
        textoEjemplo.css('text-decoration', 'underline');
}

function EliminarEstiloTexto(textoEjemplo, nombreCheckbox) {
    if (nombreCheckbox === "negritaTitulo" || nombreCheckbox === "negritaContenido")
        textoEjemplo.css("font-weight", "");
    else if (nombreCheckbox === "cursivaTitulo" || nombreCheckbox === "cursivaContenido")
        textoEjemplo.css("font-style", "");
    else
        textoEjemplo.css("text-decoration", "");
}

//Recarga la data en el Datatable con la info nueva del json al buscar por fechas.
function RecargarDataTable(data) {
    $("#bitacoraTable").DataTable().clear();
    $("#bitacoraTable").DataTable().rows.add(data.data);
    $("#bitacoraTable").DataTable().draw();
}

/*Función encargada de realizar el filtro por columna*/
function Filtro() {
    //agregar una entrada de texto a cada celda de pie de página
    $('.table thead tr').clone(true).appendTo('.table thead');

    $('.table thead tr:eq(1) th').each(function (i) {
        //Se crean los input text de filtro
        $(this).html('<input type="text" id="filtro" class="filtro"/>');
        $('input', this).on('keyup change', function () {
            if (table.column(i).search() !== this.value) {
                table
                    .column(i)
                    .search(this.value)
                    .draw()
                    .row(this)
                    .index();
            }
        });
    });
}

//Setea informacion en el input del search oculto del datatable
$('#buscar').keyup(function () {
    dataTable.search($(this).val()).draw();
});

function MostrarCargandoPagina() {
    $(".page-loader-wrapper").fadeIn();
}

function DesaparecerCargandoPagina() {
    $(".page-loader-wrapper").fadeOut();
}

//-----------------------------------------Funciones para generar HTML generico------------------------//
//Funcion genera elementos HTML mediante parametros
function GenerarHtml(etiqueta, id, clase, name, type, title, src, value, text, href, onClick, style, target, alt) {
    if (etiqueta === "div") {
        return "<div id='" + id + "' class='" + clase + "' name='" + name + "' value = '" + value + "' onclick= '" + onClick + "' style='" + style + "'>";
    } else if (etiqueta === "label") {

        return "<label id='" + id + "' class='" + clase + "'>";
    } else if (etiqueta === "button") {

        return "<button type='" + type + "' title='" + title + "' id='" + id + "' class='" + clase + "' onclick= '" + onClick + "'>";
    } else if (etiqueta === "i") {
        if (onClick === "") {
            return "<i id='" + id + "' class='" + clase + "' style='" + style + "'>";
        } else {
            return "<i id='" + id + "' class='" + clase + "' onclick= '" + onClick + "' style='" + style + "'>";
        }
    } else if (etiqueta === "p") {

        return "<p title='" + title + "' id='" + id + "' class='" + clase + "'  name='" + name + "' style='" + style + "'>";
    } else if (etiqueta === "a") {
        if (href !== "") {
            return "<a type='" + type + "' title='" + title + "' id='" + id + "' class='" + clase + "'  name='" + name + "' value='" + value + "' href='" + href + "' target='" + target + "'>";
        } else {
            return "<a type='" + type + "' title='" + title + "' id='" + id + "' class='" + clase + "'  name='" + name + "' value='" + value + "' onclick= '" + onClick + "' target='" + target + "'>";
        }

    } else if (etiqueta === "input") {

        return "<input type='" + type + "' title='" + title + "' id='" + id + "' class='" + clase + "'  name='" + name + "' value='" + value + "'/>";
    } else if (etiqueta === "li") {

        return "<li  id='" + id + "'>";
    } else if (etiqueta === "select") {

        return "<select id='" + id + "' class='" + clase + "'  name='" + name + "'><option value = '" + value + "' >" + text + "</option></select > ";
    } else if (etiqueta === "dtoption") {

        return "<option class='" + clase + "' value = '" + value + "' data-icon = '" + src + "'>" + text + "</option>";
    } else if (etiqueta === "option") {

        return "<option id='" + id + "' value = '" + value + "' >" + text + "</option>";

    } else if (etiqueta === "span") {

        return "<span id='" + id + "' class = '" + clase + "' >";
    } else if (etiqueta === "h1") {

        return "<h1 id='" + id + "' class='" + clase + "'>";
    } else if (etiqueta === "h2") {

        return "<h2 id='" + id + "' class='" + clase + "'>";
    } else if (etiqueta === "h3") {

        return "<h3 id='" + id + "' class='" + clase + "'>";
    } else if (etiqueta === "h4") {

        return "<h4 id='" + id + "' class='" + clase + "'>";
    } else if (etiqueta === "h5") {

        return "<h5 id='" + id + "' class='" + clase + "'>";
    } else if (etiqueta === "h6") {

        return "<h6 id='" + id + "' class='" + clase + "' object-fit: contain'>";
    } else if (etiqueta === "img") {

        return "<img id='" + id + "' class=" + clase + " src='" + src + "' alt=" + alt + " >";
    } else if (etiqueta === "progress") {

        return "<progress id='" + id + "' class='" + clase + "' value = '" + value + "' title='" + title + "'  alt=" + alt + ">";
    } else if (etiqueta === "textarea") {

        return "<textarea id='" + id + "' class='" + clase + "' value = '" + value + "' title='" + title + "'  alt=" + alt + ">";
    }
}

/** Generar etiqueta HTML de cierre, ej: </div> */
function GenerarCerrarHtml(etiqueta) {
    return "</" + etiqueta + ">";
}

// Adjuntar elementos (etiquetas/contenido HTML) en un [div] especifico
function AdjuntarHtml(div, elementos) {
    $(div).append(elementos);
}

// Función para inactivar y activar los botones segun sea el caso
function HabilitarDesabilitarBotones(elemento, estado) {
    // Si el estado es TRUE, habilita el boton
    if (estado === true) {
        $(elemento).prop("disabled", estado);
    }// si no inabilita el boton
    else {
        $(elemento).prop("disabled", estado);
    }
}

/* Calcular los días transcurridos entre dos fechas */
function CalcularDiasHabiles(startDate, endDate) {
    var elapsed, daysBeforeFirstSaturday, daysAfterLastSunday;
    var ifThen = function (a, b, c) {
        return a === b ? c : a;
    };

    elapsed = endDate - startDate;
    elapsed /= 86400000;

    daysBeforeFirstSunday = (7 - startDate.getDay()) % 7;
    daysAfterLastSunday = endDate.getDay();

    elapsed -= (daysBeforeFirstSunday + daysAfterLastSunday);
    elapsed = (elapsed / 7) * 5;
    elapsed += ifThen(daysBeforeFirstSunday - 1, -1, 0) + ifThen(daysAfterLastSunday, 6, 5);

    return Math.ceil(elapsed -1);
}

// Convertir una cadena de texto / string a Date, formatos dd/mm/yyyy, dd mm yyyy, dd-mm-yyyy
function ConvertirFecha(SFecha) {
    if (SFecha !== null) {
        // 24/01/2020 24-01-2020 24 01 2020
        return new Date(SFecha.substring(6, 10), SFecha.substring(3, 5) - 1, SFecha.substring(0, 2))
    }
    else {
        return new Date();
    }
}

//Extraer fecha de hoy dd-mm-yyyy
function FechaActual() {
    var fecha = new Date();
    fecha = fecha.getDate() + "-" + (fecha.getMonth() + 1) + "-" + fecha.getFullYear();
    return fecha;
}
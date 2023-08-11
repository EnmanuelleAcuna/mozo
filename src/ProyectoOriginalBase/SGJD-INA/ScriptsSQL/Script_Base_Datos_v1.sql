--Script_Base_Datos_v1.0
USE SGJD
create table Sesion (
Id int not null identity(1,1) primary key,
Numero int not null,
FechaHora DateTime not null,
Tipo nvarchar(20) not null
);

create table OrdenDia(
Id int not null identity(1,1) primary key,
IdSesion int not null foreign key references dbo.Sesion(Id),
Estado nvarchar(50) not null
);

create table Seccion(
Id int not null identity(1,1) primary key,
IdOrdenDia int not null foreign key references dbo.OrdenDia(Id),
Titulo nvarchar(100) not null
);

create table Tema(
Id int not null identity(1,1) primary key,
IdSeccion int not null foreign key references dbo.Seccion(Id),
IdSesion int not null foreign key references dbo.Sesion(Id),
Titulo nvarchar (50) not null,
Estado nvarchar (20) not null,
Resumen nvarchar (max) not null
);

create table Tomo(
Id int not null identity(1,1) primary key,
FechaApertura DateTime not null,
FechaClausura DateTime null,
ActaInicio nvarchar(50) null,
ActaFinal nvarchar(50) null
);

create table Acta(
Id int not null identity(1,1) primary key,
IdTomo int not null foreign key references dbo.Tomo(Id),
IdSesion int not null foreign key references dbo.Sesion(Id),
Estado nvarchar(20) not null,
Numero int not null 
);

create table Capitulo(
Id int not null identity(1,1) primary key,
IdActa int not null foreign key references dbo.Acta(Id),
Numero int not null,
Titulo nvarchar(200) not null,
Contenido nvarchar(max) not null 
);

create table Articulo(
Id int not null identity(1,1) primary key,
IdCapitulo int not null foreign key references dbo.Capitulo(Id),
Numero int not null,
Titulo nvarchar(100) not null,
Contenido nvarchar(max) not null,
Confidencial bit not null
);

create table Unidad(
Id int not null identity(1,1) primary key,
NombreUnidad nvarchar(50) not null
);

create table Acuerdo(
Id int not null identity(1,1) primary key,
IdArticulo int not null foreign key references dbo.Articulo(Id),
IdUnidadEjecutora int not null foreign key references dbo.UnidadEjecutora(Id),
NumeroAcuerdo int not null,
Titulo nvarchar(200) not null,
TipoFirmeza nvarchar(20) not null,
FechaFirmeza DateTime null,
Asunto nvarchar(150) not null,
NumVersion nvarchar(5) not null,
EstadoEdicion nvarchar(20) not null,
EstadoRevision nvarchar(20) not null,
EstadoEjecucion nvarchar(20) not null,
Firma nvarchar(50) not null default 'Sin firma',
FechaNotificacion DateTime null,
FechaVencimiento DateTime null,
PlazoDias int null,
PorcentajeAvance decimal null
);

create table PorTanto(
Id int not null identity(1,1) primary key,
IdAcuerdo int not null foreign key references dbo.Acuerdo(Id),
Descripcion nvarchar(max) not null
);

create table UnidadAcuerdo(
Id int not null identity(1,1) primary key,
IdAcuerdo int not null foreign key references dbo.Acuerdo(Id),
IdUnidadEjecutora int not null foreign key references dbo.UnidadEjecutora(Id)
);

create table Considerando(
Id int not null identity(1,1) primary key,
IdAcuerdo int not null foreign key references dbo.Acuerdo(Id),
Descripcion nvarchar(max) not null
);

create table Rol(
Id int not null identity(1,1) primary key,
Nombre nvarchar(50) not null
);

create table Usuario(
Id int not null identity(1,1) primary key,
IdRol int not null foreign key references dbo.Rol(Id),
IdUnidadEjecutora int not null foreign key references dbo.UnidadEjecutora(Id),
Nombre nvarchar(50) not null,
Apellidos nvarchar(50) not null,
Contraseña nvarchar(60) not null,
UltimaSesion DateTime not null
);

create table TelefonoUsuario(
Id int not null identity(1,1) primary key,
IdUsuario int not null foreign key references dbo.Usuario(Id),
Numero nvarchar(20)
);

create table CorreoUsuario(
Id int not null identity(1,1) primary key,
IdUsuario int not null foreign key references dbo.Usuario(Id),
Correo nvarchar(50)
);

create table Avance(
Id int not null identity(1,1) primary key,
IdUsuario int not null foreign key references dbo.Usuario(Id),
IdAcuerdo int not null foreign key references dbo.Acuerdo(Id),
Descripcion nvarchar(max) null
);

create table UsuarioArticulo(
Id int not null identity(1,1) primary key,
IdArticulo int not null foreign key references dbo.Articulo(Id),
IdUsuario int not null foreign key references dbo.Usuario(Id)
);

create table PermisoRol(
Id int not null identity(1,1) primary key,
IdRol int not null foreign key references dbo.Rol(Id),
Nombre nvarchar(50) not null,
NombreVista nvarchar(100) not null,
PuedeCrear bit not null,
PuedeEditar bit not null,
PuedeEliminar bit not null,
PuedeVer bit not null
);

create table Bitacora(
Id int not null identity(1,1) primary key,
IdUsuario int not null foreign key references dbo.Usuario(Id),
FechaHora DateTime not null,
Accion nvarchar(50) not null,
ValorAntiguo nvarchar(max) not null,
ValorNuevo nvarchar(max) not null,
DireccionIP nvarchar(50) not null,
DescripcionDispositivo nvarchar(50) not null,
SeccionSistema nvarchar(20) not null,
);

create table Anotacion(
Id int not null identity(1,1) primary key,
IdSesion int not null foreign key references dbo.Sesion(Id),
IdOrdenDia int not null foreign key references dbo.OrdenDia(Id),
IdTema int not null foreign key references dbo.Temas(Id),
IdActa int not null foreign key references dbo.Acta(Id),
IdArticulo int not null foreign key references dbo.Articulo(Id),
IdAcuerdo int not null foreign key references dbo.Acuerdo(Id),
Descripcion nvarchar(max) not null,
Estado nvarchar(50) not null
);

create table ArchivoAdjunto(
Id int not null identity(1,1) primary key,
IdTema int not null foreign key references dbo.Temas(Id),
IdArticulo int not null foreign key references dbo.Articulo(Id),
IdAcuerdo int not null foreign key references dbo.Acuerdo(Id),
IdAvance int not null foreign key references dbo.Avance(Id),
Observacion nvarchar(max) null,
UrlArchivo nvarchar(100) not null
);
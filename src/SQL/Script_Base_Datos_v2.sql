--Script_Base_Datos_v2.0
USE SGJD

CREATE TABLE Rol (
	Id INT NOT NULL IDENTITY (1, 1),
	Nombre NVARCHAR (50) NOT NULL UNIQUE,
	CONSTRAINT PK_Rol PRIMARY KEY CLUSTERED (Id ASC)
)

CREATE TABLE Unidad(
	Id INT NOT NULL IDENTITY (1, 1),
	Nombre NVARCHAR (50) NOT NULL,
	CONSTRAINT PK_Unidad PRIMARY KEY CLUSTERED (Id ASC)
)

CREATE TABLE Usuario (
	Id INT NOT NULL IDENTITY (1, 1),
	IdRol INT NOT NULL,
	IdUnidad INT NOT NULL,
	Nombre NVARCHAR (50) NOT NULL,
	Apellidos NVARCHAR (50) NOT NULL,
	Correo NVARCHAR (50) NOT NULL,
	Contraseña NVARCHAR( 60) NOT NULL,
	UltimaSesion DATETIME NOT NULL,
	CONSTRAINT PK_Usuario PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Usuario_Rol_IdRol FOREIGN KEY (IdRol) REFERENCES Rol (Id),
	CONSTRAINT FK_Usuario_Unidad_IdUnidad FOREIGN KEY (IdUnidad) REFERENCES Unidad (Id),
)

CREATE TABLE TipoObjeto (
	Id INT NOT NULL IDENTITY (1, 1),	
	Descripcion NVARCHAR (50) NOT NULL,
	CONSTRAINT PK_TipoObjeto PRIMARY KEY CLUSTERED (Id ASC)
)

INSERT INTO TipoObjeto VALUES ('Sesión')
INSERT INTO TipoObjeto VALUES ('Orden del Día')

SELECT * FROM TipoObjeto

CREATE TABLE Estado (
	Id INT NOT NULL IDENTITY (1, 1),
	IdTipoObjeto INT NOT NULL,
	Descripcion NVARCHAR (50) NOT NULL,
	CONSTRAINT PK_Estado PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_TipoObjeto_Estado_IdTipoObjeto FOREIGN KEY (IdTipoObjeto) REFERENCES TipoObjeto (Id)
)

CREATE TABLE TipoSesion (
	Id INT NOT NULL IDENTITY (1, 1),
	Descripcion NVARCHAR (50) NOT NULL,
	CONSTRAINT PK_TipoSesion PRIMARY KEY CLUSTERED (Id ASC)
)

INSERT INTO TipoSesion VALUES ('Ordinaria')
INSERT INTO TipoSesion VALUES ('Extraordinaria')

CREATE TABLE Sesion (
	Id INT NOT NULL IDENTITY (1,1),
	Numero INT NOT NULL,
	FechaHora DATETIME NOT NULL,
	IdTipoSesion INT NOT NULL,
	IdUsuario INT NOT NULL,
	CONSTRAINT PK_Sesion PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Sesion_TipoSesion_IdTipoSesion FOREIGN KEY (IdTipoSesion) REFERENCES TipoSesion (Id),
	CONSTRAINT FK_Sesion_Usuario_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES Usuario (Id)
)

CREATE TABLE OrdenDia (
	Id INT NOT NULL IDENTITY (1, 1),
	IdSesion INT NOT NULL,
	IdEstado INT NOT NULL,
	CONSTRAINT PK_OrdenDia PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_OrdenDia_Sesion_IdSesion FOREIGN KEY (IdSesion) REFERENCES Sesion (Id),
	CONSTRAINT FK_OrdenDia_Estado_IdEstado FOREIGN KEY (IdEstado) REFERENCES Estado (Id)
)

CREATE TABLE Seccion (
	Id INT NOT NULL IDENTITY (1, 1),
	Descripcion NVARCHAR (100) NOT NULL,
	CONSTRAINT PK_Seccion PRIMARY KEY CLUSTERED (Id ASC)
)

CREATE TABLE SeccionOrdenDia (
	IdOrdenDia INT NOT NULL,
	IdSeccion INT NOT NULL,
	CONSTRAINT PK_SeccionOrdenDia PRIMARY KEY CLUSTERED (IdOrdenDia ASC, IdSeccion ASC),
	CONSTRAINT FK_SeccionOrdenDia_OrdenDia_IdOrdenDia FOREIGN KEY (IdOrdenDia) REFERENCES OrdenDia (Id),
	CONSTRAINT FK_SeccionOrdenDia_Seccion_IdSeccion FOREIGN KEY (IdSeccion) REFERENCES Seccion (Id)
)

CREATE TABLE Tema (
	Id INT NOT NULL IDENTITY (1, 1),
	IdSeccion INT NOT NULL,
	IdSesionEjecutada INT NULL,
	IdEstado INT NOT NULL,
	Titulo NVARCHAR (50) NOT NULL,	
	Resumen NVARCHAR (MAX) NOT NULL,
	CONSTRAINT PK_Tema PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Tema_Seccion_IdSeccion FOREIGN KEY (IdSeccion) REFERENCES Seccion (Id),
	CONSTRAINT FK_Tema_Sesion_IdSesionEjecutadan FOREIGN KEY (IdSesionEjecutada) REFERENCES Sesion (Id),
	CONSTRAINT FK_Tema_Estado_IdEstado FOREIGN KEY (IdEstado) REFERENCES Estado (Id)
)

CREATE TABLE Tomo (
	Id INT NOT NULL IDENTITY (1, 1),
	FechaApertura DATETIME NOT NULL,
	FechaClausura DATETIME NULL,
	ActaInicio NVARCHAR (50) NULL,
	ActaFinal NVARCHAR (50) NULL,
	UsuarioApertura INT NOT NULL,
	UsuarioCierre INT NULL,
	CONSTRAINT PK_Tomo PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Tomo_Usuario_UsuarioApertura FOREIGN KEY (UsuarioApertura) REFERENCES Usuario (Id),
	CONSTRAINT FK_Tomo_Usuario_UsuarioCierre FOREIGN KEY (UsuarioCierre) REFERENCES Usuario (Id)
)

--Comentar sobre lapertenencia de un Orden del Día a un Acta
CREATE TABLE Acta (
	Id INT NOT NULL IDENTITY (1, 1),
	IdTomo INT NOT NULL,
	IdSesion INT NOT NULL,
	IdEstado INT NOT NULL,
	Numero INT NOT NULL,
	CONSTRAINT PK_Acta PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Acta_Tomo_IdTomo FOREIGN KEY (IdTomo) REFERENCES Tomo (Id),
	CONSTRAINT FK_Acta_Sesion_IdSesion FOREIGN KEY (IdSesion) REFERENCES Sesion (Id),
	CONSTRAINT FK_Acta_Estado_IdEstado FOREIGN KEY (IdEstado) REFERENCES Estado (Id)
)

CREATE TABLE Capitulo (
	Id INT NOT NULL,
	IdActa INT NOT NULL,
	Numero INT NOT NULL,
	Titulo NVARCHAR (200) NOT NULL,
	Contenido NVARCHAR (MAX) NOT NULL,
	CONSTRAINT PK_Capitulo PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Capitulo_Acta_IdActa FOREIGN KEY (IdActa) REFERENCES Acta (Id)
)

CREATE TABLE Articulo(
	Id INT NOT NULL IDENTITY (1, 1),
	IdCapitulo INT NOT NULL,
	Numero INT NOT NULL,
	Titulo NVARCHAR (100) NOT NULL,
	Contenido NVARCHAR (MAX) NOT NULL,
	Confidencial BIT NOT NULL,
	CONSTRAINT PK_Articulo PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Articulo_Capitulo_IdCapitulo FOREIGN KEY (IdCapitulo) REFERENCES Capitulo (Id)
)

CREATE TABLE Acuerdo (
	Id INT NOT NULL IDENTITY (1, 1),
	IdArticulo INT NOT NULL,
	IdEstado INT NOT NULL,
	NumeroAcuerdo INT NOT NULL,
	Titulo NVARCHAR (1000) NOT NULL,
	Firme BIT NOT NULL,
	FechaFirmeza DATETIME NULL,
	Asunto NVARCHAR (1000) NOT NULL,
	NumeroVersion INT NOT NULL,
	Firma NVARCHAR (512) NULL,
	FechaNotificacion DATETIME NULL,
	FechaVencimiento DATETIME NULL,
	PlazoDias INT NULL,
	PorcentajeAvance DECIMAL NULL,
	CONSTRAINT PK_Acuerdo PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Acuerdo_Articulo_IdArticulo FOREIGN KEY (IdArticulo) REFERENCES Articulo (Id),
	CONSTRAINT FK_Acuerdo_Estado_IdEstado FOREIGN KEY (IdEstado) REFERENCES Estado (Id)
)

CREATE TABLE UnidadAcuerdo (
	IdAcuerdo INT NOT NULL,
	IdUnidad INT NOT NULL,
	CONSTRAINT PK_UnidadAcuerdo PRIMARY KEY CLUSTERED (IdAcuerdo ASC, IdUnidad ASC),
	CONSTRAINT FK_UnidadAcuerdo_Acuerdo_IdAcuerdo FOREIGN KEY (IdAcuerdo) REFERENCES Acuerdo (Id),
	CONSTRAINT FK_UnidadAcuerdo_Unidad_IdUnidad FOREIGN KEY (IdUnidad) REFERENCES Unidad (Id)
)

CREATE TABLE PorTanto (
	Id INT NOT NULL IDENTITY (1, 1),
	IdAcuerdo INT NOT NULL,
	Descripcion NVARCHAR (MAX) NOT NULL,
	CONSTRAINT PK_PorTanto PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_PorTanto_Acuerdo_IdAcuerdo FOREIGN KEY (IdAcuerdo) REFERENCES Acuerdo (Id)
)

CREATE TABLE Considerando (
	Id INT NOT NULL IDENTITY (1, 1),
	IdAcuerdo INT NOT NULL,
	Descripcion NVARCHAR (MAX) NOT NULL,
	CONSTRAINT PK_Considerando PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Considerando_Acuerdo_IdAcuerdo FOREIGN KEY (IdAcuerdo) REFERENCES Acuerdo (Id)
)

CREATE TABLE Avance (
	Id INT NOT NULL IDENTITY (1, 1),
	IdUsuario INT NOT NULL,
	IdAcuerdo INT NOT NULL,
	Descripcion NVARCHAR(MAX) NULL,
	CONSTRAINT PK_Avance PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Avance_Usuario_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES Usuario (Id),
	CONSTRAINT FK_Avance_Acuerdo_IdAcuerdo FOREIGN KEY (IdAcuerdo) REFERENCES Acuerdo (Id)
)

CREATE TABLE PermisoRol (
	Id INT NOT NULL IDENTITY (1, 1),
	IdRol INT NOT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	NombreVista NVARCHAR(100) NOT NULL,
	Crear BIT NOT NULL,
	Editar BIT NOT NULL,
	Eliminar BIT NOT NULL,
	Ver BIT NOT NULL,
	CONSTRAINT PK_PermisoRol PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_PermisoRol_Rol_IdRol FOREIGN KEY (IdRol) REFERENCES Rol (Id)
)

CREATE TABLE TelefonoUsuario (
	Id INT NOT NULL IDENTITY (1, 1),
	IdUsuario INT NOT NULL,
	Numero NVARCHAR (20) NOT NULL,
	Extension NVARCHAR (10) NULL,
	CONSTRAINT PK_TelefonoUsuario PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_TelefonoUsuario_Usuario_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES Usuario (Id)
)

CREATE TABLE CorreoUsuario (
	Id INT NOT NULL IDENTITY (1, 1),
	IdUsuario INT NOT NULL,
	Correo NVARCHAR(100),
	CONSTRAINT PK_CorreoUsuario PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_CorreoUsuario_Usuario_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES Usuario (Id)
)

CREATE TABLE UsuarioArticulo (
	IdArticulo INT NOT NULL,
	IdUsuario INT NOT NULL,
	CONSTRAINT PK_UsuarioArticulo PRIMARY KEY CLUSTERED (IdArticulo, IdUsuario ASC),
	CONSTRAINT FK_UsuarioArticulo_Articulo_IdArticulo FOREIGN KEY (IdArticulo) REFERENCES Articulo (Id),
	CONSTRAINT FK_UsuarioArticulo_Usuario_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES Usuario (Id)
)

CREATE TABLE Anotacion (
	Id INT NOT NULL IDENTITY (1, 1),
	IdTipoObjeto INT NOT NULL,
	IdObjeto INT NOT NULL,
	IdEstado INT NOT NULL,
	Descripcion NVARCHAR (MAX) NOT NULL,
	CONSTRAINT PK_Anotacion PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Anotacion_TipoObjeto_IdTipoObjeto FOREIGN KEY (IdTipoObjeto) REFERENCES TipoObjeto (Id),
	CONSTRAINT FK_Anotacion_Estado_IdEstado FOREIGN KEY (IdEstado) REFERENCES Estado (Id)
)

CREATE TABLE ArchivoAdjunto (
	Id INT NOT NULL IDENTITY (1, 1),
	IdTipoObjeto INT NOT NULL,
	IdObjeto INT NOT NULL,
	Observacion NVARCHAR(MAX) NULL,
	UrlArchivo NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_ArchivoAdjunto PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_ArchivoAdjunto_TipoObjeto_IdTipoObjeto FOREIGN KEY (IdTipoObjeto) REFERENCES TipoObjeto (Id)
)

CREATE TABLE Bitacora (
	Id INT NOT NULL IDENTITY (1, 1),
	IdUsuario INT NOT NULL,
	FechaHora DATETIME NOT NULL,
	Accion NVARCHAR (MAX) NOT NULL,
	ValorAntiguo NVARCHAR (MAX) NOT NULL,
	ValorNuevo NVARCHAR (MAX) NOT NULL,
	DireccionIP NVARCHAR (50) NOT NULL,
	DescripcionDispositivo NVARCHAR (100) NOT NULL,
	SeccionSistema NVARCHAR (100) NOT NULL,
	CONSTRAINT PK_Bitacora PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Bitacora_Usuario_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES Usuario (Id)
)
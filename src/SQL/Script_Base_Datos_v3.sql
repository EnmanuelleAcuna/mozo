--Script_Base_Datos_v2.0
USE SGJD

CREATE TABLE SGJD_ADM_TAB_ROL (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	Nombre NVARCHAR (50) NOT NULL UNIQUE,
	CONSTRAINT LLP_Rol PRIMARY KEY CLUSTERED (LLP_Id ASC)
)

CREATE TABLE SGJD_ADM_TAB_PERMISO_ROL (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdRol INT NOT NULL,
	NombreVista NVARCHAR(100) NOT NULL,
	NivelAccion INT NOT NULL,
	CONSTRAINT LLP_PermisoRol PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_PermisoRol_Rol_LLF_IdRol FOREIGN KEY (LLF_IdRol) REFERENCES SGJD_ADM_TAB_ROL (LLP_Id)
)

CREATE TABLE SGJD_ADM_TAB_UNIDAD (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	Nombre NVARCHAR (50) NOT NULL,
	CONSTRAINT LLP_Unidad PRIMARY KEY CLUSTERED (LLP_Id ASC)
)

CREATE TABLE SGJD_ADM_TAB_TIPO_USUARIO (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	Nombre NVARCHAR(256) NOT NULL,
	CONSTRAINT LLP_TipoUsuario PRIMARY KEY CLUSTERED (LLP_Id ASC)
)

CREATE TABLE SGJD_ADM_TAB_USUARIO (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdRol INT NOT NULL,
	LLF_IdUnidad INT NOT NULL,
	LLF_IdTipoUsuario INT NOT NULL,
	Nombre NVARCHAR (50) NOT NULL,
	Apellidos NVARCHAR (50) NOT NULL,
	Correo NVARCHAR (50) NOT NULL,
	Contraseña NVARCHAR( 60) NOT NULL,
	UltimaSesion DATETIME NULL,
	CONSTRAINT LLP_Usuario PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_Usuario_Rol_LLF_IdRol FOREIGN KEY (LLF_IdRol) REFERENCES SGJD_ADM_TAB_ROL(LLP_Id),
	CONSTRAINT LLF_Usuario_Unidad_LLF_IdUnidad FOREIGN KEY (LLF_IdUnidad) REFERENCES SGJD_ADM_TAB_UNIDAD (LLP_Id),
	CONSTRAINT LLF_Usuario_TipoUsuario_LLF_IdTipoUsuario FOREIGN KEY (LLF_IdTipoUsuario) REFERENCES SGJD_ADM_TAB_TIPO_USUARIO (LLP_Id)	
)

CREATE TABLE SGJD_ADM_TAB_ASISTENTE_SESION (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdUsuario INT NOT NULL,
	Presente BIT NOT NULL,
	HoraInicio DATETIME NOT NULL,
	HoraFin DATETIME NOT NULL,
	CONSTRAINT LLP_AsistenteSesion PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_AsistenteSesion_Usuario_LLF_IdUsuario FOREIGN KEY (LLF_IdUsuario) REFERENCES SGJD_ADM_TAB_USUARIO(LLP_Id)
)

CREATE TABLE SGJD_ADM_TAB_OTRA_ASISTENTE_SESION (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdTipoUsuario INT NOT NULL,
	Nombre NVARCHAR (256) NOT NULL,
	Observacion NVARCHAR (256) NOT NULL,
	HoraInicio DATETIME NOT NULL,
	HoraFin DATETIME NOT NULL,
	CONSTRAINT LLP_OtraAsistenteSesion PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_OtraAsistencia_TipoUsuario_LLF_IdTipoUsuario FOREIGN KEY (LLF_IdTipoUsuario) REFERENCES SGJD_ADM_TAB_TIPO_USUARIO (LLP_Id)
)
CREATE TABLE SGJD_ADM_TAB_TIPO_OBJETO (
	LLP_Id INT NOT NULL IDENTITY (1, 1),	
	Descripcion NVARCHAR (50) NOT NULL,
	NombreTabla NVARCHAR (250) NOT NULL,
	CONSTRAINT LLP_TipoObjeto PRIMARY KEY CLUSTERED (LLP_Id ASC)
)

INSERT INTO SGJD_ADM_TAB_ROL VALUES ('Administrador')
INSERT INTO SGJD_ADM_TAB_ROL VALUES ('Director')

CREATE TABLE SGJD_ADM_TAB_ESTADO (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdTipoObjeto INT NOT NULL,
	Descripcion NVARCHAR (50) NOT NULL,
	CONSTRAINT LLP_Estado PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_TipoObjeto_Estado_LLF_IdTipoObjeto FOREIGN KEY (LLF_IdTipoObjeto) REFERENCES SGJD_ADM_TAB_TIPO_OBJETO(LLP_Id)
)

CREATE TABLE SGJD_ADM_TAB_TIPO_SESION (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	Descripcion NVARCHAR (50) NOT NULL,
	CONSTRAINT LLP_TipoSesion PRIMARY KEY CLUSTERED (LLP_Id ASC)
)

INSERT INTO SGJD_ADM_TAB_TIPO_SESION VALUES ('Ordinaria')
INSERT INTO SGJD_ADM_TAB_TIPO_SESION VALUES ('Extraordinaria')

CREATE TABLE SGJD_ACT_TAB_SESION (
	LLP_Id INT NOT NULL IDENTITY (1,1),
	Numero INT NOT NULL,
	FechaHora DATETIME NOT NULL,
	LLF_IdTipoSesion INT NOT NULL,
	LLF_IdUsuario INT NOT NULL,
	CONSTRAINT LLP_Sesion PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_Sesion_TipoSesion_LLF_IdTipoSesion FOREIGN KEY (LLF_IdTipoSesion) REFERENCES SGJD_ADM_TAB_TIPO_SESION (LLP_Id),
	CONSTRAINT FK_Sesion_Usuario_IdUsuario FOREIGN KEY (LLF_IdUsuario) REFERENCES SGJD_ADM_TAB_ROL (LLP_Id)
)

CREATE TABLE SGJD_ACT_TAB_ORDEN_DIA (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdSesion INT NOT NULL,
	LLF_IdEstado INT NOT NULL,
	CONSTRAINT LLP_OrdenDia PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_OrdenDia_Sesion_LLF_IdSesion FOREIGN KEY (LLF_IdSesion) REFERENCES SGJD_ACT_TAB_SESION (LLP_Id),
	CONSTRAINT LLF_OrdenDia_Estado_LLF_IdEstado FOREIGN KEY (LLF_IdEstado) REFERENCES SGJD_ADM_TAB_ESTADO (LLP_Id)
)

CREATE TABLE SGJD_ACT_TAB_SECCION (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	Descripcion NVARCHAR (100) NOT NULL,
	CONSTRAINT LLP_Seccion PRIMARY KEY CLUSTERED (LLP_Id ASC)
)

CREATE TABLE SGJD_ACT_TAB_SECCION_ORDEN_DIA (
	LLF_IdOrdenDia INT NOT NULL,
	LLF_IdSeccion INT NOT NULL,
	CONSTRAINT LLP_SeccionOrdenDia PRIMARY KEY CLUSTERED (LLF_IdOrdenDia ASC, LLF_IdSeccion ASC),
	CONSTRAINT LLF_SeccionOrdenDia_OrdenDia_LFF_IdOrdenDia FOREIGN KEY (LLF_IdOrdenDia) REFERENCES SGJD_ACT_TAB_ORDEN_DIA (LLP_Id),
	CONSTRAINT LLF_SeccionOrdenDia_Seccion_LLF_IdSeccion FOREIGN KEY (LLF_IdSeccion) REFERENCES SGJD_ACT_TAB_SECCION (LLP_Id)
)

CREATE TABLE SGJD_ACT_TAB_TEMA (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdSeccion INT NOT NULL,
	LLF_IdSesionEjecutada INT NULL,
	LLF_IdEstado INT NOT NULL,
	Titulo NVARCHAR (50) NOT NULL,	
	Resumen NVARCHAR (MAX) NOT NULL,
	CONSTRAINT LLP_Tema PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_Tema_Seccion_LLF_IdSeccion FOREIGN KEY (LLF_IdSeccion) REFERENCES SGJD_ACT_TAB_SECCION (LLP_Id),
	CONSTRAINT LLF_Tema_Sesion_LLF_IdSesionEjecutadan FOREIGN KEY (LLF_IdSesionEjecutada) REFERENCES SGJD_ACT_TAB_SESION (LLP_Id),
	CONSTRAINT LLF_Tema_Estado_LLF_IdEstado FOREIGN KEY (LLF_IdEstado) REFERENCES SGJD_ADM_TAB_ESTADO (LLP_Id)
)

CREATE TABLE  SGJD_ACT_TAB_TOMO (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	Numero INT NOT NULL,
	Nombre NVARCHAR (256),
	CONSTRAINT LLP_Tomo PRIMARY KEY CLUSTERED (LLP_Id ASC)
)

CREATE TABLE  SGJD_ACT_TAB_USUARIO_TOMO (
	LLF_IdTomo INT NOT NULL,
	LLF_IdUsuario INT NOT NULL,
	Fecha DATETIME NOT NULL,
	Accion NVARCHAR (50),
	CONSTRAINT LLP_TomoUsuario PRIMARY KEY CLUSTERED (LLF_IdTomo ASC, LLF_IdUsuario ASC),
	CONSTRAINT LLF_TomoUsuario_Usuario_LLF_IdTomo FOREIGN KEY (LLF_IdTomo) REFERENCES SGJD_ACT_TAB_TOMO(LLP_Id),
	CONSTRAINT LLF_TomoUsuario_Usuario_LLF_IdUsuario FOREIGN KEY (LLF_IdUsuario) REFERENCES SGJD_ADM_TAB_USUARIO(LLP_Id)
)

CREATE TABLE SGJD_ACT_TAB_ACTA (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdTomo INT NOT NULL,
	LLF_IdSesion INT NOT NULL,
	LLF_IdEstado INT NOT NULL,
	Numero INT NOT NULL,
	CONSTRAINT LLP_Acta PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_Acta_Tomo_LLF_IdTomo FOREIGN KEY (LLF_IdTomo) REFERENCES SGJD_ACT_TAB_TOMO (LLP_Id),
	CONSTRAINT LLF_Acta_Sesion_LLF_IdSesion FOREIGN KEY (LLF_IdSesion) REFERENCES SGJD_ACT_TAB_SESION (LLP_Id),
	CONSTRAINT LLF_Acta_Estado_LLF_IdEstado FOREIGN KEY (LLF_IdEstado) REFERENCES SGJD_ADM_TAB_ESTADO (LLP_Id)
)

CREATE TABLE  SGJD_ACT_TAB_TOMO_ACTA (
	LLF_IdActa INT NOT NULL,
	LLF_IdTomo INT NOT NULL,
	CONSTRAINT LLP_TomoAcuerdo PRIMARY KEY CLUSTERED (LLF_IdActa ASC, LLF_IdTomo ASC),
	CONSTRAINT LLF_TomoAcuerdo_Acuerdo_LLF_IdAcuerdo FOREIGN KEY (LLF_IdActa) REFERENCES SGJD_ACT_TAB_ACTA(LLP_Id),
	CONSTRAINT LLF_TomoAcuerdo_Unidad_LLF_IdTomo FOREIGN KEY (LLF_IdTomo) REFERENCES SGJD_ACT_TAB_TOMO(LLP_Id)
)

CREATE TABLE SGJD_ACT_TAB_CAPITULO (
	LLP_Id INT NOT NULL,
	LLF_IdActa INT NOT NULL,
	Numero INT NOT NULL,
	Titulo NVARCHAR (200) NOT NULL,
	Contenido NVARCHAR (MAX) NOT NULL,
	CONSTRAINT LLP_Capitulo PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_Capitulo_Acta_LLF_IdActa FOREIGN KEY (LLF_IdActa) REFERENCES SGJD_ACT_TAB_ACTA (LLP_Id)
)

CREATE TABLE SGJD_ACT_TAB_ARTICULO(
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdCapitulo INT NOT NULL,
	Numero INT NOT NULL,
	Titulo NVARCHAR (100) NOT NULL,
	Contenido NVARCHAR (MAX) NOT NULL,
	Confidencial BIT NOT NULL,
	CONSTRAINT LLP_Articulo PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_Articulo_Capitulo_LLF_IdCapitulo FOREIGN KEY (LLF_IdCapitulo) REFERENCES SGJD_ACT_TAB_CAPITULO (LLP_Id)
)

CREATE TABLE SGJD_ACU_TAB_ACUERDO (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdArticulo INT NOT NULL,
	LLF_IdEstado INT NOT NULL,
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
	CONSTRAINT LLP_Acuerdo PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_Acuerdo_Articulo_LLF_IdArticulo FOREIGN KEY (LLF_IdArticulo) REFERENCES SGJD_ACT_TAB_ARTICULO (LLP_Id),
	CONSTRAINT LLF_Acuerdo_Estado_LLF_IdEstado FOREIGN KEY (LLF_IdEstado) REFERENCES SGJD_ADM_TAB_ESTADO (LLP_Id)
)

CREATE TABLE SGJD_ACU_TAB_UNIDAD_ACUERDO (
	LLF_IdAcuerdo INT NOT NULL,
	LLF_IdUnidad INT NOT NULL,
	CONSTRAINT LLP_UnidadAcuerdo PRIMARY KEY CLUSTERED (LLF_IdAcuerdo ASC, LLF_IdUnidad ASC),
	CONSTRAINT LLF_UnidadAcuerdo_Acuerdo_LLF_IdAcuerdo FOREIGN KEY (LLF_IdAcuerdo) REFERENCES SGJD_ACU_TAB_ACUERDO (LLP_Id),
	CONSTRAINT LLF_UnidadAcuerdo_Unidad_LLF_IdUnidad FOREIGN KEY (LLF_IdUnidad) REFERENCES SGJD_ADM_TAB_UNIDAD (LLP_Id)
)

CREATE TABLE SGJD_ACU_TAB_PORTANTO (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdAcuerdo INT NOT NULL,
	Descripcion NVARCHAR (MAX) NOT NULL,
	CONSTRAINT LLP_PorTanto PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_PorTanto_Acuerdo_LLF_IdAcuerdo FOREIGN KEY (LLF_IdAcuerdo) REFERENCES SGJD_ACU_TAB_ACUERDO (LLP_Id)
)

CREATE TABLE SGJD_ACU_TAB_CONSIDERANDO (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdAcuerdo INT NOT NULL,
	Descripcion NVARCHAR (MAX) NOT NULL,
	CONSTRAINT LLP_Considerando PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_Considerando_Acuerdo_LLF_IdAcuerdo FOREIGN KEY (LLF_IdAcuerdo) REFERENCES SGJD_ACU_TAB_ACUERDO (LLP_Id)
)

CREATE TABLE SGJD_ACU_TAB_AVANCE (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdUsuario INT NOT NULL,
	LLF_IdAcuerdo INT NOT NULL,
	Descripcion NVARCHAR(MAX) NULL,
	CONSTRAINT LLP_Avance PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_Avance_Usuario_LLF_IdUsuario FOREIGN KEY (LLF_IdUsuario) REFERENCES SGJD_ADM_TAB_USUARIO (LLP_Id),
	CONSTRAINT LLF_Avance_Acuerdo_LLF_IdAcuerdo FOREIGN KEY (LLF_IdAcuerdo) REFERENCES SGJD_ACU_TAB_ACUERDO (LLP_Id)
)

CREATE TABLE SGJD_ADM_TAB_TELEFONO_USUARIO (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdUsuario INT NOT NULL,
	Numero NVARCHAR (20) NOT NULL,
	Extension NVARCHAR (10) NULL,
	CONSTRAINT LLP_TelefonoUsuario PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_TelefonoUsuario_Usuario_LLF_IdUsuario FOREIGN KEY (LLF_IdUsuario) REFERENCES SGJD_ADM_TAB_USUARIO (LLP_Id)
)

CREATE TABLE SGJD_ADM_TAB_CORREO_USUARIO (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdUsuario INT NOT NULL,
	Correo NVARCHAR(100),
	CONSTRAINT LLP_CorreoUsuario PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_CorreoUsuario_Usuario_LLF_IdUsuario FOREIGN KEY (LLF_IdUsuario) REFERENCES SGJD_ADM_TAB_USUARIO (LLP_Id)
)

CREATE TABLE SGJD_ADM_TAB_USUARIO_ARTICULO (
	LLF_IdArticulo INT NOT NULL,
	LLF_IdUsuario INT NOT NULL,
	CONSTRAINT LLP_UsuarioArticulo PRIMARY KEY CLUSTERED (LLF_IdArticulo, LLF_IdUsuario ASC),
	CONSTRAINT LLF_UsuarioArticulo_Articulo_LLF_IdArticulo FOREIGN KEY (LLF_IdArticulo) REFERENCES SGJD_ACT_TAB_ARTICULO (LLP_Id),
	CONSTRAINT LLF_UsuarioArticulo_Usuario_LLF_IdUsuario FOREIGN KEY (LLF_IdUsuario) REFERENCES SGJD_ADM_TAB_USUARIO (LLP_Id)
)

CREATE TABLE SGJD_ADM_TAB_ANOTACION (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdTipoObjeto INT NOT NULL,
	IdObjeto INT NOT NULL,
	LLF_IdEstado INT NOT NULL,
	Descripcion NVARCHAR (MAX) NOT NULL,
	CONSTRAINT LLP_Anotacion PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_Anotacion_TipoObjeto_LLF_IdTipoObjeto FOREIGN KEY (LLF_IdTipoObjeto) REFERENCES SGJD_ADM_TAB_TIPO_OBJETO (LLP_Id),
	CONSTRAINT LLF_Anotacion_Estado_LLF_IdEstado FOREIGN KEY (LLF_IdEstado) REFERENCES SGJD_ADM_TAB_ESTADO (LLP_Id)
)

CREATE TABLE SGJD_ADM_TAB_ARCHIVO_ADJUNTO(
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdTipoObjeto INT NOT NULL,
	IdObjeto INT NOT NULL,
	Observacion NVARCHAR(MAX) NULL,
	UrlArchivo NVARCHAR(100) NOT NULL,
	CONSTRAINT LLP_ArchivoAdjunto PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_ArchivoAdjunto_TipoObjeto_LLF_IdTipoObjeto FOREIGN KEY (LLF_IdTipoObjeto) REFERENCES SGJD_ADM_TAB_TIPO_OBJETO (LLP_Id)
)

CREATE TABLE SGJD_ADM_TAB_BITACORA (
	LLP_Id INT NOT NULL IDENTITY (1, 1),
	LLF_IdUsuario INT NOT NULL,
	FechaHora DATETIME NOT NULL,
	Accion NVARCHAR (MAX) NOT NULL,
	ValorAntiguo NVARCHAR (MAX) NOT NULL,
	ValorNuevo NVARCHAR (MAX) NOT NULL,
	DireccionIP NVARCHAR (50) NOT NULL,
	DescripcionDispositivo NVARCHAR (100) NOT NULL,
	SeccionSistema NVARCHAR (100) NOT NULL,
	CONSTRAINT LLP_Bitacora PRIMARY KEY CLUSTERED (LLP_Id ASC),
	CONSTRAINT LLF_Bitacora_Usuario_LLF_IdUsuario FOREIGN KEY (LLF_IdUsuario) REFERENCES SGJD_ADM_TAB_USUARIO (LLP_Id)
)
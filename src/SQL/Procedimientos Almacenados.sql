USE SGJD GO

--PA para listar las tablas de la base de datos
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = OBJECT_ID(N'[dbo].[SGJD_ADM_PRO_OBTENER_TABLAS_BD]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE SGJD_ADM_PRO_OBTENER_TABLAS_BD
END

GO

CREATE PROCEDURE SGJD_ADM_PRO_OBTENER_TABLAS_BD
AS
BEGIN
	SELECT TABLE_NAME AS Nombre
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME LIKE '%SGJD%'
END --Fin de SP

GO

EXEC SGJD_ADM_PRO_OBTENER_TABLAS_BD

GO

DROP PROCEDURE SGJD_ADM_PRO_OBTENER_VISTAS_PERFIL

GO

--PA para listar las vistas de un rol
--PA no utilizado
CREATE PROCEDURE SGJD_ADM_PRO_OBTENER_VISTAS_PERFIL
	@idPerfil NVARCHAR (128)
AS
BEGIN
	SELECT VP.LLF_IdPerfil AS idPerfil, P.[Name] AS nombrePerfil, VP.LLF_IdVista AS idVista, V.NombreVista, VP.Permiso AS NivelPermiso,
	CASE 
		WHEN VP.Permiso = 7 THEN 'Master' 
		WHEN VP.Permiso = 6 THEN 'Editar/Eliminar'
		WHEN VP.Permiso = 5 THEN 'Crear/Eliminar'
		WHEN VP.Permiso = 4 THEN 'Eliminar'
		WHEN VP.Permiso = 3 THEN 'Crear/Editar'
		WHEN VP.Permiso = 2 THEN 'Editar'
		WHEN VP.Permiso = 1 THEN 'Crear'
		WHEN VP.Permiso = 0 THEN 'Ver'
	END AS NombrePermiso
	FROM dbo.SGJD_ADM_TAB_VISTAS_PERFIL VP 
	INNER JOIN dbo.SGJD_ADM_TAB_ROLES P ON VP.LLF_IdPerfil = P.Id
	INNER JOIN dbo.SGJD_ADM_TAB_VISTAS V ON VP.LLF_IdVista = V.LLP_Id
	WHERE VP.LLF_IdPerfil = @idPerfil
END
--Fin de SP
GO

EXEC SGJD_ADM_PRO_OBTENER_VISTAS_PERFIL '1'
GO

DROP PROCEDURE SGJD_ADM_PRO_OBTENER_RANGO_FECHA_BITACORA
GO

--PA para obtener un rango de fechas de la bitacora
ALTER PROCEDURE SGJD_ADM_PRO_OBTENER_RANGO_FECHA_BITACORA
	@fechaInicio DATETIME,
	@fechaFin DATETIME
AS
BEGIN
	SELECT B.LLP_Id AS Id, U.Nombre As NombreUsuario, B.FechaHora, B.Accion, B.SeccionSistema
	FROM SGJD.dbo.SGJD_ADM_TAB_BITACORA B
	INNER JOIN SGJD.dbo.SGJD_ADM_TAB_USUARIOS U ON B.LLF_IdUsuario = U.LLP_Id
	WHERE B.FechaHora BETWEEN CONVERT(VARCHAR(10), @fechaInicio, 101) AND CONVERT(VARCHAR(10), @fechaFin, 101)
	ORDER BY B.FechaHora DESC
END
--Fin de SP
GO

EXEC SGJD_ADM_PRO_OBTENER_RANGO_FECHA_BITACORA '20191223', '20191231'
GO

DROP PROCEDURE SGJD_ADM_PRO_BACKUPDB
GO

--SP para realizar el respaldo de la BD
--Procedimiento no utilizado
CREATE PROCEDURE SGJD_ADM_PRO_BACKUPDB
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE
		@ArchivoBD nvarchar(100) = datename(DD,getdate())+'_'+datename(M,getdate())+'_'+datename(YYYY,getdate())+'_'+replace(replace(@BD,':','_'),'\','_')+'.bak', --Setea el formato del nombre del archivo del respaldo.bak,
		@Temp INT = 1,
		@BD NVARCHAR(100) = 'SGJD',
		@Ubicacion NVARCHAR(100)   = 'C:\Users\Sharel\Desktop\Backup\'
	EXEC ('BACKUP DATABASE ['+ @BD +'] TO  DISK = N'''+ @Ubicacion + @ArchivoBD +''' WITH COPY_ONLY,  NAME = N'''+ @BD +'-Full Database Backup''') --Ejecuta el Respaldo
	SELECT @Temp --Devuelve un 1 por default para que el sp sea agregado al model del proyecto
END
--Fn de SP
GO

DROP PROCEDURE SGJD_ACT_PRO_OBTENER_DATOS_SESIONES
GO

-- Procedimiento almacenado para obtener los datos de la cantidad de sesiones por tipo para ser
-- utilizado en ChartJS y mostrarlo en un gráfico
CREATE PROCEDURE SGJD_ACT_PRO_OBTENER_DATOS_SESIONES
AS
BEGIN
	SELECT ts.Descripcion AS 'Tipo', COUNT(s.LLP_Id) AS Cantidad
	FROM SGJD_ADM_TAB_TIPOS_SESION ts
	INNER JOIN SGJD_ACT_TAB_SESIONES s ON ts.LLP_Id = s.LLF_IdTipoSesion
	GROUP BY ts.Descripcion
END
--Fin de SP
GO

EXEC SGJD_ACT_PRO_OBTENER_DATOS_SESIONES

-- Varios
EXEC SP_HELP 'SGJD_ADM_TAB_BITACORA'

SELECT * FROM SGJD_ACT_TAB_SESIONES

SELECT * FROM SGJD_ADM_TAB_TIPOS_SESION

SELECT s.LLP_Id, s.Numero, s.FechaHora, ts.Descripcion
FROM SGJD_ACT_TAB_SESIONES s
INNER JOIN SGJD_ADM_TAB_TIPOS_SESION ts ON s.LLF_IdTipoSesion = ts.LLP_Id

SELECT * FROM SGJD_ACT_TAB_TEMAS

SELECT * FROM SGJD_ADM_TAB_SECCIONES
-- Fin de varios

DROP PROCEDURE SGJD_ACT_PRO_OBTENER_DATOS_SESIONES
GO

-- Procedimiento almacenado para realizar filtro de actas
CREATE PROCEDURE SGJD_ACT_PRO_FILTRO_PALABRA_CLAVE
	@Palabra nvarchar(50)
AS
BEGIN
	SELECT	DISTINCT Acta.LLP_Id as Id, Acta.LLF_IdTomo, Acta.LLF_IdSesion, Acta.LLF_IdEstado,  Acta.Numero as 'NumeroActa', Sesion.Numero as 'NumeroSesion', TipoSesion.Descripcion as 'tipoSesionDescripcion',
			Sesion.FechaHora as 'FechaHoraSesion', Tomo.Numero as 'NumeroTomo', Tomo.Nombre as 'NombreTomo', Estado.Descripcion as 'DescripcionEstado'
	FROM	SGJD_ACT_TAB_ACTAS Acta
	INNER JOIN SGJD_ACT_TAB_CAPITULOS Capitulo ON Capitulo.LLF_IdActa = Acta.LLP_Id 
	LEFT JOIN SGJD_ACT_TAB_ARTICULOS Articulo ON Articulo.LLF_IdCapitulo = Capitulo.LLP_Id 
	LEFT JOIN SGJD_ACU_TAB_ACUERDOS Acuerdo ON Acuerdo.LLF_IdArticulo = Articulo.LLP_Id
	LEFT JOIN SGJD_ACT_TAB_SESIONES Sesion ON Sesion.LLP_Id = Acta.LLF_IdSesion
	LEFT JOIN SGJD_ADM_TAB_TIPOS_SESION TipoSesion ON TipoSesion.LLP_Id = Sesion.LLF_IdTipoSesion
	LEFT JOIN SGJD_ACT_TAB_TOMOS Tomo ON Tomo.LLP_Id = Acta.LLF_IdTomo
	LEFT JOIN SGJD_ADM_TAB_ESTADOs Estado ON Estado.LLP_Id = Acta.LLF_IdEstado
	WHERE	Capitulo.Titulo LIKE '%'+@Palabra+'%' OR Capitulo.Contenido LIKE '%'+@Palabra+'%' OR Articulo.Titulo LIKE '%'+@Palabra+'%'
			OR Articulo.Contenido LIKE '%'+@Palabra+'%' OR Acuerdo.Titulo LIKE '%'+@Palabra+'%'
END
-- Fin de PA
GO

EXEC SGJD_ACT_PRO_FILTRO_PALABRA_CLAVE ''
GO

--PA para extraer un acuerdo por id articulo
ALTER PROCEDURE dbo.SGJD_ACU_PRO_OBTENER_ACUERDO_POR_ARTICULO
	@Articulo INT
AS
BEGIN
	SELECT TOP 1 Acuerdo.Titulo AS TituloAcuerdo, Acuerdo.DetalleConsiderando AS DescripcionConsiderando, Acuerdo.DetallePorTanto AS DescripcionPorTanto
	FROM SGJD_ACT_TAB_ARTICULOS Articulo
	INNER JOIN SGJD_ACU_TAB_ACUERDOS Acuerdo  ON Acuerdo.LLF_IdArticulo = Articulo.LLP_Id
	WHERE Articulo.LLP_Id = @Articulo AND (Acuerdo.LLF_IdEstado = 1018 OR Acuerdo.LLF_IdEstado = 1019 OR Acuerdo.LLF_IdEstado = 1020 OR Acuerdo.LLF_IdEstado = 1021)
	ORDER BY Acuerdo.NumeroVersion DESC
END
--Fin de PA
GO

EXEC SGJD_ACU_PRO_OBTENER_ACUERDO_POR_ARTICULO 1
GO

ALTER PROCEDURE dbo.SGJD_ACT_PRO_ACUERDOS_PARA_SEGUIMIENTO
AS 
BEGIN
	SELECT	DISTINCT A.LLP_Id AS IdAcuerdo, A.Titulo, A.Asunto, A.FechaFirmeza AS FechaFirmeza, E.Descripcion as Estado
	FROM	SGJD_ACU_TAB_ACUERDOS A
	INNER JOIN SGJD_ADM_TAB_ESTADOS E ON A.LLF_IdEstado = E.LLP_Id
	WHERE
	-- Acuerdo con tipo de seguimiento (1: Con seguimiento) y con estado (1021: Notificado, 1024: En ejecución) o acuerdo con tipo de seguimiento (2: Con seguimiento permanente)
	(A.LLF_IdEstado IN (1021, 1024) AND A.TipoSeguimiento IN (1, 2)) OR (A.TipoSeguimiento = 2) AND A.Firme = 1
END
-- Fin de PA
GO

EXEC SGJD_ACT_PRO_ACUERDOS_PARA_SEGUIMIENTO
GO

-- PA para obtener las Sesiones que tienen Articulos disponibles para agregar Acuerdos
ALTER PROCEDURE dbo.SGJD_ACT_PRO_OBTENER_SESIONES_PARA_ACUERDO
AS 
BEGIN
	SELECT	DISTINCT sesion.LLP_Id AS IdSesion, tipoSesion.Descripcion + ' ' + CONVERT(NVARCHAR(10), sesion.Numero)  + ' - ' + CONVERT(NVARCHAR(4), YEAR(sesion.FechaHora)) AS NombreSesion,
			sesion.FechaHora AS Fecha
	FROM SGJD_ACT_TAB_SESIONES sesion
	INNER JOIN SGJD_ADM_TAB_TIPOS_SESION tipoSesion ON tipoSesion.LLP_Id = sesion.LLF_IdTipoSesion
	INNER JOIN SGJD_ACT_TAB_ACTAS acta ON acta.LLF_IdSesion = sesion.LLP_Id
	INNER JOIN SGJD_ACT_TAB_CAPITULOS capitulo ON capitulo.LLF_IdActa = acta.LLP_Id
	INNER JOIN SGJD_ACT_TAB_ARTICULOS articulo ON articulo.LLF_IdCapitulo = capitulo.LLP_Id
	LEFT JOIN SGJD_ACU_TAB_ACUERDOS acuerdo ON acuerdo.LLF_IdArticulo = articulo.LLP_Id
	WHERE sesion.LLF_IdEstado = 2 AND acuerdo.LLP_Id IS NULL
	ORDER BY sesion.LLP_Id DESC
END
-- Fin de PA
GO

EXEC SGJD_ACT_PRO_OBTENER_SESIONES_PARA_ACUERDO
GO

--PA para obtener de una Sesion, los Articulos que no tienen un Acuerdo creado 
ALTER PROCEDURE SGJD_ACU_PRO_ARTICULOS_POR_SESION
	@IdSesion INT
AS
BEGIN
	SELECT	DISTINCT Capitulo.LLP_Id AS IdCapitulo, Capitulo.Titulo AS TituloCapitulo, Articulo.LLP_Id AS IdArticulo, Articulo.Titulo AS TituloArticulo, Articulo.Confidencial
	FROM SGJD_ACT_TAB_SESIONES Sesion
	INNER JOIN SGJD_ACT_TAB_ACTAS Acta ON Acta.LLF_IdSesion = Sesion.LLP_Id
	INNER JOIN SGJD_ACT_TAB_CAPITULOS Capitulo ON Capitulo.LLF_IdActa = Acta.LLP_Id
	INNER JOIN SGJD_ACT_TAB_ARTICULOS Articulo ON Articulo.LLF_IdCapitulo = Capitulo.LLP_Id
	LEFT  JOIN SGJD_ACU_TAB_ACUERDOS Acuerdo ON Acuerdo.LLF_IdArticulo = Articulo.LLP_Id
	WHERE Sesion.LLP_Id = @IdSesion AND Acuerdo.LLP_Id IS NULL
END
-- Fin de PA
GO

EXEC SGJD_ACU_PRO_ARTICULOS_POR_SESION 2084
GO

--PA para obtener los asistentes de una Sesion por el id de Acuerdo, que se encuentra relacionado con un articulo, el articulo está relacionado con un capitulo,
-- el capítulo está relacionado con el Acta y el Acta con la Sesión
ALTER PROCEDURE [dbo].[SGJD_ACT_PRO_OBTENER_MIEMBROS_PRESENTES_POR_IDACUERDO]
@IdAcuerdo AS INT 
AS 
BEGIN
	SELECT asistente.LLP_Id AS IdAsistente, @IdAcuerdo AS IdAcuerdo
	FROM SGJD_ADM_TAB_ASISTENTES_SESION asistente
	INNER JOIN SGJD_ADM_TAB_USUARIOS usuario ON usuario.LLP_Id = asistente.LLF_IdUsuario
	INNER JOIN SGJD_ACT_TAB_SESIONES sesion ON sesion.LLP_Id = asistente.LLF_IdSesion
	INNER JOIN SGJD_ACT_TAB_ACTAS acta ON acta.LLF_IdSesion = sesion.LLP_Id
	INNER JOIN SGJD_ACT_TAB_CAPITULOS capitulo ON capitulo.LLF_IdActa = acta.LLP_Id
	INNER JOIN SGJD_ACT_TAB_ARTICULOS articulo ON articulo.LLF_IdCapitulo = capitulo.LLP_Id
	INNER JOIN SGJD_ACU_TAB_ACUERDOS acuerdo ON acuerdo.LLF_IdArticulo = articulo.LLP_Id AND acuerdo.LLP_Id = @IdAcuerdo
END
-- Fin de PA
GO

EXEC [SGJD_ACT_PRO_OBTENER_MIEMBROS_PRESENTES_POR_IDACUERDO] 8
GO

SELECT * FROM SGJD_ACU_TAB_VOTACIONES WHERE LLF_IdCapitulo = 1111

insert into SGJD_ACU_TAB_VOTACIONES values (8, 1204 ,'Voto a favor')

-- Procedimiento almacenado para realizar búsqueda de palabras en el contenido de un acuerdo
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SGJD_ACU_PRO_FILTRO_PALABRA_CLAVE_ACUERDOS]
	@Palabra NVARCHAR(250)
AS
	SELECT	Acuerdo.LLP_Id AS IdAcuerdo, Acuerdo.Titulo, Acuerdo.NumeroAcuerdo, Acuerdo.NumeroVersion, Acuerdo.Asunto, Acuerdo.Firme, Acuerdo.FechaFirmeza,
			Estado.CodigoEstado, Estado.Nombre AS NombreEstado
	FROM	SGJD_ACU_TAB_ACUERDOS Acuerdo
	INNER JOIN SGJD_ADM_TAB_ESTADOS Estado ON  Estado.LLP_Id = Acuerdo.LLF_IdEstado
	WHERE	Acuerdo.Titulo LIKE '%' + @Palabra + '%' OR Acuerdo.Asunto LIKE '%' + @Palabra + '%' OR Acuerdo.Detalle LIKE '%' + @Palabra + '%'
			OR Acuerdo.DetalleConsiderando LIKE '%' + @Palabra + '%' OR Acuerdo.DetallePorTanto LIKE '%' + @Palabra + '%'
			OR Acuerdo.Observaciones LIKE '%' + @Palabra + '%' OR Acuerdo.ObservacionVotaciones LIKE '%' + @Palabra + '%'
	ORDER BY Acuerdo.NumeroAcuerdo DESC, Acuerdo.NumeroVersion DESC
-- Fin de PA

GO

EXEC [SGJD_ACU_PRO_FILTRO_PALABRA_CLAVE_ACUERDOS] 'lorem'

GO

-- Procedimiento almacenado para realizar búsqueda de palabras en el contenido
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SGJD_ACT_PRO_FILTRO_PALABRA_CLAVE_ACTAS]
	@Palabra nvarchar(250)
AS
	SELECT	Acta.LLP_Id AS IdActa, Acta.LLF_IdSesion AS IdSesion, Sesion.Numero AS 'NumeroSesion', Sesion.FechaHora AS 'FechaSesion', TipoSesion.Descripcion AS 'TipoSesion',
	EstadoActa.CodigoEstado AS CodigoEstado, EstadoActa.Nombre AS 'NombreEstado', Acta.LLF_IdTomo AS IdTomo, Tomo.Nombre AS 'NombreTomo'
	FROM SGJD_ACT_TAB_ACTAS Acta
	INNER JOIN SGJD_ADM_TAB_ESTADOS EstadoActa ON Acta.LLF_IdEstado = EstadoActa.LLP_Id
	INNER JOIN SGJD_ACT_TAB_SESIONES Sesion ON Acta.LLF_IdSesion = Sesion.LLP_Id
	INNER JOIN SGJD_ADM_TAB_ESTADOS EstadoSesion ON Sesion.LLF_IdEstado = EstadoSesion.LLP_Id
	INNER JOIN SGJD_ADM_TAB_TIPOS_SESION TipoSesion ON Sesion.LLF_IdTipoSesion = TipoSesion.LLP_Id
	INNER JOIN SGJD_ACT_TAB_TOMOS Tomo ON Acta.LLF_IdTomo = Tomo.LLP_Id
	INNER JOIN SGJD_ADM_TAB_ESTADOS EstadoTomo ON Tomo.LLF_IdEstado = EstadoTomo.LLP_Id

	--INNER JOIN SGJD_ACT_TAB_CAPITULO Capitulo ON Acta.LLP_Id = Capitulo.LLF_IdActa
	--LEFT JOIN SGJD_ACT_TAB_ARTICULO Articulo ON Capitulo.LLP_Id = Articulo.LLF_IdCapitulo
	
	WHERE	Acta.Encabezado LIKE '%' + @Palabra + '%' OR Acta.Observacion LIKE '%' + @Palabra + '%' OR Acta.ParrafoCierre LIKE '%' + @Palabra + '%'
			--OR Capitulo.Titulo LIKE '%' + @Palabra + '%' OR Capitulo.Contenido LIKE '%' + @Palabra + '%'
			--OR Articulo.Titulo LIKE '%' + @Palabra + '%' OR Articulo.Contenido LIKE '%' + @Palabra + '%' OR Articulo.Observacion LIKE '%' + @Palabra + '%'
	ORDER BY Sesion.LLP_Id DESC
-- Fin de PA
GO

EXEC [SGJD_ACT_PRO_FILTRO_PALABRA_CLAVE_ACTAS] 'acta'

GO

-- Procedimiento almacenado para obtener los acuerdos notificados, en ejecución y ejecutados para gráfico
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SGJD_ACU_PRO_GRAFICO_ACUERDOS]
AS
	SELECT	SUM(CASE WHEN E.CodigoEstado = 'ACU-NOTI' THEN 1 ELSE 0 END) AS Notificados,
		SUM(CASE WHEN E.CodigoEstado = 'ACU-EE' THEN 1 ELSE 0 END) AS EnEjecucion,
		SUM(CASE WHEN E.CodigoEstado = 'ACU-EJEC' THEN 1 ELSE 0 END) AS Ejecutados
	FROM SGJD_ACU_TAB_ACUERDOS A
	INNER JOIN SGJD_ADM_TAB_ESTADOS E ON A.LLF_IdEstado = E.LLP_Id
-- Fin de PA
GO

EXEC [SGJD_ACU_PRO_GRAFICO_ACUERDOS]

GO

-- Procedimiento almacenado para obtener las sesiones para gráfico
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SGJD_ACT_PRO_GRAFICO_SESIONES]
AS
	SELECT	SUM(CASE WHEN TS.LLP_Id = 1 THEN 1 ELSE 0 END) AS Ordinarias,
			SUM(CASE WHEN TS.LLP_Id = 6 THEN 1 ELSE 0 END) AS Extraordinarias
	FROM SGJD_ACT_TAB_SESIONES S
	INNER JOIN SGJD_ADM_TAB_TIPOS_SESION TS ON S.LLF_IdTipoSesion = TS.LLP_Id
-- Fin de PA
GO

EXEC [SGJD_ACT_PRO_GRAFICO_SESIONES]

GO

-- Procedimiento almacenado para obtener las actas por año para gráfico
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SGJD_ACT_PRO_GRAFICO_ACTAS]
AS
	SELECT	YEAR(S.FechaHora) AS Anno, COUNT(A.LLP_Id) AS Cantidad
	FROM	SGJD_ACT_TAB_ACTAS A
	INNER JOIN SGJD_ACT_TAB_SESIONES S ON A.LLF_IdSesion = S.LLP_Id
	WHERE	YEAR(S.FechaHora) >= YEAR(GETDATE()) -5
	GROUP BY YEAR(S.FechaHora)
	ORDER BY YEAR(S.FechaHora) DESC
-- Fin de PA
GO

EXEC [SGJD_ACT_PRO_GRAFICO_ACTAS]

GO

-- Proccedimiento almacenado para obtener todos los seguimientos que estan vencidos
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SGJD_ACU_PRO_OBTENER_INFORME_SEGUIMIENTOS_VENCIDOS]
AS
BEGIN
	SELECT SA.LLP_Id AS IdSeguimiento, SA.Observaciones, SA.FechaNotificacion, SA.FechaVencimiento, SA.PlazoDias, SA.PorcentajeAvance
	FROM SGJD_ACU_TAB_SEGUIMIENTOS SA
	WHERE SA.FechaVencimiento <cast(SYSDATETIME() as date)
-- FIN DE SP
GO

EXEC [SGJD_ACU_PRO_OBTENER_INFORME_SEGUIMIENTOS_VENCIDOS]

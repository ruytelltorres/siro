use [DBSiro]
go
IF object_id('Perfil') is not null BEGIN DROP TABLE Perfil END
go
create table Perfil(
[cUser][varchar](4),
[cImgPerfilUrl][varchar](max) null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda informacion del perfil del usuario registrado en el SIRO' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Perfil'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario del Perfil' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Perfil', @level2type=N'COLUMN',@level2name=N'cUser'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Direccion url de la foto de perfil del usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Perfil', @level2type=N'COLUMN',@level2name=N'cImgPerfilUrl'
go
IF object_id('Menu') is not null BEGIN DROP TABLE Menu END
go
create table Menu(
[cMenuId] [varchar](10) not null,
[cMenuPadre] [varchar](10) not null,
[cTitulo] [varchar](100) not null,
[cDescripcion] [varchar](100) not null,
[cUrl] [varchar](200) not null,
[cIcono] [varchar](50) not null,
[nPosicion] [int] not null,
[bEstado] [bit] not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda informacion del menu que se mostrara en el SIRO' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del item del menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'cMenuId'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del ID padre a la que pertenece el item del menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'cMenuPadre'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Titulo del item del menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'cTitulo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del item del menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'cDescripcion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Direccion url del item del menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'cUrl'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Icono del item del menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'cIcono'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Posicion del item del menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'nPosicion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Host donde se realizo el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'bEstado'
go
IF object_id('Roles') is not null BEGIN DROP TABLE Roles END
go
create table Roles(
[cGrupoUsu][varchar](150) not null,
[cMenuId][varchar](10) not null,
[bEstado][bit] not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la informacion de la relacion de items del menu a la que tiene acceso el usuario logueado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Grupo del usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'cGrupoUsu'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del menu a la que se tiene permiso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'cMenuId'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del permiso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'bEstado'
go
IF object_id('MenuCargos') is not null BEGIN DROP TABLE MenuCargos END
go
create table MenuCargos(
	[nMenuCarCod][int] identity(1, 1) not null,
	[cMenuId][varchar](10) not null,
	[cCargoCod][varchar](10) not null,
	[bEstado][bit] not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la configuracion sobre que cargo tiene permiso a determinada opcion del menu del sistema' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MenuCargos'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Grupo del usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MenuCargos', @level2type=N'COLUMN',@level2name=N'nMenuCarCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MenuCargos', @level2type=N'COLUMN',@level2name=N'cMenuId'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cargo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MenuCargos', @level2type=N'COLUMN',@level2name=N'cCargoCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del permiso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MenuCargos', @level2type=N'COLUMN',@level2name=N'bEstado'
go
DBCC CHECKIDENT ('MenuCargos',RESEED, 1000)
go
IF object_id('Riesgo') is not null BEGIN DROP TABLE Riesgo END
go
create table Riesgo
(
	[nNroRiesgo][int] identity(1, 1) not null,
	[cNroRiesgo][varchar](25) not null,
	--[cOpeCod][varchar](6) not null,
	[cDescRiesgo][varchar](255) not null,
	[nEstadoRiesgo][int] not null,
	[nFlagRiesgo][int] not null,
	[nMigraRiesgo][int] null,
	[cHost][sysname] null default host_name(),
	[cUserDB][sysname] null default suser_sname(),
	[cApp][sysname] null default app_name()
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Maestro de los riesgos identificados' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Riesgo'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Riesgo', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Agencia/Usuario del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Riesgo', @level2type=N'COLUMN',@level2name=N'cNroRiesgo'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del operacion del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Riesgo', @level2type=N'COLUMN',@level2name=N'cOpeCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Riesgo', @level2type=N'COLUMN',@level2name=N'cDescRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Riesgo', @level2type=N'COLUMN',@level2name=N'nEstadoRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Situacion actual del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Riesgo', @level2type=N'COLUMN',@level2name=N'nFlagRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'nMovRiesgo del riesgo migrado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Riesgo', @level2type=N'COLUMN',@level2name=N'nMigraRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del equipo donde se realizo el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Riesgo', @level2type=N'COLUMN',@level2name=N'cHost'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario de Base de Datos que realizo el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Riesgo', @level2type=N'COLUMN',@level2name=N'cUserDB'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Aplicacion desde se realizo el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Riesgo', @level2type=N'COLUMN',@level2name=N'cApp'
go
IF object_id('DetalleRiesgo') is not null BEGIN DROP TABLE DetalleRiesgo END
go
create table DetalleRiesgo
(
	[nNroRiesgo][int] not null,
	[cCodRiesgo][varchar](15) not null,
	[nTpoRiesgo][int] not null,
	[cRiesgoIdentificado][nvarchar](max),
	[nProcRiesgo][int],
	[nTpoEvaluacion][int],
	[dFechaDetec][date],
	[cCodControl][varchar](6),
	[cObservacion][nvarchar](max),
	[cAgeCod][varchar](4),
	[cAreaCod][varchar](4),
	[cCodProceso][varchar](10),
	[nCodSubProceso][int],
	[bControlEfect][bit],
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda informacion basica de los riesgos identificados' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del Riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'cCodRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'nTpoRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del riesgo identificado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'cRiesgoIdentificado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'nProcRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de Evaluacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'nTpoEvaluacion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de deteccion del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'dFechaDetec'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'cCodControl'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Observacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'cObservacion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Agencia donde se identifico el riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'cAgeCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Area donde se identifico el riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'cAreaCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'cCodProceso'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'nCodSubProceso'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Efectividad del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'bControlEfect'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DetalleRiesgo', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('Taller') is not null BEGIN DROP TABLE Taller END
go
create table Taller
(
	[cCodTaller][varchar](20) not null,
	--[cUsuario] [varchar](4),
	[cTallerDesc][varchar](150),
	[nEstado][int],
	[cUltimaActualizacion][varchar](25)
)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda informacion de los talleres creados' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Taller'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del taller creado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Taller', @level2type=N'COLUMN',@level2name=N'cCodTaller'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que creo el taller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Taller', @level2type=N'COLUMN',@level2name=N'cUsuario'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del taller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Taller', @level2type=N'COLUMN',@level2name=N'cTallerDesc'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado actual del taller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Taller', @level2type=N'COLUMN',@level2name=N'nEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Agecia de la actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Taller', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('TallerRiesgo') is not null BEGIN DROP TABLE TallerRiesgo END
go
create table TallerRiesgo
(
	[cCodTaller][varchar](20) not null,
	[nNroRiesgo][int] not null,
    [nCondicion][int] not null,
	[bEstado][int] not null,
	[cUltimaActualizacion][varchar](25) not null
)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda informacion de los talleres relacionados al riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TallerRiesgo'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del taller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TallerRiesgo', @level2type=N'COLUMN',@level2name=N'cCodTaller'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TallerRiesgo', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Condicion del riesgo dentrol del taller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TallerRiesgo', @level2type=N'COLUMN',@level2name=N'nCondicion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado actual del taller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TallerRiesgo', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Agecia de la actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TallerRiesgo', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
-----------------------------------------------------------------------------------
IF object_id('ProcesoArea') is not null BEGIN DROP TABLE ProcesoArea END
go
create table ProcesoArea
(
	[cCodProceso][varchar](10) not null,
	[cDescProceso][varchar](255) not null,
	[dFechaReg] [datetime] not null default getdate(), 
	[cUltimaActualizacion][varchar](25) not null,
	[bEstadoProceso][bit] not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda informacion de los proceso del area' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoArea'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del Proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoArea', @level2type=N'COLUMN',@level2name=N'cCodProceso'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoArea', @level2type=N'COLUMN',@level2name=N'cDescProceso'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fechade registro del proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoArea', @level2type=N'COLUMN',@level2name=N'dFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Agecia de la actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoArea', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoArea', @level2type=N'COLUMN',@level2name=N'bEstadoProceso'
go
insert into ProcesoArea values('0741', 'Gestion Ahorros', '201901245025361090100TORE', 1),
('0742', 'Gestion de Servicios', '201901251025361090100TORE', 1)
go
----------------------------------------------------------------------------------------------
go
IF object_id('SubProcesoArea') is not null BEGIN DROP TABLE SubProcesoArea END
go
create table SubProcesoArea
(
	[nCodSubProceso][int]identity(1, 1),
	[cCodProceso][varchar](10) not null,
	[cDescSubProceso][varchar](500) not null,
	[cAbreviatura][varchar](10) not null,
	[cUltimaActualizacion][varchar](25) not null,
	[bEstadoSubProceso][bit] not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda informacion de los proceso del area' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProcesoArea'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del Proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProcesoArea', @level2type=N'COLUMN',@level2name=N'nCodSubProceso'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProcesoArea', @level2type=N'COLUMN',@level2name=N'cCodProceso'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProcesoArea', @level2type=N'COLUMN',@level2name=N'cDescSubProceso'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProcesoArea', @level2type=N'COLUMN',@level2name=N'cAbreviatura'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Agecia de la actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProcesoArea', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProcesoArea', @level2type=N'COLUMN',@level2name=N'bEstadoSubProceso'
go
DBCC CHECKIDENT ('SubProcesoArea',RESEED, 1000)
go 
IF object_id('ControlProceso') is not null BEGIN DROP TABLE ControlProceso END
go
create table ControlProceso(
	[nCodControl][int] identity(1, 1) not null,
	[cCodProceso][varchar] (10) not null,
	[cControlDescripcion][varchar](255) not null,
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null,
	[bEstado][bit] not null
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda los controles asignados a cada area.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlProceso'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo autogenerado del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlProceso', @level2type=N'COLUMN',@level2name=N'nCodControl'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlProceso', @level2type=N'COLUMN',@level2name=N'cCodProceso'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlProceso', @level2type=N'COLUMN',@level2name=N'cControlDescripcion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlProceso', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Agecia de la actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlProceso', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la transferencia (1) Activo, (0) Inactivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlProceso', @level2type=N'COLUMN',@level2name=N'bEstado'
go
DBCC CHECKIDENT ('ControlProceso',RESEED, 1000)
go
IF object_id('Causas') is not null BEGIN DROP TABLE Causas END
go
create table Causas(
	[cCodCausa][varchar] (10) not null,
	[cCausaDesc][varchar](500) not null,
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null,
	[bEstado][bit] not null
)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda los controles asignados a cada area.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Causas'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la causa del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Causas', @level2type=N'COLUMN',@level2name=N'cCodCausa'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion de la causa del riesgo ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Causas', @level2type=N'COLUMN',@level2name=N'cCausaDesc'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Causas', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Agecia de la actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Causas', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la causa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Causas', @level2type=N'COLUMN',@level2name=N'bEstado'
go
IF object_id('CausasRiesgo') is not null BEGIN DROP TABLE CausasRiesgo END
create table CausasRiesgo(
	[nNroRiesgo] [int] NOT NULL,
	[nItem] [int] NOT NULL,
	[cCodCausa] [varchar](6) NOT NULL,
	[bEstado] [bit] NOT NULL,
	[cUltimaActualizacion] [varchar](25) NOT NULL
)
Go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la relacion Causa-Riesgo del registro de los riesgos operacionales' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CausasRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del registro del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CausasRiesgo', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Orden del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CausasRiesgo', @level2type=N'COLUMN',@level2name=N'nItem'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la causa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CausasRiesgo', @level2type=N'COLUMN',@level2name=N'cCodCausa'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CausasRiesgo', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Usuario del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CausasRiesgo', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
----------------------------------------------------------------------------------------------------------------------------------------
go
IF object_id('Constante') is not null BEGIN DROP TABLE Constante END
go
create table Constante
(
	[nConsCod][int] not null,
	[nConsValor][varchar](6),
	[cConsDescripcion][varchar](255) not null,
	[bEstado][bit] not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Registro de las constantes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Constante'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la constante' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Constante', @level2type=N'COLUMN',@level2name=N'nConsCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor de la constante' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Constante', @level2type=N'COLUMN',@level2name=N'nConsValor'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion de la constante' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Constante', @level2type=N'COLUMN',@level2name=N'cConsDescripcion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la constante' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Constante', @level2type=N'COLUMN',@level2name=N'bEstado'
go
IF object_id('RechazoRiesgo') is not null BEGIN DROP TABLE RechazoRiesgo END
go
create table RechazoRiesgo
(
	[nNroRiesgo][int],
	[cMotivoRechazo][varchar](600),
	[bEstado][bit],
	[cUltimaActualizacion][varchar](25)
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Registro de los riesgos rechazados' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RechazoRiesgo'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NroRiesgo de riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RechazoRiesgo', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Motivo del rechazo del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RechazoRiesgo', @level2type=N'COLUMN',@level2name=N'cMotivoRechazo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del rechazo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RechazoRiesgo', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/hora/usuario de la ultima actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RechazoRiesgo', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('LineaNegocio') is not null BEGIN DROP TABLE LineaNegocio END
go
create table LineaNegocio
(
	[cCodLineaNeg][varchar](6) not null,
	[cDescLineaNeg][varchar](500) not null,
	[bEstado][bit] not null,
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Registro de las lineas del negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LineaNegocio'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LineaNegocio', @level2type=N'COLUMN',@level2name=N'cCodLineaNeg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion de la linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LineaNegocio', @level2type=N'COLUMN',@level2name=N'cDescLineaNeg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la linea' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LineaNegocio', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LineaNegocio', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LineaNegocio', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('SubLineaNegocio') is not null BEGIN DROP TABLE SubLineaNegocio END
go
create table SubLineaNegocio
(
	[cCodLineaNeg][varchar](6) not null,
	[cCodSubLineaNeg][varchar](6) not null,
	[cDescSubLineaNeg][varchar](500) not null,
	[bEstado][bit] not null,
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Registro de las sub-lineas del negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubLineaNegocio'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubLineaNegocio', @level2type=N'COLUMN',@level2name=N'cCodLineaNeg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la sub-linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubLineaNegocio', @level2type=N'COLUMN',@level2name=N'cCodSubLineaNeg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion de la sub-linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubLineaNegocio', @level2type=N'COLUMN',@level2name=N'cDescSubLineaNeg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la linea' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubLineaNegocio', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubLineaNegocio', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubLineaNegocio', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('Producto') is not null BEGIN DROP TABLE Producto END
go
create table Producto
(
	[cCodLineaNeg][varchar](6) not null,
	[cCodProducto][varchar](6) not null,
	[cDescProducto][varchar](500) not null,
	[bEstado][bit] not null,
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Registro de los productos asociados a la linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Producto'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Producto', @level2type=N'COLUMN',@level2name=N'cCodLineaNeg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Producto', @level2type=N'COLUMN',@level2name=N'cCodProducto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Producto', @level2type=N'COLUMN',@level2name=N'cDescProducto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la linea' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Producto', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Producto', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Producto', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('SubProducto') is not null BEGIN DROP TABLE SubProducto END
go
create table SubProducto
(
	[cCodProducto][varchar](6) not null,
	[cCodSubProducto][varchar](6) not null,
	[cDescSubProducto][varchar](500) not null,
	[bEstado][bit] not null,
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Registro de los productos asociados a la linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProducto'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProducto', @level2type=N'COLUMN',@level2name=N'cCodProducto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del subproducto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProducto', @level2type=N'COLUMN',@level2name=N'cCodSubProducto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del subproducto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProducto', @level2type=N'COLUMN',@level2name=N'cDescSubProducto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la linea' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProducto', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProducto', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubProducto', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('ProcesoRiesgo') is not null BEGIN DROP TABLE ProcesoRiesgo END
go
create table ProcesoRiesgo
(
[nNroRiesgo][int] not null,
--Paso 1
[nFactorRiesgo][int],
[nEventPerdida][int],
[nSubEventoPerdida][int],
[cLineaNeg][varchar](6),
[cProducto][varchar](6),
[cSubProdcto][varchar](6),
--Paso 2
[nProbabilidad][int],
[nImpacto][int],
--[nNivRiesInhe][int],
--[nPosMontPerd][int],
--Paso 3
[bContRiesResidual][bit] default 0,
[cComentRiesgoResidual][varchar](500),
--Paso 4
[bContPlanAccion][bit] default 0,
--[cComentPlanAccion][varchar](500),
--Paso 5
[bContResponPlanAcc][bit] default 0,

[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda los registros de la gestion del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor del factor del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'nFactorRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'nEventPerdida'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor del sub evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'nSubEventoPerdida'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor de la linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'cLineaNeg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor del producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'cProducto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor del sub producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'cSubProdcto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor de la probabilidad' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'nProbabilidad'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor del impacto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'nImpacto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Muestra si se configuro informacion de los controles depara el riesgo residual' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'bContRiesResidual'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentario para el proceso paso 3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'cComentRiesgoResidual'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Muestra se se configuro los planes de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'bContPlanAccion'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentario para el proceso paso 4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'cComentPlanAccion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Muestra se se configuro los responsables de los planes de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'bContResponPlanAcc'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcesoRiesgo', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('ConstSistema') is not null BEGIN DROP TABLE ConstSistema END
create table ConstSistema
(
[nConsSisCod][int],
[cConsSisDesc][varchar](150),
[cConsSisValor][varchar](500),
[cUltimaActualizacion][varchar](25)
)
go
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda las constantes del sistema' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConstSistema'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la constante' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConstSistema', @level2type=N'COLUMN',@level2name=N'nConsSisCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion de la constante' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConstSistema', @level2type=N'COLUMN',@level2name=N'cConsSisDesc'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor de la constante' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConstSistema', @level2type=N'COLUMN',@level2name=N'cConsSisValor'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Usuario de la actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConstSistema', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
-----------------------------------------------------------------------------------
go
IF object_id('CriterioEvalucion') is not null BEGIN DROP TABLE CriterioEvalucion END
go
create table CriterioEvalucion
(
	[nCriterioCod][int] not null,
	[nCriterioValor][int] not null,
	[cCriterioDesc][varchar](300) not null,
	[bEstado][bit] not null,
	[cFechaReg] [varchar](25) not null, 
	[cUltimaActualizacion][varchar](25) not null,
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla para la configuracion de los valores para la evaluacion de los riesgos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CriterioEvalucion'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del criterio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CriterioEvalucion', @level2type=N'COLUMN',@level2name=N'nCriterioCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor del criterio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CriterioEvalucion', @level2type=N'COLUMN',@level2name=N'nCriterioValor'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del criterio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CriterioEvalucion', @level2type=N'COLUMN',@level2name=N'cCriterioDesc'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado actual' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CriterioEvalucion', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'echa/Hora/Agecia del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CriterioEvalucion', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Agecia de la actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CriterioEvalucion', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go

IF object_id('ControlRiesgoRisidual') is not null BEGIN DROP TABLE ControlRiesgoRisidual END
create table ControlRiesgoRisidual
(
	[nNroRiesgo][int] not null,
	[nNroItem][int] not null,
	--[cCodControlArea][varchar](6) not null,
	[cComentario][varchar](1200) not null,
	[nResponControl] [int],
	[nPeriEjec][int],
	[nEvidControl][int],
	[nEjecControl][int],
	[nCumpliObjetivo][int],
	[nEfectControl][int],
	--[nCalifControl][int] not null,
	[bEstado][bit] not null,
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla para la configuracion de los valores para la evaluacion de los riesgos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Item del control agregado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'nNroItem'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Control del Area' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'cCodControlArea'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'cComentario'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[RC] Responsable del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'nResponControl'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[FD] Periodo de Ejecucion del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'nPeriEjec'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[EC] Evidencia del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'nEvidControl'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[TC] Ejecucion del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'nEjecControl'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[CO] Cumplimiento del objetivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'nCumpliObjetivo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Efectividad del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'nEfectControl'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Calificacion del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'nCalifControl'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del control' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Agecia del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Agecia de la actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ControlRiesgoRisidual', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('EscalaNivelRiesgo') is not null BEGIN DROP TABLE EscalaNivelRiesgo END
create table EscalaNivelRiesgo
(
	[nEscala][int]identity(1,1) not null,
	[nProbabilidad][int] not null,
	[nImpacto][int] not null,
	[nValorEscala][int] not null,
	[bEstado][bit] not null,
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla para la configuracion de la escala para los niveles de riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EscalaNivelRiesgo'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Probabilidad' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EscalaNivelRiesgo', @level2type=N'COLUMN',@level2name=N'nProbabilidad'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Impacto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EscalaNivelRiesgo', @level2type=N'COLUMN',@level2name=N'nImpacto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EscalaNivelRiesgo', @level2type=N'COLUMN',@level2name=N'nValorEscala'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EscalaNivelRiesgo', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de resgistro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EscalaNivelRiesgo', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EscalaNivelRiesgo', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
DBCC CHECKIDENT ('EscalaNivelRiesgo',RESEED, 1000)
go
IF object_id('RiesgoInherente') is not null BEGIN DROP TABLE RiesgoInherente END
create table RiesgoInherente
(
	[nNroRiesgo][int] not null,
	[nProbabilidad][int] not null,
	[nImpacto][int] not null,
	[nValorEscala][int] not null,
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda el nivel de riesgo inherente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoInherente'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de Riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoInherente', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Probabilidad' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoInherente', @level2type=N'COLUMN',@level2name=N'nProbabilidad'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Impacto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoInherente', @level2type=N'COLUMN',@level2name=N'nImpacto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoInherente', @level2type=N'COLUMN',@level2name=N'nValorEscala'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de resgistro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoInherente', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoInherente', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('RiesgoResidual') is not null BEGIN DROP TABLE RiesgoResidual END
create table RiesgoResidual
(
	[nNroRiesgo][int] not null,
	[nProbabilidad][int] not null,
	[nImpacto][int] not null,
	[nValorEscala][int] not null,
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda el nivel de riesgo inherente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoResidual'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de Riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoResidual', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Probabilidad' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoResidual', @level2type=N'COLUMN',@level2name=N'nProbabilidad'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Impacto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoResidual', @level2type=N'COLUMN',@level2name=N'nImpacto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoResidual', @level2type=N'COLUMN',@level2name=N'nValorEscala'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de resgistro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoResidual', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RiesgoResidual', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('MontoPerdida') is not null BEGIN DROP TABLE MontoPerdida END
create table MontoPerdida
(
	[nMonPerdCod][int]identity(1,1) not null,
	[nProbabilidad][int] not null,
	[nImpacto][int] not null,
	[nMontoPerdida][money] not null,
	[cComentario][varchar](500),
	[bEstado][bit] not null,
	[cFechaReg][varchar](25) not null,
	[cFechaCese][varchar](25),
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la configuracion de los monto de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MontoPerdida'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de Riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MontoPerdida', @level2type=N'COLUMN',@level2name=N'nMonPerdCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Probabilidad' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MontoPerdida', @level2type=N'COLUMN',@level2name=N'nProbabilidad'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Impacto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MontoPerdida', @level2type=N'COLUMN',@level2name=N'nImpacto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor del monto de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MontoPerdida', @level2type=N'COLUMN',@level2name=N'nMontoPerdida'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MontoPerdida', @level2type=N'COLUMN',@level2name=N'cComentario'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MontoPerdida', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de resgistro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MontoPerdida', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de cese' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MontoPerdida', @level2type=N'COLUMN',@level2name=N'cFechaCese'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MontoPerdida', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
DBCC CHECKIDENT ('MontoPerdida',RESEED, 1000)
go
IF object_id('PlanAccion') is not null BEGIN DROP TABLE PlanAccion END
create table PlanAccion
(
	[nPlanCod][int]identity(1,1) not null,
	[nNroRiesgo][int] not null,
	[cPlanDescripcion][varchar](500) not null,
	--[dFechaImplement][date] not null,
	[bSugeGerenManc][bit] not null,
	[nEstado][int],
	[cComentario][nvarchar](max),
	[cNombreDocAdj][varchar](500),
	[cNombreDocBD][varchar](500),
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la configuracion de los monto de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigop del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion', @level2type=N'COLUMN',@level2name=N'nPlanCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de Riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion', @level2type=N'COLUMN',@level2name=N'cPlanDescripcion'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de Implementacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion', @level2type=N'COLUMN',@level2name=N'dFechaImplement'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si el plan de accion fue sugerido por la gerencia mancomunada' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion', @level2type=N'COLUMN',@level2name=N'bSugeGerenManc'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion', @level2type=N'COLUMN',@level2name=N'nEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentario del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion', @level2type=N'COLUMN',@level2name=N'cComentario'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del documento adjunto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion', @level2type=N'COLUMN',@level2name=N'cNombreDocAdj'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del documento adjunto en la base de datos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion', @level2type=N'COLUMN',@level2name=N'cNombreDocBD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de resgistro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PlanAccion', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
DBCC CHECKIDENT ('PlanAccion',RESEED, 1000)
go
IF OBJECT_ID('ResponsablePlanAccion') IS NOT NULL BEGIN DROP TABLE ResponsablePlanAccion END
CREATE TABLE ResponsablePlanAccion
(
	[nPlanCod][int] NOT NULL,
	[nItem][int] NOT NULL,
	[cUser][varchar](4) NOT NULL,
	[bEstado][bit] NOT NULL,
	[bConfirma] [bit] NULL,
	[cFechaReg][varchar](25) NOT NULL,
	[cUltimaActualizacion][varchar](25) NOT NULL
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la configuracion de los monto de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ResponsablePlanAccion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ResponsablePlanAccion', @level2type=N'COLUMN',@level2name=N'nPlanCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Item del responsable' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ResponsablePlanAccion', @level2type=N'COLUMN',@level2name=N'nItem'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario del responsable' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ResponsablePlanAccion', @level2type=N'COLUMN',@level2name=N'cUser'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del responsable' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ResponsablePlanAccion', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de resgistro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ResponsablePlanAccion', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ResponsablePlanAccion', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
GO
IF object_id('ReasignacionResponsablePlanAccion') is not null BEGIN DROP TABLE ReasignacionResponsablePlanAccion END
create table ReasignacionResponsablePlanAccion
(
	[nPlanCod][int] not null,
	[cUserRespon][varchar](4) not null,
	[cActual][varchar](1) not null,
	[cVigencia][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la configuracion de los monto de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReasignacionResponsablePlanAccion'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReasignacionResponsablePlanAccion', @level2type=N'COLUMN',@level2name=N'nPlanCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario del responsable' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReasignacionResponsablePlanAccion', @level2type=N'COLUMN',@level2name=N'cUserRespon'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Marca del usuario actual' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReasignacionResponsablePlanAccion', @level2type=N'COLUMN',@level2name=N'cActual'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha, usuario que realizo el cambio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReasignacionResponsablePlanAccion', @level2type=N'COLUMN',@level2name=N'cVigencia'
go
IF object_id('Incentivos') is not null BEGIN DROP TABLE Incentivos END
go
create table Incentivos
(
	[nNroRiesgo][int] not null,
	[nCodMotivo][int] not null,
	[cComentario][varchar](500),
	[nMonto][money] not null,
	[cDocAjunto][varchar](500),
	[cDocAjuntoBD][varchar](500),
	[nEstado][int] not null,
	[cFechaGestion][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la configuracion de los monto de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Incentivos'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Incentivos', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del motivo del incentivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Incentivos', @level2type=N'COLUMN',@level2name=N'nCodMotivo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentarios y/o observaciones sobre el incentivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Incentivos', @level2type=N'COLUMN',@level2name=N'cComentario'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto del incentivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Incentivos', @level2type=N'COLUMN',@level2name=N'nMonto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del doc adjunto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Incentivos', @level2type=N'COLUMN',@level2name=N'cDocAjunto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del doc adjunto en la BD' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Incentivos', @level2type=N'COLUMN',@level2name=N'cDocAjuntoBD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del incentivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Incentivos', @level2type=N'COLUMN',@level2name=N'nEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de la primera gestion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Incentivos', @level2type=N'COLUMN',@level2name=N'cFechaGestion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de las modificaciones' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Incentivos', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('ConfigIncentivos') is not null BEGIN DROP TABLE ConfigIncentivos END
go
create table ConfigIncentivos
(
	[nCodConfigInc][int] identity (1, 1),
	[nProbabilidad][int] not null,
	[nImpacto][int] not null,
	[nMontoIncentivo][money] not null,
	[bEstado][bit] not null,
	[cFechaRegistro][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la configuracion de los monto de incentivos por probabilidad y impacto del nivel de riesgo inherente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigIncentivos'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la configuracion del incentivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigIncentivos', @level2type=N'COLUMN',@level2name=N'nCodConfigInc'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Probabilidad inherente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigIncentivos', @level2type=N'COLUMN',@level2name=N'nProbabilidad'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Impacto inherente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigIncentivos', @level2type=N'COLUMN',@level2name=N'nImpacto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto del incentivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigIncentivos', @level2type=N'COLUMN',@level2name=N'nMontoIncentivo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la configuracion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigIncentivos', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigIncentivos', @level2type=N'COLUMN',@level2name=N'cFechaRegistro'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de las modificaciones' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigIncentivos', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
DBCC CHECKIDENT ('ConfigIncentivos',RESEED, 1000)
go
IF object_id('RangoIncentivo') is not null BEGIN DROP TABLE RangoIncentivo END
go
create table RangoIncentivo
(
	[nNroRiesgo][int] not null,
	[nCodMotivo][int] not null,
	[cComentario][varchar](500),
	[nMonto][money] not null,
	[cDocAjunto][varchar](500),
	[cDocAjuntoBD][varchar](500),
	[nEstado][int] not null,
	[cFechaGestion][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la configuracion de los monto de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RangoIncentivo'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RangoIncentivo', @level2type=N'COLUMN',@level2name=N'RangoIncentivos'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del motivo del incentivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RangoIncentivo', @level2type=N'COLUMN',@level2name=N'RangoIncentivos'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentarios y/o observaciones sobre el incentivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RangoIncentivo', @level2type=N'COLUMN',@level2name=N'RangoIncentivos'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto del incentivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RangoIncentivo', @level2type=N'COLUMN',@level2name=N'nMonto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del doc adjunto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RangoIncentivo', @level2type=N'COLUMN',@level2name=N'cDocAjunto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del doc adjunto en la BD' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RangoIncentivo', @level2type=N'COLUMN',@level2name=N'cDocAjuntoBD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del incentivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RangoIncentivo', @level2type=N'COLUMN',@level2name=N'nEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de la primera gestion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RangoIncentivo', @level2type=N'COLUMN',@level2name=N'cFechaGestion'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de las modificaciones' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RangoIncentivo', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'

/************************************************* Tabla de las autoevaluaciones ************************************************************/
IF object_id('Evaluacion') is not null BEGIN DROP TABLE Evaluacion END
go
create table Evaluacion
(
	[nCodEval][int] identity (1, 1) not null,
	[cEvalDesc][varchar](150) not null,
	[bEstado][bit],
	[cFechaReg][varchar](25) not null,
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la configuracion de los monto de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Evaluacion'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la evaluacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Evaluacion', @level2type=N'COLUMN',@level2name=N'nCodEval'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre/Descripcion de la evaluacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Evaluacion', @level2type=N'COLUMN',@level2name=N'cEvalDesc'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la evaluacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Evaluacion', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de la primera gestion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Evaluacion', @level2type=N'COLUMN',@level2name=N'cFechaReg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de las modificaciones' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Evaluacion', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
--DBCC CHECKIDENT ('Evaluacion',RESEED, 1000)
--go
--IF object_id('EvaluacionDet') is not null BEGIN DROP TABLE EvaluacionDet END
--go
--create table EvaluacionDet
--(
--	[nNroRiesgo][int]not null,
--	[nCodEval][int]not null,
--	[cCodProceso][varchar](10)
--)
--go
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda la informacion del tipo de evaluacion del riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EvaluacionDet'
--go
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EvaluacionDet', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de evaluacion registrado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EvaluacionDet', @level2type=N'COLUMN',@level2name=N'nCodEval'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del proceso del area' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EvaluacionDet', @level2type=N'COLUMN',@level2name=N'cCodProceso'
--go
IF object_id('ImplementaPlanAccion') is not null BEGIN DROP TABLE ImplementaPlanAccion END
go
create table ImplementaPlanAccion
(
	[nPlanCod][int] not null,
	[dFechaImplementa][date] not null,
	[nEstado][int],
	[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda las fechas de implementacion de los planes de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImplementaPlanAccion'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImplementaPlanAccion', @level2type=N'COLUMN',@level2name=N'nPlanCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de implementacion del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImplementaPlanAccion', @level2type=N'COLUMN',@level2name=N'dFechaImplementa'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la implementacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImplementaPlanAccion', @level2type=N'COLUMN',@level2name=N'nEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de las modificaciones' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImplementaPlanAccion', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
--IF object_id('EventoPerdida') is not null BEGIN DROP TABLE EventoPerdida END
--go
--create table EventoPerdida
--(
--    [nCodEvento][int] identity(1, 1)not null,
--    [cDescEvento][varchar](200) not null,
--    [bEstado][bit] not null
--	--[nCodContingencia][int] not null
--)
--go
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda de los tipos de evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida'
--go
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nCodEvento'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'cDescEvento'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'bEstado'
--go
--DBCC CHECKIDENT ('EventoPerdida',RESEED, 1000)
--go
--IF object_id('SubEventoPerdida') is not null BEGIN DROP TABLE SubEventoPerdida END
--go
--create table SubEventoPerdida
--(
--    [nCodSubEvento][int] identity(1, 1)not null,
--    [nCodEvento][int] not null,
--    [cDescSubEvento][varchar](200) not null,
--    [bEstado][bit] not null
--	--[nCodContingencia][int] not null
--)
--go
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda de los tipos de evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubEventoPerdida'
--go
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del sub evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubEventoPerdida', @level2type=N'COLUMN',@level2name=N'nCodSubEvento'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubEventoPerdida', @level2type=N'COLUMN',@level2name=N'nCodEvento'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del sub evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubEventoPerdida', @level2type=N'COLUMN',@level2name=N'cDescSubEvento'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del sub evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubEventoPerdida', @level2type=N'COLUMN',@level2name=N'bEstado'
----go
----DBCC CHECKIDENT ('SubEventoPerdida',RESEED, 1000)
--go
IF object_id('ComentariosPlanesAccion') is not null BEGIN DROP TABLE ComentariosPlanesAccion END
go
create table ComentariosPlanesAccion
(
	[nPlanCod][int] not null,
	[nItem][int] not null,
    [cUser][varchar](4) not null,
	[cComentario][varchar](250) not null,
    [dFecha][datetime] not null --,
	--[cUltimaActualizacion][varchar](25) not null
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda las fechas de implementacion de los planes de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ComentariosPlanesAccion'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ComentariosPlanesAccion', @level2type=N'COLUMN',@level2name=N'nPlanCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero correlativo del comentario del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ComentariosPlanesAccion', @level2type=N'COLUMN',@level2name=N'nItem'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realizo el comentario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ComentariosPlanesAccion', @level2type=N'COLUMN',@level2name=N'cUser'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentario del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ComentariosPlanesAccion', @level2type=N'COLUMN',@level2name=N'cComentario'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y Hora de los ingreso el comentario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ComentariosPlanesAccion', @level2type=N'COLUMN',@level2name=N'dFecha'
go
IF OBJECT_ID('ClaseEventoPerdida') IS NOT NULL BEGIN  DROP TABLE ClaseEventoPerdida END
go
CREATE TABLE ClaseEventoPerdida
([nCodClasEventoP]  [INT] IDENTITY(1, 1), 
 [cDescClasEventoP] [VARCHAR](250), 
 [bEstado]          [BIT]
);
GO
DBCC CHECKIDENT ('ClaseEventoPerdida',RESEED, 101)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda las clase primaria de los evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClaseEventoPerdida'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la clase del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClaseEventoPerdida', @level2type=N'COLUMN',@level2name=N'nCodClasEventoP'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion de la clase delevento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClaseEventoPerdida', @level2type=N'COLUMN',@level2name=N'cDescClasEventoP'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la clase del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClaseEventoPerdida', @level2type=N'COLUMN',@level2name=N'bEstado'
go
IF OBJECT_ID('SubClaseEventoPerdida') IS NOT NULL BEGIN  DROP TABLE SubClaseEventoPerdida END
go
CREATE TABLE SubClaseEventoPerdida
([nCodSubClasEventoP]  [INT] IDENTITY(1, 1), 
 [nCodClasEventoP]     [INT], 
 [cDescSubClasEventoP] [VARCHAR](250), 
 [bEstado]             [BIT]
);
GO
DBCC CHECKIDENT ('SubClaseEventoPerdida',RESEED, 100)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda las clases secundarias de los evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubClaseEventoPerdida'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la clase del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubClaseEventoPerdida', @level2type=N'COLUMN',@level2name=N'nCodClasEventoP'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la sub clase de eveto de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubClaseEventoPerdida', @level2type=N'COLUMN',@level2name=N'nCodSubClasEventoP'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion de la sub clase del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubClaseEventoPerdida', @level2type=N'COLUMN',@level2name=N'cDescSubClasEventoP'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de la sub clase del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubClaseEventoPerdida', @level2type=N'COLUMN',@level2name=N'bEstado'
go
IF OBJECT_ID('EventoPerdida') IS NOT NULL BEGIN  DROP TABLE EventoPerdida END
GO
CREATE TABLE EventoPerdida
([nNroRiesgo]           [INT] NOT NULL, 
 [cDescMedidas]		    [VARCHAR](5000),
 [cDescAcciones]	    [VARCHAR](5000),
 [nCodDescCorta]		[INT] NOT NULL,
 [nClasEvento]			[INT] NOT NULL,
 [nSubClasEvento]		[INT] NOT NULL,
 [cAgeCod]              [VARCHAR](2), 
 [cAreaCod]             [VARCHAR](3), 
 [cLineaNeg]            [VARCHAR](15), 
 [cSubLineaNeg]         [VARCHAR](15), 
 [nCobertura]           [INT], 
 [cProcesos]            [VARCHAR](15), 
 [nSubProceso]          [INT], 
 [bReportado]			[BIT],
 [cAnio]                [VARCHAR](4), 
 [dFechaRegContable]    [DATE], 
 [dFechaOcurrencia]     [DATE], 
 [dFechaDescubrimiento] [DATE], 
 [nPenMontoPerdida]     [CHAR](1), 
 [nMontoPerdida]        [MONEY], 
 [nPenMontoRecup]       [CHAR](1), 
 [nMontoRecup]          [MONEY], 
 [nMontoBruto]          [MONEY], 
 [nPenProvision]        [CHAR](1), 
 [nMontoProvision]      [MONEY], 
 [nPerdidaNeta]         [MONEY], 
 [cCodCtaCont]          [VARCHAR](1500) NOT NULL, 
 [bAsocRiesgo]          [BIT],
 [cUltimaActualizacion] [VARCHAR](25)
);
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda informacion de los evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de registro del evento de perida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion de las medidas correctivas' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'cDescMedidas'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion de las acciones realizadas' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'cDescAcciones'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion corta del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nCodDescCorta'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Clase de Evento de Perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nClasEvento'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub clase del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nSubClasEvento'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Agencia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'cAgeCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Area' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'cAreaCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'cLineaNeg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'cSubLineaNeg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cobertura' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nCobertura'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Proceso del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'cProcesos'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub Proceso del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nSubProceso'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si el evento de perdida fue reportado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'bReportado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Anio del evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'cAnio'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de registro contable' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'dFechaRegContable'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de ocurrencia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'dFechaOcurrencia'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de descubrimiento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'dFechaDescubrimiento'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Moneda del monto de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nPenMontoPerdida'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto de Perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nMontoPerdida'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Moneda del monto recuperado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nPenMontoRecup'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto recuperado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nMontoRecup'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto Bruto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nMontoBruto'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Moneda de la provision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nPenProvision'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto de la provision para el evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nMontoProvision'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto de perdida neta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'nPerdidaNeta'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cuenta contable' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'cCodCtaCont'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Asociado al riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'bAsocRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha, hora, usuario que realizado alguna actualizacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EventoPerdida', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('CuentaEventoPerdida') is not null BEGIN DROP TABLE CuentaEventoPerdida END
go
CREATE TABLE CuentaEventoPerdida
([nNroRiesgo] [INT], 
 [nItem]      [INT], 
 [cCtaCod]    [VARCHAR](1500), 
 [bEstado]    [BIT]
);
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda informacion de los evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CuentaEventoPerdida'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de registro del evento de perida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CuentaEventoPerdida', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Orden de registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CuentaEventoPerdida', @level2type=N'COLUMN',@level2name=N'nItem'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de la cuenta contable' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CuentaEventoPerdida', @level2type=N'COLUMN',@level2name=N'cCtaCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CuentaEventoPerdida', @level2type=N'COLUMN',@level2name=N'bEstado'
go
IF object_id('GastosEventoPerdida') is not null BEGIN DROP TABLE GastosEventoPerdida END
go
CREATE TABLE GastosEventoPerdida
([nNroRiesgo] [INT], 
 [nItem]      [INT], 
 [cGlosa]     [VARCHAR](1500), 
 [nMoneda]    [INT], 
 [nMonto]     [MONEY]
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda los gastos generados por el evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GastosEventoPerdida'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero del riesgo (evento perdida)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GastosEventoPerdida', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Correlativo del gasto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GastosEventoPerdida', @level2type=N'COLUMN',@level2name=N'nItem'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Glosa del gasto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GastosEventoPerdida', @level2type=N'COLUMN',@level2name=N'cGlosa'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Moneda del gasto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GastosEventoPerdida', @level2type=N'COLUMN',@level2name=N'nMoneda'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto del gasto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GastosEventoPerdida', @level2type=N'COLUMN',@level2name=N'nMonto'
go
IF object_id('GestionEventoPerdida') is not null BEGIN DROP TABLE GestionEventoPerdida END
go
create table GestionEventoPerdida
(
	[nNroRiesgo][int] not null,
	[cAgeCod][varchar](2),
    [cAreaCod][varchar](3),
	[cLineaNeg][varchar](15),
    [cSubLineaNeg][varchar](15),
    [cProcesos][varchar](15),
    [nSubProceso][int],
    [nSubEventoPerdida][int],
    [bRiesgoCred][bit],
    [bExpEventPerdida][bit],
    [cMedidasCorrectivas][varchar](1500),
    [cAccionRealizada][varchar](1500),
    [cUltimaActualizacion][varchar](25)
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda las fechas de implementacion de los planes de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de riesgo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de agencia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'cAgeCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de area' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'cAreaCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'cLineaNeg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub linea de negocio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'cSubLineaNeg'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procesos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'cProcesos'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub proceso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'nSubProceso'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub evento de perdida - descripcion corta del evento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'nSubEventoPerdida'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Riesgo Crediticio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'bRiesgoCred'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Expuesto a evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'bExpEventPerdida'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Medidas correctivas' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'cMedidasCorrectivas'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Acciones realizadas' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'cAccionRealizada'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y Hora de de la gestion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GestionEventoPerdida', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('HistorialEstadoPlanAccion') is not null BEGIN DROP TABLE HistorialEstadoPlanAccion END
go
CREATE TABLE HistorialEstadoPlanAccion
([nPlanCod]            [INT], 
 [nEstado]             [INT], 
 [cUltimaActulizacion] [VARCHAR](25)
);
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda el historial de los planes de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HistorialEstadoPlanAccion'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HistorialEstadoPlanAccion', @level2type=N'COLUMN',@level2name=N'nPlanCod'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del plan de accion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HistorialEstadoPlanAccion', @level2type=N'COLUMN',@level2name=N'nEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Usuario que actualiza' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HistorialEstadoPlanAccion', @level2type=N'COLUMN',@level2name=N'cUltimaActulizacion'
go
go
IF object_id('GruposEventoPerdida') is not null BEGIN DROP TABLE GruposEventoPerdida END
go
CREATE TABLE GruposEventoPerdida
([nNroRiesgo]           [INT] NOT NULL, 
 [nAgrupado]            [INT] NOT NULL, 
 [nItem]                [INT] NOT NULL, 
 [bEstado]              [BIT] NOT NULL, 
 [cUltimaActualizacion] [VARCHAR](25) NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guarda las agrupaciones de los evento de perdida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GruposEventoPerdida'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del riesgo a agrupar' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GruposEventoPerdida', @level2type=N'COLUMN',@level2name=N'nNroRiesgo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del riesgo agrupado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GruposEventoPerdida', @level2type=N'COLUMN',@level2name=N'nAgrupado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Correlativo del riesgo dentro de la agrupacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GruposEventoPerdida', @level2type=N'COLUMN',@level2name=N'nItem'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del evento dentro de la agrupacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GruposEventoPerdida', @level2type=N'COLUMN',@level2name=N'bEstado'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha/Hora/Usuario que actualiza' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GruposEventoPerdida', @level2type=N'COLUMN',@level2name=N'cUltimaActualizacion'
go
IF object_id('ErrorAplicacion') is not null BEGIN DROP TABLE ErrorAplicacion END
go
create table ErrorAplicacion
([nIdError] [INT] IDENTITY(1,1),
[cMetodo] [VARCHAR](500),
[cErroDesc] [VARCHAR](MAX),
[dFecha] [DATETIME]
)
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Registrar los errores internos de la aplicacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorAplicacion'
go
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador unico del error' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorAplicacion', @level2type=N'COLUMN',@level2name=N'nIdError'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del metodo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorAplicacion', @level2type=N'COLUMN',@level2name=N'cMetodo'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del Error' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorAplicacion', @level2type=N'COLUMN',@level2name=N'cErroDesc'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha del acontetecimiento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ErrorAplicacion', @level2type=N'COLUMN',@level2name=N'dFecha'
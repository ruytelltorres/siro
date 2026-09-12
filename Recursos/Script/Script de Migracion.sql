use [DBSiro]
--go
--delete from Riesgo
--DBCC CHECKIDENT ('Riesgo',RESEED, 100000)
go
--Para migracion 
declare @TpoRiesgos int = 1 --(1) Riesgos operacionales, (2) AutoEvaluaciones[Evaluaciones], (3) Eventos de Perdida -> ver constante 5000
declare @dFechaMigra datetime = getdate()

--[Insersion en la tabla maestra]
insert into Riesgo(cNroRiesgo, cDescRiesgo, nEstadoRiesgo, nFlagRiesgo)
select dbo.GenerarcNroRiesgo(dFechaHoraRegistro, AG.cAgeCod, UPPER(ro.cUser) ),
('Registro Riesgo Operacional N° ' + cRiesgoCod /*SUBSTRING(cRiesgoCod, 4,LEN(cRiesgoCod))*/),
0,
0
--select * 
from [SRVSQLDB03].[DBSro].[dbo].[SRO_RiesgoOperacional] ro 
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].RRHH R  on ro.cUser = R.cUser
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].Persona P on P.cPersCod = R.cPersCod
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].RHCargos RC on R.cPersCod = RC.cPersCod 
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].RHCargosTabla RT on RT.cRHCargoCod = RC.cRHCargoCod
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].Areas A On A.cAreaCod = R.cAreaCodActual
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].Agencias AG On AG.cAgeCod = R.cAgenciaActual
where dRHCargoFecha = (select max(dRHCargoFecha) from DBCmacMaynas..RHCargos where cPersCod = R.cPersCod) 
and not left(ro.cRiesgoCod, 2) = 'EP'


--[Insercion a la tabla detalle]
insert into DetalleRiesgo(nNroRiesgo, cCodRiesgo, nTpoRiesgo, cRiesgoIdentificado, nProcRiesgo, dFechaDetec, cObservacion, cAgeCod, cAreaCod, cCodProceso, nCodSubProceso, bControlEfect, cUltimaActualizacion)
select 
--*
distinct m.nNroRiesgo, ro.cRiesgoCod /*CONCAT('RO', '-', FORMAT(CONVERT(int,SUBSTRING(m.cDescRiesgo, 34,LEN(m.cDescRiesgo))), '1000000'))*/, 1, ro.cRiesgoIdentificado, 
case 
when ro.nEstado = 1 then 0 
when ro.nEstado = 2 then 0 --1
when ro.nEstado = 3 then 2 
when ro.nEstado = 4 then 3
when ro.nEstado = 5 then 4
when ro.nEstado = 6 then 8
when ro.nEstado = 7 then 7
when ro.nEstado = 8 then 6
when ro.nEstado = 9 then 5
when ro.nEstado = 10 then null end, 
ro.dFecha,
ro.cObservaciones, 
ro.cAgeCod,
--case ro.cAgeCod 
--when '0X' then '01'
--end,
case ro.cAreaCod 
when 101 then '00X'
when 102 then '044'
when 103 then '021'
when 104 then '00X'
when 106 then '041'
when 107 then '023'
when 108 then '042'
when 109 then '025'
when 111 then '070'
when 112 then '051'
when 114 then '074'
when 115 then '026'
when 116 then '074'
when 117 then '022'
when 118 then '060'
when 119 then '045'
when 120 then '040'
when 121 then '050'
when 122 then '064'
when 123 then '071'
when 124 then '003'
when 125 then '002'
when 126 then '056'
when 127 then '057'
when 128 then '061' end
, ro.nProcesoID, ro.nSubProcesoID, ro.nControlEfectivo,
dbo.GenerarcNroRiesgo(dFechaHoraRegistro, AG.cAgeCod, UPPER(ro.cUser))
from [SRVSQLDB03].[DBSro].[dbo].[SRO_RiesgoOperacional] ro 
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].RRHH R  on ro.cUser = R.cUser
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].Persona P on P.cPersCod = R.cPersCod
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].RHCargos RC on R.cPersCod = RC.cPersCod 
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].RHCargosTabla RT on RT.cRHCargoCod = RC.cRHCargoCod
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].Areas A On A.cAreaCod = R.cAreaCodActual
inner join [SRVSQLDB03].[DBCmacMaynas].[dbo].Agencias AG On AG.cAgeCod = R.cAgenciaActual
--inner join Riesgo m on SUBSTRING(ro.cRiesgoCod, 4,LEN(ro.cRiesgoCod)) = SUBSTRING(m.cDescRiesgo, 32,LEN(m.cDescRiesgo)) and not left(ro.cRiesgoCod, 2) = 'EP'
inner join Riesgo m on ro.cRiesgoCod  = SUBSTRING(m.cDescRiesgo, 32,LEN(m.cDescRiesgo)) and not left(ro.cRiesgoCod, 2) = 'EP'
where dRHCargoFecha = (select max(dRHCargoFecha) from DBCmacMaynas..RHCargos where cPersCod = R.cPersCod ) 

--go
--[Actualizamos el estado del riesgo de acuerdo al proceso en el que se encuentra]
--select ROW_NUMBER()over(order by r.nNroRiesgo) nItem, r.nNroRiesgo, dr.nProcRiesgo, dr.cCodRiesgo
--into #ActualizarEstadoRiesgo 
--from Riesgo r
--inner join DetalleRiesgo dr on r.nNroRiesgo = dr.nNroRiesgo
--where dr.nTpoRiesgo = 1

--declare @xItem int = 0, @xNroRiesgo int = 0, @xProceso int = 0, @xExito int = 0, @xCodRiesgo varchar(50) = ''
--while(select COUNT(*) from #ActualizarEstadoRiesgo) > 0 begin 
--set @xItem = @xItem + 1
--set @xNroRiesgo = (select nNroRiesgo from #ActualizarEstadoRiesgo where nItem = @xItem)
--set @xProceso = (select nProcRiesgo from #ActualizarEstadoRiesgo where nItem = @xItem)
--set @xCodRiesgo = (select cCodRiesgo from #ActualizarEstadoRiesgo where nItem = @xItem)

--if @xProceso = 0 begin --Cuando esta en proceso 0, podria estar en estado de modificacion
--	insert into ObservacionGestion (nNroRiesgo, cDescObservacion, bEstado, cUltimaActualizacion)
--	select @xNroRiesgo, cComentario, 0, dbo.GenerarcNroRiesgo(CONVERT(datetime, dFecha), '01', cUserAnalista) 
--	from [SRVSQLDB03].[DBSro].[dbo].[SRO_RiesgoModificado] where cRiesgoCod = @xCodRiesgo
--	update Riesgo set nEstadoRiesgo = 6 where nNroRiesgo = @xNroRiesgo
--end else if @xProceso = 1 begin
--	--select * from Riesgo
--end
--delete from #ActualizarEstadoRiesgo where nItem = @xItem
--if(select COUNT(*) from #ActualizarEstadoRiesgo) = 0 begin break end else begin continue end 
--end

--select * from ConstSistema where nConsSisCod = 50
--select * from ObservacionGestion


--go 
--[Actulizamos los procesos y subprocesos a la nueva version del SIRO]
/*Nota: La tabla proceso y sub procesos se cargaron de forma manual, tener cuidado al borrar los datos de la tabla*/
select ROW_NUMBER() over (order by r.nNroRiesgo asc) nCorrelativo,r.nNroRiesgo, dr.cCodProceso, dr.nCodSubProceso 
into #RiesgoUpdate
from DetalleRiesgo dr
inner join Riesgo r on dr.nNroRiesgo = r.nNroRiesgo
where dr.nTpoRiesgo = 1 and r.nFlagRiesgo = 0

select drx.nNroRiesgo, spa.cCodProceso, spa.nCodSubProceso
into #ProcesosSubProcesos
from [SRVSQLDB03].[DBSro].[dbo].[SRO_SubProceso] spx
inner join DetalleRiesgo drx on spx.nProcesoID = drx.cCodProceso and spx.nSubProcesoID = drx.nCodSubProceso 
inner join SubProcesoArea spa on spa.cAbreviatura = LTRIM(RTRIM(SUBSTRING(spx.cSubProcesoDesc, 1, CHARINDEX(' ', REPLACE(spx.cSubProcesoDesc, '-', ' ')))))
where spx.nTpoProceso = 1

declare @nNroRiesgoUpd int = 0, @cCodProcesoX varchar(20) = '', @nCodSubProcesoX int = 0, @nRecorrer int = 0, @nCorrectos int = 0  
while(select COUNT(nNroRiesgo) from #RiesgoUpdate) > 0 begin 
	set @nRecorrer = @nRecorrer + 1
	set @nNroRiesgoUpd = (select nNroRiesgo from #RiesgoUpdate where nCorrelativo = @nRecorrer)
	set @cCodProcesoX = (select spx.cCodProceso  
							from #RiesgoUpdate tmpr
							inner join #ProcesosSubProcesos spx on tmpr.nNroRiesgo = spx.nNroRiesgo
							where tmpr.nCorrelativo = @nRecorrer)
	set @nCodSubProcesoX = (select spx.nCodSubProceso  
							from #RiesgoUpdate tmpr
							inner join #ProcesosSubProcesos spx on tmpr.nNroRiesgo = spx.nNroRiesgo
							where tmpr.nCorrelativo = @nRecorrer)
	if(ISNULL(@cCodProcesoX, '') <> '' and ISNULL(@nCodSubProcesoX , '') <> '') begin 
		set @nCorrectos = @nCorrectos + 1
		--select @cCodProcesoX, @nCodSubProcesoX, @nNroRiesgoUpd
		update DetalleRiesgo set cCodProceso = @cCodProcesoX , nCodSubProceso = @nCodSubProcesoX where nNroRiesgo = @nNroRiesgoUpd
	end
	
	delete from #RiesgoUpdate where nCorrelativo = @nRecorrer
	if(select COUNT(*) from #RiesgoUpdate) = 0 begin select @nCorrectos Correctos break end else begin continue end
end 
drop table #RiesgoUpdate
drop table #ProcesosSubProcesos
--go
--[Insercion a la tabla proceso riesgo]
insert into ProcesoRiesgo(nNroRiesgo, nFactorRiesgo, nEventPerdida, cLineaNeg, cProducto, cSubProdcto, nProbabilidad, nImpacto, bContRiesResidual, cComentRiesgoResidual, bContPlanAccion, bContResponPlanAcc, cUltimaActualizacion)
select dt.nNroRiesgo, rod.nFactorROID, (rod.nEventoPerdidaID + 100), FORMAT(rod.nFactorROID, '000000'), FORMAT(rod.nTpoProdNiv1, '000000'),
FORMAT(rod.nTpoProdNiv2, '000000'), rod.nProbalidadID, rod.nImpactoID, IIF(isnull(rod.nNivelRiesgo, 0) > 0, 1, 0), ISNULL(rod.cMedidaTratamiento, ''),
 IIF(ISNULL(rod.dFechaImplControl, '') <> '', 1, 0), IIF(ISNULL(rp.cRiesgoCod, '') = '', 0, 1), dt.cUltimaActualizacion
from [SRVSQLDB03].[DBSro].[dbo].[SRO_RiesgoOperacional] ro
inner join [SRVSQLDB03].[DBSro].[dbo].[SRO_RiesgoOperacionalDet] rod on ro.cRiesgoCod = rod.cRiesgoCod
inner join DetalleRiesgo dt on dt.cCodRiesgo = ro.cRiesgoCod
inner join [SRVSQLDB03].[DBSro].[dbo].[SRO_RiesgoOperacionalDet] rp on rod.cRiesgoCod = rp.cRiesgoCod

--[Insercion de los planes de accion]
select ROW_NUMBER()over(order by ro.cRiesgoCod) nItem,dr.nNroRiesgo, p.cPlanDesc, p.nEmitidoGM, p.nEstado, p.dFecImpl, p.dFecImpl1, p.dFecImpl2, 
null cComentario ,null cNombreDoc, null cNombreDocDB, dbo.GenerarcNroRiesgo(@dFechaMigra, '01', 'SIST') cFechaReg, 
dbo.GenerarcNroRiesgo(@dFechaMigra, '01', 'SIST') cUltimaActualizacion
into #tmpPlanAccion
from [SRVSQLDB03].[DBSro].[dbo].[SRO_PlanAccionRO] p
inner join [SRVSQLDB03].[DBSro].[dbo].[SRO_RiesgoOperacional] ro on p.cRiesgoCod = ro.cRiesgoCod
inner join DetalleRiesgo dr on dr.cCodRiesgo = p.cRiesgoCod --FORMAT(CONVERT(int, SUBSTRING(ro.cRiesgoCod, 5, LEN(ro.cRiesgoCod))), '00000')

insert into PlanAccion
select nNroRiesgo, cPlanDesc, nEmitidoGM, nEstado, cComentario, cNombreDoc, cNombreDocDB, cFechaReg, cUltimaActualizacion from #tmpPlanAccion

--[Insercion Fecha de Implementacion]
select ROW_NUMBER()over(order by p.nNroRiesgo) nItem,p.nNroRiesgo, pa.nPlanCod, P.cPlanDesc, p.dFechaImplementacion, p.cTipoFecha 
into #tmpFechaImplementa
from #tmpPlanAccion tmpplan
unpivot (dFechaImplementacion for [cTipoFecha] in ([dFecImpl], [dFecImpl1], [dFecImpl2])) as P
inner join PlanAccion pa on P.nNroRiesgo = pa.nNroRiesgo and pa.cPlanDescripcion = P.cPlanDesc
group by p.nNroRiesgo, pa.nPlanCod, P.cPlanDesc, p.dFechaImplementacion, p.cTipoFecha order by p.nNroRiesgo, p.dFechaImplementacion asc

declare @xRecorre int = 0, @xnPlanCod int = 0,  @xdFechaImp date = null, @xUltimaActualizacion varchar(25) = ''
while(select COUNT(*) from #tmpFechaImplementa) > 0 begin
set @xRecorre = @xRecorre + 1
set @xnPlanCod = (select nPlanCod from #tmpFechaImplementa where nItem = @xRecorre)
set @xdFechaImp = (select dFechaImplementacion from #tmpFechaImplementa where nItem = @xRecorre)
set @xUltimaActualizacion = (select dFechaImplementacion from #tmpFechaImplementa where nItem = @xRecorre)

insert into ImplementaPlanAccion values(@xnPlanCod, @xdFechaImp, 0,1, CONVERT(varchar(25), @dFechaMigra, 112)+'1133351090100SIS')
delete from #tmpFechaImplementa where nItem = @xRecorre
if (select COUNT(*) from #tmpPlanAccion) = 0 begin break end else begin continue end
end

drop table #tmpPlanAccion
drop table #tmpFechaImplementa
go
--[Actualiza el estado de la fecha de implementacion del plan de accion]
WITH ActualizaEstado AS (
  SELECT ix.nPlanCod, ix.dFechaImplementa, ix.bImplementado, ix.nEstado, --rx.nEstadoRiesgo,
         ROW_NUMBER() OVER (PARTITION BY nPlanCod ORDER BY dFechaImplementa DESC) AS rn
  FROM ImplementaPlanAccion ix
  --inner join PlanAccion pax on ix.nPlanCod = pax.nPlanCod
  --inner join Riesgo rx on pax.nNroRiesgo = rx.nNroRiesgo
)

update ActualizaEstado set bImplementado = 1, nEstado = 3 --, nEstadoRiesgo = 10
WHERE rn > 1



--[Insercion del Historial del estado]
insert into HistorialEstadoPlanAccion(nPlanCod, nEstado, cUltimaActulizacion)
select nPlanCod, nEstado, cUltimaActualizacion from PlanAccion
--[Actualizacion del riesgo con planes de accion implementados]
--Cuando el plan de accion es IMPLEMENTADO





go
--select * from ResponsablePlanAccion
go
--[Insercion de los responsables de los planes de accion]
declare @dFechaMigra datetime = getdate()
--declare @tblResponsables table(nGenerado int,nPlanCod int, cUserRespon varchar(4), bEstado bit, cFechaReg varchar(25), cUltimaActualizacion varchar(25))
--insert into @tblResponsables
select --*
ROW_NUMBER() over(order by  pl.nPlanCod) nCorrelativo, p.cPlanCod, dr.nNroRiesgo, pl.nPlanCod, r.cUser, r.nEstado,
dbo.GenerarcNroRiesgo(@dFechaMigra, '01', 'SIST') cFechaReg, dbo.GenerarcNroRiesgo(@dFechaMigra, '01', 'SIST') cUltimaActualizacion
into #ResponsablesPlan
from [SRVSQLDB03].[DBSro].[dbo].[SRO_ResponsableRO] r
inner join [SRVSQLDB03].[DBSro].[dbo].[SRO_PlanAccionRO]  p on r.cPlanCod = p.cPlanCod
inner join DetalleRiesgo dr on dr.cCodRiesgo =  p.cRiesgoCod--FORMAT(CONVERT(int, SUBSTRING(p.cRiesgoCod, 5, LEN(p.cRiesgoCod))), '00000')
inner join PlanAccion pl on dr.nNroRiesgo = pl.nNroRiesgo and pl.cPlanDescripcion = p.cPlanDesc
--where dr.nTpoRiesgo = 1
--select * from DBSro..SRO_ResponsableRO
--select * from DBSro..SRO_PlanAccionRO

declare @nRecorrido int = 0, @nCodPlan int = 0, @cCodPlan varchar(10) = '', @nNroRiesgo int = 0
while(select count(nPlanCod) from #ResponsablesPlan) > 0 begin
	set @nRecorrido = @nRecorrido + 1
	set @nNroRiesgo = (select nNroRiesgo from #ResponsablesPlan where nCorrelativo = @nRecorrido)
	set @nCodPlan = (select nPlanCod from #ResponsablesPlan where nCorrelativo = @nRecorrido)
	set @cCodPlan = (select cPlanCod from #ResponsablesPlan where nCorrelativo = @nRecorrido)
	declare @item int = (select COUNT(nPlanCod) + 1 from #ResponsablesPlan where nPlanCod = @nCodPlan)

	insert into ResponsablePlanAccion
	select distinct nPlanCod, (@item - 1), cUser, nEstado, IIF(nEstado = 3, 1, 0),cFechaReg, cUltimaActualizacion 
	from #ResponsablesPlan where nCorrelativo = @nRecorrido

	delete from #ResponsablesPlan where nCorrelativo = @nRecorrido
	if(select COUNT(*) from #ResponsablesPlan) = 0 begin break end else begin continue end
end
drop table #ResponsablesPlan
go
--[Causas de los riesgo]
declare @dFechaMigra datetime = getdate()
select  ROW_NUMBER() over(order by dt.nNroRiesgo asc) nCorrelativo, cr.cRiesgoCod, dt.nNroRiesgo,FORMAT(cr.nCausaID, '0000') cCodCausa, cr.nEstado
into #TmpCausaRiesgo
from DetalleRiesgo dt 
inner join [SRVSQLDB03].[DBSro].[dbo].[SRO_CausaRiesgo] cr on dt.cCodRiesgo = cr.cRiesgoCod
inner join Riesgo r on dt.nNroRiesgo = r.nNroRiesgo
where dt.nTpoRiesgo = 1 and r.nFlagRiesgo = 0
order by dt.nNroRiesgo asc

declare @nRecorrido int = 0, @nNroRiesgoX int = 0, @nItemX int = 0
while(select COUNT(*) from #TmpCausaRiesgo) > 0 begin 
	set @nRecorrido = @nRecorrido + 1
	set @nNroRiesgoX = (select nNroRiesgo from #TmpCausaRiesgo where nCorrelativo = @nRecorrido)
	set @nItemX = (select COUNT(*) + 1 from #TmpCausaRiesgo where nNroRiesgo = @nNroRiesgoX)

	insert into CausasRiesgo
	select nNroRiesgo, @nItemX - 1, cCodCausa, nEstado, dbo.GenerarcNroRiesgo(@dFechaMigra, '01','SIST') from #TmpCausaRiesgo where nCorrelativo = @nRecorrido

	delete from #TmpCausaRiesgo where nCorrelativo = @nRecorrido
	if(select COUNT(*) from #TmpCausaRiesgo) = 0 begin break end else begin continue end 
end
drop table #TmpCausaRiesgo
go
--[Controles de riesgo residual del riesgo]
declare @dFechaMigra datetime = getdate()
select ROW_NUMBER() over (order by dr.nNroRiesgo asc) nCorrelativo,dr.nNroRiesgo, cr.* 
into #ContlrolRiesgoResidual
from DetalleRiesgo dr
inner join [SRVSQLDB03].[DBSro].[dbo].[SRO_ControlRO] cr on dr.cCodRiesgo = cr.cRiesgoCod
inner join Riesgo r on dr.nNroRiesgo = r.nNroRiesgo
where dr.nTpoRiesgo = 1 and r.nFlagRiesgo = 0

declare @nNroRiesgoX int = 0, @nItem int = 0,@nRecorrido int = 0
while(select COUNT(*) from #ContlrolRiesgoResidual) > 0 begin 
	set @nRecorrido = @nRecorrido + 1
	set @nNroRiesgoX = (select nNroRiesgo from #ContlrolRiesgoResidual where nCorrelativo = @nRecorrido)
	set @nItem = (select COUNT(nNroRiesgo) + 1 from #ContlrolRiesgoResidual where nNroRiesgo = @nNroRiesgoX)

	insert into ControlRiesgoRisidual
	select nNroRiesgo, (@nItem - 1), cComentario, nResponsableDefinido, nFrecuenciaDefinida, nEvidenciaControl,
	nTipoEjecucion, IIF(nCumpleObjetivo = 2, 1, nCumpleObjetivo), nNivEfecControl, nEstado, dbo.GenerarcNroRiesgo(dFecRegistro, '01', 'SIST'), 
	dbo.GenerarcNroRiesgo(@dFechaMigra, '01', 'SIST')
	from #ContlrolRiesgoResidual where nCorrelativo = @nRecorrido

	delete from #ContlrolRiesgoResidual where nCorrelativo = @nRecorrido
end
drop table #ContlrolRiesgoResidual


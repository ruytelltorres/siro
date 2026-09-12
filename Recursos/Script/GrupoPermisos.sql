--select * from roles
--truncate table roles
declare @menu varchar(50) = '0510'
select * from Menu where cMenuId = @menu and cMenuPadre = @menu
select * from Menu where cMenuPadre = @menu and cMenuPadre <> cMenuId

select * from Menu order by cMenuId asc --where cMenuPadre = '0220'
--update Menu set bEstado = 0 where cMenuId = '0902'
--truncate table Roles
insert into Roles(cGrupoUsu, cMenuId, bEstado) values
--TI
--('GRUPO TI', '0100', 1), --Riesgo Operacional
--('GRUPO TI', '0110', 1), --Registro de riesgos
--('GRUPO TI', '0120', 1), --Gestión de riesgos
--('GRUPO TI', '0121', 1), --Lista riesgos
--('GRUPO TI', '0123', 1), --Plan de Acción
--('GRUPO TI', '0103', 1), --Modificar

--('GRUPO TI', '0200', 1), --Evaluación
--('GRUPO TI', '0201', 1), --Registro evaluación
--('GRUPO TI', '0220', 1), --Gestión evaluación
--('GRUPO TI', '0221', 1), --Lista evaluación
--('GRUPO TI', '0222', 1), --Noticar evaluaciones
--('GRUPO TI', '0230', 1), --Monitorear evaluación

----Operaciones y canales
--('OPERACIONES-CANALES', '0100', 1), --Riesgo Operacional
--('OPERACIONES-CANALES', '0110', 1), --Registro de riesgos
--('OPERACIONES-CANALES', '0120', 1), --Gestión de riesgos
--('OPERACIONES-CANALES', '0121', 1), --Lista riesgos
--('OPERACIONES-CANALES', '0123', 1), --Plan de Acción
--('OPERACIONES-CANALES', '0103', 1), --Modificar

--('OPERACIONES-CANALES', '0200', 1), --Evaluación
--('OPERACIONES-CANALES', '0201', 1), --Registro evaluación
--('OPERACIONES-CANALES', '0220', 1), --Gestión evaluación
--('OPERACIONES-CANALES', '0221', 1), --Lista evaluación
--('OPERACIONES-CANALES', '0222', 1), --Noticar evaluaciones
--('OPERACIONES-CANALES', '0230', 1), --Monitorear evaluación

----Finanzas
--('GRUPO FINANZAS', '0100', 1), --Riesgo Operacional
--('GRUPO FINANZAS', '0110', 1), --Registro de riesgos
--('GRUPO FINANZAS', '0120', 1), --Gestión de riesgos
--('GRUPO FINANZAS', '0121', 1), --Lista riesgos
--('GRUPO FINANZAS', '0123', 1), --Plan de Acción
--('GRUPO FINANZAS', '0103', 1), --Modificar

--('GRUPO FINANZAS', '0200', 1), --Evaluación
--('GRUPO FINANZAS', '0201', 1), --Registro evaluación
--('GRUPO FINANZAS', '0220', 1), --Gestión evaluación
--('GRUPO FINANZAS', '0221', 1), --Lista evaluación
--('GRUPO FINANZAS', '0222', 1), --Noticar evaluaciones
--('GRUPO FINANZAS', '0230', 1), --Monitorear evaluación


--Riesgos

('GRUPO RIESGOS', '0100', 1), --Riesgo Operacional
('GRUPO RIESGOS', '0110', 1), --Registro de riesgos
('GRUPO RIESGOS', '0120', 1), --Gestión de riesgos
('GRUPO RIESGOS', '0121', 1), --Lista riesgos
('GRUPO RIESGOS', '0123', 1), --Plan de Acción
('GRUPO RIESGOS', '0103', 1), --Modificar
('GRUPO RIESGOS', '0130', 1), --Incentivos

('GRUPO RIESGOS', '0200', 1), --Evaluación
('GRUPO RIESGOS', '0201', 1), --Registro evaluación
('GRUPO RIESGOS', '0220', 1), --Gestión evaluación
('GRUPO RIESGOS', '0221', 1), --Lista evaluación
('GRUPO RIESGOS', '0222', 1), --Noticar evaluaciones
('GRUPO RIESGOS', '0230', 1), --Monitorear evaluación

('GRUPO RIESGOS', '0300', 1), --Evento de Pérdida
('GRUPO RIESGOS', '0310', 1), --Gestión Evento Pérdida
('GRUPO RIESGOS', '0311', 1), --Lista Evento Pérdida
('GRUPO RIESGOS', '0312', 1), --Agrupar Evento
('GRUPO RIESGOS', '0301', 1), --Registro Evento Pérdida

('GRUPO RIESGOS', '0500', 1), --Reportes
('GRUPO RIESGOS', '0505', 1), --Mapa de Riesgo
('GRUPO RIESGOS', '0510', 1), --Matriz
('GRUPO RIESGOS', '0511', 1), --Riesgo - Operacion/Evaluación
('GRUPO RIESGOS', '0512', 1), --Evento de Pérdida
('GRUPO RIESGOS', '0520', 1), --Reportes - Gráfico
('GRUPO RIESGOS', '0525', 1), --Reporte Varios

('GRUPO RIESGOS', '0900', 1), --Configuración
('GRUPO RIESGOS', '0901', 1), --Configuración de Elementos
('GRUPO RIESGOS', '0902', 1) --Admin. Permisos (se deshabilito, dado que los permisos se haran por grupo)






--update PlanAccion set nEstado = 1 
--update ResponsablePlanAccion set bConfirma = 0 

exec stp_sel_ObtenerPlanesAccionAsignados '', 1, ''
select * from Constante where nConsCod = 3000 --cConsDescripcion like 'estad%'
select * from PlanAccion where nPlanCod = 11546
select * from ResponsablePlanAccion where nPlanCod = 11546


ALTER PROC dbo.stp_upd_ActualizaEstadoPlanAccion      
(@nCodPlan             INT,       
 @nEstado              INT,       
 @cUltimaActualizacion VARCHAR(25),       
 @cFinalizar           VARCHAR(1500) = '' OUTPUT      
)      
AS      
BEGIN        
	--[Tipo de respuesta: Tipo mensaje, mensaje, numero de riesgo, finalizacion]      
	DECLARE @cFinaliza VARCHAR(1500)= '', @NroRiesgo INT= 0, @CodRiesgo varchar(25) = '', 
	@PlanEstadoActual VARCHAR(50) = '', @nTpoRiesgo INT= 0, @ValValida BIT= 1, 
	@nCantidadResponsables INT = 0, @nCantidadConfirmados INT = 0

	SET @NroRiesgo = ( SELECT PlanAccion.nNroRiesgo      
	FROM dbo.PlanAccion      
	WHERE PlanAccion.nPlanCod = @nCodPlan);      
    
    SET @nTpoRiesgo = dbo.fnc_ObtenerTipoRiesgo(@NroRiesgo);      
    
	SET @CodRiesgo = (SELECT cCodRiesgo FROM DetalleRiesgo WHERE nNroRiesgo = @NroRiesgo);  
	SET @PlanEstadoActual = (SELECT ep.cConsDescripcion    
							 FROM PlanAccion p      
							 INNER JOIN Constante ep ON p.nEstado = ep.nConsValor AND ep.nConsCod = 3000      
							 WHERE nPlanCod = @nCodPlan);      
    
    IF (SELECT nEstado FROM PlanAccion WHERE nPlanCod = @nCodPlan) > @nEstado BEGIN      
      
        SET @cFinaliza = CONCAT('advertencia', '|', ('Actualmente el estado del plan de acción es ' + @PlanEstadoActual + '; no es posible volver a un estado anterior'), '|', CONVERT(varchar(25), @NroRiesgo), '|', 'xPendiente');      
        SET @ValValida = 0;      
  
    END IF (SELECT nEstado FROM PlanAccion WHERE nPlanCod = @nCodPlan) = @nEstado BEGIN      
		
		SET @cFinaliza = CONCAT('advertencia', '|', ('Actualmente el estado del plan de acción es ' + @PlanEstadoActual + '; no es posible actualizar el estado del plan de acción al mismo estado'), '|', CONVERT(varchar(25), @NroRiesgo), '|', 'xPendiente');     
		SET @ValValida = 0;      
	
	END  

    IF @ValValida = 1  BEGIN
		--Added by TORE 20210325: Adecuacion de las confirmaciones de planes de accion

		update ResponsablePlanAccion set bConfirma = 1, cUltimaActualizacion = @cUltimaActualizacion  where nPlanCod = @nCodPlan and cUserRespon = RIGHT(@cUltimaActualizacion, 4)

		set @nCantidadResponsables = (select COUNT(nPlanCod) from ResponsablePlanAccion where nPlanCod = @nCodPlan)
		set @nCantidadConfirmados = (select COUNT(nPlanCod) from ResponsablePlanAccion where nPlanCod = @nCodPlan and bConfirma = 1)

		if @nCantidadConfirmados = @nCantidadResponsables begin 
			UPDATE dbo.PlanAccion SET 
			PlanAccion.nEstado = @nEstado,       
			PlanAccion.cUltimaActualizacion = @cUltimaActualizacion      
			WHERE PlanAccion.nPlanCod = @nCodPlan;      
               
			INSERT INTO HistorialEstadoPlanAccion (nPlanCod, nEstado, cUltimaActulizacion)     
			VALUES (@nCodPlan, @nEstado, @cUltimaActualizacion);      
		end 
       
		IF @nTpoRiesgo = 1 BEGIN      
			IF( SELECT COUNT(*) FROM dbo.PlanAccion WHERE PlanAccion.nNroRiesgo = @NroRiesgo AND PlanAccion.nEstado IN(1, 2)) = 0  BEGIN      
    
				UPDATE dbo.Riesgo SET Riesgo.nEstadoRiesgo = 10 WHERE Riesgo.nNroRiesgo = @NroRiesgo;     
				SET @cFinaliza = CONCAT('exito', '|', ('Se finalizó con la gestión del riesgo operacioal ' + @CodRiesgo), '|', CONVERT(varchar(25), @NroRiesgo), '|', 'xFinaliza');      

			END ELSE BEGIN      

				if @nCantidadConfirmados < @nCantidadResponsables begin 
					SET @cFinaliza = CONCAT('informacion', '|', ('Se confirmó la responsabilidad del Plan de Acción ' + CONVERT(varchar(25), @nCodPlan)), '|', CONVERT(varchar(25), @NroRiesgo), '|', 'xPendiente');      
				end else begin 
					SET @cFinaliza = CONCAT('informacion', '|', ('Se actualizó el estado del Plan de Acción del Riesgo Operacional ' + @CodRiesgo), '|', CONVERT(varchar(25), @NroRiesgo), '|', 'xPendiente');      			
				end 

			END
		END ELSE IF @nTpoRiesgo = 2 BEGIN      
			SET @cFinaliza = CONCAT('informacion', '|', 'aun no se implemento logica para el cambio de estado de las evaluaciones', '|', CONVERT(varchar(25), @NroRiesgo), '|', 'xPendiente');      
		END      
  
  END    
    
 SET @cFinalizar = @cFinaliza;      
    --SELECT @cFinalizar;      
END;

go
ALTER PROC stp_sel_ObtenerPlanesAccionAsignados            
(@cUser      VARCHAR(4),             
 @nTpoRiesgo INT,             
 @cBuscar    VARCHAR(200) = ''            
)            
AS            
BEGIN                      
    --Cuando el usuario que ingresa es el analista de riesgo operacional, Listar todos los planes de accion          
    SELECT r.nNroRiesgo, dr.cCodRiesgo, dr.nTpoRiesgo, dr.cRiesgoIdentificado, er.cConsDescripcion cEstadoActual,   
			pa.nPlanCod, pa.cPlanDescripcion, pa.nEstado, epa.cConsDescripcion cEstado,             
            ie.cConsDescripcion cIconoEstado,       
			dFechaImplementa =  (SELECT TOP 1 ix.dFechaImplementa            
									FROM ImplementaPlanAccion ix            
									WHERE ix.nPlanCod = pa.nPlanCod AND ix.nEstado = 1            
									ORDER BY ix.dFechaImplementa DESC ),                  
            cResponsables = dbo.fnc_ObtenerResponsablePlanesAccionRiesgo(r.nNroRiesgo, pa.nPlanCod),                     
            ISNULL(dr.dFechaDetec, '01/01/1999') dFechaDetec,
			cConfirmados = dbo.fnc_ObtenerConfirmacionResponsables(pa.nPlanCod, 1),
			cUserConfirmados = dbo.fnc_ObtenerConfirmacionResponsables(pa.nPlanCod, 2)
    FROM Riesgo r            
            INNER JOIN DetalleRiesgo dr ON r.nNroRiesgo = dr.nNroRiesgo            
            INNER JOIN PlanAccion pa ON r.nNroRiesgo = pa.nNroRiesgo            
            INNER JOIN ResponsablePlanAccion rp ON pa.nPlanCod = rp.nPlanCod            
		   --Commented by TORE: 20210211, error por tiempo de respuesta del servidor        
		   --INNER JOIN (            
		   -- SELECT rh.cUser,             
		   --     per.cPersNombre            
		   -- FROM DBCmacMaynas..Persona per            
		   --   INNER JOIN DBCmacMaynas..RRHH rh ON per.cPersCod = rh.cPersCod            
		   --) Usuario ON rp.cUserRespon = Usuario.cUser            
            INNER JOIN Constante er ON dr.nProcRiesgo = er.nConsValor AND er.nConsCod = 1002            
            INNER JOIN Constante epa ON pa.nEstado = epa.nConsValor  AND epa.nConsCod = 3000            
            INNER JOIN Constante ie ON pa.nEstado = ie.nConsValor  AND ie.nConsCod = 3001            
    WHERE rp.cUserRespon LIKE '' + @cUser + '%'            
            AND (CONVERT(VARCHAR(12), pa.nPlanCod) LIKE '' + @cBuscar + '%'            
                OR dr.cCodRiesgo LIKE '' + @cBuscar + '%')            
            AND dr.nTpoRiesgo = @nTpoRiesgo            
            AND dr.nProcRiesgo = 5 --add 20201110, se añadio "dr.nProcRiesgo = 5" con la finalidad de mostrar los planes de accion que fueron confirmados en la gestion                  
            AND pa.nEstado NOT IN(3, 4, 5) --IN(1, 2)  modyfied by TORE 20210101: Solo no se listara los planes de accion en estado rechazado.  --modyfied se añadio el estado 3, 4        
            AND rp.bEstado = 1            
            AND r.nFlagRiesgo = 0            
    GROUP BY r.nNroRiesgo, dr.cCodRiesgo,  dr.nNroRiesgo, dr.nTpoRiesgo, dr.cRiesgoIdentificado, epa.cConsDescripcion, er.cConsDescripcion,             
                pa.nPlanCod, pa.cPlanDescripcion, pa.nEstado, ie.cConsDescripcion, dr.dFechaDetec            
    ORDER BY dr.nNroRiesgo DESC,             
                pa.nPlanCod ASC;            
END; 
go



alter function fnc_ObtenerConfirmacionResponsables
(@nPlanCod int, @nMostrar int = 1)
returns varchar(500) as
begin
	declare @cCadena varchar(500) = '', @nConfirmados int = 0, @nRespondables int = 0

	if @nMostrar = 1 begin
		set @nConfirmados = (select COUNT(nPlanCod) from ResponsablePlanAccion where nPlanCod = @nPlanCod and bConfirma = 1)
		set @nRespondables = (select COUNT(nPlanCod) from ResponsablePlanAccion where nPlanCod = @nPlanCod)
		set @cCadena = CONCAT('Confirmados ', CONVERT(varchar(15), @nConfirmados),' de ', CONVERT(varchar(15), @nRespondables))
	end else if @nMostrar = 2 begin
		set @cCadena = (select ISNULL(              
						  STUFF((select '| ' + UPPER(cUserRespon)  
								 from ResponsablePlanAccion 
								 where nPlanCod = @nPlanCod and bConfirma = 1      
								 for xml path('')),1, 2, ''),              
						  '') AS Confirmados)    
	end
		

	return @cCadena
end

alter function fnc_ObtenerResponsablePlanesAccionRiesgo           
(@nNroRiesgo int, @nPlanCod int) returns nvarchar(max) as          
begin          
 declare @ResponsablePlanesAccion nvarchar(max) = (select ISNULL(              
              STUFF((              
               select '| ' + CONCAT('[', UPPER(cxx.cUser) , '] ', cxxx.cPersNombre)              
               from ResponsablePlanAccion cx     
      inner join PlanAccion pax on cx.nPlanCod = pax.nPlanCod  
               inner join DBCmacMaynas..RRHH cxx on cx.cUserRespon = cxx.cUser      
               inner join DBCmacMaynas..Persona cxxx on cxx.cPersCod = cxxx.cPersCod      
               where pax.nNroRiesgo = @nNroRiesgo and cx.nPlanCod = @nPlanCod  --and bEstado = 1      
               order by cx.nItemResponPlan asc            
               for xml path('')              
              ),               
              1, 2, ''),              
              '') AS RespnsablePlanesAccion)          
 return @ResponsablePlanesAccion;          
end   


go



ALTER PROC stp_ins_AsignarRiesgosTaller  
(@cCodRiesgos          VARCHAR(500),   
 @cCodTaller           VARCHAR(20),   
 @cUltimaActualizacion VARCHAR(25),   
 @cMensaje             VARCHAR(500) = '' OUTPUT  
)  
AS  
BEGIN  
    
	DECLARE @MensajeSis VARCHAR(500)= '', @nRecorrido INT= 0, @bInvalido BIT= 0
	DECLARE @tmpNroRiesgoAnt INT = 0, @tmpNroRiesgoPos INT = 0, @tmpTpoEvalAnt INT = 0, @tmpTpoEvalPos INT = 0

	BEGIN TRANSACTION RegTallerEvaluacion BEGIN TRY  
  
        /*Verificamos los riesgos para las evaluaciones*/  
        DECLARE @cNroRiesgoTratados VARCHAR(500)= (SELECT SUBSTRING(@cCodRiesgos, 1, LEN(@cCodRiesgos) - 1));  
  
        /*Condiciones para la creacion del taller*/  
        SELECT ValorId nItem, Valor nNroRiesgo  
        INTO #RiesgosTaller  
        FROM dbo.fnc_getTblValoresTexto(@cNroRiesgoTratados, DEFAULT);  
            
		WHILE (SELECT COUNT(*) FROM #RiesgosTaller) > 0  BEGIN  
            SET @nRecorrido+=1;  
                    
			SET @tmpNroRiesgoAnt = (SELECT nNroRiesgo FROM #RiesgosTaller WHERE nItem = @nRecorrido)  
			SET @tmpNroRiesgoPos = (SELECT nNroRiesgo FROM #RiesgosTaller  WHERE nItem = @nRecorrido + 1)  
            SET @tmpTpoEvalAnt  = (SELECT nTpoEvaluacion FROM DetalleRiesgo WHERE nNroRiesgo = @tmpNroRiesgoAnt)  
            SET @tmpTpoEvalPos = (SELECT nTpoEvaluacion FROM DetalleRiesgo WHERE nNroRiesgo = @tmpNroRiesgoPos)  

            IF @tmpTpoEvalAnt <> @tmpTpoEvalPos  BEGIN  
                SET @MensajeSis = CONCAT('informacion', '-', 'Le informamos que las evaluaciones seleccionadas para la creación y notificación del taller, no pertenecen al mismo tipo de evaluación.');  
                SET @bInvalido = 1  

            END ELSE BEGIN  
                IF @tmpTpoEvalAnt = 1001  BEGIN  
                        DECLARE @tmpAreaAnt INT = (SELECT cAreaCod FROM DetalleRiesgo WHERE nNroRiesgo = @tmpNroRiesgoAnt) 
                        DECLARE @tmpAreaPos INT = (SELECT cAreaCod FROM DetalleRiesgo WHERE nNroRiesgo = @tmpNroRiesgoPos)  
                        
						IF @tmpAreaAnt <> @tmpAreaPos 
						BEGIN  
                                SET @MensajeSis = CONCAT('informacion', '-', 'Le informamos que las evaluaciones seleccionadas para la creación y notificación del taller, no pertenecen a la misma area.');  
                                SET @bInvalido = 1
                        END
                END
            END  

            DELETE FROM #RiesgosTaller  WHERE nItem = @nRecorrido
        END  
        DROP TABLE #RiesgosTaller;  
        DECLARE @nPendienteGestion INT = (SELECT COUNT(rx.nNroRiesgo)  
										  FROM Riesgo rx  
										  INNER JOIN DetalleRiesgo drx ON rx.nNroRiesgo = drx.nNroRiesgo  
										  WHERE RIGHT(rx.cNroRiesgo, 4) = RIGHT(@cUltimaActualizacion, 4)  
										  AND rx.nEstadoRiesgo = 2  
										  AND drx.nTpoRiesgo = 2)

        IF @nPendienteGestion > 0  
		BEGIN  
            SET @MensajeSis = CONCAT('informacion', '-', 'Se le informar que aún mantienes evaluaciones pendientes de gestión. Es necesario realizar la gestión de todas sus evaluaciones para proceder con la notificación y creación del taller.')
            SET @bInvalido = 1
        END  
  
        /*end condiciones para la creacion del taller*/  
        IF @bInvalido = 0  BEGIN 
			declare @FiltroEvaluacion varchar(25) = '' 
			declare @nNroRiesgo int = (SELECT TOP 1 Valor FROM dbo.fnc_getTblValoresTexto(@cNroRiesgoTratados, DEFAULT))
			
			if @tmpTpoEvalAnt = 1001 begin
				--set @FiltroEvaluacion = (select CONCAT(nTpoEvaluacion, cAgeCod, cAreaCod, cCodProceso, nCodSubProceso) from DetalleRiesgo where nNroRiesgo = @nNroRiesgo)
				set @FiltroEvaluacion = (select CONCAT(nTpoEvaluacion, cAreaCod, cCodProceso, nCodSubProceso) from DetalleRiesgo where nNroRiesgo = @nNroRiesgo)
			end if @tmpTpoEvalAnt = 1002 or  @tmpTpoEvalAnt = 1003 begin
				set @FiltroEvaluacion = (select CONVERT(varchar(4),nTpoEvaluacion) from DetalleRiesgo where nNroRiesgo = @nNroRiesgo)
			end if @tmpTpoEvalAnt = 1004 begin
				set @FiltroEvaluacion = (select CONCAT(nTpoEvaluacion, cAgeCod) from DetalleRiesgo where nNroRiesgo = @nNroRiesgo)
			end 

			INSERT INTO Taller (cCodTaller, cCodTpoEval, nEstado, cUltimaActualizacion) VALUES  
								(@cCodTaller, @FiltroEvaluacion,  100,  @cUltimaActualizacion)
								
		    INSERT INTO TallerRiesgo (cCodTaller, nNroRiesgo, nCondicion, bEstado, cUltimaActualizacion) 
			SELECT @cCodTaller, Valor, 200, 1, @cUltimaActualizacion FROM dbo.fnc_getTblValoresTexto(@cNroRiesgoTratados, DEFAULT)
			
			UPDATE rx SET rx.nEstadoRiesgo = 4  
			FROM dbo.fnc_getTblValoresTexto(@cNroRiesgoTratados, DEFAULT) tmp  
			INNER JOIN Riesgo rx ON tmp.Valor = rx.nNroRiesgo  
			WHERE rx.nNroRiesgo = tmp.Valor
			
			SET @MensajeSis = CONCAT('exito', '-', 'Se ha creado el taller ', @cCodTaller, ' correctamente')  
        END  
        
		SELECT @cMensaje = @MensajeSis  
        
        COMMIT TRAN RegTallerEvaluacion
    END TRY  
    BEGIN CATCH  
        SET @MensajeSis = CONCAT('error', '-', 'Lo sentimos !!!, ocurrió un error al procesar la información; por favor comunicarse con TI.')  
        SELECT @cMensaje = @MensajeSis
        ROLLBACK TRAN RegTallerEvaluacion
    END CATCH
END

go


select * from Taller
select LEN('1001020250251161')

--1001 02 025 0251 161

exec stp_sel_ObtenerRiesgosTaller '025-20210327-1'

ALTER PROC stp_sel_ObtenerRiesgosTaller(@cCodTaller VARCHAR(20))    
AS    
BEGIN    
    SELECT cCodTaller,     
            dr.nNroRiesgo,     
            dr.cCodRiesgo,     
			eval.cConsDescripcion cEvalDesc,
            dbo.FechaHoraMov(r.cNroRiesgo) dFechaReg    
    FROM TallerRiesgo tr    
            INNER JOIN DetalleRiesgo dr ON tr.nNroRiesgo = dr.nNroRiesgo    
            INNER JOIN Riesgo r ON dr.nNroRiesgo = r.nNroRiesgo    
			inner join Constante eval on dr.nTpoEvaluacion = eval.nConsValor and eval.nConsCod = 1501
    WHERE cCodTaller = @cCodTaller;    
END;


ALTER PROC stp_sel_ObtenerTallerUsuario  
(@cCodTpoEval   VARCHAR(25),   
 @cEstado VARCHAR(200) = ''  
)  
AS  
BEGIN  
    SELECT r.cCodTaller,   
            r.cTallerDesc  
    FROM Taller r  
            INNER JOIN TallerRiesgo tr ON r.cCodTaller = tr.cCodTaller --added by TORE 20201206: No se listara el taller a menos que tenga riesgos asignados.  
    WHERE RIGHT(r.cUltimaActualizacion, 4) = @cUser  
            AND nEstado IN  
    (  
        SELECT Valor  
        FROM dbo.fnc_getTblValoresTexto(@cEstado, DEFAULT)  
    )  
    GROUP BY r.cCodTaller,   
                r.cTallerDesc,   
                r.nEstado;  
	
	select cCodTaller from Taller 
	where cCodTpoEval = @cCodTpoEval and 
	nEstado in (select * from dbo.fnc_getTblValoresTexto(@cEstado, DEFAULT))


END;
go
select * from Taller --1001 02 025 0251 161
select * from DBCmacMaynas..Agencias where cAgeCod = '02'
select * from DBCmacMaynas..Areas where cAreaCod = '025'
select * from ProcesoArea where cCodProceso = '0251'
select * from SubProcesoArea where nCodSubProceso = 161
exec stp_sel_ObtenerUsuariosTpoEvaluacion '10010250251161'
--update Taller set cCodTpoEval = '10010250251161'
exec stp_sel_ObtenerUsuariosTpoEvaluacion '1001020250251161'
alter proc stp_sel_ObtenerUsuariosTpoEvaluacion
(@cCodTpoEval VARCHAR(25))as
begin
	select distinct UPPER(rh.cUser) cUser, CONCAT('[', UPPER(rh.cUser), ']-', p.cPersNombre) cUsuario
	from Taller t
	inner join DBCmacMaynas..RRHH rh on RIGHT(t.cUltimaActualizacion, 4) = rh.cUser
	inner join DBCmacMaynas..Persona p on rh.cPersCod = p.cPersCod
	where cCodTpoEval = @cCodTpoEval and 
	nEstado = 100
end 
go
create proc stp_sel_ObtenerTallerCodTpoEvalUsuario
(@cCodTpoEval varchar(25), @cUser varchar(4))as
begin 
	select cCodTaller from Taller where cCodTpoEval = @cCodTpoEval and RIGHT(cUltimaActualizacion, 4) = @cUser
end


select * from Constante where nConsCod = 2000 --cConsDescripcion like '%taller%'


stp_sel_ObtenerTallerUsuario '10010250250251161'


ALTER PROC stp_sel_ObtenerTallerUsuario  
(@cCodTpoEval varchar(25),
 @cUser   varchar(4),   
 @cEstado varchar(200) = ''  
)  
AS  
BEGIN  
    --SELECT r.cCodTaller,r.cTallerDesc  
    --FROM Taller r  
    --        INNER JOIN TallerRiesgo tr ON r.cCodTaller = tr.cCodTaller --added by TORE 20201206: No se listara el taller a menos que tenga riesgos asignados.  
    --WHERE RIGHT(r.cUltimaActualizacion, 4) = @cUser  
    --        AND nEstado IN (SELECT Valor  
				--			FROM dbo.fnc_getTblValoresTexto(@cEstado, DEFAULT))  
    --GROUP BY r.cCodTaller, r.cTallerDesc, r.nEstado;  

	select cCodTaller 
	from Taller 
	where cCodTpoEval = @cCodTpoEval 
	and RIGHT(cUltimaActualizacion, 4) = @cUser

END;


ALTER PROC stp_sel_ObtenerDatosTaller(@cCodTaller VARCHAR(20))    
AS    
BEGIN    
    SELECT t.cCodTaller,     
            t.cCodTpoEval,     
            t.nEstado,     
            et.cConsDescripcion cEstado,     
            RIGHT(t.cUltimaActualizacion, 4) cUserCrea,     
            dbo.FechaHoraMov(t.cUltimaActualizacion) dFechaCreacion    
    FROM Taller t    
            INNER JOIN Constante et ON t.nEstado = et.nConsValor    
                                    AND et.nConsCod = 2000    
    WHERE cCodTaller = @cCodTaller;    
END;



--select * from Roles
--select * from MenuCargos
exec stp_sel_ObtenerMenuUser ''
go
alter procedure stp_sel_ObtenerMenuUser          
(@cGrupoUser nvarchar(max),
@cRHCargoUser varchar(15) = '') as  --Modified by TORE, obtener opciones por grupo.          
begin             
	 -- select m.cMenuId, m.cMenuPadre, m.cTitulo, m.cDescripcion, m.cUrl,  
	 -- m.cIcono, m.nPosicion, m.bEstado, m.cUrlTitulo       
	 -- from Menu m            
	 -- inner join Roles r on m.cMenuId = r.cMenuId            
	 -- where r.cGrupoUsu in ( select Valor from dbo.fnc_getTblValoresTexto(@cGrupoUser, default))  and m.bEstado = 1 and r.bEstado = 1            
	 -- group by m.cMenuId, m.cMenuPadre, m.cTitulo, m.cDescripcion, m.cUrl, m.cIcono,            
	 --m.nPosicion, m.bEstado, m.cUrlTitulo         
	declare @MenuUser table(cMenuId varchar(50), cMenuPadre varchar(50), cTitulo varchar(500),
						cDescripcion varchar(1500), cUrl varchar(250), cIcono varchar(150),
						nPosicion int, bEstado bit, cUrlTitulo varchar(150))
	declare @CargoPrincipal varchar(50) = (select cConsSisValor from ConstSistema where nConsSisCod = 20)

	insert into @MenuUser
	select m.cMenuId, m.cMenuPadre, m.cTitulo, m.cDescripcion, m.cUrl,  
	m.cIcono, m.nPosicion, m.bEstado, m.cUrlTitulo       
	from Menu m            
	inner join Roles r on m.cMenuId = r.cMenuId            
	where r.cGrupoUsu in ( select Valor from dbo.fnc_getTblValoresTexto(@cGrupoUser, default))  
	and m.bEstado = 1 and r.bEstado = 1            
	group by m.cMenuId, m.cMenuPadre, m.cTitulo, m.cDescripcion, m.cUrl, m.cIcono,            
	m.nPosicion, m.bEstado, m.cUrlTitulo

	if(@CargoPrincipal <> @cRHCargoUser)
	begin 
		declare @Excluir nvarchar(max) = (select ISNULL(                
										  STUFF((                
										   select ', ' + x.cMenuId
										   from MenuCargos x       
										   where x.cCargoCod = @CargoPrincipal and x.bEstado = 1        
										   for xml path('')                
										  ),                 
										  1, 2, ''),                
										  '') AS Exclusiones)
		delete from @MenuUser where cMenuId not in (select Valor from dbo.fnc_getTblValoresTexto(@Excluir, default))
	end

	select * from @MenuUser
end  
go

declare 
@cGrupoUser nvarchar(max) = 'GRUPO RIESGOS',
@cRHCargoUser varchar(15) = '005011'

declare @MenuUser table(cMenuId varchar(50), cMenuPadre varchar(50), cTitulo varchar(500),
						cDescripcion varchar(1500), cUrl varchar(250), cIcono varchar(150),
						nPosicion int, cUrlTitulo varchar(150))
declare @CargoPrincipal varchar(50) = (select cConsSisValor from ConstSistema where nConsSisCod = 20)

insert into @MenuUser
select m.cMenuId, m.cMenuPadre, m.cTitulo, m.cDescripcion, m.cUrl,  
m.cIcono, m.nPosicion, m.bEstado, m.cUrlTitulo       
from Menu m            
inner join Roles r on m.cMenuId = r.cMenuId            
where r.cGrupoUsu in ( select Valor from dbo.fnc_getTblValoresTexto(@cGrupoUser, default))  
and m.bEstado = 1 and r.bEstado = 1            
group by m.cMenuId, m.cMenuPadre, m.cTitulo, m.cDescripcion, m.cUrl, m.cIcono,            
m.nPosicion, m.bEstado, m.cUrlTitulo

if(@CargoPrincipal <> @cRHCargoUser)
begin 
	declare @Excluir nvarchar(max) = (select ISNULL(                
									  STUFF((                
									   select ', ' + x.cMenuId
									   from MenuCargos x       
									   where x.cCargoCod = @CargoPrincipal and x.bEstado = 1        
									   for xml path('')                
									  ),                 
									  1, 2, ''),                
									  '') AS Exclusiones)
	delete from @MenuUser where cMenuId not in (select Valor from dbo.fnc_getTblValoresTexto(@Excluir, default))
end

select * from @MenuUser

select dbo.fnc_ObtenerResponsablePlanesAccionRiesgo


CREATE function fnc_ObtenerResponsablePlanesAccionRiesgo             
(@nNroRiesgo int, @nPlanCod int) returns nvarchar(max) as            
begin            
 declare @ResponsablePlanesAccion nvarchar(max) = (select ISNULL(                
              STUFF((                
               select '| ' + CONCAT('[', UPPER(cxx.cUser) , '] ', cxxx.cPersNombre)                
               from ResponsablePlanAccion cx       
      inner join PlanAccion pax on cx.nPlanCod = pax.nPlanCod    
               inner join DBCmacMaynas..RRHH cxx on cx.cUserRespon = cxx.cUser        
               inner join DBCmacMaynas..Persona cxxx on cxx.cPersCod = cxxx.cPersCod        
               where pax.nNroRiesgo = @nNroRiesgo and cx.nPlanCod = @nPlanCod  --and bEstado = 1        
               order by cx.nItemResponPlan asc              
               for xml path('')                
              ),                 
              1, 2, ''),                
              '') AS RespnsablePlanesAccion)            
 return @ResponsablePlanesAccion;            
end 


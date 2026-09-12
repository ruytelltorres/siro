using Autofac;
using Autofac.Integration.Mvc;
using CMACMaynas.Web.SIRO.AccesoDatos;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Aplicacion;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using System.Reflection;
using System.Web.Mvc;

namespace SIRO.App_Start
{
    public class IoCConfig
    {
        public static IContainer Container { get; set; }

        public static T GetInstance<T>()
        {
            return Container.Resolve<T>();
        }

        public static void Configure()
        {
            var builder = new ContainerBuilder();

            RegisterRepositories(builder);
            RegisterServices(builder);
            RegisterControllers(builder);

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<AgenciasApp>().As<IAgenciasApp>().SingleInstance();
            builder.RegisterType<AreasApp>().As<IAreasApp>().SingleInstance();
            builder.RegisterType<CausaRiesgoApp>().As<ICausaRiesgoApp>().SingleInstance();
            builder.RegisterType<ConstanteApp>().As<IConstantesApp>().SingleInstance();
            builder.RegisterType<ConstSistemaApp>().As<IConstSistemaApp>().SingleInstance();
            builder.RegisterType<ControlProcesoApp>().As<IControlProcesoApp>().SingleInstance();
            builder.RegisterType<CriterioEvaluacionApp>().As<ICriterioEvaluacionApp>().SingleInstance();
            builder.RegisterType<TallerApp>().As<ITallerApp>().SingleInstance();
            builder.RegisterType<AutoevaluacionApp>().As<IAutoevaluacionApp>().SingleInstance();
            builder.RegisterType<GestionIncentivoApp>().As<IGestionIncentivoApp>().SingleInstance();
            builder.RegisterType<InicioApp>().As<IInicioApp>().SingleInstance();
            builder.RegisterType<LineaNegocioApp>().As<ILineaNegocioApp>().SingleInstance();
            builder.RegisterType<MaestroApp>().As<IMaestroApp>().SingleInstance();
            builder.RegisterType<MenuApp>().As<IMenuApp>().SingleInstance();
            builder.RegisterType<MontoPerdidaApp>().As<IMontoPerdidaApp>().SingleInstance();
            builder.RegisterType<PlanAccionApp>().As<IPlanAccionApp>().SingleInstance();
            builder.RegisterType<ProcesoAreaApp>().As<IProcesoAreaApp>().SingleInstance();
            builder.RegisterType<ProductoApp>().As<IProductoApp>().SingleInstance();
            builder.RegisterType<RiesgoInherenteApp>().As<IRiesgoInherenteApp>().SingleInstance();
            builder.RegisterType<RiesgoOperacionalApp>().As<IRiesgoOperacionalApp>().SingleInstance();
            builder.RegisterType<RiesgoResidualApp>().As<IRiesgoResidualApp>().SingleInstance();
            builder.RegisterType<SubProcesoApp>().As<ISubProcesoApp>().SingleInstance();
            builder.RegisterType<SubLineaNegocioApp>().As<ISubLineaNegocioApp>().SingleInstance();
            builder.RegisterType<SubProductoApp>().As<ISubProductoApp>().SingleInstance();
            builder.RegisterType<UsuarioApp>().As<IUsuarioApp>().SingleInstance();
            builder.RegisterType<EventoPerdidaApp>().As<IEventoPerdidaApp>().SingleInstance();
            builder.RegisterType<ReportesApp>().As<IReportesApp>().SingleInstance();
            builder.RegisterType<AuthPowerBiApp>().As<IAuthPowerBiApp>().SingleInstance(); //Added by TORE: 20210528
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<Agencias>().As<IAgencias>().SingleInstance();
            builder.RegisterType<Areas>().As<IAreas>().SingleInstance();
            builder.RegisterType<CausaRiesgo>().As<ICausaRiesgo>().SingleInstance();
            builder.RegisterType<Constantes>().As<IConstantes>().SingleInstance();
            builder.RegisterType<ConstSistema>().As<IConstSistema>().SingleInstance();
            builder.RegisterType<ControlProceso>().As<IControlProceso>().SingleInstance();
            builder.RegisterType<CriteriosEvaluacion>().As<ICriterioEvaluacion>().SingleInstance();
            builder.RegisterType<Taller>().As<ITaller>().SingleInstance();
            builder.RegisterType<Autoevaluacion>().As<IAutoevaluacion>().SingleInstance();
            builder.RegisterType<GestionIncentivo>().As<IGestionIncentivo>().SingleInstance();
            builder.RegisterType<Inicio>().As<IInicio>().SingleInstance();
            builder.RegisterType<LineaNegocio>().As<ILineaNegocio>().SingleInstance();
            builder.RegisterType<Maestro>().As<IMaestro>().SingleInstance();
            builder.RegisterType<Menu>().As<IMenu>().SingleInstance();
            builder.RegisterType<MontoPerdida>().As<IMontoPerdida>().SingleInstance();
            builder.RegisterType<PlanAccion>().As<IPlanAccion>().SingleInstance();
            builder.RegisterType<ProcesoArea>().As<IProcesoArea>().SingleInstance();
            builder.RegisterType<Producto>().As<IProducto>().SingleInstance();
            builder.RegisterType<RiesgoInherente>().As<IRiesgoInherente>().SingleInstance();
            builder.RegisterType<RiesgoOperacional>().As<IRiesgoOperacional>().SingleInstance();
            builder.RegisterType<RiesgoResidual>().As<IRiesgoResidual>().SingleInstance();
            builder.RegisterType<SubProcesos>().As<ISubProceso>().SingleInstance();
            builder.RegisterType<SubLineaNegocio>().As<ISubLineaNegocio>().SingleInstance();
            builder.RegisterType<SubProducto>().As<ISubProducto>().SingleInstance();
            builder.RegisterType<Usuario>().As<IUsuario>().SingleInstance();
            builder.RegisterType<EventoPerdida>().As<IEventoPerdida>().SingleInstance();
            builder.RegisterType<Reportes>().As<IReportes>().SingleInstance();
            builder.RegisterType<AuthPowerBi>().As<IAuthPowerBi>().SingleInstance();
        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
        }

    }
}
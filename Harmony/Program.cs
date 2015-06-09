using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using log4net;
using Harmony.Core;
using Harmony.Core.OAuth;

namespace Harmony
{
    public class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [STAThread]
        public static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            Application.ThreadException += Application_ThreadException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (IsProcessAlreadyOpen())
            {
                Log.Info("Harmony is already open. Exiting...");
                Environment.Exit(1);
            }

            Log.Info("Bootstrapping Harmony...");
            var container = RegisterContainer();
            var meshPresenter = container.Resolve<HarmonyPresenter>();
            
            Log.Info("Starting up Harmony...");
            Application.Run(meshPresenter.View as HarmonyView);
            Log.Info("Exiting Harmony...");
        }

        private static IContainer RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof (CoreAssemblyHandle))).AsImplementedInterfaces();

            builder.RegisterType<AppSettingsHarmonyConfiguration>().As<IGoogleConfiguration>()
                .As<IHarmonyConfiguration>();

            builder.RegisterType<AuthorisationCodeDialogView>().AsImplementedInterfaces();
            builder.RegisterType<SettingsView>().AsImplementedInterfaces();
            builder.RegisterType<SettingsPresenter>().AsImplementedInterfaces();
            builder.RegisterType<HarmonyView>().AsImplementedInterfaces();
            builder.RegisterType<HarmonyPresenter>().AsSelf();

            var container = builder.Build();
            return container;
        }

        private static bool IsProcessAlreadyOpen()
        {
            var name = Process.GetCurrentProcess().ProcessName;

            return Process.GetProcesses().Count(process => process.ProcessName.Equals(name)) > 1;
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Log.Fatal("Unhandled exception caused Harmony to terminate", e.Exception);
        }
    }
}

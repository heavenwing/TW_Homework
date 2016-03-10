using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;

namespace CheckoutConsole
{
    static class Program
    {
        public static IContainer Container;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();
            builder.RegisterType<MainForm>()
                .PropertiesAutowired();
            builder.RegisterType<DefaultResultPrinter>()
                .As<IResultPrinter>();
            Container = builder.Build();

            var mf = Container.Resolve<MainForm>();
            Application.Run(mf);
        }
    }
}

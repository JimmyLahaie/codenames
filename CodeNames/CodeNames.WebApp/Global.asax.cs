using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CodeNames.Cores.Services;
using CodeNames.Interfaces;
using CodeNames.Repositories;
using CodeNames.Utils;
using Ninject;
using Ninject.Web.Common.WebHost;

namespace CodeNames.WebApp
{
    public class MvcApplication :  NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IWordsRepository>().To<WordsRepository>();
            kernel.Bind<IGameBuilder>().To<GameBuilder>();
            kernel.Bind<IRandomUtils>().To<RandomUtils>();
            kernel.Bind<IFileReader>().To<FileReader>();
            return kernel;
        }
    }
}
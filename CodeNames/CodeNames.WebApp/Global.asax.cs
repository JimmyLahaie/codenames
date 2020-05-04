using System.IO;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CodeNames.Cores.Services;
using CodeNames.Models.Interfaces;
using CodeNames.Repositories;
using CodeNames.Utils;
using Ninject;
using Ninject.Web.Common.WebHost;
using IRandomUtils = CodeNames.Interfaces.IRandomUtils;
using IWordsRepository = CodeNames.Interfaces.IWordsRepository;

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
            var gameFolder = Server.MapPath("~/App_Data/Games");
            if (!Directory.Exists(gameFolder))
            {
                Directory.CreateDirectory(gameFolder);
            }
            var kernel = new StandardKernel();
            kernel.Bind<IWordsRepository>().To<WordsRepository>();
            kernel.Bind<IGameBuilder>().To<GameBuilder>();
            kernel.Bind<IGameServices>().To<GameServices>();
            kernel.Bind<IGameRepository>().To<GameRepository>().WithConstructorArgument(gameFolder);
            kernel.Bind<IRandomUtils>().To<RandomUtils>();
            kernel.Bind<IFileReader>().To<FileReader>();
            return kernel;
        }
    }
}
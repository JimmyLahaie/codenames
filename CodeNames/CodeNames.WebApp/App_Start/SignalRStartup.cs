using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CodeNames.WebApp;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace CodeNames.WebApp
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
	
			// Any connection or hub wire up and configuration should go here
			var resolver = new NinjectSignalRDependencyResolver(MvcApplication.CurrentKernel);
			var config = new HubConfiguration();
			config.Resolver = resolver;
	
			app.MapSignalR(config);
		}
	}
	
	internal class NinjectSignalRDependencyResolver : DefaultDependencyResolver
	{
		private readonly IKernel _kernel;
		public NinjectSignalRDependencyResolver(IKernel kernel)
		{
			_kernel = kernel;
		}

		public override object GetService(Type serviceType)
		{
			return _kernel.TryGet(serviceType) ?? base.GetService(serviceType);
		}

		public override IEnumerable<object> GetServices(Type serviceType)
		{
			return _kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
		}
	}
}
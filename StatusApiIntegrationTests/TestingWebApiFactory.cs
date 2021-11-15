using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatusApi;
using StatusApi.Services;
using Moq;
using Microsoft.Extensions.DependencyInjection;

namespace StatusApiIntegrationTests
{
    public class TestingWebApiFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // See if the ISystemTime Service is in the services collection
                var systemTimeDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(ISystemTime));

                // If there is, remove it
                if (systemTimeDescriptor != null)
                {
                    services.Remove(systemTimeDescriptor);
                }
                // And add in a fake version of it.
                var stubbedSystemTime = new Mock<ISystemTime>();
                stubbedSystemTime.Setup(s => s.GetCurrent()).Returns(
                    new DateTime(1969, 4, 20, 23, 59, 00));
                services.AddTransient<ISystemTime>(_ => stubbedSystemTime.Object);
            });
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using br.com.galdino.mocroservice.two.api.Configurations.Injection;
using br.com.galdino.mocroservice.two.api.Filters.Performace;
using br.com.galdino.mocroservice.two.domain.crosscouting.Infrastrscture;
using br.com.galdino.mocroservice.two.utils.Interface.Request;
using br.com.galdino.mocroservice.two.utils.Service.Request;
using Microsoft.Extensions.Options;
using Polly;

namespace br.com.galdino.mocroservice.two
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServices();
            services.AddControllers();

            var timeout = TimeSpan.FromSeconds(50);
            var retry = TimeSpan.FromMilliseconds(600000);


            var infraConfig = new InfrastructureConfig();
            new ConfigureFromConfigurationOptions<InfrastructureConfig>(
                    Configuration.GetSection("Infrastructure"))
                .Configure(infraConfig);
            services.AddSingleton(infraConfig);

            services.AddHttpClient<IWebRequestService, WebRequestService>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(2, _ => retry))
                .AddPolicyHandler(request => Policy.TimeoutAsync<HttpResponseMessage>(timeout))
                    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = AcceptAllCertifications });

            services.AddMvc(options =>
            {
                options.Filters.AddService<PerformaceFilters>();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "br.com.galdino.mocroservice.two", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "br.com.galdino.mocroservice.two v1"));
            }



            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
    }
}

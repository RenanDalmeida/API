using System;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EF_Core_Code_First
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
            //Antes tinha só esse método, mas ai gerou um erro de Loop, pois a classe Pedido tem PedidosProdutos que tem Produto, e a classe Produto tem PedidosProdutos que tem Pedido.
            //services.AddControllers();
            //Agora ficou:
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //Esse método também não tinha e faz nao retornar produtos nulos nos pedidos.
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            //Adicionamos o gerador do Swagger.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Loja API",
                    Description = "API que contém cadastrados no banco de dados produtos e pedidos da loja.",
                    TermsOfService = new Uri("https://naotem.com/naotem"),
                    Contact = new OpenApiContact
                    {
                        Name = "Daniel Mendes do Amaral",
                        Email = "daniel.amaral720@gmail.com",
                        Url = new Uri("https://github.com/DanielMendesdoAmaral"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Copyright",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                //Adiciona os comentários dos métodos dos controllers, passando caminhos e etc.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Coloque isso pro caminho da imagem funcionar.
            app.UseStaticFiles();

            //Usa o Swagger.
            app.UseSwagger();

            //Definimos o endpoint e o nome da versão.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Loja_V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

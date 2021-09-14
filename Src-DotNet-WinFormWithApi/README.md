# 1. Win Form with Http Server

- [1. Win Form with Http Server](#1-win-form-with-http-server)
  - [1.1. Set up](#11-set-up)
  - [2.1. Project Config](#21-project-config)
  - [2.2. Enable DI and add the Http Server](#22-enable-di-and-add-the-http-server)
  - [2.3. Sort out the form](#23-sort-out-the-form)
  - [2.4. Add pages](#24-add-pages)
  - [3.1. All Done](#31-all-done)

---

## 1.1. Set up

Windows forms with http server for API (required from javascript environment)

## 2.1. Project Config

1. Create Windows Forms Project (.NET 6.0 preview)
2. To enable the web server

            <ItemGroup>
                <FrameworkReference Include="Microsoft.AspNetCore.App" />
            </ItemGroup>

## 2.2. Enable DI and add the Http Server

Dont forget the CORS policy!

1. Add a new class Startup.cs
2. Replace the using block with..

            using Microsoft.AspNetCore.Builder;
            using Microsoft.AspNetCore.Hosting;
            using Microsoft.Extensions.Configuration;
            using Microsoft.Extensions.DependencyInjection;

3. Add to Statrup.cs

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DevCorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddControllers();
            services.AddLogging();
            services.AddScoped<Form1>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage()
                .UseRouting()
                .UseCors("DevCorsPolicy")
                .UseEndpoints(endpoints => {
                    endpoints.MapControllers();
                });
        }

4. Add to the bottom of the main method replace Application.Run(new Form1()); with..

        var host = Host.CreateDefaultBuilder(Array.Empty<string>())
                       .ConfigureWebHostDefaults(webBuilder => {
                           webBuilder.UseStartup<Startup>().UseUrls("http://localhost:37964");
                        }).Build();

        //Start everything
        _ = DgtEbWrapper.Init();
        _ = host.RunAsync();
        Application.Run(host.Services.GetRequiredService<Form1>());

5. Replace the using block

            using Microsoft.AspNetCore.Hosting;
            using Microsoft.Extensions.DependencyInjection;
            using Microsoft.Extensions.Hosting;
            using System;
            using System.Windows.Forms;

## 2.3. Sort out the form

1. Add the Microsoft.Extensions.Logging namespace
2. Replace the Form1 class declatation with...

        private readonly ILogger _logger;

        public Form1(ILogger<Form1> logger)
        {
            _logger = logger;
            InitializeComponent();
        }

## 2.4. Add pages

The project type is win form so web items dont appear - use another project to generate controllers and copy them in.  Probably need to figure this out.  an example looks like this.

        using Microsoft.AspNetCore.Mvc;
        using Microsoft.Extensions.Logging;

        namespace DgtCherub
        {
            [Route("api/[controller]")]
            [ApiController]
            public class DgtBoardController : ControllerBase
            {
                private readonly ILogger<DgtBoardController> _logger;

                public DgtBoardController(ILogger<DgtBoardController> logger)
                {
                    _logger = logger;
                }

                [HttpGet]
                [Route("{action}/{string1}/{string2}/{int1:int}")]
                public object TestResponse(string string1 = "none", string string2 = "none", int int1 = -1)
                {
                    return new { TestString1 = string1, TestString2 = string2, TestInt1 = int1, CalledAt = System.DateTime.Now };
                }
            }
        }

## 3.1. All Done

To test the controller hit it with...

    `curl [http://localhost:37964/api/DgtBoard/TestResponse/st1a/st2a/3] ; echo`

---

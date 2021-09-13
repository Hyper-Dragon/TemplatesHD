﻿using CommandLine;
using ConsoleTemplate.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConsoleTemplate
{
    public sealed class Program
    {
        private static IServiceProvider? _serviceProvider;

        private static void RegisterServices()
        {
            ServiceCollection? services = new();
            services.AddLogging();
            services.AddSingleton<Parser>(new Parser((settings) =>
            {
                settings.AllowMultiInstance = true;
                settings.AutoHelp = true;
                settings.AutoVersion = true;
                settings.CaseInsensitiveEnumValues = true;
                settings.CaseSensitive = false;
                settings.EnableDashDash = true;
                settings.HelpWriter = Console.Out;
                settings.IgnoreUnknownArguments = false;
                settings.MaximumDisplayWidth = Console.BufferWidth;
                settings.ParsingCulture = System.Globalization.CultureInfo.InvariantCulture;
            }));

            // Auto register classes tagged with the AutoRegisterAttribute
            foreach (Type? type in (from type in typeof(Program).Assembly.GetTypes()
                                    where !type.IsAbstract
                                    where type.GetCustomAttributes().Where((item) => item.GetType() == typeof(AutoRegisterAttribute)).Any()
                                    select type))
            {
                switch (((AutoRegisterAttribute)type.GetCustomAttributes().First((item) => item.GetType() == typeof(AutoRegisterAttribute))).RegType)
                {
                    case AutoRegisterAttribute.RegistrationType.TRANSIENT:
                        services.AddTransient(type, type);
                        break;
                    case AutoRegisterAttribute.RegistrationType.SCOPED:
                        services.AddScoped(type, type);
                        break;
                    case AutoRegisterAttribute.RegistrationType.SINGLETON:
                        services.AddSingleton(type, type);
                        break;
                }
            }

            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static async Task<int> Main(string[] args)
        {
            int retVal = -1;

            RegisterServices();

            if (_serviceProvider != null)
            {
                retVal = await _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ConsoleApplication>().RunAsync(args);
                DisposeServices();
            }

            return retVal;
        }

        private static void DisposeServices()
        {
            if (_serviceProvider != null && _serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}


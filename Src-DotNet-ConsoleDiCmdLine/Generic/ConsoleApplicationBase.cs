using CommandLine;

namespace ConsoleTemplate.Generic
{
    public abstract class ConsoleApplicationBase
    {
        private readonly Parser _parser;
        private readonly Header _header;
        protected readonly GlobalSettings _globalSettings;
        protected readonly Helpers _helpers;

        public ConsoleApplicationBase(Parser parser, Header header, GlobalSettings globalSettings, Helpers helpers)
        {
            _parser = parser;
            _header = header;
            _globalSettings = globalSettings;
            _helpers = helpers;
        }

        protected virtual async Task PreRunImplAsync(CommandLineOptions commandLineOptions)
        {
            await Task.Run(() =>
            {
                throw new NotImplementedException("This console app has not been fully implemented!");
            });
        }

        protected virtual Task<int> RunImplAsync(CommandLineOptions commandLineOptions)
        {
            throw new NotImplementedException("This console app has not been fully implemented!");
        }

        protected virtual async Task PostRunImplAsync(CommandLineOptions commandLineOptions)
        {
            await Task.Run(() =>
            {
                throw new NotImplementedException("This console app has not been fully implemented!");
            });
        }

        private async Task<int> RunAsync(CommandLineOptions opts)
        {
            if (_globalSettings.ShouldExecutePreRun)
            {
                _helpers.DisplaySection($"Pre-Run Started", true);
                await PreRunImplAsync(opts);
                _helpers.DisplaySection($"Pre-Run Completed", true);
            }

            _helpers.DisplaySection($" Run Started  @ {DateTime.UtcNow.ToLongTimeString()} UTC", true);
            int returnVal = await RunImplAsync(opts);
            _helpers.DisplaySection($"Run Completed @ {DateTime.UtcNow.ToLongTimeString()} UTC", true);
            

            if (_globalSettings.ShouldExecutePostRun)
            {
                _helpers.DisplaySection($"Post-Run Started", true);
                await PostRunImplAsync(opts);
                _helpers.DisplaySection($"Post-Run Completed", true);
            }

            return returnVal;
        }

        public async Task<int> RunAsync(string[] args)
        {
            return await _parser.ParseArguments<CommandLineOptions>(args)
                               .MapResult(async (CommandLineOptions opts) =>
                               {
                                   int returnVal = int.MaxValue;

                                   try
                                   {
                                       Console.ForegroundColor = ConsoleColor.Yellow;
                                       _header.DisplayTitle(true);
                                       Console.ResetColor();

                                       if (_globalSettings.ShouldDisplayParams)
                                       {
                                           _helpers.DisplaySection($"Parameters Start", true);
                                           Console.Write(opts.ToString());
                                           Console.WriteLine("");
                                           _helpers.DisplaySection($"Parameters End", true);
                                       }

                                       if (!_globalSettings.ShouldForceSilentMode && !opts.Silent)
                                       {
                                           Console.Write($"<Continue? [Y/N]>");
                                           
                                           while (returnVal == int.MaxValue)
                                           {
                                               switch (Console.ReadKey(true).Key)
                                               {
                                                   case ConsoleKey.Y:
                                                       Console.WriteLine("");
                                                       returnVal = await RunAsync(opts);
                                                       _helpers.PressToContinue();
                                                       break;
                                                   case ConsoleKey.N:
                                                       Console.Write($"{Environment.NewLine}** Run Terminated **{Environment.NewLine}");
                                                       returnVal = -4;
                                                       break;
                                               }
                                           }
                                       }
                                       else
                                       {
                                           returnVal = await RunAsync(opts);
                                       }
                                   }
                                   catch (Exception ex)
                                   {
                                       HelpersStatic.DisplayException(ex);
                                       returnVal = -3; // Unhandled error
                                   }

                                   return returnVal;
                               },
                               errs => Task.FromResult(-1)); // Invalid arguments
        }
    }
}

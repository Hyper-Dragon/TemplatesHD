using CommandLine;
using ConsoleTemplate.Generic;
using static ConsoleTemplate.Generic.AutoRegisterAttribute;

namespace ConsoleTemplate
{
    [AutoRegister(RegistrationType.SINGLETON)]
    public sealed class ConsoleApplication : ConsoleApplicationBase
    {
        public ConsoleApplication(Parser parser, Header customProcessor, GlobalSettings globalSettings, Helpers helpers) : base(parser, customProcessor, globalSettings, helpers) { }

        protected override async Task PreRunImplAsync(CommandLineOptions commandLineOptions)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(">>>> REPLACE WITH IMPL");
            });
        }

        protected override Task<int> RunImplAsync(CommandLineOptions commandLineOptions)
        {
            _helpers.StartTimedSection(">> Test Section Start");
            Console.WriteLine(">>>> REPLACE WITH IMPL");
            _helpers.EndTimedSection(">> Test Section Completed");
            return Task<int>.FromResult(0);
        }

        protected override async Task PostRunImplAsync(CommandLineOptions commandLineOptions)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(">>>> REPLACE WITH IMPL");
            });
        }

    }
}

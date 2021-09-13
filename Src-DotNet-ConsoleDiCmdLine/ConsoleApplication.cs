namespace ConsoleTemplate;

[AutoRegister(RegistrationType.SINGLETON)]
public sealed class ConsoleApplication : ConsoleApplicationBase
{
    public ConsoleApplication(Parser parser, Header customProcessor, GlobalSettings globalSettings, Helpers helpers) : base(parser, customProcessor, globalSettings, helpers)
    {
    }

    protected override async Task<bool> PreRunImplAsync(CommandLineOptions commandLineOptions)
    {
        await Task.Run(() =>
        {
            Console.WriteLine(">>>> REPLACE WITH IMPL");
        });

        return true;
    }

    protected override async Task<int> RunImplAsync(CommandLineOptions cmdOpts)
    {
        await Task.Run(() =>
        {
            _helpers.StartTimedSection(">> Test Section Start");
            Console.WriteLine(">>>> REPLACE WITH IMPL");
            _helpers.EndTimedSection(">> Test Section Completed");
        });

        return 0;
    }

    protected override async Task<bool> PostRunImplAsync(CommandLineOptions commandLineOptions)
    {
        await Task.Run(() =>
        {
            Console.WriteLine(">>>> REPLACE WITH IMPL");
        });

        return true;
    }

}


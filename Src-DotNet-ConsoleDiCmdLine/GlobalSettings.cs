﻿using ConsoleTemplate.Generic;
using static ConsoleTemplate.Generic.AutoRegisterAttribute;

namespace ConsoleTemplate
{
    [AutoRegister(RegistrationType.SINGLETON)]
    public sealed record GlobalSettings : GlobalSettingsBase
    {
        //public override bool ShouldDisplayHeader { get { return false; } }
        //public override bool ShouldBeepOnEnd { get { return false; } }
        //public override bool ShouldDisplayParams { get { return false; } }
        //public override bool ShouldDisplaySectionHeaders { get { return false; } }
        //public override bool ShouldExecutePreRun { get { return false; } }
        //public override bool ShouldExecutePostRun { get { return false; } }
        //public override bool ShouldForceSilentMode { get { return true; } }
    }
}

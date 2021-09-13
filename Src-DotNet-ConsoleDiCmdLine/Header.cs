﻿namespace ConsoleTemplate;

[AutoRegister(RegistrationType.SINGLETON)]
public sealed class Header : HeaderBase
{
    public Header(GlobalSettings globalSettings) : base(globalSettings) { }

    protected override string[] DisplayTitleImpl()
    {
        string[] headLines = {@"<PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER>",
                                  @"<PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER>",
                                  @"<PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER>",
                                  @"<PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER>",
                                  @"<PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER>",
                                  @"<PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER>",
                                  @"<PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER>",
                                  @"<PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER><PLACEHOLDER>"};

        return headLines;
    }
}


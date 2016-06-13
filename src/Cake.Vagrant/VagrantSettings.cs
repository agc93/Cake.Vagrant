using System;
using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Vagrant
{
    public class VagrantSettings : ToolSettings
    {

    }

    internal interface IVagrantCommandSettings
    {
        Action<ProcessArgumentBuilder> GetToolArguments();
    }

    
}
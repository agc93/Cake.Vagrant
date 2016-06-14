using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

namespace Cake.Vagrant
{
    public class VagrantDockerRunner : VagrantCommandRunner
    {
        public VagrantDockerRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback, VagrantSettings settings) : base(log, runCallback, settings)
        {
        }

        public void Exec(string name, string command)
        {
            
        }
    }

    public class VagrantSnapshotRunner : VagrantCommandRunner
    {
        public VagrantSnapshotRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback, VagrantSettings settings) : base(log, runCallback, settings)
        {
        }
    }
}

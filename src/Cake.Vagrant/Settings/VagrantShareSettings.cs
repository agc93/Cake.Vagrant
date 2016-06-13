using System;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    public class VagrantShareSettings : IVagrantCommandSettings
    {
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(DisableHttp, "disable-http");
                args.Add(HttpPort, "http");
                args.Add(HttpsPort, "https");
                args.Add(EnableSSH, "ssh");
                args.Add(DisableSSHPassword, "ssh-no-password");
                args.Add(SSHPort, "ssh-port");
                args.Add(OneTimeSSH, "ssh-once");
            };
        }

        public bool? DisableHttp { get; set; }
        public int? HttpPort { get; set; }
        public int? HttpsPort { get; set; }
        public bool? EnableSSH { get; set; }
        public bool? DisableSSHPassword { get; set; }
        public int? SSHPort { get; set; }
        public bool? OneTimeSSH { get; set; }
    }
}
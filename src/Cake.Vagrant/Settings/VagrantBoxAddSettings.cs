using System;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    public class VagrantBoxAddSettings : IVagrantCommandSettings
    {
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(Version, "version");
                args.Add(CACertificate, "cacert");
                args.Add(CAPath, "capath");
                args.Add(CertificatePath, "cert");
                args.Add(Clean, "clean");
                args.Add(Force, "force");
                args.Add(Insecure, "insecure");
                args.Add(Provider, "provider");
            };
        }

        public string Version { get; set; }
        public string CACertificate { get; set; }
        public string CAPath { get; set; }
        public string CertificatePath { get; set; }
        public bool? Clean { get; set; }
        public bool? Force { get; set; }
        public bool? Insecure { get; set; }
        public string Provider { get; set; }
    }
}
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

    public static class VagrantBoxAddSettingsExtensions
    {
        public static VagrantBoxAddSettings ConstrainVersion(this VagrantBoxAddSettings settings, string version)
        {
            settings.Version = version;
            return settings;
        }

        public static VagrantBoxAddSettings UseCertificateAuthority(this VagrantBoxAddSettings settings, IDirectory caPath)
        {
            settings.CAPath = caPath.Path.FullPath;
            return settings;
        }

        public static VagrantBoxAddSettings UseCertificateAuthority(this VagrantBoxAddSettings settings,
            FilePath certPath)
        {
            settings.CACertificate = certPath.FullPath;
            return settings;
        }

        public static VagrantBoxAddSettings WithClientCertificate(this VagrantBoxAddSettings settings, FilePath certFile)
        {
            settings.CertificatePath = certFile.FullPath;
            return settings;
        }

        public static VagrantBoxAddSettings AllowInsecure(this VagrantBoxAddSettings settings, bool allow = true)
        {
            settings.Insecure = allow;
            return settings;
        }

        public static VagrantBoxAddSettings UseProvider(this VagrantBoxAddSettings settings, string provider)
        {
            settings.Provider = provider;
            return settings;
        }

        public static VagrantBoxAddSettings Force(this VagrantBoxAddSettings settings, bool force = true)
        {
            settings.Force = force;
            return settings;
        }

        public static VagrantBoxAddSettings CleanFirst(this VagrantBoxAddSettings settings, bool clean = true)
        {
            settings.Clean = clean;
            return settings;
        }
    }
}
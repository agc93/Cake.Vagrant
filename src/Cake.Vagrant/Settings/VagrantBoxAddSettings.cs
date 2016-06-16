using System;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    /// <summary>
    /// Additional settings for <c>vagrant box add</c> command
    /// </summary>
    public class VagrantBoxAddSettings : IVagrantCommandSettings
    {
        /// <summary>
        ///     The version of the box you want to add.
        /// </summary>
        /// <remarks>By default, the latest version will be added</remarks>
        /// <value>
        ///     Can be an exact version number such as "1.2.3" or it can be a set of version constraints like "&gt;= 1.0, &lt;
        ///     2.0"
        /// </value>
        public string Version { get; set; }

        /// <summary>
        ///     The certificate for the CA used to verify the peer. This should be used if the remote end does not use a standard
        ///     root CA.
        /// </summary>
        public string CACertificate { get; set; }

        /// <summary>
        ///     The certificate directory for the CA used to verify the peer. This should be used if the remote end does not use a
        ///     standard root CA.
        /// </summary>
        public string CAPath { get; set; }

        /// <summary>
        ///     A client certificate to use when downloading the box, if necessary.
        /// </summary>
        public string CertificatePath { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to clean any existing temporary files before downloading
        /// </summary>
        /// <remarks>
        ///     This is useful if you do not want Vagrant to resume a download from a previous point, perhaps because the
        ///     contents changed.
        /// </remarks>
        public bool? Clean { get; set; }

        /// <summary>
        ///     When present, the box will be downloaded and overwrite any existing box with this name.
        /// </summary>
        public bool? Force { get; set; }

        /// <summary>
        ///     When present, SSL certificates will not be verified if the URL is an HTTPS URL.
        /// </summary>
        public bool? Insecure { get; set; }

        /// <summary>
        ///     If given, Vagrant will verify the box you are adding is for the given provider.
        /// </summary>
        /// <remarks>
        ///     By default, Vagrant automatically detects the proper provider to use.
        /// </remarks>
        public string Provider { get; set; }

        /// <summary>
        /// Gets the command arguments corresponding to the specified settings
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
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
    }

    /// <summary>
    ///     Fluent extension methods for the <see cref="VagrantBoxAddSettings" /> class
    /// </summary>
    public static class VagrantBoxAddSettingsExtensions
    {
        /// <summary>
        ///     Sets the version or version constraint to use when downloading the box
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="version">A version constraint to use (see <see cref="VagrantBoxAddSettings.Version" />)</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxAddSettings ConstrainVersion(this VagrantBoxAddSettings settings, string version)
        {
            settings.Version = version;
            return settings;
        }

        /// <summary>
        ///     Sets the <see cref="VagrantBoxAddSettings.CAPath" /> property for the CA used to verify the peer
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="caPath">The certificate directory to use</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxAddSettings UseCertificateAuthority(this VagrantBoxAddSettings settings,
            IDirectory caPath)
        {
            settings.CAPath = caPath.Path.FullPath;
            return settings;
        }

        /// <summary>
        ///     Sets the <see cref="VagrantBoxAddSettings.CACertificate" /> property for the CA certificate used to verify the peer
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="certPath">The certificate file to use</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxAddSettings UseCertificateAuthority(this VagrantBoxAddSettings settings,
            FilePath certPath)
        {
            settings.CACertificate = certPath.FullPath;
            return settings;
        }

        /// <summary>
        ///     Sets the <see cref="VagrantBoxAddSettings.CertificatePath" /> property for a client certificate to use when
        ///     downloading the box, if necessary.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="certFile">Path to the cert file to use</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxAddSettings WithClientCertificate(this VagrantBoxAddSettings settings, FilePath certFile)
        {
            settings.CertificatePath = certFile.FullPath;
            return settings;
        }

        /// <summary>
        ///     Enables unverified/untrusted SSL certificates when using HTTPS URLs.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="allow">Whether to allow insecure certs. Defaults to true</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxAddSettings AllowInsecure(this VagrantBoxAddSettings settings, bool allow = true)
        {
            settings.Insecure = allow;
            return settings;
        }

        /// <summary>
        ///     Requests a specific provider as per the <see cref="VagrantBoxAddSettings.Provider" /> property
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="provider">The requested provider</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxAddSettings UseProvider(this VagrantBoxAddSettings settings, string provider)
        {
            settings.Provider = provider;
            return settings;
        }

        /// <summary>
        ///     Forces downloading and overwriting any box with the same name
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="force">Whether to force the download. Defaults to true.</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxAddSettings Force(this VagrantBoxAddSettings settings, bool force = true)
        {
            settings.Force = force;
            return settings;
        }

        /// <summary>
        ///     Enables cleaning any temporary or partial download files first.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="clean">Whether to clean first. Defaults to true.</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxAddSettings CleanFirst(this VagrantBoxAddSettings settings, bool clean = true)
        {
            settings.Clean = clean;
            return settings;
        }
    }
}
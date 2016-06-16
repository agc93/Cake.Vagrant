using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    /// <summary>
    ///     Additional settings for the <c>vagrant package</c> command
    /// </summary>
    public class VagrantPackageSettings : IVagrantCommandSettings
    {
        /// <summary>
        ///     Name or UUID of a VirtualBox VM to package
        /// </summary>
        public string BaseImageName { get; set; }

        /// <summary>
        ///     File name for the output package. Defaults to <c>package.box</c>
        /// </summary>
        public string OutputFile { get; set; }

        /// <summary>
        ///     Additional files to be packaged with the box.
        /// </summary>
        /// <remarks>These can be used by a packaged <see cref="VagrantFile" /> to perform additional tasks.</remarks>
        public IEnumerable<string> AdditionalFilePaths { get; set; } = new string[] {};

        /// <summary>
        ///     Packages a Vagrantfile with the box, that is loaded as part of the Vagrantfile load order when the resulting box is
        ///     used.
        /// </summary>
        public string VagrantFile { get; set; }

        /// <summary>
        ///     Gets the command arguments corresponding to the specified settings
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(BaseImageName, "base");
                args.Add(OutputFile, "output");
                args.Add(VagrantFile, "vagrantfile");
                if (AdditionalFilePaths.Any())
                {
                    args.Add(string.Join(",", AdditionalFilePaths), "include");
                }
            };
        }
    }

    /// <summary>
    ///     Fluent extension methods for the <see cref="VagrantPackageSettings" /> class
    /// </summary>
    public static class VagrantPackageSettingsExtensions
    {
        /// <summary>
        ///     Sets the base image to use when packaging the machine
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="id">VM name or UUID</param>
        /// <returns>The updated settings object</returns>
        public static VagrantPackageSettings UseBaseImage(this VagrantPackageSettings settings, string id)
        {
            settings.BaseImageName = id;
            return settings;
        }

        /// <summary>
        ///     Sets the filename for the package output
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="path">Path to output the package to</param>
        /// <returns>The updated settings object</returns>
        public static VagrantPackageSettings OutputToFile(this VagrantPackageSettings settings, FilePath path)
        {
            settings.OutputFile = path.FullPath;
            return settings;
        }

        /// <summary>
        ///     Adds extra files to be included with the box
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="files">Collection of <see cref="FilePath" /> objects to add to the package</param>
        /// <returns>The updated settings object</returns>
        public static VagrantPackageSettings IncludeInPackage(this VagrantPackageSettings settings,
            params FilePath[] files)
        {
            var paths = settings.AdditionalFilePaths.ToList();
            paths.AddRange(files.Select(f => f.FullPath));
            settings.AdditionalFilePaths = paths;
            return settings;
        }

        /// <summary>
        ///     Includes an additional Vagrantfile with the packaged box
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="vagrantFile"><see cref="FilePath" /> for the Vagrantfile to include</param>
        /// <returns>The updated settings object</returns>
        public static VagrantPackageSettings IncludeVagrantFile(this VagrantPackageSettings settings,
            FilePath vagrantFile)
        {
            settings.VagrantFile = vagrantFile.FullPath;
            return settings;
        }
    }
}
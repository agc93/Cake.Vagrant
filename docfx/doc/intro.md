# Getting Started

Make sure you have Vagrant installed *first*, as the addin (obviously) doesn't include it.

## Including the addin

At the top of your script, just add the following to install the addin:

```
#addin nuget:?package=Cake.Vagrant
```

## Usage

The addin exposes a single property alias `Vagrant` with all the normal Vagrant operations (up, init etc) available as methods. Subcommands are included as properties with their own methods. This keeps things as clean and readable as possible while retaining it's syntactic similarity to the real Vagrant CLI.

|CLI|Cake|
|---|----|
|`vagrant up`|`Vagrant.Up()`|
|`vagrant init`|`Vagrant.Init()`|
|`vagrant docker run`|`Vagrant.Docker.Run()`|
|`vagrant box delete dev`|`Vagrant.Box.Delete("dev")`|

## Settings

Command arguments are generally included as an `Action` object, so you can use the familiar lambda syntax from the `MSBuild` etc aliases. So a more complex example might be:

```
Vagrant.Up(settings =>
             settings.DestroyOnError()
             .InstallProvider()
             .WithProvisioners("chef", "shell")
             .EnableParallel()
             .UseProvider("hyperv"));
```
which is equivalent to:
```
vagrant up --destroy-on-error --install-provider --parallel --provider "hyperv" --provision --provision-with chef,shell
```

Each settings class has full documentation as well as extension methods for complete control with the fluent API.

## Directory switching

To run Vagrant commands in a directory other than the current one, use the `FromPath()` method chain to switch directory:

```
Vagrant.FromPath("./path/to/directory").Up();
Vagrant.FromPath("./path/to/other/dir").Destroy();
```

This method will switch directory only for the current command, and will not affect subsequent commands.
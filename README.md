# Cake.Vagrant
Cake addin to control Vagrant through the CLI

|`master`|`develop`|
|:------:|:-------:|
|![VSTS Build Status][vsts-badge]|![VSTS Develop Status][develop-badge]|

## Getting started

You can find documentation either on [Cake's site](https://cakebuild.net/dsl/vagrant) or [here on GitHub](https://agc93.github.io/Cake.Vagrant/). This should be enough to get started with Vagrant assuming you're familiar with the CLI.

You may find Vagrant's documentation helpful as well, both the [Command line](https://www.vagrantup.com/docs/cli/) and [Getting started](https://www.vagrantup.com/docs/getting-started/) guides.

## Sample script

```
Task("Usage")
    .Does(() => {
        Vagrant.Init("hashicorp/precise64");
        Vagrant.Up(s => s.UseProvider("virtualbox"));
        Vagrant.Suspend();
    });

Task("Destroy")
    .Does(() => {
        Vagrant.Destroy();
        DeleteFile("Vagrantfile");
    });
```

[vsts-badge]: https://vs01.visualstudio.com/_apis/public/build/definitions/09d675bd-0b92-45dc-8a6c-f8c4976b4ef0/18/badge
[develop-badge]: https://vs01.visualstudio.com/_apis/public/build/definitions/09d675bd-0b92-45dc-8a6c-f8c4976b4ef0/19/badge
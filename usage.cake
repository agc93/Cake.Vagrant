#r "dist/build/Cake.Vagrant/Cake.Vagrant.dll"

var target = Argument<string>("target", "Usage");

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

RunTarget(target);
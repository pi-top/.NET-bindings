# .NET Core API for [pi-top \[4\]](https://www.pi-top.com/products/pi-top-4)

Check out [dotnet/iot](https://github.com/dotnet/iot) for loads of device bindings!

[![Build status](https://ci.appveyor.com/api/projects/status/dcv5pwhl9n1vt8pi/branch/master?svg=true)](https://ci.appveyor.com/project/pi-top/pi-top-4-net-core-api/branch/master)

## Quick Installation
### Core
To install and configure your pi-top with core .NET API support, run this from your pi-top:
```sh
pi@pi-top:~ curl -L https://raw.githubusercontent.com/pi-top/pi-top-4-.NET-Core-API/master/setup.sh | bash
```

Note: you should be able to re-run this at a later date to update

### Python Environment
To extend with .NET Jupyter Notebook support, run this from your pi-top:
```sh
pi@pi-top:~ curl -L https://raw.githubusercontent.com/pi-top/pi-top-4-.NET-Core-API/master/setup-jupyter.sh | bash
```

## Quick Tour
Once executed, you will have this repo cloned at
```sh
/home/pi/pi-top-net-api
```

The code is compiled and the latest NuGet packages are located at
```sh
/home/pi/localNuget
```

You can activate the Python environment using the command:
```sh
pi@pi-top:~ source /home/pi/.jupyter_venv/bin/activate
```

## Running Visual Studio Code Insiders and the .NET Interactive notebook extension on the device

You can install VS Code Insiders on your pi-top:
```sh
pi@pi-top:~ wget https://update.code.visualstudio.com/latest/linux-deb-armhf/insider -O code-insiders_1.50.0-1601271790_armhf.deb
pi@pi-top:~ sudo dpkg -i code-insiders_1.50.0-1601271790_armhf.deb
```
Then add the [.NET interactive extension](https://github.com/dotnet/interactive#visual-studio-code) and now you can use .NET Interactive notebooks directly on your pi-top.

## Manual installation steps

The `src` directory contains the code for libraries you can use to create .NET Core apps for the amazing [pi-top4 platform](https://www.pi-top.com/products/pi-top-4). Get one and get creative.

Requires [.NET Core SDK 3.1 LTS for ARM32](./docs/install-dotnet-sdk.md)

The libraries comes along with [`dotnet interactive`](https://github.com/dotnet/interactive/) integration so you can use notebooks to explore the power of pi-top.

 * install the `dotnet interactive` tool as shown [here](./docs/install-dotnet-interactive.md) 
 * install [Camera library dependencies](./docs/install-camera-dependencies.md)

To use the notebook examples in the folder `examples/notebooks` you will need jupyter lab 
* install [jupyter and jupyter lab modules](./docs/install-jupyter.md)

To use the notebook sampples in the folder `examples/vs-code` you will need vs-code and vs-code extension on your local machine
* install [vscode extension](./docs/vscode-extension.md)

Build the libraries and packages

 * build the project `> dotnet build`
 * if you do not have it, create the folder `/home/pi/localNuget`
 * pack the projects with `> sh tools/pack.sh 1.1.1` it will package the project into the `/home/pi/localnuget` using version 1.1.1


## Example

![image](https://user-images.githubusercontent.com/375556/80700336-71322400-8ad5-11ea-8eb1-6122c9cac554.png)

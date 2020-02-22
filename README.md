# JimmyDeploy

JimmyDeploy is a standalone application that helps in setting up new Windows installations. It is built as a replacement for the traditional Windows Server Essentials Connector for less tech savvy users to be able to get a computer up and running. 

## Features
  
* Joins the computer to an Active Directory
* Changes the computer's network name
* Installs applications via exe or msi
* Full install customisation through a JSON file

## Usage

A config file must be loaded for JimmyDeploy to operate. By default when it is run it will by look for a JimmyDeploy.json file to load in the apps starting directory. If not found, it will ask for you to locate a configuration to load. See the [Example.json](Example.json) for an example configuration.

JimmyDeploy will ask for any additional details such as Domain Admin credentials and allows you to select commands that you do not want to run.

## Download

Coming soon

## Build From Source

> JimmyDeploy is in early stages of development, so chances are you will run into issues using it. It is highly recommended not to use it in a production environment unless you understand the risks.

To build JimmyDeploy from source, you will need Visual Studio 2019 with the .NET Desktop Development workload installed.

Open the JimmyDeploy.sln in Visual Studio and build as you would any other .NET program.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## Authors

* **James Dearlove** - [JamesDearlove](https://github.com/JamesDearlove)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Acknowledgments

* [MahApps.Metro](https://mahapps.com/)
* [Newtonsoft.Json](https://www.newtonsoft.com/json)
* [Font Awesome](https://fontawesome.com/)

# Clean Architecture Template

The purpose of this template is to provide a starter template for a Clean Architecture solution in ASP.NET Core. The sample consists of a simple Hello World web application with a Postgres database to demonstrate this Clean Architecure implementation. The application and the database is containerized to quickly run out of the box, and debug and test.

## Getting Started

Simply fork this repository and rename the project and solutions files. The dockerfile and docker compose files need to be modified to reference the correct path and project files.

## Local Development Setup

To run the Clean Architecture application locally you need the following:

* Windows, macOS or Linux development machine
* Docker (for Windows, Docker needs to be set to Linux container mode)
* Visual Studio, Visual Studio Code, or Rider
  - For Visual Studio, the "Container development tools" component of Visual Studio is required, which can be added from the Visual Studio Installer
* The latest version of .NET Core 6 SDK (or higher)

### Running locally

The solution comes with a docker compose project containing various docker compose stacks that can be run from the command line or one of the IDEs above. The easiest and recommended IDE for debugging is Visual Studio Code. See [Debugging in Visual Studio Code](#debugging-in-visual-studio-code) for instructions on debugging the application in Visual Studio Code.

To run the application locally, open a Bash terminal and type `.\run-local.sh` and press enter. The first time stack is
run, it will go through the process of pulling down all the required images. Once the stack is up and everyting is configured correctly, you will begin to see application logs in the terminal window. A ReDoc page containing the API documention will also open automatically.

### Running tests

_Instructions pending_

### Debugging in Visual Studio Code

_Instructions pending_

## Guidlines

The following sections describes the different layers in Clean Architecture and how they are used in this template.
Each sections will also describe which code should go in what layer.
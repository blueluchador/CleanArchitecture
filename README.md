# Clean Architecture Template

The purpose of this template is to provide a starter template for a Clean Architecture solution in ASP.NET Core. The sample consists of a simple Hello World web application with a Postgres database to demonstrate this Clean Architecure implementation. The application and the database is containerized to quickly run out of the box, and debug and test.

## Getting Started

Simply fork this repository and rename the project and solutions files. The dockerfile and docker compose files need to be modified to reference the correct path and project files.

## Local Development Setup

To run the Clean Architecture application locally you need the following:

* Windows, macOS or Linux development machine
  - For Windows, [Git Bash](https://git-scm.com/download/win) or [WSL](https://learn.microsoft.com/en-us/windows/wsl/install) is recommended
* Docker (for Windows, Docker needs to be set to Linux container mode)
* Visual Studio, Visual Studio Code, or Rider
  - For Visual Studio, the "Container development tools" component of Visual Studio is required, which can be added from the Visual Studio Installer
* .NET Core SDK version 6.0.414 or higher

### Running locally

The solution comes with a docker compose project containing various docker compose stacks that can be run from the command line or one of the IDEs above. The easiest and recommended IDE for debugging is Visual Studio Code. See [Debugging in Visual Studio Code](#debugging-in-visual-studio-code) for instructions on debugging the application in Visual Studio Code.

Open a Bash terminal and type `.\run-local.sh` to run the application locally. The first time the stack is run, it will go through the process of pulling down all the required images. Once the stack is up you will begin to see application logs in the terminal window. A ReDoc page containing the API documention will also open automatically.

To manually start and teardwon the application locally, the following commands can be executed, respectively.

```bash
# Running container locally
$ docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build --always-recreate-deps

# Teardown the container
$ docker-compose -f docker-compose.yml -f docker-compose.override.yml down -v --remove-orphans
```

Once the container stack is up and running, you can browse to `http://localhost:5999/api-docs` to view the API documentation.

### Running tests

_Instructions pending_

### Debugging in Visual Studio Code

_Instructions pending_

## Guidlines

The following sections describes the different layers in Clean Architecture and how they are used in this template.
Each sections will also describe which code should go in what layer.
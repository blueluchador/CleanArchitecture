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

## Running Locally

The solution comes with a docker compose project containing various docker compose stacks that can be run from the command line or one of the IDEs above. The easiest and recommended IDE for debugging is Visual Studio Code. See [Debugging in Visual Studio Code](#debugging-in-visual-studio-code) for instructions on debugging the application in Visual Studio Code.

Open a Bash terminal and type `.\run-local.sh` to run the application locally. The first time the stack is run, it will go through the process of pulling down all the required images. Once the stack is up you will begin to see application logs in the terminal window. A ReDoc page containing the API documention will also open automatically.

### Run local options

|Option|Description|
|:--------:|--|
|_none_|Run the application locally. Press <kbd>Ctrl</kbd>+<kbd>C</kbd> to terminate the running application.|
|`--test` or `-t`|Run the end-to-end and integration tests when the stack is up. The stack will be torn down and removed when the tests have completed.|
|`--debug` or `-d`|Start the stack in debug mode for debugging in VS Code. Press <kbd>Ctrl</kbd>+<kbd>C</kbd> to terminate the debug session.|

### Compose stacks from command line

The docker compose stacks can also be built and run manually from the command line. The following are the commands for composing and tearing down the stacks.

```bash
# Run application locally
$ docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build --always-recreate-deps

# Teardown application
$ docker-compose -f docker-compose.yml -f docker-compose.override.yml down -v --remove-orphans

# Run tests
$ docker-compose -f docker-compose.tests.yml up --build --always-recreate-deps

# Teardown tests
$ docker-compose -f docker-compose.tests.yml down -v --remove-orphans

# Run application in debug mode
$ docker-compose -f docker-compose.yml -f docker-compose.debug.yml up --build --always-recreate-deps

# Teardown debug mode
$ docker-compose -f docker-compose.yml -f docker-compose.debug.yml down -v --remove-orphans
```

## Debugging in Visual Studio Code

VS Code supports debugging applications running in Docker. For this to work the docker stack must be running in debug mode. The run-local script has the option to run the application in debug mode.

1) In a Bash terminal type `./run-local.sh -d` or `./run-local.sh --debug`
2) Wait for the build to complete and the docker compose stack to start running (the message "You can now start your debugging session in VS Code." will display when ready)
3) In the VS Code activity sidebar, select the **Run and Debug** icon to bring up the **Run and Debug** view
4) Make sure the "Debug in Docker" dropdown configuration is selected, and click the green **Play** icon to start debugging

## Guidelines

The following sections describes the different layers in Clean Architecture and how they are used in this template.
Each sections will also describe in what layer code should belong.

Now my favorite and easiest to understand Clean Architecture using .NET is in [this blog post](Clean Architecture with .NET and .NET Core — Overview) by [Ashish Patel](https://medium.com/@iamaashishpatel).

I'll be referencing his diagrams, one of which is good represntation of Clean Architecture shown below.

![Clean Architecture](https://miro.medium.com/v2/resize:fit:720/format:webp/1*GiykAevGwTtP_6LQ1CB1Ug.png)

The next diagram I'll be refrencing is an excellent guide to what a .NET Clean Architecture project structure should look like.

![Clean Architecture Project Structure](https://miro.medium.com/v2/resize:fit:720/format:webp/1*Vk7quy-rCWYom9kJ00V4sw.png)

### Domain

_In progress_

### Application

_In progress_

### Infrastructure (and Persistance)

_In progress_

### API

_In progress_
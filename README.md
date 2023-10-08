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

Open a Bash terminal and type `.\run-local.sh` to run the application locally. The first time the stack is run, it will go through the process of pulling down all the required images. Once the stack is up you will begin to see application logs in the terminal window. A ReDoc page containing the API documention will also open automatically. <kbd>Ctrl</kbd>+<kbd>C</kbd> terminates and tears down the application.

To manually start the application locally, type the following command in the terminal window.

```bash
docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build --always-recreate-deps
```

When the container stack is up and running, browse to `http://localhost:5999/api-docs` to view the API documentation. Press <kbd>Ctrl</kbd>+<kbd>C</kbd> when done to terminate the application.

To teardown the application, type the following command.

```bash
docker-compose -f docker-compose.yml -f docker-compose.override.yml down -v --remove-orphans
```

### Running tests

The same script for running the application locally can also be used to run end-to-end tests, including integration tests. Type `.\run-local.sh -t` or `.\run-local.sh --test` to run the tests. The tests are torn down immediately after all tests are completed.

As with running the application locally, tests cans also be run manually by typing the following in a terminal window.

```bash
docker-compose -f docker-compose.tests.yml up --build --always-recreate-deps
```

The tests results are logged to the terminal window. When done, press <kbd>Ctrl</kbd>+<kbd>C</kbd> to terminate the test, and type the following to tear down the stack.

```bash
docker-compose -f docker-compose.tests.yml down -v --remove-orphans
```

### Debugging in Visual Studio Code

VS Code supports debugging applications running in Docker. For this to work the docker stack must be running in debug mode. The run-local script has the option to run the application in debug mode.

1) In a Bash terminal type `./run-local.sh -d` or `./run-local.sh --debug`
2) Wait for the build to complete and the docker compose stack to start running (the message "You can now start your debugging session in VS Code." will display when ready)
3) In the VS Code activity sidebar, select the **Run and Debug** icon to bring up the **Run and Debug** view
4) Make sure the "Debug in Docker" dropdown configuration is selected, and click the green **Play** icon to start debugging

When you are done debugging, press <kbd>Ctrl</kbd>+<kbd>C</kbd> to terminate the debug session. The docker compose stack is automatically torn down.

## Guidlines

The following sections describes the different layers in Clean Architecture and how they are used in this template.
Each sections will also describe which code should go in what layer.
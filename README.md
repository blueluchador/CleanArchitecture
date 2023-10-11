# Clean Architecture Template

The purpose of this template is to provide a starter template for a Clean Architecture (CA) solution in ASP.NET Core. The sample consists of a simple Hello World web application with a Postgres database to demonstrate this CA implementation. The application and the database is containerized to quickly run locally out of the box, and to debug and test.

## Getting Started

Simply fork this repository and rename the project and solutions files. The dockerfile and docker compose files need to be modified to reference the correct path and project files.

## Local Development Setup

To run the application locally you need the following:

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
|:--:|---------|
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

The following sections describes the different layers in CA and how they are used in this template. One of my favorite and easiest articles on CA using .NET is from [this blog post](https://medium.com/dotnet-hub/clean-architecture-with-dotnet-and-dotnet-core-aspnetcore-overview-introduction-getting-started-ec922e53bb97) by [Ashish Patel](https://medium.com/@iamaashishpatel).

The following diagrams from the blog post depicts a good represnetation of CA. For those familiar with CA, it doesn't exactly look like the diagrams you will find in most explanations. For one there is no "Frameworks & Drivers" layer, but other than that the rest of it is essentially the same, and a lot more clearer.

![Clean Architecture](https://miro.medium.com/v2/resize:fit:720/format:webp/1*GiykAevGwTtP_6LQ1CB1Ug.png)

I like that the "Frameworks & Drivers" was left out of this diagram because that usually consists of the Web, database, UI, and other external components. The only thing that I disagree with is the User Interface being a part of the _Interface Adapter_ layer of CA. I like to leave that out of this layer becuase I am a strong beliver in micro frontends. I strictly limited this to the API for creating microservices.

The next diagram I am refrencing from the blog post is an excellent guide to what a .NET CA project structure should look like. Use this diagram and the table that follows as a guide to determine where code should belong.

![Clean Architecture Project Structure](https://miro.medium.com/v2/resize:fit:720/format:webp/1*Vk7quy-rCWYom9kJ00V4sw.png)

|Project / Namespace |CA Layer|Description|Implements|
|:--:|:--:|---------|---------|
|CleanArchitecture<br/>.Domain|Entities|This is the Enterprise Business Rules layer, and it holds core business or domain specific business rules.|<ul><li>Entities</li><li>Aggregates</li><li>Value objects</li><li>Domain events</li><li>Enum</li><li>Constants</li></ul>|
|CleanArchitecture<br/>.Application|Use Cases|This is the Application Business Rules layer, and it holds every use case of the application. It also implments the application business logic.|<ul><li>Abstractions/Contracts</li><li>Application Services/Handlers</li><li>DTO objects</li><li>Mappers</li><li>Validators</li><li>Exceptions</li><li>Behaviors</li><li>Specifications</li></ul>|
|CleanArchitecture<br/>.Infrastructure|Interface Adapters|This layer is responsible for implementing the contracts defined in the application layer. It also implements abstractions and integrations to systems, downstream services, and third-party libraries.|<ul><li>Identity services</li><li>File storage</li><li>Queue storate</li><li>Notification services</li><li>Other third-party services</li></ul>|
|CleanArchitecture<br/>.Infrastructure.Persistence|Interface Adapters|This layer handles database concerns and other data access operations.|<ul><li>ORM</li><li>Data connectors</li><li>Repositories</li><li>Data seeding</li><li>Data migrations</li><li>Caching</li></ul>|
|CleanArchitecture<br/>.API|Interface Adapters|This layer only focuses on the API. It is preferred to handle the presentation externally (or a separate project).|<ul><li>API controllers</li><li>Requests/Responses</li><li>Middleware</li><li>Filters/Attributes</li><li>Web/API utilities</li></ul>|
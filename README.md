# Employee Time Tracking Backend
![Callibrity Logo](/screenshots/CallibrityLogoNavy.png)

## Technologies used
![Stack Overview](/screenshots/StackOverviewBackend.png)

- [C# \- Web API template](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio)
- [PostgreSQL](https://www.postgresql.org/docs/9.1/git.html)
- [Xunit testing](https://xunit.net/docs/getting-started/netcore/cmdline)
- [Docker](https://www.docker.com/get-started)

## Database
We are using a postgres SQL database in this project. It is isolated from other Callibrity data and solely intended to serve this project.


- Staging and Production databases are located on Google Cloud. To view the database settings, go to https://console.cloud.google.com/ \-> SQL -> intranet-pgsql.
- Make sure you are using your callibrity google account and the 'intranet' project is selected. 

To access the database and view tables, download a database tool such as [SQuirreL](http://squirrel-sql.sourceforge.net/) (lightweight and recommended) or [DBeaver](https://dbeaver.io/) (Only recommended if you are already familiar with it or more robust Relational Database Management Tools)

## Environment Setup
### Prerequisites
- [VS Code](https://code.visualstudio.com/) is the preferred IDE for this project (it is lightweight and especially configurable for the frontend of the project). It will recommend C#, dotnet and likely other extensions when you open this project - install these.
- [Docker Desktop](https://www.docker.com/get-started) should be installed on your machine follow the most current documentation here to get started with docker.
  - it's recommended to try their hello-world project first to make sure it is running on your machine.
- you may want to install [.Net Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1), It is not necessary if using the recommended docker-compose to spin up, but in case a reason arises to run it locally outside the container.



## Set up
### Docker-compose (preferred)
1. Open the project at the root level in VS Code
2. in vs code select Terminal -> New Terminal
3. ```cd scripts/ && ls```
4. run the appropriate script based on your operating system with sudo ( e.g ```sudo bash mac-certs.sh``` ) you will be prompted for your system password multiple times as the script progresses
5. Change directories in the terminal ```cd ../ETT-Backend/``` to the /ETT-Backend/ETT-Backend/ folder where the docker-compose.yaml is located
6. run ```docker-compose up``` from this location.
7. Pull down the ETT_Database repo and use ```dotnet run``` from this project to populate the database that docker-compose just spun up. More detail on this step can be found in that repo if needed - but it should know where to point already so touching config files shouldn't be required.

### Run Locally outside container 
#### (Not recommended, but could be useful in edge cases such as troubleshooting a potential docker problem)
1. Clone repo
2. Open cloned project in IDE of choice
3. Populate appsettings
    - Create a file named appsettings.json in the main project directory (ETT-Backend/ETT-Backend)
    - Copy the contents of appsettings.Development.json and paste it into the newly created appsettings.json
    - Ask a team member to help you fill in the values since this is a skeleton of the settings
4. If using VS Code:
    - In the extensions tab, install the following extensions: 
        - C# for Visual Studio Code
        - C# IDE Extensions for VSCode
        - EditorConfig for VSCode
    - In a terminal (in the root project folder), run 'dotnet build' to build the project and sync dependencies
    - To run, you can either use 'dotnet run' in the terminal or use the menu: Run \-> Run Without Debugging

## Troubleshooting
### refresh on save
When making modifications running the project with docker-compose sometimes a cached artifact will prevent the project from reflecting changes. 
If this occurs you can run the following from the directory where the docker-compose.yaml file is located.
```
docker-compose down
docker-compose build --no-cache
docker-compose up
```
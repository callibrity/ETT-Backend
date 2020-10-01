# Employee Time Tracking Backend
![Callibrity Logo](/screenshots/CallibrityLogoNavy.png)

## Technologies used
![Stack Overview](/screenshots/StackOverviewBackend.png)

- [C# \- Web API template](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio)
- [PostgreSQL](https://www.postgresql.org/docs/9.1/git.html)
- [Xunit testing](https://xunit.net/docs/getting-started/netcore/cmdline)
- [Docker](https://www.docker.com/get-started)

## Environments
We currently have 3 Google Cloud Platform (GCP) projects associated with this site. 
- [intranet](https://console.cloud.google.com/run?organizationId=876666147511&project=intranet-277714) is where we store items that are shared among environments (credentials, container images, etc).
- [intranet-staging](https://console.cloud.google.com/run?organizationId=876666147511&project=intranet-staging-285714) is where images get deployed automaticallty when they are merged into master in this repository. It interacts with staging environments for the backend of the application and is useful for end to end testing before deploying to production
- [intranet-production](https://console.cloud.google.com/run?project=intranet-production-285714) is meant to be the "live" site in which deployments will only be made manually when it is deemed appropriate.

## Hosting
The site is currently hosted on Google Cloud Run at the following URL's: 
- [Staging](https://intranet-api-uxl72aopia-uk.a.run.app)
- [Production](https://intranet-api-yygv4n2zyq-uk.a.run.app)

To view the Cloud Run instances, go to https://console.cloud.google.com/ -> Cloud Run -> intranet-app within the respective project/environment.

## Deployment
Staging is automatically deployed upon a push to the master branch.  

To deploy to production, go to the github repository then under Code -> Tags -> Releases, click "Draft a New Release." Once you create a new release, github actions will deploy it to production. 

For both scenarios, github will create an image within the GCP [Container Registry](https://console.cloud.google.com/gcr/images/intranet-277714/GLOBAL/intranet-app?project=intranet-277714&gcrImageListsize=30) which will then be deployed to the appropriate environment.

## Database
We are using a postgres SQL database located on Google Cloud. To view the database settings, go to the SQL tab in the [GCP console](https://console.cloud.google.com)
- Make sure you are using your callibrity google account and the correct project is selected. 

To access the database and view tables, download a database tool such as [SQuirreL](http://squirrel-sql.sourceforge.net/) (lightweight and recommended) or [DBeaver](https://dbeaver.io/) (Only recommended if you are already familiar with it or more robust Relational Database Management Tools)

## Environment Setup
### Prerequisites
- [VS Code](https://code.visualstudio.com/) is the preferred IDE for this project (it is lightweight and especially configurable for the frontend of the project). Please install the following extensions:
  - C# for VSCode
  - C# IDE Extensions for VSCode
  - EditorConfig for VSCode
  - .NET Core Test Explorer for VSCode (you only need this if you wish to run the ETT-Backend.Tests project, you will need to change the dotnet-test-explorer.testProjectPath VSCode setting to the relative path of the ETT-Backend tests directory if you're not directly in it. see extension docs for clarification if needed.)
  - Docker
- [Docker Desktop](https://www.docker.com/get-started) should be installed on your machine follow the most current documentation here to get started with docker.
  - it's recommended to try their hello-world project first to make sure it is running on your machine.
- You may want to install [.Net Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1), It is not necessary if using the recommended docker-compose to spin up, but in case a reason arises to run it locally outside the container.


## Set up
### Docker-compose (preferred)
1. Open the project at the root level in VS Code
2. Use a terminal or file explorer to run the certs.sh script matching your OS located in ETT-Backend/scripts. You may be prompted for your system password multiple times as the script progresses
3. To run, use a terminal to run ```docker-compose build && docker-compose up``` in the /ETT-Backend/ETT-Backend/ folder where the docker-compose.yaml is located.   
4. Pull down the [ETT_Database repo](https://github.com/callibrity/ETT_Database/blob/master/README.md) and use ```dotnet run``` from this project to populate the database that docker-compose just spun up. More detail on this step can be found in that repo if needed. This step is only necessary for the first run or when the database schema changes.

### Run Locally outside container 
#### (Not recommended, but could be useful in edge cases such as troubleshooting a potential docker problem)
1. Clone repo
2. Open cloned project in IDE of choice
4. If using VS Code:
    - In the extensions tab, install the following extensions: 
        - C# for Visual Studio Code
        - C# IDE Extensions for VSCode
        - EditorConfig for VSCode
    - In a terminal (in the root project folder), run 'dotnet build' to build the project and sync dependencies
    - To run, you can either use 'dotnet run' in the terminal or use the menu: Run \-> Run Without Debugging

## Run Code Coverage Report
1. Navigate to ETT-Backend.Tests directory
2. dotnet test --collect:"XPlat Code Coverage"
3. Move file coverage.cobertura.xml from newly generated folder in TestResults
   to the root of the TestResults folder
4. dotnet reportgenerator -reports:TestResults/coverage.cobertura.xml -reporttypes:HTML -targetdir:Coverage
5. Open Coverage/index.html in browser to view report
    - In a terminal (in the root project folder), run `dotnet build` to build the project and sync dependencies
    - To run, you can either use `dotnet run` in the terminal or use the menu: Run -> Run Without Debugging

## Troubleshooting
### refresh on save
When making modifications running the project with docker-compose sometimes a cached artifact will prevent the project from reflecting changes. 
If this occurs you can run the following from the directory where the docker-compose.yaml file is located.
```
docker-compose down
docker-compose build --no-cache
docker-compose up
```

## Deployment Notes
Must be deployed with environment variables API_USERNAME and API_PASSWORD in order for the ETL process to send data through the Table Controller. When sending POST requests to the Table endpoint, the username and password need to be sent base64 encoded as an Authorization header. In Postman, navigate to Authorization tab, select Basic Auth from the dropdown, and supply the same username and password as was deployed with the app.


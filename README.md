# Employee Time Tracking Backend

## Technologies used
- C# \- Web API template
- Xunit testing

## Database
We are using a postgres SQL database located on Google Cloud. To view the database settings, go to https://console.cloud.google.com/ \-> SQL -> intranet-pgsql.
- Make sure you are using your callibrity goole account and the 'intranet' project is selected. 

To access the database and view tables, download a database tool such as DBeaver or SQuirrel and ask someone on the team for credentials to connect.

## Environment Setup
### Prerequisites
- If not using Visual Studio, download .Net Core 3.1 (https://dotnet.microsoft.com/download/dotnet-core/3.1)

### Set up
1. Clone repo
2. Open cloned project in IDE of choice
3. Populate appsettings
    - Create a file named appsettings.json in the main project directory (ETT-Backend/ETT-Backend)
    - Copy the contents of appsettings.Development.json and paste it into the newly created appsettings.json
    - Ask a team member to help you fill in the values since this is a skeleton of the settings.
4. If using VS Code:
    - In the extensions tab, install the following extensions: 'C# for Visual Studio Code' and 'C# IDE Extensions for VSCode'
    - In a terminal (in the root project folder), run 'dotnet build' to build the project and sync dependencies
    - To run, you can either use 'dotnet run' in the terminal or use the menu: Run \-> Run Without Debugging

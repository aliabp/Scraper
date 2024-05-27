# Scraper Solution

## Overview
This repository contains a solution for scraping search results from Google. It includes three projects: a backend API for scraping, a test project for unit and integration testing, and a desktop application with a user interface to interact with the API.

## Projects
1. **Scraper.api**: A backend Web API built to consume HTTP requests, perform web scraping on search results, and return the positions of a target URL as a string.
2. **Scraper.api.test**: A test project developed using xUnit and Moq for unit and integration testing of the Scraper API.
3. **Scraper.wpf**: A desktop application built with WPF that provides a user interface for calling the Scraper API and managing responses.

## Design Patterns
- **Option Pattern**: Used to handle configuration settings and provide strongly typed access to configuration data.
- **Singleton Pattern**: Ensures a class has only one instance and provides a global point of access to it.
- **Facade Pattern**: Provides a simplified interface to a complex subsystem.

## API Endpoint
You can interact with the API at the following endpoint:
```
POST https://yourserver/api/v1/Scraper
```

### JSON POST Request
```json
{
  "url": "targetUrl",
  "keywords": "your keywords"
}
```

## Requirements
- .NET 6
- Chrome version 125 should be installed on your host to selenium web driver perform scrapping HtML and Javascript.
- SSL is disabled for development purposes.
- If necessary, update the URL of the Scraper.api project in the `App.config` file of the Scraper.wpf project to match the URL and port where Scraper.api is running on your host.

## Running the Project
To run the project:
1. Ensure that **Scraper.api** is running.
2. Then, start **Scraper.wpf**.

You can run these projects using `dotnet run`, through your IDE, or by hosting them on your web server.

## Running Tests
Navigate to the test project directory and run:
```
dotnet test
```

## Configuration
To configure the `Scraper.wpf` project:
- Open `App.config`.
- Replace the URL of the Scraper.api project with the appropriate URL and port where Scraper.api is hosted on your machine.

## Notes
- Make sure to update the `App.config` file in the Scraper.wpf project if the API URL changes.

For any issues or contributions, please submit a pull request or open an issue on the repository page.


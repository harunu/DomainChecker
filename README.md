# Domain Checker

This is for Domain Checker Backend and UI documentation

## Getting Started

These instructions will cover usage information and for the docker-compose setup of your project.

### Prerequisites

Before you begin, ensure you have met the following requirements:
- You have installed the latest version of [Docker](https://www.docker.com/products/docker-desktop) and Docker Compose.

## Configuration

Before running the application locally, you may need to adjust some configurations:

- For local testing, the backend IIS URL can be configured in `domainService.js`. This is only necessary if you need to point your UI service to a different backend location during development. Note: .env configuration has not been implemented. 

## Screenshots
![image](https://github.com/harunu/DomainChecker/assets/34203838/c02cc7b4-b4e0-48fe-a98f-4ccb62339125)
![image](https://github.com/harunu/DomainChecker/assets/34203838/a4965ecb-6d59-4989-9976-05ecbba385b1)
## Running the Application

To start the application, run the following command in the root directory of your project:

```bash
git clone https://github.com/harunu/DomainChecker.git
cd yourprojectdirectory (C:\Users\Dell\Documents\GitHub\DomainChecker)
docker-compose up --build



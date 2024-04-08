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

## Running the Application

To start the application, run the following command in the root directory of your project:

```bash
git clone https://github.com/harunu/DomainChecker.git
cd yourprojectdirectory (C:\Users\Dell\Documents\GitHub\DomainChecker)
docker-compose up --build
![image](https://github.com/harunu/DomainChecker/assets/34203838/92bbb9cf-28fc-432a-aa0d-bebe89b2122b)

![image](https://github.com/harunu/DomainChecker/assets/34203838/685148a8-cc98-46b9-81b4-db9a2218d3bb)



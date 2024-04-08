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

### Possible Improvements and Shortcomings

State Management Could Be Improved, Especially with the Last-Minute Additions in favorites.js:
The state management in the application, particularly with the modifications introduced late in the development of favorites.js, could be better organized. Integrating a more structured state management approach or utilizing context/providers more effectively could enhance maintainability and scalability.

Componentization:
The application could benefit from further breaking down the UI into smaller, reusable components. This approach would not only improve the readability and maintainability of the code but also facilitate easier testing and reusability across the application.

Unit Testing:
More comprehensive unit tests could have been written to ensure the reliability and robustness of individual components and functions. Increased unit testing coverage would help catch bugs early in the development process and aid in ensuring that future changes do not introduce regressions.

Babel Transition Issues:
There were some challenges encountered with Babel transitions that took time to resolve. Streamlining the build and transpilation process using Babel could help avoid similar issues in the future, ensuring smoother development and deployment processes.

Backend Logging and Exception Handling Could Be Enhanced:
The backend could benefit from improved logging mechanisms and more robust exception handling. Implementing more detailed logging would aid in troubleshooting and monitoring, while enhanced exception handling would make the application more resilient to errors and unexpected conditions.

Unit Tests on the Backend Could Be Increased:
Just like with the frontend, the backend could use a broader set of unit tests to cover more scenarios and edge cases. Expanding the unit tests on the backend would further safeguard the application's integrity and functionality.

Lack of Endpoint for Completely Removing a Domain:
Currently, the application supports updating the favorites status (true/false) but does not provide functionality to completely remove a domain from favorites. Adding a dedicated button and corresponding backend endpoint for this purpose could improve the application's flexibility and user experience.

Additional Information Could Be Displayed on the Pages:
Incorporating more information and data presentation on the pages could enhance the user experience by providing users with more context and details at a glance.

User Experience (UX) Could Be Further Improved:
There is room for enhancement in the UX design of the application. Focusing on user feedback, simplifying the user journey, and improving the overall aesthetic and functionality could make the application more engaging and easier to use.

Integration Tests:
Conducting integration tests would ensure that different parts of the application work together seamlessly. Integration testing would help validate the interaction between components, services, and the backend, providing an additional layer of confidence in the application's overall performance and reliability.

![image](https://github.com/harunu/DomainChecker/assets/34203838/c02cc7b4-b4e0-48fe-a98f-4ccb62339125)
![image](https://github.com/harunu/DomainChecker/assets/34203838/a4965ecb-6d59-4989-9976-05ecbba385b1)
## Running the Application

To start the application, run the following command in the root directory of your project:

```bash
git clone https://github.com/harunu/DomainChecker.git
cd yourprojectdirectory Example: (C:\Users\Dell\Documents\GitHub\DomainChecker)
docker-compose up --build



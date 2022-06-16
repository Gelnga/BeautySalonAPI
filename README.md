# Beauty salon project API
## Project core technologies: ASP.NET Core framework and C# (.NET Core version: 6.0.100), Docker (Docker Desktop version: 20.10.14)
### General information about the project
The goal of the project was to create an application, that could be used by beauty salon clients to register on appointments. This project was done during university under the Distributed Apps subject.

**This project has a separate client**, which can be found under the following link: https://github.com/Gelnga/BeautySalonClient

Project root folders contains 2 main folders. **BeautySalonAPI folder** contains project codebase. **Documentation files** folder contains all project documentation related files.

### Some of the crucial topics that were covered during a project implementation: 
- Application architecture patterns
  - Data access layer (in project context - repositories, unit of work)
  - Business logic layer (in project context - services)
  - Data transfer objects (including public API objects)
  - Mappers
- API implementation (ASP.NET Core API)
- API versioning and documentation (Swagger)
- Dependency injection
- Basic creation of a database Docker container using docker-compose
- Integration testing
- Unit testing
- Basic user authentication using JWT tokens

### Application views (**[from client project](https://github.com/Gelnga/BeautySalonClient)**)

Home page
![image](https://user-images.githubusercontent.com/73603988/174031093-130f6299-ee7f-4920-bf2a-168e9295f4c5.png)

Log in form  
![image](https://user-images.githubusercontent.com/73603988/174031136-dc047723-255a-44be-ab24-34816505afa0.png) 

Registration form  
![image](https://user-images.githubusercontent.com/73603988/174031212-7faf0c8c-c569-4d47-b37f-cf28d6cc4727.png)

Salons
![image](https://user-images.githubusercontent.com/73603988/174048441-1a000e0a-cee3-474e-9a6a-5ed20844c0c0.png)

Salon services
![image](https://user-images.githubusercontent.com/73603988/174048707-55eda983-2946-4fcf-abca-302f2d640535.png)

Appointment booking form
![image](https://user-images.githubusercontent.com/73603988/174048829-97b26bcc-014b-40cc-9b62-b440d6ec83c1.png)

User profile
![image](https://user-images.githubusercontent.com/73603988/174049172-3507980f-c3c2-4dd4-9dbf-de4b188c9821.png)

BlogPosts
![image](https://user-images.githubusercontent.com/73603988/174049254-674ec2e8-d058-4f8f-8a50-7009d0a09f82.png)

Single worker blogposts (available only for logged in salon workers)
![image](https://user-images.githubusercontent.com/73603988/174049409-94ac2f9d-f7f4-4a8a-8ae1-1ca83ae7fa37.png)

BlogPost creation 
![image](https://user-images.githubusercontent.com/73603988/174049496-57d60209-be05-460a-86f1-a624b68fae3a.png)



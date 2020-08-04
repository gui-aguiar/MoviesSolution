# MoviesSolution

Implementation of REST API server representing a simple video rental application. 

The solution was developed using:
  - .Net framework in its 4.8 version
  - Visual Studio 2019

The following steps are necessary to run the application: 
  - Pre requisites:    
    - .Net framework 4.8
    - SQL Server 2019
  
  - Download and build the application
  - Run the generated Movies.Server.SelfHost.exe  

The app will run and use the default port and local user login to connect to the database.
The addreess to access the server API will be localhost:9000
  - To change the serving port, the code mus be edited in the Program.cs file, line 16;

- Available URLs

    /api/gender - GET, GET/id, POST, PUT, DELETE
    
    /api/movie - GET, GET/id, POST, PUT, DELETE
    
    /api/rental - GET, GET/id, POST, PUT, DELETE
  
    /api/user - GET, POST
    
    /token - POST

- To use the API os necessary to authenticate. To do it, the user must make a POST request to the /token API as it follows: 
  - The content type must be application/x-www-form-urlencoded 
  - the parameteters are:
    - "username" : that contains the user login
    - "password" : that contains the user password
    - "grant_type" : with the valeu "password"
  
  - The response will contain an authentication token that must be provided in the requestes Header "Authentication" with the value containing the word "bearer" followed by the provided token  
    
- The application starts with no user registered. To create the users, the API client should use the /api/user URL. This URL does not ask for authentication.

Author: Guilherme Aguiar

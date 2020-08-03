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

    /api/gender - GET, POST, PUT, DELETE
    /api/movie - GET, POST, PUT, DELETE
    /api/rental - GET, POST, PUT, DELETE
  

Author: Guilherme Aguiar

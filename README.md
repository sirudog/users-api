## Assignment for Application Developer by Adam Biro
Create a RESTful API in C# to retrieve and manage a list of users. The API should be able to list, add, update and delete users from a NoSQL database like MongoDB. The API should be self-documenting and contain full test coverage.
The users dataset we would like you to use can be downloaded from https://jsonplaceholder.typicode.com/users

### Steps for starting the local development environment: 
1. In Powershell terminal, switch to the project root:
```
cd $PROJECT_ROOT\one-identity\users
```

2. Start the database (this will pull mongo image if it does not exist locally):
```
docker run --name database -d -p 27017:27017 mongo:6.0
```

3. Run the `UsersApi` project from VS.NET

4. Run the database seed powershell script. Seeding is done by calling the Users API with the test user data.
```
.\seed
``` 
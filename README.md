# accounts

This project contains 4 projects includings


accounts.DAL
- This is data layer project. Models, repositories and DbContext of this project reside here.


account.core
- This is business logic layer. All features are in this project to keep it separate from models.


account.web
- This is .net core project as an end point.


account.test 
- This is test project. We use XUnit and InMemory database for testing purpose.



## How to run this project
Set account.web project and run. This will bring up swagger page at https://localhost:44379/swagger/index.html.

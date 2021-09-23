# Junior Accelerator Program Task 2 - Movie Application

Technologies used:
 - Angular on the frontend
 - ASP .NET Core API on the backend

# What is this project about?

JAP Task 1 is exstension of JAP Task 1. 

- Implementation of movie screeings and ticket buying for those screenings
- Only authorized users can buy tickets
- Implementation of three store procedures
- NUnit tests for Search, Rating Add, Average Rating Calculation and buying tickets (not allowed to buy tickets for past screenings)


# How to run the project?
 - Install Microsoft SQL Server Managment studio and make sure you have the right connection string.
 Change the connection string in the appsettings.Development.json
 
 ![Screenshot_6](https://user-images.githubusercontent.com/89447689/134517032-5b65e267-5ed7-4efd-82c9-a8acf7f28f4a.png)
 
 
 - Run dotnet ef database update and dotnet watch run (data will be seeded into movieapp.db when application is started)

 





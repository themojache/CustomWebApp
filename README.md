# CustomWebApp
ASP.NET Core Example

**To open** Clone/Download the master branch of the repository. Open 
CustomWebApp (either the sln or the csproj file). Run using ISS Express (Ctrl + F5, F5, or the Run button from withing Visual Studio).

## Features
Three Controllers, two of which capture form information and would generally use it to make a database query. I utilize custom data types to allow it to be functional without a Database hit. Since speed is a priority above all else I have focused on that, hopefully this presents at least C# competency (Difficult to present skills without a good use case). Basic form validation.

1. Search functionality (Search Controller).
2. General logic of a login system (SigninController and User Model).
3. Simple redirects to pages (HomeController).

## Tech Stack
ASP.NET Core (HTML5, CSS3). JQuery due to usage of Bootstrap.

## TODO
1. Hash and Salt Password before being sent to the Database. (Store Hash and Salt, then verify the login by hashing an attempted pass with said salt).
2. Add more form validation (check client side and server side). Ex: Create a Password Requirement (Atleast x characters long, with: a number, upper alpha, lower alpha, and special character).
3. Create more Secure SQL Queries and Test them, ensure little threat of SQLi.
4. Update Front End.

## License
[GNU GPLv3](https://choosealicense.com/licenses/gpl-3.0/)

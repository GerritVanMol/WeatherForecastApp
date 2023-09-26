# WeatherForecastApp
A small demonstration application, for displaying the climate of Hasselt. Written in Blazor C#.


### A brief description of the task/scenario:

"*Retrieve data from https://open-meteo.com/ or a similar weather api using typescript, .NET or python code. Do this for the city of **Hasselt**. The weather data should be **updated daily**. Data should be **stored** no more or less than **30 days** in either **XML** or **CSV** format. 
Think of all common best practices while developing this integration. 
Use Github to store and share your project.*"

All bold items marked in the previous task description are important things to keep in mind while developing a sollution.



*I will document my thoughts and decisions, so you can follow along as we go. Trough out my code I will also provide some little comments (for demonstration purposes only, comments in production are not best practice as they get out dated and do not present the code next to it correctly).*


When assigned to a task, it is important to consider the possibilities in tech-stack/language options for the scenario. This is a fundamental step for mid/large and long term projects, as it influences all future decisions and possibilities.
Research is there for a complementary step before starting.

However, considering the short time span in which this project needs to be finished, I will choose a language that I am the most familiar with (C#).

The other languages like Python and Typescript will not be used for the following reasons:
    - (Unknown) I am unfamiliar with Typescript
    - (Hosting) I use Python in Django however, hosting would take some time extra as deployment is not yet automated on my local server(s).    

Last but not least, the project structure is very important to keep everything clean and separate.
I will make use of the MVVM architecture pattern, it has been a while since I used this, so a code review is always welcome.

# Geek Hunters

You are working at IT-recruiting agency "Geek Hunters". Your employer asked you to implement Geek Registration System
(GRS). 

Using GRS a recruitment agent should be able to:
  - register a new candidate:
     - first name / last name
     - select technologies candidate has experience in from the predefined list 
  - view all candidates
  - filter candidates by technology

Another developer has partially designed and implemented a
SQLite DB for this project - GeekHunters.sqlite. Feel free to modify a structure to
your needs.

Please fork the project and commit your source code (please do not archive it :) ).

You are free to use **ANY** .net web frameworks you need - aspnet / webapi / spa etc. However, if you decide to go with third
party package manager or dev tool - don't forget to mention them in the
README.md of your fork.

Good luck!

P.S: And unit tests! We love unit tests!

# Shaun Hutchinson - Geek Hunters Test

Technologies used:
  - ASP.NET MVC in .NET Framework 4.7.2
  - [System.Data.Sqlite Nuget package](https://system.data.sqlite.org/)
  - [jQuery](https://jquery.com/)
  - [Materialize CSS framework](https://materializecss.com/)
  - LESS compiler for custom CSS

The application is a simple SPA which contain simple lists for Candidates and Skills, along with a small form to add a new candidate and their skills from a multi-select dropdown. To filter by skill, simply click on any of the skills in the list, and the candidate list will filter out those who have that skill.

I added an extra column into the Candidate table in the GeekHunter.sqlite file, which stores the skills as a comma delimited list. A simple comparison is done in the javascript, comparing if the Skill ID is in the comma delimited list.

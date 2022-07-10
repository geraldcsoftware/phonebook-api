# PhoneBook Api

Api for a simple phonebook application. 
This api requires a connection to a Postgres database.
It can be built and run as a docker image or as a dotnet application.

It exposes 4 end points

1. /api/phonebooks [GET] - This lists the phone books in the database
2. /api/phonebooks [POST] - We can use this to create a new phonebook in the database
3. /api/phonebooks/<<phonebookid>>/entries [GET] - This lists the entries in a phonebook, and accepts a `searchText` query parameter to filter by name or phonenumber
4. /api/phonebooks/<<phonebookid>>/entries [POST] - Adds a phonebook entry to the database



### CQRS with MediatR

The application makes use of the MediatR package to implement CQRS, which helps with implementing a vertical architecture where each request is handled by a unique handler/pipeline.
With minimal apis, this helps keep the api declarations as clean and slim as possible.

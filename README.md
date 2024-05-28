# City API overview

.NET 8.0 WEB API that provides information about cities and points of interest for each city.

The API uses Entity Framework Core 8.0 and SQLite to access city and points of interest data.

# City API endpoints

The following endpoints are available:

| Endpoint                                                      | Method | Description                                                                                                                            |
| :------------------------------------------------------------ | :----: | :------------------------------------------------------------------------------------------------------------------------------------- |
| /api/v1.0/cities                                              |  GET   | returns JSON structure with all cities without associated points of interest; the endpoint provides pagination and search capabilities |
| /api/v1.0/cities/{id}                                         |  GET   | Returns a JSON response with a single city based on {id} and all associated points of interest                                         |
| api/v1.0/cities/{cityId}/pointsofinterest                     |  GET   | Returns a JSON response with all points of interest for a single city based on {cityId}                                                |
| api/v1.0/cities/{cityId}/pointsofinterest/{pointOfInterestId} |  GET   | Returns a JSON response with a single point of interest based on {pointOfInterestId} and {cityId}                                      |
| api/v1.0/cities/{cityId}/pointsofinterest                     |  POST  | Creates a new point of interest for a single city based on {cityId} and returns a JSON response with the new point of interest         |
| api/v1.0/cities/{cityId}/pointsofinterest/{pointofinterestid} |  PUT   | Updates a point of interest based on {pointOfInterestId} and {cityId} and returns a JSON response with the updated point of interest   |

# City API design considerations

The API utilizes SQLite to access underlaying data but this can be replaced with a different database engine ( Azure SQL or AWS RDS for example).

For securing the API endpoints we use JWT token based authentication. For simplification purposes, this is generated via an endpoint on the same API (rather than a separate Authentication and Identity API):
/api/authentication

Patterns used:

- Dependency Injection
- Repository Pattern

# Running the API locally

The API endpoints running on a local machine can be called via a client like POSTMAN.

Before calling the /cities and /pointofinterest endpoints, the client needs to call the /authentication endpoint to generate a JWT token and pass that as Bearer Token with ecah request

# API File Structure

## Models

### [Entity].cs

Defines the fields from the database tables.

### OpenAuburnContext.cs

Establishes a read-only connection to the database. Using the [Entity].cs definitons, it is able provide access to the database context throughtout the program.

<br>

## Controllers

### [Entities]Controller.cs

Handles GET requests for different entities. 

<br>

## Filters

### [Entity]Filter.cs

Defines and stores incoming filter information for an entity.

### PaginationFilter.cs

Defines and stores page size and page number information for a request.

<br>

## Wrappers

### Response

Wraps the requested data and provides information about the response status.

### PagedResponse.cs

Wrapes the requested paged data and provides information about the response, as well as page navigation uri’s.

<br>

## Services

### URIService.cs

Generates uri given route and query information.

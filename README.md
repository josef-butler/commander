# Commander
A simple .NET Core API to store and retrieve CLI commands

## API

### Get list of all commands
#### Request
* `GET /api/commands`

#### Response
```json
[
    {
        "id": 1,
        "howTo": "Create migrations",
        "line": "dotnet ef migrations add <Name of Migration>"
    },
    {
        "id": 2,
        "howTo": "Run migrations",
        "line": "dotnet ef database update"
    }
]
```

### Get a single command
#### Request
* `GET /api/commands/{id}` will return a single command when given its id.
* If there is no command with the specified Id, a `404 Not Found` will be returned.

#### Response
```json
{
    "id": 1,
    "howTo": "Create migrations",
    "line": "dotnet ef migrations add <Name of Migration>"
}
```

## Status codes

Commander returns the following status codes in its API:

| Status code | Description |
| --- | --- |
| 200 | `OK` |
| 400 | `Bad Request` |
| 404 | `Not Found` |
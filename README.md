# Commander
A simple .NET Core API to store and retrieve CLI commands

## API

### Get list of all commands
#### Request
`GET /api/commands`

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
`GET /api/commands/{id}`
* Returns a single command when given its id.
* If there is no command with the specified Id, a `404 Not Found` will be returned.

#### Response
```json
{
    "id": 1,
    "howTo": "Create migrations",
    "line": "dotnet ef migrations add <Name of Migration>"
}
```

### Create a new command
#### Request
`POST /api/commands`

```json
{
    "HowTo": "Clear the terminal screen",
    "Line": "cls",
    "Platform": "Windows Command Prompt"
}
```

#### Response
* URI to the newly created resource will be included as a header

```json
{
    "id": 5,
    "howTo": "Clear the screen",
    "line": "cls"
}
```

### Update an existing command
#### Request
`PUT /api/commands/{id}`
* If a command is found and updated, a 204 is returned to indicate success
* If a command is not found, a 404 is returned

```json
{
    "id": 4,
    "howTo": "Create migrations",
    "line": "dotnet ef migrations add <Name of Migration>"
}
```

## Status codes

Commander returns the following status codes in its API:

| Status code | Description |
| --- | --- |
| 200 | `OK` |
| 201 | `Created` |
| 204 | `No Content` |
| 400 | `Bad Request` |
| 404 | `Not Found` |
# addresses-api

## app
The application depends on the Connnection String environment variable. To launch an application use the command:

```sh
CONNECTION_STRING="Host=localhost;Port=5433;Database=yourDockerDB;Username=postgres;Password=yourPassword" dotnet run --project ./AddressesAPI/AddressesAPI.csproj
```

However, you will need to substitute your database's environment variables.

## tests

To run the tests use:

```sh
dotnet test
```

## endpoint
The endpoint to be tested is:
```
http://localhost:5194/api/v1/addresses?postcode=E28BA
```
The port on your machine may vary

## Notes
- All the tests are passing
- The application works as requested.

# King Karel - server

## First steps

Before the app can be started,
the latest released version of .NET has to be downloaded.
Available version can be found [here][dotnet].

## How to run

We will need to start the DB using the following command:

```sh
docker-compose up -d
```

After that, you can start up the project in your favorite IDE
or by running the run command:

```sh
dotnet run --project ./src/HonzaBotner/
```

## Settings

For configuration,
we use a combination of the standard `appsettings.json` file and `secrets.json`.
Non-sensitive information is stored in the `appsettings.json` file
and sensitive information is stored in place outside of the git repository.
(for more details see the [documentation][secrets])

## Database

Install the tool that process database migrations.

```sh
dotnet tool install --global dotnet-ef
```

To create a migration, use the tool this way:

```sh
cd KingKarel
dotnet ef migrations add "Name of migration"
```

The migration is based on our code.
All `DbSet<T>` in `KingKarelDbContext` will be used and their mappings will be applied.

All generated migration files are located in `KingKarel/Migrations`.

To update connected database, run update command:

```sh
dotnet ef database update
```

[dotnet]: https://dotnet.microsoft.com/download
[secrets]: https://docs.microsoft.com/cs-cz/aspnet/core/security/app-secrets

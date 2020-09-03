# Blogger
Blogging web application

### Get and run

To get the source code you need to clone the repo:
```
git clone https://github.com/catman0745/Blogger.git
```

Set few environment variables:
- `BLOGGER_DB_CONNECTION` is postgres db connection string (e.g. Server=localhost;Port=5432;Database=blogger;User Id=catman0745;Password=qwerty123)
- `BLOGGER_AUTH_ISSUER` is some string (e.g. API)
- `BLOGGER_AUTH_AUDIENCE` is some string (e.g. Client)
- `BLOGGER_AUTH_LIFETIME` is token lifetime in minutes (e.g. 60)
- `BLOGGER_AUTH_KEY` is jwt encryption key and it **MUST be at least 16 characters long** (e.g. blogger_token_key)

Apply migrations (run inside the cloned repo folder):
```
dotnet ef database update -p src/back/Catman.Blogger.API
```

Start the API (default address is `localhost` on port `5000`):
```
dotnet run -p src/back/Catman.Blogger.API
```

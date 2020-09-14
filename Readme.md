# Blogger
Blogging web application

### Get and run

To get the source code you need to clone the repo:
```
git clone https://github.com/catman0745/Blogger.git
```

Set few environment variables:
- `BLOGGER_DB_CONNECTION` is postgres db connection string (e.g. Server=localhost;Port=5432;Database=blogger;User Id=catman0745;Password=qwerty123)
- `BLOGGER_AUTH_ISSUER` is some string (**default** is Catman.Blogger.API)
- `BLOGGER_AUTH_AUDIENCE` is some string (**default** is Catman.Blogger.Client)
- `BLOGGER_AUTH_LIFETIME` is token lifetime in minutes (**default** is 10080 e.g. 7 days)
- `BLOGGER_AUTH_KEY` is jwt encryption key and it **must be** at least 16 characters long (e.g. blogger_token_key)
- `BLOGGER_FILES_IMG_MAX_MB` is max image size im MegaBytes (**default** is 5 MB)
- `BLOGGER_FILES_IMG_TYPES` is list of MIME content types recognized as image
- `BLOGGER_FILES_UPLOAD_DIR` is relative path from the API project directory to the uploads folder

Apply migrations (run inside the cloned repo folder):
```
dotnet ef database update -s src/back/Catman.Blogger.API -p src/back/Catman.Blogger.Data
```

Start the API (default address is `localhost` on port `5000`):
```
dotnet run -p src/back/Catman.Blogger.API
```

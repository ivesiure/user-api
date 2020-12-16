# User API
A generic C# microservice API with custom authentication and MySQL.

## Database
This project uses a MySQL database, you can easily create a MySQL container instance with docker for this project with the following command:

```
docker run -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=P@ssw0rd -e MYSQL_DATABASE=usr_db -e MYSQL_PASSWORD=P@ssw0rd mysql:latest
```

After your first run, the Migrations will be applied into the database, creating the *User* table, and seeding it with the Admin user.

## Admin credentials
If you prefer, change these values before the first run. You can check it in the Migrations folder:

**User:** admin@email.com
**Password:** P@ssw0rd

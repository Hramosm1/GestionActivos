<img src="itmanager/images/itmanager.jpg" width="600" alt="IT Manager">

## ITMANAGER

<p align="center">
<a href="https://travis-ci.org/laravel/framework"><img src="https://travis-ci.org/laravel/framework.svg" alt="Build Status"></a>
<a href="https://packagist.org/packages/laravel/framework"><img src="https://img.shields.io/packagist/l/laravel/framework" alt="License"></a>
</p>

### ConnectionString
Set the connectionString in

itmanager\Models\itmanagerContext.cs
```
optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=itmanager;User Id=sa;Password=password");
```

appsettings.json
```
"ITManagerConnectionString": "Data Source=localhost;Initial Catalog=itmanager;User Id=sa;Password=password"
```

### Database
Database Script in /datatabase/itmanager.sql



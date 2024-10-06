## ResumeApp 

### Coding
1. Generar proyecto dotnet webapi 
   ```
   dotnet new webapi -n Jasson.Codes.Api --use-controllers -o Jasson.Codes.Api
   ```

2. Generar archivo solucion y agregar proyecto
   ```
   dotnet new sln -n jassoncodes.api
   dotnet sln add "src\Jasson.Codes.Api\jasson.codes.api.csproj"
   ```

3. Generar archivos vscode para build y debug
   ```
   ctrl + shift + p > .NET: Generate Assets for Build and Debug
   ``` 

4. Instalar dependencias
   ```bash
   dotnet tool install --global dotnet-aspnet-codegenerator
   dotnet tool install --global dotnet-ef
   dotnet add package Microsoft.EntityFrameworkCore.Design
   dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
   dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
   ```

5. Generar dev-certificates
   ```
   dotnet dev-certs https --clean
   dotnet dev-certs https --trust
   ```

6. Definir Models\Entities\Entity.cs class
7. Definir Models\DTOs\DTOs.cs y Metodo extension para convertir Entity a DTO (Extensions.cs)
8. Definir Interfaces y Servicios para cada entidad que seran inyectados y utilizados en sus respectivos  controladores para las operaciones especificas. Luego de esto es necesario Agrergar el  Scoped Service al builder de la aplicacion, enviando como parametro la Interfaz y el Servicio que lo implementa:
```c#
   builder.Services.AddScoped<IExperienceService, ExperienceService>();
```
9.  Agregar migraciones iniciales o Actualizar base de datos con dotnet ef tool
   
   ```bash
   # agregar migracion
   dotnet ef migrations add "InitialCreate"

   # actualizar base de datos
   dotnet ef database update
   ``` 

10. Generar controller basico y DbContext o definirlos manualmente
   ```
   dotnet aspnet-codegenerator controller -name ContactInfoController -m ContactInfo -dc AppDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres -api -nv
   ``` 

11. Agregar DbContext a los servicios de la aplicacion
   ```c#
   builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));
   ```

> #### *Optional*: 
> ##### Run the database in a container to test*:
>  ```bash 
>  # With autoremove: 
>  docker run -d --rm --name postgres_container -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -v api_db:/etc/data postgres
>  # No autoremove:   
>  docker run -d --name postgres_container -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -v api_db:/etc/data postgres
> ```
> ##### *Run PgAdmin4 Image*
> ```bash
> docker run --name pgadmin-container -p 5050:80 -e PGADMIN_DEFAULT_EMAIL=admin@admin.com -e PGADMIN_DEFAULT_PASSWORD=root -d dpage/pgadmin4
>```



### App Image configuration
11. Generar Dockerfiles into Workspace
   ```
   ctrl + shift + p > Docker: Add Docker files into Workspace
   ```

> **Opcional:** Modificar el valor de la propiedad "tag" en el archivo tasks.json para que la imagen del app se genere con un nombre personalizado, caso contrario se generara con el nombre de la carpeta raiz de la solucion
> 
>            "dockerBuild": { 
>                "tag": "jassoncodesapi:latest" 
>            }


12.   Modificar Dockerfile generado y agregar: ENV ASPNETCORE_HTTP_PORTS=5000;5001 para que se mapeen los puertos de la aplicacion en la imagen a generar

### Docker Compose configuration
13. Generar Docker-Compose files
   ```
   Ctrl + Shift + P > Docker: Add Docker Compose Files to Workspace
   ```

14. Configurar servicios adicionales en el archivo docker-compose.yml


## Pendiente
/api controlador que enlista todas las rutas disponibles
/me controlador que devuelve la informacion consumida por todos los endpoints disponibles

markdown css style

#### Links
https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code

https://learn.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator?view=aspnetcore-8.0#controller-options

https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-8.0&tabs=visual-studio-code

https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli#install-entity-framework-core

https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-8.0

https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-8.0&tabs=net-cli%2Clinux-sles


https://hub.docker.com/_/postgres

https://docs.docker.com/compose/startup-order/


https://code.visualstudio.com/docs/containers/quickstart-aspnet-core

https://code.visualstudio.com/docs/containers/reference#:~:text=The%20docker%2Dbuild%20task%20builds,application%20within%20a%20Docker%20container.

https://code.visualstudio.com/docs/containers/reference


[@CodingDroplets](https://www.youtube.com/@CodingDroplets): [DotNet Core PostgreSQL [C# PostgreSQL Entity Framework Core]](https://www.youtube.com/watch?v=A1pr5hXArE4)

[@MilanJovanovicTech](https://www.youtube.com/@MilanJovanovicTech): [Docker Compose with .NET 8, PostgreSQL, and Redis (step by step)](https://www.youtube.com/watch?v=WQFx2m5Ub9M)

[@Randomcode_0 ](https://www.youtube.com/@Randomcode_0): [How to create a docker-compose setup with PostgreSQL and pgAdmin4](https://www.youtube.com/watch?v=qECVC6t_2mU)





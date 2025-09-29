# perla-metro-users-service

Servicio que permite la gestión de toda la información de los pasajeros del sistema de Perla Metro

## Tecnologías utilizadas

- **Framework:** ASP.NET Core 9.0
- **Base de Datos:** PostgreSQL
- **Deployment:** azure app service
- **Control de Versiones:** Git con Conventional Commits

## Modelo de Datos

### Entidad User
```
{
  "Id": "UUID v4 string",
  "Role": "string", // "Admin" | "User"
  "Name": "string",
  "Surename": "string", 
  "Email": "string",
  "Password": "BCRYPT string",
  "RegistrationDate": "DateOnly",
  "isActive": bool,
  "DeactivationDates": List<DateOnly>
}
```

### Estados de un Usuario:

- **Activo:** Usuario esta.
- **Inactivo:** Estación se encuentra inactiva.

### Endpoint Disponibles

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/User/GetAll` | Listado de todos los usuarios |
| `GET` | `/api/User/GetUser/{id}` | Obtener un usuario por id  | 
| `GET` | `/api/User/UserFilter` | Listado de usuarios aplicando un filtro| 
| `Post` | `/api/User/Register` | Registrar a un nuevo usuario |
| `Post` | `/api/User/login` | Permite el login de un usuario | 
| `Put` | `/api/User/enable-disable/{id}` | Permite actualizar el estado de un usuario, es decir activar o desactivar la cuenta de un usuario (soft delete) | 
| `Put` | `/api/User/update-user` | Permite actualizar la información del usuario | 

Revisar el archivo UsersService.postman_collection.json para toda la información acerca de los endpoints

## Instalación y Configuración para entorno local

### Prerrequisitos

- **.NET 9 SDK:** [Download](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Visual Studio Code o Visual Studio 2022:** [Download](https://code.visualstudio.com/)
- **Docker desktop** [Download for windows](https://docs.docker.com/desktop/setup/install/windows-install/)

### Pasos de Configuración
1.  **Clonar el Repositorio**:
    ```bash
    git clone https://github.com/Proyecto-Perla-Metro-2025/perla-metro-users-service.git
    cd Perla-Metro-Users-Service
    cd UsersService
    ```
2.  **Configurar la Base de Datos**:
    Para la creacion local de base de datos se debe de tener abierta la aplicación Docker desktop, una vez abierta se debe correr el comando:
    ```bash
    docker-compose up -d
    ```
    las credenciales se encuetran en el archivo appsettings.json en el apartado siguiente como base y ejemplo:
    
    ```
    "ConnectionStrings": {
    "Postgres": "Host=localhost;Port=5432;Database=db;Username=user;Password=password"
    }
    ```
    También se pueden configurar dentro de las variables de entorno (.env) siguiendo este formato:
    ```
    POSTGRES_CONNECTION=Host=hostname;Database=db;Username=user;Password=password
    ```
    si se tienen las dos, las credenciales de la variable de entorno toman prioridad.

3. **Instalar Dependencias**
    ```bash
    dotnet restore
    ```

4. **Ejecutar el Proyecto**
    ```bash
    dotnet run
    ```

Cuando quieras apagar definitivamente la aplicación recuerda utilizar el comando 
    ```bash
    docker-compose down
    ```
Para asegurarte de que la base de datos también se detenga    

## Aplicación desplegada
Esta aplicación se encuentra en desplegada en Azure web service en el siguiente enlace: https://perla-metro-users-service.azurewebsites.net

Para revisar el estado ir al apartado health https://perla-metro-users-service.azurewebsites.net/health


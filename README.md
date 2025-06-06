## Contenido

Este proyecto consiste en una API para un sistema de autenticación de usuarios, desarrollada con .NET 8 y estructurada bajo los principios de Clean Architecture, siguiendo específicamente el enfoque de Onion Architecture.
A lo largo del proyecto se han implementado distintos patrones de diseño que permiten mantener una arquitectura robusta y flexible, adaptándose a distintos escenarios. Entre los más relevantes se encuentran:    
    * Repository Pattern
    * Dependency Injection
    * Dto
    * CQRS y MeditR
    * Inyección de Dependencias
    * Option Pattern.
    
El flujo del sistema también integra diversas librerías y buenas prácticas que optimizan su rendimiento y mantenibilidad. Algunas de las más destacadas son:    
    * AutoMapper
    * Middlewares
    * Filters
    * Helpers

Las conexiones a las bases de datos y el manejo de entidades fueron realizadas con el ORM EntityFramework.

## Diagrama
![image](https://github.com/user-attachments/assets/4f9d6835-780c-4346-b2a0-582639fffcba)


## Pasos para configurar el entorno de la app.

### 1. Ir al AppSettingJson

Nos dirigimos al AppSettingJson en DefaultConnection, cambiaremos el servidor hacia donde apunta la base de datos.

```
"DefaultConnection": "Server=¨(Aqui se debe de colocar el servidor)¨;Database=UserAuthenticationDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
```
Nota: importante debe ser SQL SERVER.

![image](https://github.com/user-attachments/assets/746e8035-08dc-4714-8dfa-2dba063909c4)

### 2. Comando para ejecutar la configuración de la base de datos.

Después de finalizar el paso anterior, es necesario abrir la Consola del Nugget Package. Esto permitirá aplicar a la base de datos las configuraciones realizadas con Fluent API, para lo cual se debe ejecutar el siguiente comando:
```
Update-Database
```

### 3. Endpoints

Luego de que hayamos cargado la base de datos, nos encontraremos con un controlador "UserController" el cual contiene los siguiente:
Existen 2 endpoints que se pueden probar sin autenticarse:

```
Post - RegisterUser
```
![image](https://github.com/user-attachments/assets/dc2c0cd4-f2e5-4553-83f6-bfa78220d10f)

```
Post - Login
```
![image](https://github.com/user-attachments/assets/15cb1aad-7b58-4fc6-9358-c391c31ce380)


### 4. Autenticación y JWT


Después crear un usuario o loguearte, te devolverá una respuesta con el token, el cual utilizarás con bearer para poder autenticarte 
y que se permita utilizar los otros 3 endpoint que hay:
```
Get - GetAllUsers
```
![image](https://github.com/user-attachments/assets/954b662c-0da7-4d53-aa77-d4f5c2859378)

```
Put - User
```
![image](https://github.com/user-attachments/assets/4a9c793a-44d4-44c0-9026-30b06061dd65)

```
Delete - User
```
![image](https://github.com/user-attachments/assets/4cfc2b9a-3e10-448e-a8b0-f4decc0d1d39)



En la parte superior de swagger te aparecerá el botón authorize, que al presionarlo te permitirá colocar el JWT.
Se debe de introducir de la siguiente manera
```
Bearer TOKEN
Para poder autenticarse se debe de iniciar sesion , y luego copiar el token, a de intgresar el token se debe de poner de esta manera:
Bearer [Token]
```
![image](https://github.com/user-attachments/assets/55e1f3de-c8de-4422-9a77-97612bfa1d85)

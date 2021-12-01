Caracter�sticas:

- NetCore 3.1 (RestAPI + WebAPI) 
- DDD + CQRS.
- Command Handler NetCore 3.0+ .NET format. (Isolaci�n de capas de seguridad controladas por memoria)
- HttpStatus, RestFul, Inyecci�n de dependencia.
- Informaci�n parametrizada desde appsetting. 
- Entity Framework Core (genera bases de datos y tablas din�micamente desde Clases en RestAPI)
- Api Key que controla la seguridad de los servicios Rest, mediante log-in de Usuario.

API que permite:

Crear una tarjeta. 
Cada invocaci�n a m�todo debe realizarse de manera segura y utilizando un token que debe entregar en el api v�a HEADER. Dicho token debe tener una vigencia de 10 minutos. Use usuario/contrase�a creado anteriormente para este prop�sito. 
Crear un usuario valido de acceso a sistema. 
Una tarjeta debe ser creada con estado �no vigente� 
Los campos a ingresar son obligatorios 
Quitar todo el saldo de una tarjeta. 
Quitar una parte del saldo de una tarjeta. 
A�adir saldo a una tarjeta. 
Obtener una tarjeta por su GUID 
Obtener las tarjetas por nombre de tarjetahabiente. -
Actualizar el nombre del tarjetahabiente. 

-

Uso:

- Set up SQL Express 2017
- Set up SQM Management Studio
- Use Windows login in SQL Express, add "Prueba" database
- Open Postman, load project postman collection:

Open RestAPI, run in background.
Open WebAPI, and run.
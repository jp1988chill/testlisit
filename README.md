Espa�ol:

Arquitectura:
(Frontend) WebAPI + motor Typescript -> (Backend) Llamada RestAPI -> Modelo Inyecci�n de Dependencias -> IRequest -> IRequestHandler (Inicializa EFCore) -> DBContext (Uso de EFCore API)

NetCore 6.0 LTS RestAPI + WebAPI que implementa las siguientes caracter�sticas:
- DDD + CQRS.
- Command Handler NetCore 3.0+ .NET format. (Isolaci�n de capas de seguridad controladas por memoria)
- HttpStatus, RestFul, Inyecci�n de dependencia.
- Informaci�n parametrizada desde appsetting. 
- Entity Framework Core (genera bases de datos y tablas din�micamente desde Clases en RestAPI)
- Api Key que controla la seguridad de los servicios Rest, mediante log-in de Usuario.

Funcionalidad de RestAPI:
- Crea una tarjeta. 
- Cada invocaci�n a m�todo se realiza con Bearer Token autorizado previamente por usuario, formato HTTPS (en Header). Cada token tiene una vigencia de 10 minutos. Utiliza usuario/contrase�a creado anteriormente por Usuario. 
- Crea un usuario v�lido de acceso a sistema. 
- Crea tarjetas con estado �no vigente� 
- Elimina saldos de una tarjeta. 
- Elimina saldo parcial de una tarjeta. 
- Agrega saldo a una tarjeta. 
- Obtiene una tarjeta por GUID (Id �nico de .NET framework)
- Obtiene muchas tarjetas por Cliente, previamente vinculado por un usuario del sistema.
- Actualiza el Nombre del Cliente. 

Uso:
- Instalar SQL Express 2017
- Instalar SQM Management Studio
- Instalar Postman
- Usar inicio de sesi�n de Windows en SQL Express, agregar la base de datos "Prueba"
- Abrir Postman, cargar el Postman Collection del proyecto, y ejecutar la funcionalidad de API anteriormente descrita:
- Abrir RestAPI, ejecutar en segundo plano y no cerrar.
- Abrir WebAPI y ejecutarlo desde el navegador.

-----------------------------------------------------------------------------
English:

Architecture:
(Frontend) WebAPI + Typescript engine -> (Backend) RestAPI Call -> Dependency Injection Model -> IRequest -> IRequestHandler (Initialize EFCore) -> DBContext (EFCore API usage)

NetCore 6.0 LTS RestAPI + WebAPI implementing the following extensions:
- DDD + CQRS.
- Command Handler NetCore 3.0+ .NET format. (Isolated security layer, protected by memory)
- HttpStatus, RestFul, Dependency Injection.
- All settings are read from appsetting. 
- Entity Framework Core (Generates database & entities dynamically from Classes through RestAPI into datasets)
- Api Key controlling RestAPI security and login, if user is registered.

RestAPI Features:
- Create a Card. 
- Each RestAPI call goes through a 10-minute tokenized Api Key, previously authored by User, HTTPS format (Header). Uses user/password created previously through the services. 
- Create a valid User to get authored access.
- Create Cards with status �expired� 
- Empty balance on a Card. 
- Empty desired balance on a Card. 
- Add balance on a Card. 
- Retrieve Card by GUID (.NET framework Unique ID)
- Retrieve all Cards by Client, previously added through an authored User.
- Update Client's name, through an authored User. 

Usage:
- Set up SQL Express 2017
- Set up SQM Management Studio
- Use Windows login in SQL Express, add "Prueba" database
- Open Postman, load project postman collection
- Open RestAPI, run in background, do not close.
- Open WebAPI, and run through the browser.

-

Local SQL:

Server=localhost\SQLEXPRESS;Database=Prueba;Trusted_Connection=True;

-

Pa�s, regi�n y comuna

1 Pa�s : N Regiones
1 Region : N Comunas

-

Todo:

Make CRUD services for entities:
-Pais (so far implemented: CrearPais)
-Region
-Comuna
-ServicioSocial
-RolUser

misc services required:
-CrearLoginUsuarioModel OK

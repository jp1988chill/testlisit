Arquitectura:
(Frontend) WebAPI + motor Typescript -> (Backend) Llamada RestAPI -> Modelo Inyección de Dependencias -> IRequest -> IRequestHandler (Inicializa EFCore) -> DBContext (Uso de EFCore API)

NetCore 6.0 LTS RestAPI + WebAPI que implementa las siguientes características:
- DDD + CQRS.
- Command Handler NetCore 3.0+ .NET format. (Isolación de capas de seguridad controladas por memoria)
- HttpStatus, RestFul, Inyección de dependencia.
- Información parametrizada desde appsetting. 
- Entity Framework Core (genera bases de datos y tablas dinámicamente desde Clases en RestAPI)
- Api Key que controla la seguridad de los servicios Rest, mediante log-in de Usuario.

Funcionalidad de RestAPI:
Servicios de autenticación: debe contener mínimo los endpoints para login y registrar usuarios.
• Servicios de localización: considerando la estructura País, región y comuna se debe permitir un CRUD básico para 
cada estructura. (País puede tener varias regiones, y una región puede tener varias comunas)
o Los GETS son de libre consumo para cualquier sistema
o Los POST, PUTS, DELETE estarán sometidos a validación de rol, solo administrador.
• Servicios de ayudas sociales: Están asignados por comuna y solo a los residentes de dichas comunas
o A una persona no se le puede asignar más de una vez con el mismo servicio social el mismo año.
o El administrador puede ver personas y los servicios asignados, le puede asignar alguna ayuda social.
o Una persona puede obtener sus ayudas sociales asignados por año y el último vigente.
o El administrador puede obtener las ayudas sociales asignadas a un usuario.
o El administrador puede crear nuevas ayudas sociales para las comunas o regiones. Si se crea en una región 
se asigna a todas las comunas de esta.
• Logging: Debe registrar cada acción de un usuario, quien hizo que y a qué hora. Un administrador puede rescatar 
esta información cuando desee escogiendo el día.

Uso:
- Instalar Visual Studio 2022 (Todas las versiones sirven)
- Instalar SQL Express 2017
- Instalar SQM Management Studio
- Instalar Postman
- Usar inicio de sesión de Windows en SQL Express, agregar la base de datos "Prueba", asegurarse de que no exista tabla alguna, y de existir, eliminarlas todas.
- Abrir Postman, cargar el Postman Collection del proyecto, y ejecutar la funcionalidad de API anteriormente descrita:
- Abrir RestAPI, ubicada en RestAPISwagger/Prueba.sln, ejecutar en segundo plano y no cerrar. 

Parámetro Connection String SQL:
Server=localhost\SQLEXPRESS;Database=Prueba;Trusted_Connection=True;

Y se puede actualizar en archivo:
Prueba.WebApi.appsettings.json
Prueba.WebApi.appsettings.Development.json

Diagrama UML:
Abrir Plataforma Web https://draw.io/ -> Open From -> Device -> Seleccionar archivo: UML.drawio

Postman:
Iniciar sesión, My Workspace -> Import -> Seleccionar "files" -> Seleccionar archivo: UnitTestMicroservicios.postman_collection.json

Nota: Se recomienda utilizar el servicio de pruebas unitarias para depurar el comportamiento de la plataforma, por la complejidad de los requests.
Por ejemplo, si se quiere Actualizar un Servicio Social, se requiere tanto de un usuario Administrador, como de un Token (capa seguridad), además de la colección de Servicios Sociales,
todo resultará complejo de depurar si se hace mediante Postman u otro cliente que consuma dicha RestAPI.


Como ejecutar RestAPI de Pruebas Unitarias para evaluar en cuestión de minutos la plataforma, sin tener que pasar manualmente por cada RestAPI incluída en Postman Collection o en plataforma:
Postman -> Pestaña "Collections -> UnitTestMicroservicios -> y ejecutar el servicio EjecutarPruebaUnitaria con el botón "Send" de color azúl;
Mientras se depura en Visual Studio el servicio "Prueba.WebApi.Controllers.EjecutarPruebaUnitaria" en tiempo real, será mas fácil revisar el modelo relacional y las validaciones en tiempo real.
Todo es generado automáticamente a partir de ahora a nivel de base de datos, para verificarlo, puedes abrir SQL Server Management Studio, conectarse, abrir la base de datos "Prueba", y verás que las tablas/tuplas fueron autogeneradas.


Gracias por su tiempo!
Atte.
Juan Pablo Neira Valenzuela
Analista de Sistemas / Software Developer

https://bitbucket.org/JP20182018/
https://coto88.bitbucket.io/
https://github.com/cotodevelhttps://www.linkedin.com/in/juan-pablo-neira-valenzuela-66b5b165/

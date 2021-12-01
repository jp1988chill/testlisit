Caracter�sticas:
 
Simplicidad.
Conceptos de DDD.
Utilizaci�n de CQRS.
Correcta segregaci�n de capas.
Utilizaci�n de recursos existentes.
Correcto uso de HttpStatus
Conceptos de RestFul.
Conceptos de Inyecci�n de dependencia.
Seguridad de la informaci�n (no exponer informaci�n sensible). 

Tareas

Debe modificar el desarrollo del API que permita:

Crear una tarjeta. -> OK
Cada invocaci�n a m�todo debe realizarse de manera segura y utilizando un token que debe entregar en el api v�a HEADER. Dicho token debe tener una vigencia de 10 minutos. Use usuario/contrase�a creado anteriormente para este prop�sito. -> OK
Crear un usuario valido de acceso a sistema. -> OK
Una tarjeta debe ser creada con estado �no vigente� -> OK
Los campos a ingresar son obligatorios -> OK
Quitar todo el saldo de una tarjeta. -> OK
Quitar una parte del saldo de una tarjeta. -> OK
A�adir saldo a una tarjeta. -> OK
Obtener una tarjeta por su GUID -> OK
Obtener las tarjetas por nombre de tarjetahabiente. -> OK
Actualizar el nombre del tarjetahabiente. -> OK
 
Junto con las funcionalidades, se requiere que:
El n�mero de tarjeta (PAN) debe ser v�lido seg�n el formato 1234-1234-1234-1234.
El n�mero de tarjeta debe mostrarse como XXX-XXXX-XXXX-1234
Los datos obligatorios son: Usuario, Contrase�a, PAN, Nombre del TajetaHabiente
El campo PIN es de car�cter sensible.

-

Usage:

- Set up SQL Express 2017
- Set up SQM Management Studio
- Use Windows login in SQL Express, add "Prueba" database
- Open Postman, load project postman collection:

CrearUsuario (copy user from here)
CrearToken (into here, and copy token into Subsequent Postman Header RestAPI services)
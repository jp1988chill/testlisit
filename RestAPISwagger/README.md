Características:
 
Simplicidad.
Conceptos de DDD.
Utilización de CQRS.
Correcta segregación de capas.
Utilización de recursos existentes.
Correcto uso de HttpStatus
Conceptos de RestFul.
Conceptos de Inyección de dependencia.
Seguridad de la información (no exponer información sensible). 

Tareas

Debe modificar el desarrollo del API que permita:

Crear una tarjeta. -> OK
Cada invocación a método debe realizarse de manera segura y utilizando un token que debe entregar en el api vía HEADER. Dicho token debe tener una vigencia de 10 minutos. Use usuario/contraseña creado anteriormente para este propósito. -> OK
Crear un usuario valido de acceso a sistema. -> OK
Una tarjeta debe ser creada con estado “no vigente” -> OK
Los campos a ingresar son obligatorios -> OK
Quitar todo el saldo de una tarjeta. -> OK
Quitar una parte del saldo de una tarjeta. -> OK
Añadir saldo a una tarjeta. -> OK
Obtener una tarjeta por su GUID -> OK
Obtener las tarjetas por nombre de tarjetahabiente. -> OK
Actualizar el nombre del tarjetahabiente. -> OK
 
Junto con las funcionalidades, se requiere que:
El número de tarjeta (PAN) debe ser válido según el formato 1234-1234-1234-1234.
El número de tarjeta debe mostrarse como XXX-XXXX-XXXX-1234
Los datos obligatorios son: Usuario, Contraseña, PAN, Nombre del TajetaHabiente
El campo PIN es de carácter sensible.

-

Usage:

- Set up SQL Express 2017
- Set up SQM Management Studio
- Use Windows login in SQL Express, add "Prueba" database
- Open Postman, load project postman collection:

CrearUsuario (copy user from here)
CrearToken (into here, and copy token into Subsequent Postman Header RestAPI services)
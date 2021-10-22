Cada ítem está hecho para analizar una o más características de desarrollo.

Lo más relevante (y que evaluaremos) es:
 

Simplicidad.
Ocupar herramientas nativas.
Buenas prácticas.
Conceptos de DDD.
Utilización de CQRS.
Correcta segregación de capas.
Utilización de recursos existentes.
Correcto uso de HttpStatus
Conceptos de RestFul.
Conceptos de Inyección de dependencia.
Seguridad de la información (no exponer información sensible).
Interpretación y entendimiento del requerimiento/necesidad.
Uso de transformación de archivos de configuración según ambiente DEV/UAT/PROD
 

Tareas

Debe modificar el desarrollo del API que permita:

Crear una tarjeta. -> OK
Cada invocación a método debe realizarse de manera segura y utilizando un token que debe entregar en el api vía HEADER. Dicho token debe tener una vigencia de 10 minutos. Use usuario/contraseña creado anteriormente para este propósito. -> OK
Crear un usuario valido de acceso a sistema. -> OK
Una tarjeta debe ser creada con estado “no vigente” -> OK
Los campos a ingresar son obligatorios -> OK
Quitar todo el saldo de una tarjeta. -> OK

Todo:
Quitar una parte del saldo de una tarjeta.
Añadir saldo a una tarjeta.
Obtener una tarjeta por su GUID
Obtener las tarjetas por nombre de tarjetahabiente.
Actualizar el nombre del tarjetahabiente.
Establecer el PIN (clave de 4 dígitos) de la tarjeta.
Modificar el estado de una tarjeta, “vigente” o “no vigente”. Si la tarjeta se encuentra “no vigente” sólo se podrá modificar el estado.
Exportar un *.csv con los datos de las tarjetas vigentes con saldo mayor a cero.
 
Junto con las funcionalidades, se requiere que:
El número de tarjeta (PAN) debe ser válido según el formato 1234-1234-1234-1234.
El número de tarjeta debe mostrarse como XXX-XXXX-XXXX-1234
Los datos obligatorios son: Usuario, Contraseña, PAN, Nombre del TajetaHabiente
El campo PIN es de carácter sensible.

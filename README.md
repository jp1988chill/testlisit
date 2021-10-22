Cada �tem est� hecho para analizar una o m�s caracter�sticas de desarrollo.

Lo m�s relevante (y que evaluaremos) es:
 

Simplicidad.
Ocupar herramientas nativas.
Buenas pr�cticas.
Conceptos de DDD.
Utilizaci�n de CQRS.
Correcta segregaci�n de capas.
Utilizaci�n de recursos existentes.
Correcto uso de HttpStatus
Conceptos de RestFul.
Conceptos de Inyecci�n de dependencia.
Seguridad de la informaci�n (no exponer informaci�n sensible).
Interpretaci�n y entendimiento del requerimiento/necesidad.
Uso de transformaci�n de archivos de configuraci�n seg�n ambiente DEV/UAT/PROD
 

Tareas

Debe modificar el desarrollo del API que permita:

Crear una tarjeta.
Quitar todo el saldo de una tarjeta.
Quitar una parte del saldo de una tarjeta.
A�adir saldo a una tarjeta.
Obtener una tarjeta por su GUID
Obtener las tarjetas por nombre de tarjetahabiente.
Actualizar el nombre del tarjetahabiente.
Establecer el PIN (clave de 4 d�gitos) de la tarjeta.
Modificar el estado de una tarjeta, �vigente� o �no vigente�. Si la tarjeta se encuentra �no vigente� s�lo se podr� modificar el estado.
Exportar un *.csv con los datos de las tarjetas vigentes con saldo mayor a cero.
Crear un usuario valido de acceso a sistema.
Cada invocaci�n a m�todo debe realizarse de manera segura y utilizando un token que debe entregar en el api v�a HEADER. Dicho token debe tener una vigencia de 10 minutos. Use usuario/contrase�a creado anteriormente para este prop�sito.
 

Junto con las funcionalidades, se requiere que:

El n�mero de tarjeta (PAN) debe ser v�lido seg�n el formato 1234-1234-1234-1234.
El n�mero de tarjeta debe mostrarse como XXX-XXXX-XXXX-1234
Los datos obligatorios son: Usuario, Contrase�a, PAN, Nombre del TajetaHabiente
Una tarjeta debe ser creada con estado �no vigente�
Los campos a ingresar son obligatorios
El campo PIN es de car�cter sensible.

-



todo:

- set up sql server 2017 / connect to instance from net core -> OK
- build tuples according to model -> OK
- add CQRS + DI from repositories / command handler format
- create CQRS so they connect to model (tuples) in netcore
- implement above tasks
# Menu-backend
Esta es una aplicación web que permite a los usuarios ver el menú de un restaurante y realizar pedidos.

# Requisitos
* .NET 6
* SQL server
* visual studio
* visual studio code

# Características de la Base de datos :
* Se utiliza un ORM(Entity Framework Core ).
* Se utiliza SQL server como base de datos.
* El modelo de datos debe ser como el que visualiza la figura 1.
* Se utiliza Code-First para crear el modelo de base de datos.
* La mercadería, los tipos de mercadería y formas de entrega deben ser cargados automáticamente en la creación de la base de datos. 

# Figura 1: Modelo de Datos
![image](https://github.com/hernan9011/Menu-backend/assets/53132799/8ba7caf2-f7ef-462d-be37-c546786ac0b3)

 
# Características de la API
La aplicación proporciona una API que permite que otras aplicaciones accedan al menú y ordenen datos. Los puntos finales de la API son los siguientes:
* GET ​/api​/v1​/Comanda Enlista las comandas con el detalle de los platos según la fecha que se le ingrese. 
* POST ​/api​/v1​/Comanda Permite Registrar las comandas (el pedido del cliente). 
* ​GET /api​/v1​/Comanda/{id} Tiene búsqueda de comanda por id. 
​* GET /api​/v1​/Mercadería Enlista la información de la mercadería, permite filtrar por nombre y/o tipo y ordenar por precio. 
* POST /api​/v1​/Mercadería Permite registrar la mercadería (platos, bebida o postre). 
* GET /api​/v1​/Mercadería/{id} Tiene búsqueda de mercadería por id. 
* PUT /api​/v1​/Mercadería/{id} Permite modificar la información de la mercadería. 
* DELETE /api​/v1​/Mercadería/{id} Permite eliminar la mercadería.

# Características de la web:
* En la web se muestran todas las opciones de menú que cuenta el restaurante.
* El usuario puede realizar búsquedas, filtrados y ordenamiento de la mercadería. 
* El usuario puede acceder al detalle de la mercadería donde se debe visualizar toda la información que esta presenta. 
* Permite que el usuario arme la comanda con la mercadería que desea y elegir la forma de entrega. 
* La web tiene un sector donde se visualizan todas las comandas del día. 
* La web solo utiliza HTML, CSS y JS. 

# Configuración 
Para configurar y desplegar la aplicación Menú Digital, siga los siguientes pasos:
1. Clonar el repositorio del proyecto desde GitHub.
2. Asegurarse de tener instalado .NET Core SDK en su máquina.
3. Abrir el proyecto en un entorno de desarrollo Visual Studio.
4. Configurar la cadena de conexión a la base de datos en el archivo de configuración correspondiente.
5. Para crear la base de datos utilizando el comando  update-database.

# links
* Enlace al repositorio Frontend: https://github.com/hernan9011/Menu-backend
* Enlace al repositorio Backend: https://github.com/hernan9011/Menu-frontend

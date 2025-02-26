# PostsAPI

1. Lo primero que debe hacer es clonar el repositorio en su dispositivo("git clone https://github.com/MateoBarrios/PostsAPI.git").
2. Despues que el repositorio haya sido clonado se creará una carpeta con el nombre "PostAPI".
3. deberá ir al archivo ("appsettings.json") y colocar el nombre del servidor de sqlserver local. 
4. Dentro de esa carpeta habrá con el mismo nombre, ahi en esa es donde tiene abrir el proyecto(dentro de esa carpeta encontrará lo que es el frontend, controladores, modelos, etc.).
5. Una vez abierto el proyecto deberá ejectur en esa ruta los siguiente comandos("dotnet ef migrations add Proyecto" y despues "dotnet ef database update").
6. Despues inicia el servidor del backend con el siguiente comando("dotnet watch -lp https").
7. Una vez realizado estos pasos se dirige a la carpeta frontend creando otra nueva terminal("cd .\frontend\").
8. Aquí en esta ruta deberá ejectur este comando("npm install").
9. Cuando haya terminado deberá ejecutar este comando ("ng serve --open").
10. Al ejecturse el comando anterior se abrirá una ventana automaicamente donde verá todo el front del proyecto.
11. al momento de crear un post si no le aparece nada en select es porque tiene que agregar las categorias a la base de datos, porque desde allá es que se cargan.

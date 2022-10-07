insert into rol (NombreRol,estado) values('Administrador','True');
insert into rol (NombreRol,estado) values('Auxiliar','True');
insert into rol (NombreRol,estado) values('Cajero','True');
insert into rol (NombreRol,estado) values('Bodeguero','True');


insert into Proveedor(nit,nombreProveedor,direccion,telefono,ciudad,estado)
values('75080013','Juan Calderon','Carrera 9A #10-36','3117479006','Manizales','true')

insert into Proveedor(nit,nombreProveedor,direccion,telefono,ciudad,estado)
values('75080014','Antonio Calderon','Carrera 9A #10-36','6068730216','Manizales','true')


insert into Producto(numeroSerie,descripcion,precio,costo,cantidad,imagenUrl,proveedorId,estado)
values(1234,'Vinos','50000','60000','10','c:\fotos\','1','true');

insert into Producto(numeroSerie,descripcion,precio,costo,cantidad,imagenUrl,proveedorId,estado)
values(1235,'Juguetes','30000','40000','10','c:\fotos\','1','true');

insert into Producto(numeroSerie,descripcion,precio,costo,cantidad,imagenUrl,proveedorId,estado)
values(1236,'Balones','10000','20000','10','c:\fotos\','1','true');

insert into Producto(numeroSerie,descripcion,precio,costo,cantidad,imagenUrl,proveedorId,estado)
values(1237,'Carros','5000','10000','10','c:\fotos\','1','true');


insert into Producto(numeroSerie,descripcion,precio,costo,cantidad,imagenUrl,proveedorId,estado)
values(1238,'Casas','5000','10000','10','c:\fotos\','2','true');

insert into UsuarioAplicacion(nombreUsuario,apellido,telefono,email,userapp,passwordapp,rolId,estado) 
values('Juan','Calderon','3117479006','jcalderongomez@gmail.com','jcalderongomez','12345','1','true');



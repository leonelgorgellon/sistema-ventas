create database DBVenta 

use DBVenta

create table Rol (
	idRol int primary key identity (1,1),
	nombre varchar (50),
	fechaRegistro datetime default getdate() 
)

create table Menu (
	idMenu int primary key identity (1,1),
	nombre varchar (50),
	icono varchar (50),
	url varchar (50)
)

--Relacionamos tabla Menu y Rol 
create table MenuRol (
	idMenuRol int primary key identity (1,1),
	idMenu int references Menu (idMenu),
	idRol int references Rol (idRol)
)

create table Usuario(
	idUsuario int primary key identity (1,1),
	nombreCompleto varchar(100),
	correo varchar (40),
	idRol int references Rol (idRol),
	clave varchar (40),
	esActivo bit default 1, 
	fechaRegistro datetime default getdate()
)

create table Categoria(
	idCategoria int primary key identity (1,1),
	nombre varchar (50),
	esActivo bit default 1,
	fechaRegistro datetime default getdate()
)

create table Producto(
	idProducto int primary key identity (1,1),
	nombre varchar (100),
	idCategoria int references Categoria (idCategoria),
	stock int, 
	precio decimal (10,2),
	esActivo bit default 1, 
	fechaRegistro datetime default getdate()
)

--tabla para un tema de configuracion, aca almaceno los numeros de venta 
-- que voy registrando a medida que vaya subiendo los numeros y por eso registramos el ultimo 
create table NumeroDocumento(
	idNumeroDocumento int primary key identity (1,1),
	ultimo_Numero int not null,
	fechaRegistro datetime default getdate()
)

create table Venta(
	idVenta int primary key identity (1,1),
	numeroDocumento varchar (40),
	tipoPago varchar(50),
	total decimal (10,2),
	fechaRegistro datetime default getdate()
)

--Relacionamos la tabla venta y producto con esta tabla. 
create table DetalleVenta(
	idDetalleVenta int primary key identity (1,1),
	idVenta int references Venta(idVenta),
	idProducto int references Producto(idProducto),
	cantidad int, 
	precio decimal (10,2),
	total decimal (10,2)
)







--Insertamos datos a las tablas. 

insert into Rol (nombre) values 
('Administrador'),
('Empleado'),
('Supervisor')

select * from Rol

insert into Usuario (nombreCompleto, correo, idRol, clave) values 
('Leonel Gorgellon','code@example.com', 1, '123')

select * from Usuario

insert into Categoria(nombre,esActivo) values 
('Laptops',1),
('Monitores',1),
('Teclados',1),
('Auriculares',1),
('Memorias',1),
('Accesorios',1)

select * from Categoria 

insert into Producto(nombre, idCategoria, stock, precio, esActivo) values 
('laptop samsung book pro', 1,20,2500,1),
('laptop lenovo idea pad', 1,30,2200,1),
('laptop asus zenbook duo', 1,30,2100,1),
('monitor teros gaming te-2', 2,25,1050,1),
('monitor samsung curvo', 2,15,1400,1),
('monitor huawei gamer', 2,10,1350,1),
('teclado seisen gamer', 3,10,800,1),
('teclado antryx gamer', 3,10,1000,1),
('teclado logitech', 3,10,1000,1),
('auricular logitech gamer', 4,15,800,1),
('auricular hyperx gamer', 4,20,680,1),
('auricular redragon rgb', 4,25,950,1),
('memoria kingston rgb', 5,10,200,1),
('ventilador cooler master', 6,20,2500,1),
('mini ventilador lenovo', 6,15,200,1)

select * from Producto

--los iconos utilizados aca son de angular Materia 
insert into Menu(nombre,icono,url) values 
('DashBoard','dashboard','/pages/dashboard'),
('Usuarios', 'group','/pages/usuarios'),
('Productos','collections_bookmark','/pages/productos'),
('Venta','currency_exchange','/pages/venta'),
('Historial Ventas','edit_note','/pages/historial_venta'),
('Reportes','receipt','/pages/reportes')

select * from Menu

select * from MenuRol 

--asignamos todos los menu a mi rol que es el administrador. 
insert into MenuRol (idMenu, idRol) values 
(1,1),
(2,1),
(3,1),
(4,1),
(5,1),
(6,1)

--asignamos los menu correspondientes para el rol de los empleados. 
insert into MenuRol (idMenu, idRol) values 
(4,2),
(5,2)

--asignamos los menu correspondientes para el rol de supervisor. 
insert into MenuRol (idMenu, idRol) values
(3,3),
(4,3),
(5,3),
(6,3)


--asignamos cero para que el numero de registro de venta arranque en 0 y sea incremental, y le asignamos fecha actual a la fecha de registro
insert into NumeroDocumento (ultimo_Numero, fechaRegistro) values 
(0,GETDATE())

select * from NumeroDocumento
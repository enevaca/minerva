--DROP TABLE CompraDetalle;
--DROP TABLE Compra;
--DROP TABLE Usuario;
--DROP TABLE Empleado;
--DROP TABLE Producto;
--DROP TABLE Proveedor;
CREATE TABLE Proveedor (
	id INT IDENTITY(1,1) PRIMARY KEY,
	nit BIGINT NOT NULL UNIQUE,
	razonSocial VARCHAR(250) NOT NULL,
	direccion VARCHAR(250) NULL,
	telefono VARCHAR(50) NULL,
	representante VARCHAR(100) NULL
);
CREATE TABLE Producto (
	id INT IDENTITY(1,1) PRIMARY KEY,
	codigo VARCHAR(10) NOT NULL UNIQUE,
	descripcion VARCHAR(250) NOT NULL,
	unidadMedida VARCHAR(50) NOT NULL,
	saldo DECIMAL NOT NULL,
	precioVenta DECIMAL NOT NULL
);
CREATE TABLE Empleado (
	id INT IDENTITY(1,1) PRIMARY KEY,
	cedulaIdentidad VARCHAR(20) NOT NULL UNIQUE,
	nombres VARCHAR(50) NOT NULL,
	paterno VARCHAR(50) NULL,
	materno VARCHAR(50) NULL,
	direccion VARCHAR(250) NULL,
	celuar BIGINT NULL,
	cargo VARCHAR(250)
);
CREATE TABLE Usuario (
	id INT IDENTITY(1,1) PRIMARY KEY,
	usuario VARCHAR(20) NOT NULL UNIQUE,
	clave VARCHAR(50) NOT NULL,
	idEmpleado INT NOT NULL,
	CONSTRAINT FK_Usuario_Empleado FOREIGN KEY(idEmpleado) REFERENCES Empleado(id)
);
CREATE TABLE Compra (
	id INT IDENTITY(1,1) PRIMARY KEY,
	transaccion VARCHAR(10) NOT NULL UNIQUE,
	fecha DATE NOT NULL,
	idProveedor INT NOT NULL,
	CONSTRAINT FK_Compra_Proveedor FOREIGN KEY(idProveedor) REFERENCES Proveedor(id)
);
CREATE TABLE CompraDetalle (
	id INT IDENTITY(1,1) PRIMARY KEY,
	idCompra INT NOT NULL,
	idProducto INT NOT NULL,
	cantidad DECIMAL NOT NULL DEFAULT 0,
	precioUnitario DECIMAL NOT NULL DEFAULT 0,
	total DECIMAL NOT NULL,
	CONSTRAINT FK_CompraDetalle_Compra FOREIGN KEY(idCompra) REFERENCES Compra(id),
	CONSTRAINT FK_CompraDetalle_Producto FOREIGN KEY(idProducto) REFERENCES Producto(id)
);

--Repetir para las otras tablas
ALTER TABLE Proveedor ADD usuarioRegistro VARCHAR(50) DEFAULT SUSER_SNAME();
ALTER TABLE Proveedor ADD fechaRegistro DATETIME DEFAULT GETDATE();
ALTER TABLE Proveedor ADD registroActivo BIT DEFAULT 1;


CREATE PROC paUsuarioListar @parametro VARCHAR(100)
AS
	SELECT u.id as idUsuario, u.idEmpleado, u.usuario, 
		   e.cedulaIdentidad, e.nombres, e.paterno, e.materno,
		   e.direccion,e.celuar,e.cargo
	FROM Usuario u
	INNER JOIN Empleado e ON u.idEmpleado=e.id
	WHERE u.registroActivo=1 
		  AND e.nombres+e.paterno+e.materno LIKE '%'+@parametro+'%'
		  
		  
ALTER PROC paProveedorListar @parametro VARCHAR(50)
AS 
	select id,nit,razonSocial,direccion,telefono,representante,
		   usuarioRegistro,fechaRegistro
	from Proveedor
	where registroActivo=1 and convert(varchar(15),nit)+razonSocial+representante LIKE '%'+@parametro+'%'
	order by razonSocial


EXEC paProveedorListar 'julia'
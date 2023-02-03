--Creo la base de datos Calendar Evento
Create database CalendarEvento
go

--Me ubico en la base de datos Calendar Evento
use CalendarEvento
go

--Creo la tabla Tarea
Create table Tareas (
Id UNIQUEIDENTIFIER primary key not null,
Nombre varchar(100),
Descripcion varchar(100),
Jornada varchar(10),
State bit not null
)

--Creo la tabla Fecha
Create table Fechas(
Id UNIQUEIDENTIFIER primary key not null,
Fecha DateTime not null,
Dia	int,
Mes int,
A�o int,
State bit not null,
Id_Eventos UNIQUEIDENTIFIER not null
)
go

--Relaciono las tablas
Alter table Fechas Add foreign key(Id_Eventos) References Tareas(Id)
Go

--Agrego Columna a la tabla Tarea
--Alter table Tareas add Jornada varchar(10)
--go


--Busqueda de datos
select * from Tareas
go

select * from Fechas
go

select * from Fechas join Tareas on Fechas.Id_Eventos = Tareas.Id
go
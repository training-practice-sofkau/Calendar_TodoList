--Creo la base de datos Calendar Evento
Create database CalendarEvento
go

--Uso la base de datos Calendar Evento
use CalendarEvento
go

--Creo la tabla Tarea
Create table Fechas(
Id UNIQUEIDENTIFIER primary key not null,
Fecha DateTime not null,
Dia	int,
Mes int,
Año int,
State bit not null,
Id_Eventos UNIQUEIDENTIFIER not null
)
go

--Creo la tabla Tarea
Create table Tareas (
Id UNIQUEIDENTIFIER primary key not null,
Nombre varchar(100) not null,
Descripcion varchar(100) not null,
State bit not null
)

--Relaciono las tablas
Alter table Fechas Add foreign key(Id_Eventos) References Tareas(Id)
Go
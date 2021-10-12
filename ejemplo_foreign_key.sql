create table provincia (
	provincia_id	serial primary key, -- Esta línea define una clave primaria autoincremental. 
	                                  -- Será del tipo integer, y generará una secuencia para obtener el valor a utilizar.
	nombre			  varchar(100)
);

create table ciudad (
  ciudad_id     serial primary key,
  nombre        varchar(100),
  provincia_id  integer references provincia -- Esta línea define una foreign key que hace referencia a la clave primaria de la tabla provincia
);

insert into provincia(nombre) values ('Buenos Aires'), ('Santa Fe');
insert into ciudad (nombre, provincia_id) values ('San Nicolás', (select provincia_id from provincia where nombre = 'Buenos Aires'));

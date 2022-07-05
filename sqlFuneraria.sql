create database funerariabdv2;

use funerariabdv2;

create table usuario(
id varchar(50) primary key,
id_rol int  not null,
ci varchar(10) not null,
ci_exp varchar(2) not null,
ciudad varchar(50),
nombre varchar(50) not null,
appaterno varchar(50),
apmaterno varchar(50),
telefono varchar(50),
direccion varchar(150),
contrasenna varchar(50),
foto longtext,
estado bit
);

create table plan_paquete(
id varchar(50) primary key,
nombre varchar(50),
descripcion varchar(150),
precio decimal(10,2),
estado bit
);

create table plan_paquete_imagenes(
id varchar(50) primary key,
id_plan_paquete varchar(50),
imagen longtext,
estado bit
);

create table plan_paquete_servicios(
id varchar(50) primary key,
id_plan_paquete varchar(50),
id_servicio_adicional varchar(50),
precio_costo decimal(10,2),
estado bit
);

create table plan_paquete_items(
id varchar(50) primary key,
id_plan_paquete varchar(50),
id_item_presentacion varchar(50),
precio_costo decimal(10,2),
cantidad int,
estado bit
);

create table categoria(
id varchar(50) primary key,
nombre varchar(50),
descripcion varchar(150),
cajon bit,
estado bit
);

create table item(
id varchar(50) primary key,
id_categoria varchar(50),
codigo varchar(50),
rango_nivel int,
cajon bit,
descripcion varchar(150),
cajon_detalle varchar(50),
cajon_acabado varchar(50),
estado bit
);

create table item_imagenes(
id varchar(50) primary key,
id_item varchar(50),
imagen longtext,
estado bit
);

create table item_presentacion(
id varchar(50)primary key,
id_item varchar(50),
descripcion varchar(150),
cajon bit,
color varchar(50),
unidad_medida varchar(10),
tamaño varchar(50),
precio_compra decimal(10,2),
precio_venta decimal(10,2),
imagen longtext,
stock int,
estado bit
);

create table servicio_adicional(
id varchar(50) primary key,
nombre varchar(50),
descripcion varchar(150),
precio decimal(10,2),
estado bit
);

create table proveedor(
id varchar(50) primary key,
nit varchar(20) not null,
ciudad varchar(50),
razon_social varchar(50),
telefono varchar(20),
direccion varchar(150),
estado bit
);

create table compra(
id varchar(50) primary key,
numero int,
id_proveedor varchar(50),
id_usuario varchar(50),
fecha date,
hora time,
cocepto varchar(150),
estado bit
);

create table compra_detalle(
id int auto_increment primary key,
id_compra varchar(50),
id_item_presentacion varchar(50),
precio decimal(10,2),
cantidad int,
estado bit
);

create table cliente(
id varchar(50) primary key,
ci varchar(10) not null,
ci_exp varchar(2) not null,
ciudad varchar(50),
nombre varchar(50) not null,
appaterno varchar(50),
apmaterno varchar(50),
telefono varchar(50),
direccion varchar(150),
estado bit
);

create table venta(
id varchar(50) primary key,
plan_paquete varchar(50),
id_cliente varchar(50),
id_usuario varchar(50),
correlativo int,
fecha date,
hora time,
concepto varchar(150),
venta bit,
ci_fallecido varchar(10) not null,
ci_exp_fallecido varchar(2) not null,
ciudad_fallecido varchar(50),
nombre_fallecido varchar(50) not null,
appaterno_fallecido varchar(50),
apmaterno_fallecido varchar(50),
fecha_nacimiento_fallecido date,
fecha_fallecimiento_fallecido date,
detalle_fallecimiento varchar(150),
cert_defuncion bit,
cert_defuncion_img longtext,
cert_medico bit,
cert_medico_img longtext,
cortejo_fecha date,
cortejo_hora time,
corejo_direccion varchar(150),
estado bit
);

create table venta_item_presentacion(
id varchar(50) primary key,
id_venta varchar(50),
id_item_presentacion varchar(50),
precio decimal(10,2),
cantidad int,
estado bit
);

create table venta_pagos(
id varchar(50) primary key,
id_usuario varchar(50),
id_venta varchar(50),
fecha date,
hora time,
saldo_inicial decimal(10,2),
saldo_actual decimal(10,2),
saldo_cancelado decimal(10,2),
estado bit
);

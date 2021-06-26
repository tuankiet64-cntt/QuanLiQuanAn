create database  Quanliquancafe;
go
use Quanliquancafe;
go
-- Food
-- Table
-- FoodCategory
-- Account 
-- Bill
-- BillInfo
CREATE TABLE TableFood (
    id INT identity PRIMARY KEY NOT NULL,
    name NVARCHAR(100) NOT NULL,
    status NVARCHAR(100)
)
CREATE TABLE  Account (
    username VARCHAR(100) UNIQUE,
    id INT identity PRIMARY KEY NOT NULL,
    displayname NVARCHAR(255),
    password VARCHAR(255) DEFAULT 123,
    type INT DEFAULT 0
)
create table FoodCategory
(
	id int identity primary key not null,
    name nvarchar(100)
)
CREATE TABLE  Food (
    id INT identity PRIMARY KEY NOT NULL,
    name NVARCHAR(100),
    idCategory INT NOT NULL,
    price FLOAT NOT NULL DEFAULT 0,
    FOREIGN KEY (idCategory)
        REFERENCES FoodCategory (id)
)
create table  bill
(
	id int identity primary key not null,
    datacheckin Date not null default getDate(),
    datecheckout date,
    idtable int not null,
    status int not null default 0, -- 1 là đã thanh toan 0 là chưa thanh toán
    foreign key (idtable) references TableFood(id)
)
create table billInfo
(
	id int identity primary key not null,
    idbill int not null,
    idFood int not null,
    count int not null default 1,
	foreign key (idbill) references bill(id),
    foreign key (idFood) references Food(id)
)

alter table TableFood
alter column status NVARCHAR(100)  default 0;
insert into Account(username,displayname,password,type) 
values('KietTT','KietAdmin',123456,1);


go

create proc USP_Login
@userName varchar(100),@password nvarchar(100)
as
begin
	select * from Account where username=@userName and  password = @password
end
go

DECLARE @i int = 0
while @i<10
begin
Insert TableFood(name,status)
values (N'Bàn'+ cast(@i as nvarchar(100)))
set @i = @i+1
end

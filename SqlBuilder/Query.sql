CREATE DATABASE Quanliquancafe;
GO
USE Quanliquancafe;
GO
-- Food
-- Table
-- FoodCategory
-- Account 
-- Bill
-- BillInfo
CREATE TABLE TableFood (
  id int IDENTITY PRIMARY KEY NOT NULL,
  name nvarchar(100) NOT NULL,
  status nvarchar(100)
)
CREATE TABLE Account (
  username varchar(100) UNIQUE,
  id int IDENTITY PRIMARY KEY NOT NULL,
  displayname nvarchar(255),
  password varchar(255) DEFAULT 123,
  type int DEFAULT 0
)
CREATE TABLE FoodCategory (
  id int IDENTITY PRIMARY KEY NOT NULL,
  name nvarchar(100)
)
CREATE TABLE Food (
  id int IDENTITY PRIMARY KEY NOT NULL,
  name nvarchar(100),
  idCategory int NOT NULL,
  price float NOT NULL DEFAULT 0,
  FOREIGN KEY (idCategory)
  REFERENCES FoodCategory (id)
)
CREATE TABLE bill (
  id int IDENTITY PRIMARY KEY NOT NULL,
  datacheckin date NOT NULL DEFAULT GETDATE(),
  datecheckout date,
  idtable int NOT NULL,
  status int NOT NULL DEFAULT 0, -- 1 là đã thanh toan 0 là chưa thanh toán
  FOREIGN KEY (idtable) REFERENCES TableFood (id)
)
CREATE TABLE billInfo (
  id int IDENTITY PRIMARY KEY NOT NULL,
  idbill int NOT NULL,
  idFood int NOT NULL,
  count int NOT NULL DEFAULT 1,
  FOREIGN KEY (idbill) REFERENCES bill (id),
  FOREIGN KEY (idFood) REFERENCES Food (id)
)

-- add default 

ALTER TABLE TableFood
ADD DEFAULT (N'Trống') FOR status;

-- Insert dữ liệu mẫu
-- Account 
INSERT INTO Account (username, displayname, password, type)
  VALUES ('KietTT', 'KietAdmin', 123456, 1);
GO
DECLARE @i int = 0
WHILE @i < 10
BEGIN
  -- Table
  INSERT TableFood (name)
    VALUES (N'Bàn' + CAST(@i AS nvarchar(100)))
  SET @i = @i + 1
END
GO
-- Category
INSERT INTO FoodCategory (name)
  VALUES (N'Hải sản');
INSERT INTO FoodCategory (name)
  VALUES (N'Heo');
INSERT INTO FoodCategory (name)
  VALUES (N'Bò');
INSERT INTO FoodCategory (name)
  VALUES (N'Ốc');
INSERT INTO FoodCategory (name)
  VALUES (N'Nước ngọt');
INSERT INTO FoodCategory (name)
  VALUES (N'Bia');
GO
-- Food
INSERT INTO Food (name, idCategory, price)
  VALUES (N'Mực một nắng', 1, 20000);
INSERT INTO Food (name, idCategory, price)
  VALUES (N'Sườn cây', 2, 40000);
INSERT INTO Food (name, idCategory, price)
  VALUES (N'Bò xào Hành Tây', 3, 35000);
INSERT INTO Food (name, idCategory, price)
  VALUES (N'Hầu nướng phô mai', 4, 20000);
INSERT INTO Food (name, idCategory, price)
  VALUES (N'7 up', 5, 20000);
INSERT INTO Food (name, idCategory, price)
  VALUES (N'Tiger', 6, 20000);
GO
--Thêm bill
INSERT INTO bill (datecheckout, idtable, status)
  VALUES (NULL, 21, 0);
INSERT INTO bill (datecheckout, idtable, status)
  VALUES (NULL, 22, 0);
INSERT INTO bill (datecheckout, idtable, status)
  VALUES (NULL, 23, 1);
INSERT INTO bill (datecheckout, idtable, status)
  VALUES (NULL, 21, 1);
GO
-- thêm billinfo
INSERT INTO billInfo (idbill, idFood, count)
  VALUES (4, 1, 2);
INSERT INTO billInfo (idbill, idFood, count)
  VALUES (5, 2, 3);
INSERT INTO billInfo (idbill, idFood, count)
  VALUES (2, 1, 2);
INSERT INTO billInfo (idbill, idFood, count)
  VALUES (3, 4, 2);
GO
-- Define proc
-- Proc login

CREATE PROC USP_Login @userName varchar(100), @password nvarchar(100)
AS
BEGIN
  SELECT
    *
  FROM Account
  WHERE username = @userName
  AND password = @password
END
GO


CREATE PROC USP_InstertBill @idTable int
AS
BEGIN
  INSERT INTO bill (datecheckout, idtable, status)
    VALUES (NULL, @idTable, 0);
END
GO

CREATE PROC USP_InstertBillInfo @idbill int, @idFood int, @count int
AS
BEGIN
  INSERT INTO billInfo (idbill, idFood, count)
    VALUES (@idbill, @idFood, @count);
END
GO

ALTER PROC USP_InstertBillInfo @idbill int, @idFood int, @count int
AS
BEGIN
  DECLARE @isExillBillInfo int
  DECLARE @foodCount int = 0

  SELECT
    @isExillBillInfo = id,
    @foodCount = b.count
  FROM billInfo AS b
  WHERE idbill = @idbill
  AND idFood = @idFood
  IF (@isExillBillInfo > 0)
  BEGIN
    DECLARE @newCount int = @foodCount + @count
    IF (@newCount > 0)
      UPDATE billInfo
      SET count = @foodCount + @count
      WHERE idFood = @idFood
    ELSE
      DELETE dbo.billInfo
      WHERE idbill = @idbill
        AND idFood = @idFood
  END
  ELSE
  BEGIN
    IF (@count > 0)
    BEGIN
      INSERT INTO billInfo (idbill, idFood, count)
        VALUES (@idbill, @idFood, @count);
    END
  END
END
GO

 create trigger UTG_UpdateBillInfo
 on billinfo for insert,update
 as 
 begin
	Declare @idbill int
	select @idbill=idbill from Inserted
	declare @idTable int
	select @idTable =idtable from bill where id=@idbill and status =0
	update  TableFood set status =N'Có người' where id =@idTable
 end
 go

 create trigger UTG_UpdateBill
 on bill for update
 as
 begin
	declare @idBill int
	declare @idTable int
	declare @count int
	select @idBill = id from inserted
	select @idTable = idtable from bill where id=@idBill
	-- kiểm tra bàn đó còn cái bill nào chưa thanh toán hay k
	select @count=count(*) from bill where idtable=@idTable and status =0
	if(@count = 0)
	update TableFood set status = N'Trống' 
 end 
 go

 alter trigger UTG_UpdateBill
 on bill for update
 as
 begin
	declare @idBill int
	declare @idTable int
	declare @count int
	select @idBill = id from inserted
	select @idTable = idtable from bill where id=@idBill
	-- kiểm tra bàn đó còn cái bill nào chưa thanh toán hay k
	select @count=count(*) from bill where idtable=@idTable and status =0
	if(@count = 0)
	update TableFood set status = N'Trống' where  id=@idTable
 end 
 go

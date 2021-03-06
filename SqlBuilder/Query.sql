
create DATABASE Quanliquancafe
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
  REFERENCES FoodCategory (id)  ON DELETE CASCADE 
)
CREATE TABLE bill (
  id int IDENTITY PRIMARY KEY NOT NULL,
  datacheckin date NOT NULL DEFAULT GETDATE(),
  datecheckout date,
  idtable int NOT NULL,
  status int NOT NULL DEFAULT 0, -- 1 là đã thanh toan 0 là chưa thanh toán
  FOREIGN KEY (idtable) REFERENCES TableFood (id)  ON DELETE CASCADE 
)
CREATE TABLE billInfo (
  id int IDENTITY PRIMARY KEY NOT NULL,
  idbill int NOT NULL,
  idFood int NOT NULL,
  count int NOT NULL DEFAULT 1,
  FOREIGN KEY (idbill) REFERENCES bill (id) ON DELETE CASCADE ,
  FOREIGN KEY (idFood) REFERENCES Food (id) ON DELETE CASCADE 
)

-- add default 
go 

ALTER TABLE TableFood
ADD DEFAULT (N'Trống') FOR status;
go

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

alter PROC USP_InstertBillInfo @idbill int, @idFood int, @count int
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
  order by id desc
  -- check món đã tồn tại chưa nếu chưa thì add rồi thì update
  IF (@isExillBillInfo > 0)
  BEGIN
    DECLARE @newCount int = @foodCount + @count
    IF (@newCount > 0)
      UPDATE billInfo
      SET count = @foodCount + @count
      WHERE id = @isExillBillInfo
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

CREATE TRIGGER UTG_UpdateBillInfo
ON billinfo
FOR INSERT, UPDATE
AS
BEGIN
  DECLARE @idbill int
  SELECT
    @idbill = idbill
  FROM Inserted
  DECLARE @idTable int
  DECLARE @countBill int
  SELECT
    @idTable = idtable
  FROM bill
  WHERE id = @idbill
  AND status = 0
  SELECT
    @countBill = COUNT(*)
  FROM billInfo
  WHERE idbill = @idbill
  IF (@countBill > 0)
    UPDATE TableFood
    SET status = N'Có người'
    WHERE id = @idTable
  ELSE
    UPDATE TableFood
    SET status = N'Trống'
    WHERE id = @idTable
END
GO



CREATE TRIGGER UTG_UpdateBill
ON bill
FOR UPDATE
AS
BEGIN
  DECLARE @idBill int
  DECLARE @idTable int
  DECLARE @count int
  SELECT
    @idBill = id
  FROM inserted
  SELECT
    @idTable = idtable
  FROM bill
  WHERE id = @idBill
  -- kiểm tra bàn đó còn cái bill nào chưa thanh toán hay k
  SELECT
    @count = COUNT(*)
  FROM bill
  WHERE idtable = @idTable
  AND status = 0
  IF (@count = 0)
    UPDATE TableFood
    SET status = N'Trống'
    WHERE id = @idTable
END
GO


ALTER TABLE bill
ADD discount int

ALTER TABLE bill
ADD DEFAULT 0 FOR discount
GO

create PROC USP_switchtable @idTable1 int, @idTable2 int
AS
BEGIN
  DECLARE @idFirstBill int
  DECLARE @idSecondBill int
  DECLARE @isFirstBIllEmpty int = 1;
  DECLARE @isSecondBIllEmpty int = 1;


  SELECT
    @idFirstBill = id
  FROM bill
  WHERE idtable = @idTable1
  AND status = 0

  SELECT
    @idSecondBill = id
  FROM bill
  WHERE idtable = @idTable2
  AND status = 0

  IF (@idFirstBill IS NULL)
  BEGIN
    EXEC USP_InstertBill @idTable = @idTable1
    SELECT
      @idFirstBill = MAX(id)
    FROM bill
    WHERE idtable = @idTable1
    AND status = 0
  END



  IF (@idSecondBill IS NULL)
  BEGIN
    EXEC USP_InstertBill @idTable = @idTable2
    SELECT
      @idSecondBill = MAX(id)
    FROM bill
    WHERE idtable = @idTable2
    AND status = 0
  END

  SELECT
    id INTO IDBIllinfoTable2
  FROM billInfo
  WHERE idbill = @idSecondBill

  UPDATE billInfo
  SET idbill = @idSecondBill
  WHERE idbill = @idFirstBill

  UPDATE billInfo
  SET idbill = @idFirstBill
  WHERE id IN (SELECT id FROM IDBIllinfoTable2)

  SELECT
    @isFirstBIllEmpty = COUNT(*)
  FROM billinfo
  WHERE idbill = @idFirstBill

  SELECT
    @isSecondBIllEmpty = COUNT(*)
  FROM billinfo
  WHERE idbill = @idSecondBill

  DROP TABLE IDBIllinfoTable2;

  IF (@isFirstBIllEmpty = 0)
    UPDATE TableFood
    SET status = N'Trống'
    WHERE id = @idTable1

  IF (@isSecondBIllEmpty = 0)
    UPDATE TableFood
    SET status = N'Trống'
    WHERE id = @idTable2
END
GO

Create PROC USP_mergetable @idTable1 int, @idTable2 int
AS
BEGIN
  DECLARE @idFirstBill int
  DECLARE @idSecondBill int
  DECLARE @isFirstBIllEmpty int = 1;
  DECLARE @isSecondBIllEmpty int = 1;


  SELECT
    @idFirstBill = id
  FROM bill
  WHERE idtable = @idTable1
  AND status = 0
  SELECT
    @idSecondBill = id
  FROM bill
  WHERE idtable = @idTable2
  AND status = 0

  IF (@idFirstBill IS NULL)
  BEGIN
    EXEC USP_InstertBill @idTable = @idTable1
    SELECT
      @idFirstBill = MAX(id)
    FROM bill
    WHERE idtable = @idTable1
    AND status = 0
  END



  IF (@idSecondBill IS NULL)
  BEGIN
    EXEC USP_InstertBill @idTable = @idTable2
    SELECT
      @idSecondBill = MAX(id)
    FROM bill
    WHERE idtable = @idTable2
    AND status = 0
  END
  
  UPDATE billInfo
  SET idbill = @idSecondBill
  WHERE idbill = @idFirstBill

  SELECT
    @isFirstBIllEmpty = COUNT(*)
  FROM billinfo
  WHERE idbill = @idFirstBill

  SELECT
    @isSecondBIllEmpty = COUNT(*)
  FROM billinfo
  WHERE idbill = @idSecondBill

  IF (@isFirstBIllEmpty = 0)
    UPDATE TableFood
    SET status = N'Trống'
    WHERE id = @idTable1
  IF (@isSecondBIllEmpty = 0)
    UPDATE TableFood
    SET status = N'Trống'
    WHERE id = @idTable2
END
GO
alter table bill
add totalPrice float 
go

create  proc USP_GetListBill
@datecheckin date , @datecheckout date
as
begin	 
	select tb.name as N'Tên bàn',b.datacheckin as N'Ngày tạo đơn',b.datecheckout as N'Ngày chốt đơn', b.totalPrice as N'Tổng tiền',b.discount as N'Giảm Giá'
	from bill b inner join TableFood tb  on b.idtable=tb.id  
	where  datacheckin >= @datecheckin and datecheckout <= @datecheckout and b.status=1
end
go

insert Account  (username,displayname,password)
values('ketoan',N'Kế toán',123)
go

create proc USP_EditInfoAccount
@userName nvarchar(100),@pw nvarchar(100),@displayName nvarchar(100),@newPassword nvarchar(100)
as
begin
	declare @isTrueUser int = 0

	select @isTrueUser=count(*) from Account where username=@userName and password=@pw
	if(@isTrueUser>0)
	begin
	if(@newPassword =null  or @newPassword= '')
	begin
	update Account set displayname=@displayName where username=@userName
	end
	else
		update Account set displayname=@displayName,password=@newPassword where username=@userName
	end
end
go

create trigger UTG_DeleteBillInfo
on billInfo for delete
as
begin
		declare @idbll int
		declare @idbillInfo int
		declare @count int
		declare @idTable int
		select @idbll = idbill,@idbillInfo=deleted.id from deleted
		select @idTable=idtable from bill where id=@idbll;
		select @count=Count(*) from billInfo bi,bill b where b.id=bi.idbill  and bi.idbill=@idbll  and b.status=0
		if(@count=0)
		update TableFood set status= N'Trống' where id=@idTable
end

alter table bill
add default 0 for totalPrice

create proc USP_LoadTableList
as
begin
  select * from TableFood
end

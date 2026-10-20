USE [CreditWorksDB]
GO

INSERT INTO [dbo].[Manufacturers]
           ([Name])
     VALUES
        ('Mazda'),
        ('Mercedes'),
        ('Honda'),
        ('Ferrari'),
        ('Toyota')
GO

INSERT INTO [dbo].[WeightCategories]
           ([Name]
           ,[Icon]
           ,[EndWeight]
           ,[StartWeight])
     VALUES
           ('Light', '/1.jpg', 500.00, 0.00),
('Medium', '/2.jpg', 2500.00, 500.00),
('Heavy', '/3.jpg', NULL, 2500.00)
GO



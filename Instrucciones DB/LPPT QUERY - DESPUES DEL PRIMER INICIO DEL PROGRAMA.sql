USE [LPPT]
GO


INSERT INTO [dbo].[User] ([Username] ,[PasswordHash] ,[Salt])
     VALUES
           ('admin','admin','Administrador del sistema')
GO

INSERT INTO [dbo].[Privileges]([Description])
     VALUES ('GETUSERS')
GO
INSERT INTO [dbo].[Privileges]([Description])
     VALUES ('CREATEUSERS')
GO
INSERT INTO [dbo].[Privileges]([Description])
     VALUES ('UPDATEUSERS')
GO
INSERT INTO [dbo].[Privileges]([Description])
     VALUES ('DELETEUSERS')
GO
INSERT INTO [dbo].[Privileges]([Description])
     VALUES ('GETPRIVILEGES')
GO
INSERT INTO [dbo].[Privileges]([Description])
     VALUES ('CREATEPRIVILEGES')
GO
INSERT INTO [dbo].[Privileges]([Description])
     VALUES ('UPDATEPRIVILEGES')
GO
INSERT INTO [dbo].[Privileges]([Description])
     VALUES ('DELETEPRIVILEGES')
GO

INSERT INTO [dbo].[UsersPrivileges]([UserId],[PrivilegeId])
	VALUES (1,1)
GO
INSERT INTO [dbo].[UsersPrivileges]([UserId],[PrivilegeId])
	VALUES (1,2)
GO
INSERT INTO [dbo].[UsersPrivileges]([UserId],[PrivilegeId])
	VALUES (1,3)
GO
INSERT INTO [dbo].[UsersPrivileges]([UserId],[PrivilegeId])
	VALUES (1,4)
GO
INSERT INTO [dbo].[UsersPrivileges]([UserId],[PrivilegeId])
	VALUES (1,5)
GO
INSERT INTO [dbo].[UsersPrivileges]([UserId],[PrivilegeId])
	VALUES (1,6)
GO
INSERT INTO [dbo].[UsersPrivileges]([UserId],[PrivilegeId])
	VALUES (1,7)
GO
INSERT INTO [dbo].[UsersPrivileges]([UserId],[PrivilegeId])
	VALUES (1,8)
GO



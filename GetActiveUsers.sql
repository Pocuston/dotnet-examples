/****** Object:  StoredProcedure [dbo].[GetActiveUsers]    Script Date: 8/1/2023 12:40:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[GetActiveUsers] @U_ID int
As

SELECT	U_ID, 'Name' = U.FirstName + ' ' + U.LastName
FROM	Users U
WHERE	U.Status IN ('Active', 'Locked')
ORDER BY U.FirstName

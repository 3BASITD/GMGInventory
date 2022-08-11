-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ryan Galano
-- Create date: 8/2/2022
-- Description:	spGetUnservedItemReport
-- =============================================
IF OBJECT_ID ( 'spGetUnservedItemReport', 'P' ) IS NOT NULL
	DROP PROCEDURE spGetUnservedItemReport
GO
CREATE PROCEDURE spGetUnservedItemReport
(
	@StartDate DATETIME = GETDATE,
	@EndDate DATETIME = GETDATE,
	@CategoryId INTEGER = NULL,
	@ItemTypeId INTEGER = NULL,
	@BrandId INTEGER = NULL
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT sol.Id 'Id'
		 , i.Code 'Item'
		 , so.DateCreated 'OrderDate'
		 , so.SalesOrderNumber 'SalesOrderNumber'
		 , c.Name 'Customer'
		 , sol.UnitPrice 'UnitPrice'
		 , sol.Quantity 'Quantity'
		 , sol.PackedQuantity 'PackedQuantity'
		 , sol.Discount1 'Discount1'
		 , sol.Discount2 'Discount2'
		 , a.Code 'Agent'
		 , u.Nickname 'CreatedBy'
	  FROM SalesOrders so
	  JOIN SalesOrderLineItems sol ON so.Id = sol.SalesOrderId
	  JOIN Items i ON sol.ItemId = i.Id
	  JOIN Customers c ON so.CustomerId = c.Id
	  JOIN Agents a ON so.AgentId = a.Id
	  JOIN AspNetUsers u ON so.UserCreated = u.Id
	 WHERE (sol.Quantity - sol.PackedQuantity > 0)
	   AND CAST(@StartDate AS DATE) <= CAST(so.DateCreated AS DATE)
	   AND CAST(so.DateCreated AS DATE) <= CAST(@EndDate AS DATE)
	   AND (@CategoryId IS NULL OR i.CategoryId = @CategoryId)
	   AND (@ItemTypeId IS NULL OR i.ItemTypeId = @ItemTypeId)
	   AND (@BrandId IS NULL OR i.BrandId = @ItemTypeId)
END
GO

--spGetUnservedItemReport '2020-01-01', '2022-08-02'
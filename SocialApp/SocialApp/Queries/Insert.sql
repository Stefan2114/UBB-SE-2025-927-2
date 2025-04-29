go
create procedure InsertNewUser(@u_name varchar(100), @id INT OUTPUT)
as
begin
set nocount on;
insert into users(u_name, u_height, u_weight) values (@u_name, 0, 0);
SET @id = SCOPE_IDENTITY();
end;

GO
CREATE PROCEDURE InsertMealIngredient
    @mealId INT,
    @ingredientId INT,
    @quantity FLOAT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO meal_ingredient (m_id, i_id, quantity)
    VALUES (@mealId, @ingredientId, @quantity);
    RETURN @@ROWCOUNT;
END;
GO

CREATE PROCEDURE sp_GetUserGroceryList
    @UserId INT
AS
BEGIN
    SELECT g.i_id, i.i_name, g.is_checked
    FROM grocery_lists g
    JOIN ingredients i ON g.i_id = i.i_id
    WHERE g.u_id = @UserId
END

GO
CREATE OR ALTER PROCEDURE sp_AddIngredientToUserList
    @UserId INT,
    @IngredientName NVARCHAR(255)
AS
BEGIN
    DECLARE @IngredientId INT;

    SELECT @IngredientId = i_id FROM ingredients WHERE i_name = @IngredientName;
    
	IF @IngredientId IS NULL
    BEGIN
        INSERT INTO ingredients(u_id, i_name, category) VALUES (@UserId, @IngredientName, 'Uncategorized');
        SET @IngredientId = SCOPE_IDENTITY();
    END

    IF NOT EXISTS (
        SELECT 1 FROM grocery_lists WHERE u_id = @UserId AND i_id = @IngredientId
    )
    BEGIN
        INSERT INTO grocery_lists(u_id, i_id, is_checked)
        VALUES (@UserId, @IngredientId, 0);
    END

    SELECT @IngredientId AS i_id;
END
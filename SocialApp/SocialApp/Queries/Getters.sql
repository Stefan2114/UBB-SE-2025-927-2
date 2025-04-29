go
create function GetUserByName(@u_name nvarchar(500))
--returns the id of the user
returns int
as
begin
	declare @u_id int
	select @u_id = u_id from Users where u_name = @u_name
	return @u_id
end

go
create function GetIngredientByName(@i_name nvarchar(50))
--returns the id of the ingredient
returns int
as
begin
	declare @i_id int
	select @i_id = i_id from Ingredients where i_name = @i_name
	return @i_id
end

go
create function GetAllIngredients()
--returns all the ingredients
returns table
as
return (select * from Ingredients)

go
create function GetGoalByDescription(@g_description nvarchar(100))
--returns the id of the goal
returns int
as
begin
	declare @g_id int
	select @g_id = g_id from goals where g_description = @g_description
	return @g_id
end

go
create function GetActivityByDescription(@a_description nvarchar(100))
--returns the id of the activity
returns int
as
begin
	declare @a_id int
	select @a_id = al_id from activity_levels where al_description = @a_description
	return @a_id
end	

go
create function GetCookingSkillByDescription(@cs_description nvarchar(150))
--returns the id of the cooking skill
returns int
as
begin
	declare @cs_id int
	select @cs_id = cs_id from cooking_skills where cs_description = @cs_description
	return @cs_id
end

go
create function GetDietaryPreferencesByDescription(@dp_description nvarchar(150))
--returns the id of the dietary preferences
returns int
as
begin
	declare @dp_id int
	select @dp_id = dp_id from dietary_preferences where dp_description = @dp_description
	return @dp_id
end

go
create function GetAllergiesByDescription(@a_description nvarchar(150))
----returns the id of the allergy
returns int
as 
begin
	declare @a_id int
	select @a_id = a_id from allergies where a_description = @a_description
	return @a_id
end

go
CREATE PROCEDURE GetMealsByCategory
    @category VARCHAR(50)
AS
BEGIN
    SELECT 
        m.m_name as Name,
        m.recipe as Recipe,
        m.calories as Calories,
        mt.m_description as Category,
        m.protein as Protein,
        m.carbohydrates as Carbohydrates,
        m.fat as Fat,
        m.fiber as Fiber,
        m.sugar as Sugar,
        m.photo_link as PhotoLink,
        m.preparation_time as PreparationTime,
        m.servings as Servings
    FROM meals m
    JOIN meal_types mt ON m.mt_id = mt.mt_id
    WHERE mt.m_description = @category;
END


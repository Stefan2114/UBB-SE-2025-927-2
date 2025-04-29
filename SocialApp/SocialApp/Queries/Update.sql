go
create procedure UpdateUserBodyMetrics(@u_id int, @u_weight float, @u_height float, @u_goal float)
as
--if the user is newly created the bodymetrics are going to be null but here this is changed
begin 
set nocount on;
update users set  
	u_weight = @u_weight,
	u_height = @u_height,
	target_weight = @u_goal
where u_id = @u_id;
return @@ROWCOUNT
end;

go
create procedure UpdateUserGoals(@u_id int, @g_id int)
as
begin
set nocount on;
update users set g_id = @g_id 
where u_id = @u_id;
return @@ROWCOUNT
end;

go
create procedure UpdateUserActivity(@u_id int, @a_id int)
as
begin
set nocount on;
update users set al_id = @a_id 
where u_id = @u_id;
return @@ROWCOUNT
end;

go
create procedure UpdateUserCookingSkill(@u_id int, @cs_id int)
as
begin
set nocount on;
update users set cs_id = @cs_id 
where u_id = @u_id;
return @@ROWCOUNT
end;

go
create procedure UpdateUserDietaryPreference(@u_id int, @dp_id int)
as
begin
set nocount on;
update users set dp_id = @dp_id 
where u_id = @u_id;
return @@ROWCOUNT
end;

go
create procedure UpdateUserAllergies(@u_id int, @a_id int)
as
begin
set nocount on;
update users set a_id = @a_id 
where u_id = @u_id;
return @@ROWCOUNT
end;


GO
CREATE PROCEDURE sp_UpdateGroceryItemChecked
    @UserId INT,
    @IngredientId INT,
    @IsChecked BIT
AS
BEGIN
    UPDATE grocery_lists
    SET is_checked = @IsChecked
    WHERE u_id = @UserId AND i_id = @IngredientId
END
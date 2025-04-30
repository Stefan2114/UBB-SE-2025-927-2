use [Meal_Planner];

create table goals (
	g_id int primary key identity (1,1),
	g_description varchar(100) not null
)

create table cooking_skills (
	cs_id int primary key identity(1,1),
	cs_description varchar(150) not null
)

create table allergies (
	a_id int primary key identity(1,1),
	a_description varchar(150) not null
)

create table dietary_preferences (
	dp_id int primary key identity(1,1),
	dp_description varchar(150) not null
)

create table activity_levels (
	al_id int primary key identity(1,1),
	al_description varchar(150) not null
)

create table users (
	u_id int primary key identity (1,1),
	u_name varchar(150) not null,
	dob date,
	email varchar(250),
	u_height float not null,
	u_weight float not null,
	g_id int foreign key references goals(g_id),
	cs_id int foreign key references cooking_skills(cs_id),
	dp_id int foreign key references dietary_preferences(dp_id),
	a_id int foreign key references allergies(a_id),
	al_id int foreign key references activity_levels(al_id),
	target_weight float
)

create table calorie_trackers(
	u_id int foreign key references users(u_id),
	today date,
	daily_intake float,
	calories_consumed float,
	calories_burned float
)

create table water_trackers(
	u_id int foreign key references users(u_id),
	water_intake float,
    water_goal float
)

create table ingredients (
	i_id int primary key identity(1,1),
	u_id int foreign key references users(u_id), -- can be null
	i_name varchar(150) not null,
	category varchar(150) not null,
	protein float,
	calories float,
	carbohydrates float,
	fat float,
	fiber float,
	sugar float
)

create table grocery_lists(
	u_id int foreign key references users(u_id),
	i_id int foreign key references ingredients(i_id),
	is_checked bit
)

create table meal_types(
	mt_id int primary key identity(1,1),
	m_description varchar(150) not null
)

create table meals(
	m_id int primary key identity(1,1),
	u_id int foreign key references users(u_id), --can be null
	m_name varchar(150) not null,
	recipe varchar(2000) not null,
	cs_id int foreign key references cooking_skills(cs_id),
	dp_id int foreign key references dietary_preferences(dp_id),
	mt_id int foreign key references meal_types(mt_id),
	preparation_time float,
	servings float,
	protein float,
	calories float,
	carbohydrates float,
	fat float,
	fiber float,
	sugar float,	
	photo_link varchar(1000)
)

create table meal_ingredient(
	m_id int foreign key references meals(m_id),
	i_id int foreign key references ingredients(i_id),
	quantity float
)

create table favourite_meal(
	m_id int foreign key references meals(m_id),
	u_id int foreign key references users(u_id),
	is_fav bit
)

create table calorie_trackers(
	u_id int foreign key references users(u_id),
	today date,
	daily_intake float, -- goal
	calories_consumed float, -- food
	calories_burned float -- exercise
)

CREATE TABLE serving_units (
    unit_name VARCHAR(50) PRIMARY KEY,  -- Unique key for each unit, 'unit_name' as the primary key
);

CREATE TABLE daily_meals (
    dm_id INT PRIMARY KEY IDENTITY(1,1),  -- Unique entry for each meal logged
    u_id INT NULL,  -- The user who ate the meal, now optional (can be NULL)
    m_id INT NOT NULL FOREIGN KEY REFERENCES meals(m_id),  -- The meal they ate
    date_eaten DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE), -- The day they ate it
    servings FLOAT NOT NULL DEFAULT 1,  -- How many servings were eaten
    unit_name VARCHAR(50) NOT NULL,  -- The serving unit type (now referring to 'unit_name' from 'serving_units')
    
    -- Foreign key reference to the serving_units table using unit_name
    FOREIGN KEY (unit_name) REFERENCES serving_units(unit_name),

    total_calories FLOAT,  -- Total calories based on servings
    total_protein FLOAT,   -- Total protein based on servings
    total_carbohydrates FLOAT,  -- Total carbohydrates based on servings
    total_fat FLOAT,       -- Total fat based on servings
    total_fiber FLOAT,     -- Total fiber based on servings
    total_sugar FLOAT,     -- Total sugar based on servings
    FOREIGN KEY (u_id) REFERENCES users(u_id)  -- User foreign key can be null now
);
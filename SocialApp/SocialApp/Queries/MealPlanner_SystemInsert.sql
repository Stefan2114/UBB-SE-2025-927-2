use [Meal_Planner];

insert into goals(g_description) values 
('Lose weight'),
('Gain weight'),
('Maintain weight'),
('Body recomposition'),
('Improve overall health');

insert activity_levels (al_description) values 
('Sedentary'),
('Lightly Active'),
('Moderately Active'),
('Very Active'),
('Super Active');

insert into cooking_skills (cs_description) values 
('I am a beginner'),
('I cook sometimes'),
('I love cooking'),
('I prefer quick meals'),
('I meal prep');

insert into dietary_preferences (dp_description) values
('None'),
('Mediterranean'),
('Low-Fat'),
('Diabetic-Friendly'),
('Kosher'),
('Halal');

insert into allergies (a_description) values
('None'),
('Peanuts'),
('Tree Nuts'),
('Dairy'),
('Eggs'),
('Gluten'),
('Shellfish'),
('Soy'),
('Fish'),
('Sesame');

INSERT INTO meal_types (m_description) VALUES 
('Breakfast'),
('Lunch'),
('Dinner'),
('Snack'),
('Dessert');

INSERT INTO meals (m_name, recipe, cs_id, dp_id, mt_id, preparation_time, servings, protein, calories, carbohydrates, fat, fiber, sugar, photo_link) VALUES 
('Tomato and Feta Baked Eggs', 'Bake eggs with tomatoes and feta cheese.', 2, 1, 1, 25, 2, 20, 350, 15, 10, 3, 5, 'https://hips.hearstapps.com/hmg-prod/images/ghk050124tomatofetabakedeggs-66194dcb8115d.jpg?crop=1.00xw:0.669xh;0,0.221xh&resize=1200:*'),
('Double Apple Baked Oatmeal', 'Mix oats, apples, and bake.', 1, 2, 1, 30, 4, 10, 280, 40, 5, 6, 12, 'https://hips.hearstapps.com/hmg-prod/images/double-apple-baked-oatmeal-64c95ef3000a6.jpg?crop=1.00xw:0.668xh;0,0.0932xh&resize=1200:*'),
('Sweet Potato Breakfast Burritos', 'Wrap scrambled eggs and sweet potatoes in a tortilla.', 3, 1, 1, 20, 2, 18, 400, 35, 12, 7, 6, 'https://hips.hearstapps.com/hmg-prod/images/sweet-potato-breakfast-burritos-64fa102ec946c.jpg?crop=0.901xw:0.602xh;0.0815xw,0.165xh&resize=1200:*'),
('Loaded Pancake Tacos', 'Make pancakes and use them as taco shells with fillings.', 2, 2, 2, 15, 2, 12, 450, 55, 10, 5, 9, 'https://hips.hearstapps.com/hmg-prod/images/loaded-pancake-tacos-1675720416.jpg?crop=0.669xw:1.00xh;0.172xw,0&resize=1200:*'),
('Sausage and Egg Sandwiches', 'Cook sausage and eggs, place in a bun.', 2, 3, 2, 10, 1, 22, 500, 45, 15, 4, 2, 'https://hips.hearstapps.com/hmg-prod/images/sausage-and-egg-sandwiches-1647548891.jpg?crop=1xw:0.9989806320081549xh;center,top&resize=1200:*'),
('Rice With Salmon and Broccoli', 'Cook rice and serve with grilled salmon and steamed broccoli.', 4, 3, 3, 25, 2, 40, 600, 50, 15, 7, 4, 'https://hips.hearstapps.com/hmg-prod/images/creamy-kale-pasta-6400be3282ed9.jpg?crop=0.738xw:0.493xh;0,0.311xh&resize=1200:*'),
('Sheet Pan Roasted Chicken', 'Roast chicken with vegetables on a sheet pan.', 3, 1, 3, 35, 3, 50, 700, 20, 20, 6, 3, 'https://hips.hearstapps.com/hmg-prod/images/easy-rice-with-ginger-soy-salmon-and-broccoli-64c95ab03fbfe.jpg?crop=1.00xw:0.670xh;0,0.190xh&resize=1200:*'),
('Air Fryer Strawberry-Thyme Scones', 'Mix dough with strawberries and bake in an air fryer.', 1, 4, 4, 20, 6, 6, 250, 35, 8, 2, 15, 'https://hips.hearstapps.com/hmg-prod/images/sheet-pan-roasted-chicken-64d1462c915de.jpg?crop=0.881xw:0.588xh;0.119xw,0.258xh&resize=1200:*'),
('Manchego Devils on Horseback', 'Wrap Manchego cheese with bacon and bake.', 2, 2, 4, 15, 4, 14, 300, 10, 18, 3, 2, 'https://hips.hearstapps.com/hmg-prod/images/fall-mushroom-pizza-1638485679.jpg?crop=0.923xw:0.616xh;0.0765xw,0.136xh&resize=1200:*'),
('Creamy Kale Pasta', 'Cook pasta and mix with creamy kale sauce.', 3, 3, 3, 25, 2, 25, 480, 50, 12, 5, 6, 'https://hips.hearstapps.com/hmg-prod/images/strawberry-thyme-mini-scones-65bd45d31ab00.jpg?crop=0.857xw:0.572xh;0.0816xw,0.299xh&resize=1200:*'),
('Cheesy Mushroom Pizza', 'Prepare pizza dough, add mushrooms and cheese, then bake.', 3, 4, 3, 30, 3, 28, 550, 60, 18, 4, 7, 'https://hips.hearstapps.com/hmg-prod/images/manchego-devils-on-horseback-654e8ce46109e.jpg?crop=1.00xw:0.667xh;0,0.143xh&resize=1200:*'),
('Parmesan Seeded Crackers', 'Mix dough with parmesan and seeds, bake until crispy.', 2, 5, 4, 20, 8, 5, 200, 25, 8, 3, 10, 'https://hips.hearstapps.com/hmg-prod/images/parmesan-seeded-crackers-1667927056.jpg?crop=1.00xw:0.667xh;0,0.0999xh&resize=1200:*'); 
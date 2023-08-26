-- Insert mock data into the [Menus] table
INSERT INTO [dbo].[Menus] ([BusinessId], [MenuName], [MenuUrl], [OrderBy], [isActive])
VALUES
    (1, 'Breakfast', '/menus/breakfast', 1, 1),
    (1, 'Lunch', '/menus/lunch', 2, 1),
    (1, 'Dinner', '/menus/dinner', 3, 1),
    (2, 'Appetizers', '/menus/appetizers', 1, 1),
    (2, 'Main Course', '/menus/main-course', 2, 1),
    (2, 'Desserts', '/menus/desserts', 3, 1);



-- -- Insert mock data into the [Menus] table
-- INSERT INTO [dbo].[Menus] ([BusinessId], [MenuName], [MenuUrl], [OrderBy], [isActive])
-- VALUES
--     (1, 'Home', '/menus/breakfast', 1, 1),
--     (1, 'Products', '/menus/lunch', 2, 1),
--     (1, 'Delivery', '/menus/dinner', 3, 1),
--     (1, 'Gallary', '/menus/appetizers', 1, 1)

-- -- Display message
-- PRINT 'Mock data inserted into [Menus] table.';

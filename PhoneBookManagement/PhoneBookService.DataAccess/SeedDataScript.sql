DECLARE @Counter INT = 0;
DECLARE @UserId INT = 0;
DECLARE @Counter2 INT = 0;

SELECT * INTO #TEMP_TBL FROM 
	(VALUES
			('Nancy', 1),
			('Davolio', 2),
			('Andrew', 3),
			('Fuller', 4),
			('Janet', 5),
			('Leverling', 6),
			('Margaret', 7),
			('Peacock', 8),
			('Steven', 9),
			('Buchanan', 10),
			('Suyama', 11),
			('Michael', 12),
			('Robert', 13),
			('King', 14),
			('Laura', 15),
			('Callahan', 16),
			('Anne', 17),
			('Dodsworth', 18),
			('Albert', 19),
			('Hellstern', 20),
			('Tim', 21),
			('Smith', 22),
			('Caroline', 23),
			('Patterson', 24),
			('Justin', 25),
			('Brid', 26),
			('Xavier', 27),
			('Martin', 28),
			('Laurent', 29),
			('Pereira', 30)
	) T(Name, Id);

WHILE @Counter < 100
BEGIN
	DECLARE @Name NVARCHAR(MAX) = (SELECT name FROM #TEMP_TBL WHERE id = (SELECT FLOOR(RAND()*(30-1+1)+1)));
	DECLARE @Surname NVARCHAR(MAX) = (SELECT name FROM #TEMP_TBL WHERE id = (SELECT FLOOR(RAND()*(30-1+1)+1)));

	INSERT INTO dbo.UserProfile 
	values(@Name, @Surname);

	SET @UserId = SCOPE_IDENTITY();
	SET @Counter2 = 0;
	DECLARE @IsMulti INT = (SELECT FLOOR(RAND()*(3-1+1)+1));

	WHILE @Counter2 < @IsMulti
	BEGIN
		INSERT INTO dbo.PhoneNumber
		VALUES (@UserId, CONCAT('+', (SELECT FLOOR(RAND()*(99-10+1)+10))), (SELECT FLOOR(RAND()*(999999999-100000000+1)+100000000)));

		SET @Counter2 = @Counter2 + 1;
	END

	SET @Counter = @Counter + 1;
END

DROP TABLE #TEMP_TBL;
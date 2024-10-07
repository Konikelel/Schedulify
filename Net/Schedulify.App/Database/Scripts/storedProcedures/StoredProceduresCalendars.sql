CREATE OR ALTER PROCEDURE dbo.spCalendarsCount
    @Id UNIQUEIDENTIFIER = NULL,
    @OwnerId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SELECT COUNT(1)
    FROM Calendars
    WHERE Id = ISNULL(@Id, Id) AND OwnerId = ISNULL(@OwnerId, OwnerId)
END;
GO;



CREATE OR ALTER PROCEDURE dbo.spCalendarsGet
    @ReturnFirst BIT,
    @Id UNIQUEIDENTIFIER = NULL,
    @OwnerId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    IF (@ReturnFirst = 1)
        BEGIN
            SELECT TOP 1 Id, Name, CreatedAt, UpdatedAt, OwnerId
            FROM Calendars
            WHERE Id = ISNULL(@Id, Id) AND OwnerId = ISNULL(@OwnerId, OwnerId)
        END
    ELSE
        BEGIN
            SELECT Id, Name, CreatedAt, UpdatedAt, OwnerId
            FROM Calendars
            WHERE Id = ISNULL(@Id, Id) AND OwnerId = ISNULL(@OwnerId, OwnerId)
        END
END;
GO;



CREATE OR ALTER PROCEDURE dbo.spCalendarsDelete
    @Id UNIQUEIDENTIFIER = NULL,
    @OwnerId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    DELETE FROM Calendars
    WHERE Id = ISNULL(@Id, Id) AND OwnerId = ISNULL(@OwnerId, OwnerId)
END;
GO;



CREATE OR ALTER PROCEDURE dbo.spCalendarsInsert
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(64),
    @CreatedAt DATETIMEOFFSET,
    @UpdatedAt DATETIMEOFFSET,
    @OwnerId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO Calendars (Id, Name, CreatedAt, UpdatedAt, OwnerId)
    VALUES (@Id, @Name, ISNULL(@CreatedAt, @UpdatedAt), @UpdatedAt, @OwnerId)
END;
GO;



CREATE OR ALTER PROCEDURE dbo.spCalendarsUpdate
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(64),
    @UpdatedAt DATETIMEOFFSET,
    @OwnerId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE Calendars
    SET Name = @Name, UpdatedAt = @UpdatedAt, OwnerId = @OwnerId
    WHERE Id = @Id
END;
GO;
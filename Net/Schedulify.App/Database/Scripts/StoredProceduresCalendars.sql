CREATE OR ALTER PROCEDURE dbo.spCalendarsCount
/* EXEC dbo.spCalendarsCount @OwnerId=# */
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
/* EXEC dbo.spCalendarsGet @OwnerId=# */
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
/* EXEC dbo.spCalendarsDelete @OwnerId=# */
    @Id UNIQUEIDENTIFIER = NULL,
    @OwnerId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    DELETE FROM Calendars
    WHERE Id = ISNULL(@Id, Id) AND OwnerId = ISNULL(@OwnerId, OwnerId)
END;
GO;

CREATE OR ALTER PROCEDURE dbo.spCalendarsUpsert
/* EXEC dbo.spCalendarsUpsert @Id=NEWID(), @Name=N'John Doe', @CreatedAt=SYSDATETIMEOFFSET(), @UpdatedAt=SYSDATETIMEOFFSET(), @OwnerId=# */
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(64),
    @CreatedAt DATETIMEOFFSET = NULL,
    @UpdatedAt DATETIMEOFFSET,
    @OwnerId UNIQUEIDENTIFIER
AS
BEGIN
    IF EXISTS(SELECT 1 FROM Calendars WHERE Id=@Id)
        BEGIN
            UPDATE Calendars
            SET Name = @Name, UpdatedAt = @UpdatedAt, OwnerId = @OwnerId
            WHERE Id = @Id
        END
    ELSE
        BEGIN
            INSERT INTO Calendars (Id, Name, CreatedAt, UpdatedAt, OwnerId)
            VALUES (@Id, @Name, ISNULL(@CreatedAt, @UpdatedAt), @UpdatedAt, @OwnerId)
        END
END;
GO;


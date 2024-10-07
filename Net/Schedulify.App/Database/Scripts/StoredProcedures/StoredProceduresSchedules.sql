CREATE OR ALTER PROCEDURE dbo.spSchedulesCount
    @Id UNIQUEIDENTIFIER = NULL,
    @CalenderId UNIQUEIDENTIFIER = NULL,
    @CategoryId UNIQUEIDENTIFIER = NULL, /* CAN BE NULL */
    @OwnerId UNIQUEIDENTIFIER = NULL,
    @UseCategoryId BIT = 0
AS
BEGIN
    SELECT COUNT(1)
    FROM Schedules
    WHERE Id = ISNULL(@Id, Id) AND
          CalendarId = ISNULL(@CalenderId, CalendarId) AND
          CategoryId = IIF(@UseCategoryId = 1, @CategoryId, CategoryId) AND
          OwnerId = ISNULL(@OwnerId, OwnerId)
END;
GO;


CREATE OR ALTER PROCEDURE dbo.spSchedulesGet
    @ReturnFirst BIT,
    @Id UNIQUEIDENTIFIER = NULL,
    @CalenderId UNIQUEIDENTIFIER = NULL,
    @CategoryId UNIQUEIDENTIFIER = NULL, /* CAN BE NULL */
    @OwnerId UNIQUEIDENTIFIER = NULL,
    @UseCategoryId BIT = 0
AS
BEGIN
    IF (@ReturnFirst = 1)
        BEGIN
            SELECT TOP 1 Id, CalendarId, CategoryId, TimeStart, TimeEnd, Frequency, Title, Description, Link, CreatedAt, UpdatedAt, OwnerId
            FROM Schedules
            WHERE Id = ISNULL(@Id, Id) AND
                  CalendarId = ISNULL(@CalenderId, CalendarId) AND
                  CategoryId = IIF(@UseCategoryId = 1, @CategoryId, CategoryId) AND
                  OwnerId = ISNULL(@OwnerId, OwnerId)
        END
    ELSE
        BEGIN
            SELECT Id, CalendarId, CategoryId, TimeStart, TimeEnd, Frequency, Title, Description, Link, CreatedAt, UpdatedAt, OwnerId
            FROM Schedules
            WHERE Id = ISNULL(@Id, Id) AND
                  CalendarId = ISNULL(@CalenderId, CalendarId) AND
                  CategoryId = IIF(@UseCategoryId = 1, @CategoryId, CategoryId) AND
                  OwnerId = ISNULL(@OwnerId, OwnerId)
        END
END;
GO;



CREATE OR ALTER PROCEDURE dbo.spSchedulesDelete
    @Id UNIQUEIDENTIFIER = NULL,
    @CalenderId UNIQUEIDENTIFIER = NULL,
    @CategoryId UNIQUEIDENTIFIER = NULL, /* CAN BE NULL */
    @OwnerId UNIQUEIDENTIFIER = NULL,
    @UseCategoryId BIT = 0
AS
BEGIN
    DELETE FROM Schedules
    WHERE Id = ISNULL(@Id, Id) AND
          CalendarId = ISNULL(@CalenderId, CalendarId) AND
          CategoryId = IIF(@UseCategoryId = 1, @CategoryId, CategoryId) AND
          OwnerId = ISNULL(@OwnerId, OwnerId)
END;
GO;



CREATE OR ALTER PROCEDURE dbo.spSchedulesInsert
    @Id UNIQUEIDENTIFIER,
    @CalendarId UNIQUEIDENTIFIER,
    @CategoryId UNIQUEIDENTIFIER,
    @TimeStart DATETIMEOFFSET,
    @TimeEnd DATETIMEOFFSET,
    @Frequency INT,
    @Title NVARCHAR(64),
    @Description NVARCHAR(MAX),
    @Link NVARCHAR(MAX),
    @CreatedAt DATETIMEOFFSET,
    @UpdatedAt DATETIMEOFFSET,
    @OwnerId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO Schedules (
        Id, CalendarId, CategoryId, TimeStart, TimeEnd, Frequency, Title, Description, Link, CreatedAt, UpdatedAt, OwnerId
    ) VALUES (@Id,
              @CalendarId,
              @CategoryId,
              @TimeStart,
              @TimeEnd,
              @Frequency,
              @Title,
              @Description,
              @Link,
              ISNULL(@CreatedAt,SYSDATETIMEOFFSET()),
              @UpdatedAt,
              @OwnerId
    )
END;
GO;



CREATE OR ALTER PROCEDURE dbo.spSchedulesUpdate
    @Id UNIQUEIDENTIFIER,
    @CalendarId UNIQUEIDENTIFIER,
    @CategoryId UNIQUEIDENTIFIER,
    @TimeStart DATETIMEOFFSET,
    @TimeEnd DATETIMEOFFSET,
    @Frequency INT,
    @Title NVARCHAR(64),
    @Description NVARCHAR(MAX),
    @Link NVARCHAR(MAX),
    @CreatedAt DATETIMEOFFSET = NULL,
    @UpdatedAt DATETIMEOFFSET,
    @OwnerId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE Schedules SET
        CalendarId = @CalendarId,
        CategoryId = @CategoryId,
        TimeStart = @TimeStart,
        TimeEnd = @TimeEnd,
        Frequency = @Frequency,
        Title = @Title,
        Description = @Description,
        Link = @Link,
        UpdatedAt = @UpdatedAt,
        OwnerId = @OwnerId
    WHERE Id = @Id

END;
GO;



CREATE OR ALTER PROCEDURE dbo.spSchedulesUpdateCategory
    @NewCategoryId UNIQUEIDENTIFIER,
    @OldCategoryId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE Schedules
    SET CategoryId = @NewCategoryId
    WHERE CategoryId = @OldCategoryId
END;
GO;
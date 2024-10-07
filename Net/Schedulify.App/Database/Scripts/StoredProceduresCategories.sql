CREATE OR ALTER PROCEDURE dbo.spCategoriesCount
/* EXEC dbo.spCategoriesCount @OwnerId=# */
    @Id UNIQUEIDENTIFIER = NULL,
    @OwnerId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SELECT COUNT(1)
    FROM Categories
    WHERE Id = ISNULL(@Id, Id) AND OwnerId = ISNULL(@OwnerId, OwnerId)
END;
GO;


CREATE OR ALTER PROCEDURE dbo.spCategoriesGet
/* EXEC dbo.spCategoriesGet @OwnerId=# */
    @ReturnFirst BIT,
    @Id UNIQUEIDENTIFIER = NULL,
    @OwnerId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    IF (@ReturnFirst = 1)
        BEGIN
            SELECT TOP 1 Id, Name, CreatedAt, UpdatedAt, OwnerId
            FROM Categories
            WHERE Id = ISNULL(@Id, Id) AND OwnerId = ISNULL(@OwnerId, OwnerId)
        END
    ELSE
        BEGIN
            SELECT Id, Name, CreatedAt, UpdatedAt, OwnerId
            FROM Categories
            WHERE Id = ISNULL(@Id, Id) AND OwnerId = ISNULL(@OwnerId, OwnerId)
        END
END;
GO;

CREATE OR ALTER PROCEDURE dbo.spCategoriesDelete
/* EXEC dbo.spCategoriesDelete @OwnerId=# */
    @Id UNIQUEIDENTIFIER = NULL,
    @OwnerId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    DELETE FROM Categories
    WHERE Id = ISNULL(@Id, Id) AND OwnerId = ISNULL(@OwnerId, OwnerId)
END;
GO;

CREATE OR ALTER PROCEDURE dbo.spCategoriesUpsert
/* EXEC dbo.spCategoriesUpsert @Id=NEWID(), @Name=N'John Doe', @CreatedAt=SYSDATETIMEOFFSET(), @UpdatedAt=SYSDATETIMEOFFSET(), @OwnerId=# */
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(64),
    @CreatedAt DATETIMEOFFSET = NULL,
    @UpdatedAt DATETIMEOFFSET,
    @OwnerId UNIQUEIDENTIFIER
AS
BEGIN
    IF EXISTS(SELECT 1 FROM Categories WHERE Id=@Id)
        BEGIN
            UPDATE Categories
            SET Name = @Name, UpdatedAt = @UpdatedAt, OwnerId = @OwnerId
            WHERE Id = @Id
        END
    ELSE
        BEGIN
            INSERT INTO Categories (Id, Name, CreatedAt, UpdatedAt, OwnerId)
            VALUES (@Id, @Name, ISNULL(@CreatedAt, @UpdatedAt), @UpdatedAt, @OwnerId)
        END
END;
GO;

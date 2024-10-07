CREATE OR ALTER PROCEDURE dbo.spCategoriesCount
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
    @Id UNIQUEIDENTIFIER = NULL,
    @OwnerId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    DELETE FROM Categories
    WHERE Id = ISNULL(@Id, Id) AND OwnerId = ISNULL(@OwnerId, OwnerId)
END;
GO;



CREATE OR ALTER PROCEDURE dbo.spCategoriesInsert
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(64),
    @CreatedAt DATETIMEOFFSET,
    @UpdatedAt DATETIMEOFFSET,
    @OwnerId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO Categories (Id, Name, CreatedAt, UpdatedAt, OwnerId)
    VALUES (@Id, @Name, ISNULL(@CreatedAt, @UpdatedAt), @UpdatedAt, @OwnerId)
END;
GO;

CREATE OR ALTER PROCEDURE dbo.spCategoriesUpdate
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(64),
    @UpdatedAt DATETIMEOFFSET,
    @OwnerId UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE Categories
    SET Name = @Name, UpdatedAt = @UpdatedAt, OwnerId = @OwnerId
    WHERE Id = @Id
END;
GO;
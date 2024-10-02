DROP TABLE Users;
GO;

CREATE TABLE Users
(
    Id           UNIQUEIDENTIFIER PRIMARY KEY,
    Username     NVARCHAR(64)   NOT NULL,
    Email        NVARCHAR(64)   NOT NULL,
    ImageUrl     NVARCHAR(MAX),
    PasswordHash VARBINARY(64)  NOT NULL,
    PasswordSalt VARBINARY(128) NOT NULL,
    CreatedAt    DATETIMEOFFSET NOT NULL,
    UpdatedAt    DATETIMEOFFSET NOT NULL
);
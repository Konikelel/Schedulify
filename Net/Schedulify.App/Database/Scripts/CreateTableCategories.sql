﻿DROP TABLE Categories;
GO;

CREATE TABLE Categories (
    Id        UNIQUEIDENTIFIER  PRIMARY KEY,
    Name      NVARCHAR(64)      NOT NULL,
    CreatedAt DATETIMEOFFSET    NOT NULL,
    UpdatedAt DATETIMEOFFSET    NOT NULL,
    Author    UNIQUEIDENTIFIER  NOT NULL,
    
    CONSTRAINT FK_CategoriesAuthor FOREIGN KEY (Author) REFERENCES Users(Id)
);
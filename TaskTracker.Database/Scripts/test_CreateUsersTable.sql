CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Email NVARCHAR(255) NOT NULL UNIQUE,
    FullName NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(MAX) NULL, --For regular registration
    AvatarUrl NVARCHAR(MAX) NULL,    -- Azure Blob URL
    Role NVARCHAR(50) NOT NULL DEFAULT 'User', -- 'Admin', 'User'
    IsActive BIT NOT NULL DEFAULT 1,
    
    -- Audit
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
);
GO

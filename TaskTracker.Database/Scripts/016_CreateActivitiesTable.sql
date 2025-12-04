CREATE TABLE Activities (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    ActionType NVARCHAR(50) NOT NULL, -- 'Create', 'Update', 'Delete', 'Archive', 'Comment'
    EntityType NVARCHAR(50) NOT NULL, -- 'User', 'Board', 'Task', 'Comment', 'Attachment'
    EntityId UNIQUEIDENTIFIER NOT NULL,
    BoardId UNIQUEIDENTIFIER NULL,
    OldValues NVARCHAR(MAX) NULL,
    NewValues NVARCHAR(MAX) NULL,
    IPAddress NVARCHAR(45) NULL,
    UserAgent NVARCHAR(500) NULL,
    
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    
    CONSTRAINT FK_Activities_User FOREIGN KEY (UserId) 
        REFERENCES Users(Id),
    CONSTRAINT FK_Activities_Board FOREIGN KEY (BoardId) 
        REFERENCES Boards(Id) ON DELETE SET NULL
);

CREATE INDEX IX_Activities_Entity ON Activities(EntityType, EntityId);
CREATE INDEX IX_Activities_Board ON Activities(BoardId, CreatedAt DESC);
CREATE INDEX IX_Activities_User ON Activities(UserId, CreatedAt DESC);

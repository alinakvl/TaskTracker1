namespace TaskTracker.Domain.Constants;

public static class BoardMemberRoles
{
    public const int Owner = 1;
    public const int Admin = 2;
    public const int Member = 3;

    public static string GetRoleName(int role) => role switch
    {
        Owner => "Owner",
        Admin => "Admin",
        Member => "Member",
        _ => "Unknown"
    };
}

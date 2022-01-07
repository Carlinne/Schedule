namespace App.Common.Enums
{
    public enum AppStatus
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3,
        Cancel = 4,
        Done = 5
    }
    public enum ResultCode
    {
        Success = 1,
        Alert = 2,
        Warning = 3,
        Fatal = 4,
        Unauthorized = 5,
        AttemptsExceeded = 6,
        BlockBySystem = 7
    }
}

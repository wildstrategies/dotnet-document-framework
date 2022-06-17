namespace WildStrategies.DocumentFramework
{
    public static class Extensions
    {
        public static string SerializeAsString(this TimeOnly value) => value.ToString("HH:mm:ss.fffffff");
        public static string SerializeAsString(this DateOnly value) => value.ToString("yyyy-MM-dd");
    }
}

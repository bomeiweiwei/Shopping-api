namespace MyShop.Api.Options
{
    public class ApiLoggingOptions
    {
        /// <summary>
        /// 符合其中任一條件的 Path，將不記錄 Request / Response Body
        /// </summary>
        public List<string> SensitivePathKeywords { get; set; } = new();
    }

}

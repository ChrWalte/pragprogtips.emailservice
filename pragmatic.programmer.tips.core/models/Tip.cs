namespace pragmatic.programmer.tips.core.models
{
    /// <summary>
    /// Object used to store tip data. Used in the tip repository and service.
    /// </summary>
    public class Tip
    {
        public string Number { get; set; } = string.Empty;
        public string Page { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
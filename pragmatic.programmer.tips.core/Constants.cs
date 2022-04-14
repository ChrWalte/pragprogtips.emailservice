namespace pragmatic.programmer.tips.core
{
    /// <summary>
    /// file containing magic variables that are used throughout the core of the application
    /// </summary>
    internal class Constants
    {
        public const string FileLocationOfRawTipsTextFile = "\\data\\raw\\raw.tips.txt";
        public const string FileLocationOfRawTipsJsonFile = "\\data\\raw\\raw.tips.json";
        public const string FileLocationOfRawTipIdentifierTextFile = "\\data\\raw\\raw.tip.identifiers.txt";
        public const string FileLocationOfRawEmailTemplateHtmlFile = "\\data\\raw\\raw.email.template.html";

        public const string TodoAddMethodOfPullingTipsFromSource =
            "TODO: Add a way of getting tips from https://pragprog.com/tips/";

        public const string TodoAddMethodOfPullingTipsFromJsonFile =
            "TODO: Add a way of getting tips from raw.tips.json file";

        public const string TodoAddMethodForGettingRandomTipsFromRemembranceCache =
            "TODO: Add a cache system for the random numbers to remember what numbers have already been sent";
    }
}
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

        // Created using Regex Generator:
        // https://regex-generator.olafneumann.org/?sampleText=%5B%402022-04-14%2017%3A00%3A01Z%20%7C%20DEBUG%5D&flags=i&onlyPatterns=true&matchWholeLine=false&selection=0%7CSquare%20brackets,1%7CCharacter,2%7CDate,12%7CCharacter,13%7CTime,21%7CCharacter,22%7CCharacter,23%7CCharacter,24%7CCharacter,25%7CMultiple%20characters
        public const string LogRegex = @"\[@(?<datetimestamp>[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}(\.[0-9]{1,3})?Z) \| (?<loglevel>[a-zA-Z]+)]";
    }
}
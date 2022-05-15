namespace pragmatic.programmer.tips.core
{
    /// <summary>
    /// file containing magic variables that are used throughout the core of the application
    /// </summary>
    internal static class Constants
    {
        public const string FileLocationOfRawTipsTextFile = "data/raw/raw.tips.txt";
        public const string FileLocationOfRawTipsJsonFile = "data/raw/raw.tips.json";
        public const string FileLocationOfRawTipIdentifierTextFile = "data/raw/raw.tip.identifiers.txt";
        public const string FileLocationOfRawEmailTemplateHtmlFile = "data/raw/raw.email.template.html";
        public const string FileLocationOfRawMailingListJsonFile = "data/raw/raw.mailing.list.json";

        public const string TodoAddMethodOfPullingTipsFromSource =
            "TODO: Add a way of getting tips from https://pragprog.com/tips/";

        // Created using Regex Generator:
        // https://regex-generator.olafneumann.org/?sampleText=%5B%402022-04-14%2017%3A00%3A01Z%20%7C%20DEBUG%5D&flags=i&onlyPatterns=true&matchWholeLine=false&selection=0%7CSquare%20brackets,1%7CCharacter,2%7CDate,12%7CCharacter,13%7CTime,21%7CCharacter,22%7CCharacter,23%7CCharacter,24%7CCharacter,25%7CMultiple%20characters
        public const string LogRegex = @"\[@(?<datetimestamp>[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}(\.[0-9]{1,3})?Z) \| (?<loglevel>[a-zA-Z]+)]";

        public const string ThisDirectory = "./";
        public const string ColonDelimiter = ":";
        public const string LogLevelRegexGroupKey = "loglevel";

        public const string CertificateCouldNotBeConvertedIntoX509Certificate2 = "The certificate could not be converted into a X509Certificate2, could not continue";

        public const string CertificateValidationCallbackChainErrorStartMessage =
            "The certificate for the server could not be validated for the following reasons: ";

        public const string EnteredGetRandomTip = "entered GetRandomTipAsync()";
        public const string ExitedGetRandomTip = "exited GetRandomTipAsync()";
        public const string ExitedGetRandomTipWithRemembrance = "exited GetRandomTipWithRemembranceAsync()";
        public const string ResetTipIdentifierRemembranceFile = "reset tip identifier text file using _tipsRepository.DeleteTipIdentifierTextFile()";
        public const string StoredTipIdentifierInRemembranceFile = "stored tip identifier using _tipsRepository.WriteTipIdentifiersToTextFile(...)";
        public const string EnteredGetAllTips = "entered GetAllTipsAsync()";
        public const string ExitedGetAllTips = "exited GetAllTipsAsync()";
        public const string EnteredGetEntireMailingList = "entered GetEntireMailingListAsync()";
        public const string ExitedGetEntireMailingList = "exited GetEntireMailingListAsync()";
        public const string EnteredAddEmailToMailingList = "entered AddEmailToMailingListAsync(...)";
        public const string ExitedAddEmailToMailingList = "exited AddEmailToMailingListAsync(...)";
        public const string EnteredRemoveEmailFromMailingList = "entered RemoveEmailFromMailingListAsync(...)";
        public const string ExitedRemoveEmailFromMailingList = "exited RemoveEmailFromMailingListAsync(...)";
        public const string EnteredGetRandomTipWithRemembrance = "entered GetRandomTipWithRemembranceAsync()";

        public const string GotRandomTipUsingRandomNumber = "got random tip using random number";
        public const string NoEmailInMailingListMatchingEmail = "no email in mailing list matching provided email";
        public const string FoundExistingEmailInMailingList = "found existing email in mailing list";
        public const string EmailAlreadyIncludedInMailingList = "existingMailingList already contains provided email";
        public const string EmailAddedToMailingList = "email added to existingMailingList";
        public const string EmailRemovedFromMailingList = "email removed from existingMailingList";
    }
}
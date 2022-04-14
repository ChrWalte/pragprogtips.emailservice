using Newtonsoft.Json;
using pragmatic.programmer.tips.core.data.interfaces;
using pragmatic.programmer.tips.core.models;

namespace pragmatic.programmer.tips.core.data
{
    /// <summary>
    /// Methods for obtaining Pragmatic Programmer Tip data.
    /// </summary>
    public class PragmaticProgrammerTipsRepository : IPragmaticProgrammerTipsRepository
    {
        public PragmaticProgrammerTipsRepository()
        { }

        /// <summary>
        /// reads a list of tip objects from a plain text file containing lines of Pragmatic Programmer Tip data.
        /// </summary>
        /// <returns>list of Pragmatic Programmer Tips</returns>
        public async Task<IEnumerable<Tip>> ReadFromRawTipsTextFile()
        {
            var tips = new List<Tip>();
            var rootPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) ?? ".\\";
            var tipsTextFilePath = Path.Join(rootPath, Constants.FileLocationOfRawTipsTextFile);
            var rawLines = await File.ReadAllLinesAsync(tipsTextFilePath);

            // see ./raw/raw.tips.txt OR Constants.FileLocationOfRawTipsTextFile to make sense of this
            const int startOfFile = 0;
            const int tipNumberOffset = 1;
            const int tipPageOffset = 2;
            const int tipTitleOffset = 3;
            const int tipDescriptionOffset = 4;
            const int tipsOffset = 5;

            for (var i = startOfFile; i < rawLines.Length; i += tipsOffset)
            {
                // if current index plus number of lines per tip is greater than
                // the length of the entire file, reached the end of the file
                if (i + tipDescriptionOffset > rawLines.Length)
                    break;

                tips.Add(new Tip
                {
                    Number = rawLines[i + tipNumberOffset],
                    Page = rawLines[i + tipPageOffset],
                    Title = rawLines[i + tipTitleOffset],
                    Description = rawLines[i + tipDescriptionOffset],
                });
            }

            return tips;
        }

        /// <summary>
        /// reads a list of tip objects from a JSON file containing the Pragmatic Programmer Tip data.
        /// </summary>
        /// <returns>list of Pragmatic Programmer Tips</returns>
        public async Task<IEnumerable<Tip>> ReadFromRawTipsJsonFile()
        {
            var rootPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) ?? ".\\";
            var tipsTextFilePath = Path.Join(rootPath, Constants.FileLocationOfRawTipsJsonFile);
            var rawLines = await File.ReadAllTextAsync(tipsTextFilePath);
            var tips = JsonConvert.DeserializeObject<List<Tip>>(rawLines);
            return tips ?? new List<Tip>();
        }

        // TODO: Add a way of getting/updating raw.tips.txt file.
        // source: Pragmatic Programmer Tips https://pragprog.com/tips/
        /// <summary>
        /// TODO: Add summary...
        /// </summary>
        /// <returns>TODO: Add what it returns...</returns>
        /// <exception cref="NotImplementedException">because its not implemented</exception>
        public IEnumerable<Tip> GetTipsFromPragProgSource()
        {
            throw new NotImplementedException(Constants.TodoAddMethodOfPullingTipsFromSource);
        }
    }
}
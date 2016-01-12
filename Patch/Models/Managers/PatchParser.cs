using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patch.Models.Managers
{
    class PatchParser
    {
        /// <summary>
        ///  List contains parsed file objects 
        /// </summary>
        private List<Difference> diffs;

        /// <summary>
        /// Constructor
        /// </summary>
        public PatchParser()
        {
            diffs = new List<Difference>();
        }

        /// <summary>
        /// Returns parsed patch file
        /// </summary>
        /// <param name="patch"></param>
        /// <returns></returns>
        public List<Difference> ParsePatchToDiffs(List<string> patch)
        {
            return ParsePatch(patch);
        }

        /// <summary>
        /// Method parses patch file to list witth diffData objects using during showing changes
        /// </summary>
        /// <param name="patch"></param>
        private List<Difference> ParsePatch(List<string> patch)
        {
            int firstIndex, secondIndex, deletedAmount, insertedAmount;

            for (int index = 0; index < patch.Count; ++index)
            {
                // set init values
                firstIndex = secondIndex = deletedAmount = insertedAmount = 0;

                // check if line contains @@ -l,s +l,s @@ structure
                if (patch[index].StartsWith("@@ -"))
                {
                    // pull out start indexes
                    int startIndex = patch[index].IndexOf('-') + 1;
                    firstIndex = Int32.Parse(patch[index].Substring(startIndex, patch[index].IndexOf(',') - startIndex)) - 1;

                    startIndex = patch[index].IndexOf('+') + 1;
                    secondIndex = Int32.Parse(patch[index].Substring(startIndex, patch[index].LastIndexOf(',') - startIndex)) - 1;

                    // increase index to start counting deleted and inserted lines
                    ++index;

                    // Counts deleted and inserted lines
                    while (!patch[index].StartsWith("@@ -"))
                    {
                        if (patch[index].StartsWith("-"))
                        {
                            ++deletedAmount;
                        }
                        else if (patch[index].StartsWith("+"))
                        {
                            ++insertedAmount;
                        }

                        ++index;

                        if (index >= patch.Count)
                        {
                            break;
                        }
                    }

                    /// insert Difference object to list
                    diffs.Add(new Difference(firstIndex, secondIndex, deletedAmount, insertedAmount));

                    /// decrease index becouse there is increase in for loop
                    --index;
                }
            }

            return diffs;
        }
    }
}

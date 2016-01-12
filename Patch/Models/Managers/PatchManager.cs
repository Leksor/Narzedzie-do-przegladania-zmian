using Patch.Models.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Patch.Models
{
    class PatchManager
    {
        // List with data to generating patch
        private List<string> _firstFile;
        private List<string> _secondFile;

        /// <summary>
        /// Construcor
        /// </summary>
        /// 
        public PatchManager()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstFile"></param>
        /// <param name="secondFile"></param>
        public PatchManager(List<string> firstFile, List<string> secondFile)
        {
            _firstFile = firstFile;
            _secondFile = secondFile;
        }

        #region Methods


        public List<string> RollbackChangesFromFile(List<string> data, List<string> patch)
        {
            // collection on updated file
            List<string> updatedFile = new List<string>();

            // iterate foreach line and create updated file
            // we start from 3rd line cuz 2 first lines ar file names with timestamps
            for (int i = 2; i < patch.Count; ++i)
            {
                if (!patch[i].StartsWith("@@ -") && !patch[i].StartsWith("+"))
                {
                    if (patch[i].StartsWith("-"))
                    {
                        updatedFile.Add(patch[i].Remove(0, 1));
                    }
                    else
                    {
                        updatedFile.Add(patch[i]);
                    }
                }
            }

            // return updated file
            return updatedFile;
        }

        public void RollbackChangesFromFolder(List<FileProperties> files, List<string> patch, FileManager fileManager)
        {
            string name;
            List<string> updatedFile;
            List<string> singlePatch = new List<string>();

            for (int i = 0; i < patch.Count; ++i)
            {
                // we found new patch and we have to find file to lay it 
                if (patch[i].StartsWith("--- "))
                {
                    name = patch[i];
                    updatedFile = new List<string>();

                    singlePatch.Add(patch[i]);
                    singlePatch.Add(patch[i + 1]);

                    // Load patch for 1 file
                    for (int j = i + 2; j < patch.Count; ++j)
                    {
                        // check if new patch started
                        if (patch[j].StartsWith("--- "))
                        {
                            j = j - 1;
                            break;
                        }
                        if (!patch[j].StartsWith("@@ -") && !patch[j].StartsWith("+"))
                        {
                            if (patch[j].StartsWith("-"))
                            {
                                updatedFile.Add(patch[j].Remove(0, 1));
                            }
                            else
                            {
                                updatedFile.Add(patch[j]);
                            }
                        }

                        singlePatch.Add(patch[j]);
                    }

                    // Apply patch
                    SaveUpdatedFile(files, updatedFile, singlePatch, name, fileManager);

                    // clear patch collection
                    singlePatch.Clear();
                    updatedFile.Clear();
                }
            }
        }

        private void SaveUpdatedFile(List<FileProperties> files, List<string> updatedFile, List<string> singlePatch, string filePath, FileManager fileManager)
        {
            foreach (FileProperties fp in files)
            {
                if (fp.AbsolutePath != string.Empty && filePath.Contains(fp.AbsolutePath))
                {
                    // We found searching object, and we add updatedFile to it
                    fp.AddUpdatedFile(updatedFile, singlePatch);
                    fileManager.SaveUpdatedFile(updatedFile, fp.FilePath);
                }
                else if (fp.children != null)
                {
                    SaveUpdatedFile(fp.children, updatedFile, singlePatch, filePath, fileManager);
                }
            }
        }

        /// <summary>
        /// Method generates patch
        /// </summary>
        public List<string> GeneratePatch(FileProperties firstFileInfo, FileProperties secondFileInfo)
        {
            /// Creates file with changes (final version of patch)
            return CreatePatch(_firstFile, _secondFile, DiffText(_firstFile, _secondFile, false), firstFileInfo, secondFileInfo);
        }

        /// <summary>
        /// Method returns generated patch from two loaded folders as collection of strings
        /// </summary>
        /// <param name="firstFolderFiles"></param>
        /// <param name="secondFolderFiles"></param>
        /// <returns></returns>
        public List<string> GeneratePatchFromFolder(List<FileProperties> firstFolderFiles, List<FileProperties> secondFolderFiles)
        {
            List<string> patch = new List<string>();

            CreatePatchFromFolders(firstFolderFiles[0].children, secondFolderFiles[0].children, patch);

            return patch;
        }

        /// <summary>
        /// Method creates patch from two loaded folders
        /// </summary>
        /// <param name="firstFolderFiles"></param>
        /// <param name="secondFolderFiles"></param>
        /// <param name="patch"></param>
        private void CreatePatchFromFolders(List<FileProperties> firstFolderFiles, List<FileProperties> secondFolderFiles, List<string> patch)
        {
            foreach (FileProperties fp in firstFolderFiles)
            {
                if (fp.AbsolutePath != string.Empty)
                {
                    FileProperties obj = secondFolderFiles.Find(x => x.AbsolutePath.Equals(fp.AbsolutePath));

                    if (obj != null && fp.fileData.Count != 0 && obj.fileData.Count != 0)
                    {
                        patch.AddRange(CreatePatch(fp.fileData, obj.fileData, DiffText(fp.fileData, obj.fileData, false), fp, obj));
                    }
                }
                else if (fp.children != null)
                {
                    FileProperties obj = secondFolderFiles.Find(x => x.Name.Equals(fp.Name));

                    if (obj != null)
                    {
                        CreatePatchFromFolders(fp.children, obj.children, patch);
                    }
                }
            }
        }

        /// <summary>
        /// Method lays patch on choosen file
        /// </summary>
        public List<string> GenerateUpdatedFile(List<string> fileData, List<string> patchData)
        {
            // parse patch to diffs data objects and pass it to LayPatch method
            return LayPatch(fileData, patchData);
        }

        public void GenerateUpdatedFileFromFolder(List<FileProperties> files, List<string> patch)
        {
            // Lays patch on loaded folder files
            LayPatchFromFolder(files, patch);           
        }

        /// <summary>
        /// Method lays patch on changed files
        /// </summary>
        /// <param name="files"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        private void LayPatchFromFolder(List<FileProperties> files, List<string> patch)
        {
            string name;
            List<string> updatedFile;
            List<string> singlePatch = new List<string>();

            for (int i = 0; i < patch.Count; ++i)
            {
                // we found new patch and we have to find file to lay it 
                if (patch[i].StartsWith("--- "))
                {
                    name = patch[i];
                    updatedFile = new List<string>();

                    singlePatch.Add(patch[i]);
                    singlePatch.Add(patch[i + 1]);

                    // Load patch for 1 file
                    for (int j = i + 2; j < patch.Count; ++j)
                    {                       
                        if (!patch[j].StartsWith("@@ -") && !patch[j].StartsWith("-"))
                        {
                            if (patch[j].StartsWith("+"))
                            {
                                updatedFile.Add(patch[j].Remove(0, 1));
                            }
                            else
                            {
                                updatedFile.Add(patch[j]);
                            }
                        }
                        // check if new patch started
                        if(patch[j].StartsWith("--- "))
                        {
                            i = j - 1;
                            break;
                        }

                        singlePatch.Add(patch[j]);
                    }

                    // Apply patch
                    AddUpdatedFile(files, updatedFile, singlePatch, name);

                    // clear patch collection
                    singlePatch.Clear();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="updatedFile"></param>
        private void AddUpdatedFile(List<FileProperties> files, List<string> updatedFile, List<string> singlePatch, string filePath)
        {
            foreach (FileProperties fp in files)
            {
                if (fp.AbsolutePath != string.Empty && filePath.Contains(fp.AbsolutePath))
                {
                    // We found searching object, and we add updatedFile to it
                    fp.AddUpdatedFile(updatedFile, singlePatch);
                }
                else if (fp.children != null)
                {
                    AddUpdatedFile(fp.children, updatedFile, singlePatch, filePath);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        private List<string> LayPatch(List<string> fileData, List<string> patch)
        {
            // collection on updated file
            List<string> updatedFile = new List<string>();

            // iterate foreach line and create updated file
            // we start from 3rd line cuz 2 first lines ar file names with timestamps
            for(int i = 2; i < patch.Count; ++i)
            {
                if(!patch[i].StartsWith("@@ -") && !patch[i].StartsWith("-"))
                {
                    if(patch[i].StartsWith("+"))
                    {
                        updatedFile.Add(patch[i].Remove(0, 1));
                    }
                    else
                    {
                        updatedFile.Add(patch[i]);
                    }                   
                }
            }

            // return updated file
            return updatedFile;
        }

        /// <summary>
        /// Method creates patch from files difference objects
        /// </summary>
        /// <param name="differences"></param>
        /// <returns></returns>
        private List<string> CreatePatch(List<string> firstFile, List<string> secondFile, List<Difference> differences, FileProperties firstFileInfo, FileProperties secondFileInfo)
        {
            List<string> patch = new List<string>();
            List<string> diffRange = new List<string>();
            string lastAction = string.Empty;
            int linesLevel = 0;
            int firstRange = 0;
            int secondRange = 0;
            int firstIndex = 0;
            int secondIndex = 0;
            int deletedIndex, insertedIndex;

            // if there are diffs beetwen files we create patch
            if (differences.Count != 0)
            {
                patch.Add("--- " + firstFileInfo.FilePath + " " + DateTime.Now.ToString());
                patch.Add("+++ " + secondFileInfo.FilePath + " " + DateTime.Now.ToString());

                // Iterate every diff object and update first file
                for (int x = 0; x < differences.Count; ++x)
                {
                    // diffrence between deleted and inserted lines
                    // result give us 3 possible operations
                    int res = differences[x].deletedAmount - differences[x].insertedAmount;

                    // set indexes to patch file
                    deletedIndex = firstIndex + 1;
                    insertedIndex = secondIndex + 1;

                    // 1. if deleted and inserted are equal
                    if (res.Equals(0))
                    {
                        // complete start lines
                        for (int i = firstIndex; i < differences[x].deletedIndex; ++i)
                        {
                            diffRange.Add(firstFile[i]);

                            ++firstIndex; ++firstRange;
                            ++secondIndex; ++secondRange;
                        }

                        // Insert changed lines with modyfied style
                        for (int i = 0; i < differences[x].deletedAmount; ++i)
                        {
                            diffRange.Add("-" + firstFile[firstIndex]);
                            diffRange.Add("+" + secondFile[secondIndex]);

                            ++firstIndex; ++firstRange;
                            ++secondIndex; ++secondRange;
                        }

                        // complete lines to next diff

                        // there is more than 1 diff
                        if (x + 1 < differences.Count)
                        {
                            int size;

                            if (lastAction.Equals("d"))
                            {
                                size = Math.Min(differences[x + 1].deletedIndex, differences[x + 1].insertedIndex - linesLevel);
                                for (int i = firstIndex; i < size; ++i)
                                {
                                    diffRange.Add(firstFile[i]);

                                    ++firstIndex; ++firstRange;
                                    ++secondIndex; ++secondRange;
                                }
                            }
                            else
                            {
                                size = Math.Min(differences[x + 1].deletedIndex - linesLevel, differences[x + 1].insertedIndex);
                                for (int i = secondIndex; i < size; ++i)
                                {
                                    diffRange.Add(secondFile[i]);

                                    ++firstIndex; ++firstRange;
                                    ++secondIndex; ++secondRange;
                                }
                            }


                        }
                        // we are at last diff
                        else
                        {
                            for (int i = firstIndex; i < firstFile.Count; ++i)
                            {
                                diffRange.Add(firstFile[i]);

                                ++firstIndex; ++firstRange;
                                ++secondIndex; ++secondRange;
                            }
                        }
                    }

                    // 2. if deleted < inserted
                    if (res < 0)
                    {
                        if (lastAction.Equals("d"))
                        {
                            linesLevel *= -1;
                        }
                        // complete start lines to first file
                        for (int i = firstIndex; i < differences[x].insertedIndex + linesLevel; ++i)
                        {
                            diffRange.Add(firstFile[i]);

                            ++firstIndex; ++firstRange;
                            ++secondIndex; ++secondRange;
                        }

                        // if there is some changed lines
                        if (differences[x].deletedAmount > 0)
                        {
                            // we add changed lines to first file
                            for (int i = 0; i < differences[x].deletedAmount; ++i)
                            {
                                diffRange.Add("-" + firstFile[firstIndex]);
                                diffRange.Add("+" + secondFile[secondIndex]);

                                ++firstIndex; ++firstRange;
                                ++secondIndex; ++secondRange;
                            }
                        }

                        // add inserted lines
                        for (int i = 0; i < differences[x].insertedAmount - differences[x].deletedAmount; ++i)
                        {
                            diffRange.Add("+" + secondFile[secondIndex]);

                            ++secondIndex; ++secondRange;
                        }

                        /// set actual line lvl
                        linesLevel += res;
                        lastAction = "i";

                        // complete lines to next diff

                        // there is more than 1 diff
                        if (x + 1 < differences.Count)
                        {
                            for (int i = secondIndex; i < Math.Min(differences[x + 1].deletedIndex - linesLevel, differences[x + 1].insertedIndex); ++i)
                            {
                                diffRange.Add(secondFile[i]);

                                ++firstIndex; ++firstRange;
                                ++secondIndex; ++secondRange;
                            }
                        }
                        // we are at last diff
                        else
                        {
                            for (int i = firstIndex; i < firstFile.Count; ++i)
                            {
                                diffRange.Add(firstFile[i]);

                                ++firstIndex; ++firstRange;
                                ++secondIndex; ++secondRange;
                            }
                        }
                    }

                    // 3. if deleted > inserted
                    if (res > 0)
                    {
                        if (lastAction.Equals("i"))
                        {
                            linesLevel *= -1;
                        }
                        // complete start lines to first file
                        for (int i = firstIndex; i < differences[x].deletedIndex; ++i)
                        {
                            diffRange.Add(firstFile[i]);

                            ++firstIndex; ++firstRange;
                            ++secondIndex; ++secondRange;
                        }

                        // if there is some changed lines
                        if (differences[x].insertedAmount > 0)
                        {
                            // we add changed lines to first file
                            for (int i = 0; i < differences[x].insertedAmount; ++i)
                            {
                                diffRange.Add("-" + firstFile[firstIndex]);
                                diffRange.Add("+" + secondFile[secondIndex]);

                                ++firstIndex; ++firstRange;
                                ++secondIndex; ++secondRange;
                            }
                        }

                        // add deleted lines
                        for (int i = 0; i < differences[x].deletedAmount - differences[x].insertedAmount; ++i)
                        {
                            diffRange.Add("-" + firstFile[firstIndex]);

                            ++firstIndex; ++firstRange;
                        }

                        linesLevel -= res;
                        lastAction = "d";

                        // complete lines to next diff

                        // there is more than 1 diff
                        if (x + 1 < differences.Count)
                        {
                            for (int i = firstIndex; i < Math.Min(differences[x + 1].deletedIndex, differences[x + 1].insertedIndex - linesLevel); ++i)
                            {
                                diffRange.Add(firstFile[firstIndex]);

                                ++firstIndex; ++firstRange;
                                ++secondIndex; ++secondRange;
                            }
                        }
                        // we are at last diff
                        else
                        {
                            for (int i = firstIndex; i < firstFile.Count; ++i)
                            {
                                diffRange.Add(firstFile[i]);

                                ++firstIndex; ++firstRange;
                                ++secondIndex; ++secondRange;
                            }
                        }
                    }

                    // add range line and collection with lines
                    patch.Add("@@ -" + deletedIndex.ToString() + "," + firstRange.ToString() + " +" + insertedIndex.ToString() + "," + secondRange.ToString() + " @@");
                    patch.AddRange(diffRange);

                    // Clear diffrange collection
                    diffRange.Clear();

                    // init variables
                    firstRange = secondRange = 0;
                }
            }

            return patch;
        }

        /// <summary>
        /// Methods count file length including new line signs
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private int GetFileLength(List<string> file)
        {
            int charSum = 0;

            /// Iterate after each line and sum their length
            foreach(string s in file)
            {
                charSum += s.Length;
            }

            return charSum + file.Count - 1;
        }

        /// <summary>
        /// Find the difference in 2 text documents, comparing by textlines.
        /// The algorithm itself is comparing 2 arrays of numbers so when comparing 2 text documents
        /// each line is converted into a (hash) number. This hash-value is computed by storing all
        /// textlines into a common hashtable so i can find dublicates in there, and generating a 
        /// new number each time a new textline is inserted.
        /// </summary>
        /// <param name="TextA">A-version of the text (usualy the old one)</param>
        /// <param name="TextB">B-version of the text (usualy the new one)</param>
        /// <param name="trimSpace">When set to true, all leading and trailing whitespace characters are stripped out before the comparation is done.</param>
        /// <param name="ignoreSpace">When set to true, all whitespace characters are converted to a single space character before the comparation is done.</param>
        /// <param name="ignoreCase">When set to true, all characters are converted to their lowercase equivivalence before the comparation is done.</param>
        /// <returns>Returns a array of Differences that describe the differences.</returns>
        private List<Difference> DiffText(List<string> TextA, List<string> TextB, bool ignoreCase)
        {
            // prepare the input-text and convert to comparable numbers.
            Hashtable h = new Hashtable(GetFileLength(TextA) + GetFileLength(TextB));

            // The A-Version of the data (original data) to be compared.
            FileData DataA = new FileData(DiffCodes(TextA, h, ignoreCase));

            // The B-Version of the data (modified data) to be compared.
            FileData DataB = new FileData(DiffCodes(TextB, h, ignoreCase));

            h = null; // free up hashtable memory (maybe)

            int MAX = DataA.LineAmount + DataB.LineAmount + 1;

            /// vector for the (0,0) to (x,y) search
            int[] DownVector = new int[2 * MAX + 2];

            /// vector for the (u,v) to (N,M) search
            int[] UpVector = new int[2 * MAX + 2];

            LCS(DataA, 0, DataA.LineAmount, DataB, 0, DataB.LineAmount, DownVector, UpVector);

            Optimize(DataA);
            Optimize(DataB);

            return CreateDiffs(DataA, DataB);
        }

        /// <summary>
        /// If a sequence of modified lines starts with a line that contains the same content
        /// as the line that appends the changes, the difference sequence is modified so that the
        /// appended line and not the starting line is marked as modified.
        /// This leads to more readable diff sequences when comparing text files.
        /// </summary>
        /// <param name="Data">A Diff data buffer containing the identified changes.</param>
        private void Optimize(FileData Data)
        {
            int StartPos, EndPos;

            StartPos = 0;

            while (StartPos < Data.LineAmount)
            {
                while ((StartPos < Data.LineAmount) && (Data.ModifiedLinesState[StartPos] == false))
                {
                    StartPos++;
                }
                    
                EndPos = StartPos;

                while ((EndPos < Data.LineAmount) && (Data.ModifiedLinesState[EndPos] == true))
                {
                    EndPos++;
                }

                if ((EndPos < Data.LineAmount) && (Data.LinesNumbers[StartPos] == Data.LinesNumbers[EndPos]))
                {
                    Data.ModifiedLinesState[StartPos] = false;
                    Data.ModifiedLinesState[EndPos] = true;
                }
                else
                {
                    StartPos = EndPos;
                }
            }
        }


        /// <summary>
        /// Find the difference in 2 arrays of integers.
        /// </summary>
        /// <param name="ArrayA">A-version of the numbers (usualy the old one)</param>
        /// <param name="ArrayB">B-version of the numbers (usualy the new one)</param>
        /// <returns>Returns a array of Items that describe the differences.</returns>
        private List<Difference> DiffInt(List<int> ArrayA, List<int> ArrayB)
        {
            // The A-Version of the data (original data) to be compared.
            FileData DataA = new FileData(ArrayA);

            // The B-Version of the data (modified data) to be compared.
            FileData DataB = new FileData(ArrayB);

            int MAX = DataA.LineAmount + DataB.LineAmount + 1;

            /// vector for the (0,0) to (x,y) search
            int[] DownVector = new int[2 * MAX + 2];

            /// vector for the (u,v) to (N,M) search
            int[] UpVector = new int[2 * MAX + 2];

            LCS(DataA, 0, DataA.LineAmount, DataB, 0, DataB.LineAmount, DownVector, UpVector);

            return CreateDiffs(DataA, DataB);
        }


        /// <summary>
        /// This function converts all textlines of the text into unique numbers for every unique textline
        /// so further work can work only with simple numbers.
        /// </summary>
        /// <param name="aText">the input text</param>
        /// <param name="h">This extern initialized hashtable is used for storing all ever used textlines.</param>
        /// <param name="trimSpace">ignore leading and trailing space characters</param>
        /// <returns>a array of integers.</returns>
        private List<int> DiffCodes(List<string> aText, Hashtable h, bool ignoreCase)
        {
            // get all codes of the text
            int lastUsedCode = h.Count;
            object aCode;
            string s;

            List<int> Codes = new List<int>();

            for (int i = 0; i < aText.Count; ++i)
            {
                s = aText[i];

                if (ignoreCase)
                {
                    s = s.ToLower();
                }
                    
                aCode = h[s];

                if (aCode == null)
                {
                    lastUsedCode++;
                    h[s] = lastUsedCode;
                    Codes.Add(lastUsedCode);
                }
                else
                {
                    Codes.Add((int)aCode);
                }
            }
            return Codes;
        }

        /// <summary>
        /// Method generates SMS coords
        /// </summary>
        /// <param name="firstFileData"></param>
        /// <param name="LowerA"></param>
        /// <param name="UpperA"></param>
        /// <param name="secondFileData"></param>
        /// <param name="LowerB"></param>
        /// <param name="UpperB"></param>
        /// <param name="DownVector"></param>
        /// <param name="UpVector"></param>
        /// <returns></returns>
        private static SMSRD GenerateSMSCoords(FileData firstFileData, int LowerA, int UpperA, FileData secondFileData, int LowerB, int UpperB, int[] DownVector, int[] UpVector)
        {
            int linesSummary = firstFileData.LineAmount + secondFileData.LineAmount + 1;

            int DownK = LowerA - LowerB; // the k-line to start the forward search
            int UpK = UpperA - UpperB; // the k-line to start the reverse search

            int Delta = (UpperA - LowerA) - (UpperB - LowerB);
            bool oddDelta = (Delta & 1) != 0;

            // The vectors in the publication accepts negative indexes. the vectors implemented here are 0-based
            // and are access using a specific offset: UpOffset UpVector and DownOffset for DownVektor
            int DownOffset = linesSummary - DownK;
            int UpOffset = linesSummary - UpK;

            int MaxD = ((UpperA - LowerA + UpperB - LowerB) / 2) + 1;

            // init vectors
            DownVector[DownOffset + DownK + 1] = LowerA;
            UpVector[UpOffset + UpK - 1] = UpperA;

            for (int D = 0; D <= MaxD; D++)
            {

                // Extend the forward path.
                for (int k = DownK - D; k <= DownK + D; k += 2)
                {
                    // find the only or better starting point
                    int x, y;

                    if (k == DownK - D)
                    {
                        // down
                        x = DownVector[DownOffset + k + 1];
                    }
                    else
                    {
                        // a step to the right
                        x = DownVector[DownOffset + k - 1] + 1;

                        if ((k < DownK + D) && (DownVector[DownOffset + k + 1] >= x))
                        {
                            // down
                            x = DownVector[DownOffset + k + 1];
                        }                       
                    }
                    y = x - k;

                    // find the end of the furthest reaching forward D-path in diagonal k.
                    while ((x < UpperA) && (y < UpperB) && (firstFileData.LinesNumbers[x] == secondFileData.LinesNumbers[y]))
                    {
                        x++; y++;
                    }
                    DownVector[DownOffset + k] = x;

                    // overlap
                    if (oddDelta && (UpK - D < k) && (k < UpK + D))
                    {
                        if (UpVector[UpOffset + k] <= DownVector[DownOffset + k])
                        {
                            return new SMSRD(DownVector[DownOffset + k], DownVector[DownOffset + k] - k);
                        }
                    }

                }

                // Extend the reverse path.
                for (int k = UpK - D; k <= UpK + D; k += 2)
                {
                    // find the only or better starting point
                    int x, y;

                    if (k == UpK + D)
                    {
                        // up
                        x = UpVector[UpOffset + k - 1];
                    }
                    else
                    {
                        // left
                        x = UpVector[UpOffset + k + 1] - 1;

                        if ((k > UpK - D) && (UpVector[UpOffset + k - 1] < x))
                        {
                            // up
                            x = UpVector[UpOffset + k - 1];
                        }
                            
                    }
                    y = x - k;

                    while ((x > LowerA) && (y > LowerB) && (firstFileData.LinesNumbers[x - 1] == secondFileData.LinesNumbers[y - 1]))
                    {
                        // diagonal
                        x--; y--;
                    }
                    UpVector[UpOffset + k] = x;

                    // overlap
                    if (!oddDelta && (DownK - D <= k) && (k <= DownK + D))
                    {
                        if (UpVector[UpOffset + k] <= DownVector[DownOffset + k])
                        {
                            return new SMSRD(DownVector[DownOffset + k], DownVector[DownOffset + k] - k);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// This is the divide-and-conquer implementation of the longes common-subsequence (LCS) algorithm.
        /// </summary>
        /// <param name="DataA">sequence A</param>
        /// <param name="LowerA">lower bound of the actual range in DataA</param>
        /// <param name="UpperA">upper bound of the actual range in DataA (exclusive)</param>
        /// <param name="DataB">sequence B</param>
        /// <param name="LowerB">lower bound of the actual range in DataB</param>
        /// <param name="UpperB">upper bound of the actual range in DataB (exclusive)</param>
        /// <param name="DownVector">a vector for the (0,0) to (x,y) search. Passed as a parameter for speed reasons.</param>
        /// <param name="UpVector">a vector for the (u,v) to (N,M) search. Passed as a parameter for speed reasons.</param>
        private void LCS(FileData DataA, int LowerA, int UpperA, FileData DataB, int LowerB, int UpperB, int[] DownVector, int[] UpVector)
        {
            // Fast walkthrough equal lines at the start
            while (LowerA < UpperA && LowerB < UpperB && DataA.LinesNumbers[LowerA] == DataB.LinesNumbers[LowerB])
            {
                LowerA++; LowerB++;
            }

            // Fast walkthrough equal lines at the end
            while (LowerA < UpperA && LowerB < UpperB && DataA.LinesNumbers[UpperA - 1] == DataB.LinesNumbers[UpperB - 1])
            {
                --UpperA; --UpperB;
            }

            if (LowerA == UpperA)
            {
                // mark as inserted lines.
                while (LowerB < UpperB)
                {
                    DataB.ModifiedLinesState.Insert(LowerB++, true);
                }                  
            }
            else if (LowerB == UpperB)
            {
                // mark as deleted lines.
                while (LowerA < UpperA)
                {
                    DataA.ModifiedLinesState.Insert(LowerA++, true);
                }                   
            }
            else
            {
                // Find the middle snakea and length of an optimal path for A and B
                SMSRD smsrd = GenerateSMSCoords(DataA, LowerA, UpperA, DataB, LowerB, UpperB, DownVector, UpVector);

                // The path is from LowerX to (x,y) and (x,y) to UpperX
                LCS(DataA, LowerA, smsrd.GetX, DataB, LowerB, smsrd.GetY, DownVector, UpVector);
                LCS(DataA, smsrd.GetX, UpperA, DataB, smsrd.GetY, UpperB, DownVector, UpVector); 
            }
        }


        /// <summary>Scan the tables of which lines are inserted and deleted,
        /// producing an edit script in forward order.  
        /// </summary>
        /// dynamic array
        private List<Difference> CreateDiffs(FileData DataA, FileData DataB)
        {
            List<Difference> a = new List<Difference>();
            Difference aItem;

            int StartA, StartB;
            int LineA, LineB;

            LineA = 0;
            LineB = 0;

            while (LineA < DataA.LineAmount || LineB < DataB.LineAmount)
            {
                if ((LineA < DataA.LineAmount) && (!DataA.ModifiedLinesState[LineA]) && (LineB < DataB.LineAmount) && (!DataB.ModifiedLinesState[LineB]))
                {
                    // equal lines
                    LineA++;
                    LineB++;
                }
                else
                {
                    // maybe deleted and/or inserted lines
                    StartA = LineA;
                    StartB = LineB;

                    while (LineA < DataA.LineAmount && (LineB >= DataB.LineAmount || DataA.ModifiedLinesState[LineA]))
                    {
                        LineA++;
                    }

                    while (LineB < DataB.LineAmount && (LineA >= DataA.LineAmount || DataB.ModifiedLinesState[LineB]))
                    {
                        LineB++;
                    }
                        
                    if ((StartA < LineA) || (StartB < LineB))
                    {
                        // store a new difference-item
                        aItem = new Difference(StartA, StartB, LineA - StartA, LineB - StartB);
                        a.Add(aItem);
                    }
                }
            }

            return a;
        }

        #endregion // Methods
    }
}

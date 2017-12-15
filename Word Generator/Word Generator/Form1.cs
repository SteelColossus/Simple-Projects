using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Word_Generator
{
    public partial class Form1 : Form
    {
        private string[] wordList;
        private PatternTree patternTree;
        private readonly bool forceRewrite = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            patternTree = new PatternTree();
            encodingComboBox.SelectedIndex = 0;
        }

        private static string[] SplitByLength(string toSplit, int length)
        {
            string[] splitStrings = new string[(toSplit.Length - length) + 1];

            for (int i = 0; i < ((toSplit.Length - length) + 1); i++)
            {
                splitStrings[i] = toSplit.Substring(i, length).ToLower();
            }

            return splitStrings;
        }

        private void FindWordPatterns()
        {
            progressBar.Maximum = wordList.Length;
            progressBar.Value = 0;

            foreach (string word in wordList)
            {
                string[] patterns = SplitByLength(word, 2);

                for (int i = 0; i < patterns.Length; i++)
                {
                    patternTree.AddPattern(patterns[i], i == 0);

                    if (i > 0)
                    {
                        patternTree.AddPatternLink(patterns[i - 1], patterns[i]);
                    }
                }

                progressBar.Value++;
            }
        }

        private void randomWordButton_Click(object sender, EventArgs e)
        {
            if (wordList == null)
            {
                MessageBox.Show(@"A dictionary must be opened first.", @"Dictionary required", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            Random rand = new Random();

            int randomIndex = rand.Next(1, wordList.Length);
            int runningTotal = 0;

            int length = 0;

            foreach (string word in wordList)
            {
                runningTotal++;

                if (runningTotal == randomIndex)
                {
                    length = word.Length;
                    break;
                }
            }

            if (length < 2) length = 2;

            string[] randomPatterns = new string[length - 1];

            for (int i = 0; i < (length - 1); i++)
            {
                string patternToAdd;

                patternToAdd = i == 0 ? patternTree.GetRandomStartingPattern() : patternTree.GetRandomFollowingPattern(randomPatterns[i - 1]);

                randomPatterns[i] = patternToAdd;
            }

            string randomWord = "";

            for (int i = 0; i < randomPatterns.Length; i++)
            {
                if (i == 0)
                {
                    randomWord = randomPatterns[i];
                }
                else
                {
                    randomWord += randomPatterns[i].Substring(1);
                }
            }

            MessageBox.Show(@"Your random word is:" + Environment.NewLine + randomWord, @"Random word", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void openDictionaryButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string dictionaryPath = openFileDialog.FileName;

                if (dictionaryPath == null) return;

                string dictionaryFile = dictionaryPath.Substring(dictionaryPath.LastIndexOf(@"\", StringComparison.Ordinal));

                string patternsFile = dictionaryFile.Contains("dictionary") ?
                    dictionaryFile.Replace("dictionary", "patterns") :
                    "patterns_" + dictionaryFile;

                string patternsPath = dictionaryPath.Substring(0, dictionaryPath.LastIndexOf(@"\", StringComparison.Ordinal)) +
                                      patternsFile;

                Encoding dictionaryEncoding;

                switch (encodingComboBox.SelectedItem)
                {
                    case "ANSI":
                        dictionaryEncoding = Encoding.Default;
                        break;
                    case "UTF-8":
                        dictionaryEncoding = Encoding.UTF8;
                        break;
                    default:
                        dictionaryEncoding = Encoding.ASCII;
                        break;
                }

                try
                {
                    wordList = File.ReadAllLines(dictionaryPath, dictionaryEncoding);
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex);
                    return;
                }

                if (File.Exists(patternsPath) && !forceRewrite)
                {
                    try
                    {
                        List<string> lines = File.ReadAllLines(patternsPath).ToList();

                        patternTree.TotalPatternOccurrences =
                            int.Parse(lines[0].Substring(lines[0].IndexOf(":", StringComparison.Ordinal) + 1));

                        int firstLinkLine = lines.FindIndex(v => v.Contains("->")) + 1;

                        foreach (string line in lines.Skip(1).Take(firstLinkLine - 2))
                        {
                            string[] tokens = line.Split(':');

                            patternTree.AddPatterns(tokens[0], int.Parse(tokens[1]));
                        }

                        foreach (string line in lines.Skip(firstLinkLine - 1))
                        {
                            string[] tokens = line.Split(new[] {":", "->"}, StringSplitOptions.RemoveEmptyEntries);

                            patternTree.AddPatternLinks(tokens[0], tokens[1], int.Parse(tokens[2]));
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex);
                        return;
                    }
                }
                else
                {
                    FindWordPatterns();
                    patternTree.OrderAll();

                    try
                    {
                        if (!File.Exists(patternsPath) || forceRewrite)
                        {
                            StringBuilder stringBuilder = new StringBuilder();

                            stringBuilder.AppendLine("total:" + patternTree.TotalPatternOccurrences);

                            foreach (KeyValuePair<string, int> kvp in patternTree.PatternList)
                            {
                                stringBuilder.AppendLine(kvp.Key + ":" + kvp.Value);
                            }

                            foreach (PatternLink patternLink in patternTree.PatternLinks)
                            {
                                stringBuilder.AppendLine(patternLink.FromPattern +
                                                         "->" +
                                                         patternLink.ToPattern +
                                                         ":" +
                                                         patternLink.Occurrences);
                            }

                            File.WriteAllText(patternsPath, stringBuilder.ToString());
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                MessageBox.Show(@"Dictionary was loaded successfully!", @"Loaded successfully", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }
    }
}
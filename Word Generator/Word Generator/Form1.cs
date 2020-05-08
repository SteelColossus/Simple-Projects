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
        private const bool forceRewrite = false;

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

            toSplit = toSplit.ToLower();

            for (int i = 0; i < ((toSplit.Length - length) + 1); i++)
            {
                splitStrings[i] = toSplit.Substring(i, length);
            }

            return splitStrings;
        }

        private void FindWordPatterns()
        {
            progressBar.Maximum = wordList.Length;
            progressBar.Value = 0;

            foreach (string word in wordList)
            {
                const int splitLength = 3;

                if (word.Length < splitLength)
                {
                    continue;
                }

                string[] patterns = SplitByLength(word, splitLength);

                for (int i = 0; i < patterns.Length; i++)
                {
                    PatternTree.WordPlacement wordPlacement = PatternTree.WordPlacement.NONE;

                    if (i == 0)
                    {
                        wordPlacement = PatternTree.WordPlacement.START;
                    }
                    else if (i == patterns.Length - 1)
                    {
                        wordPlacement = PatternTree.WordPlacement.END;
                    }

                    patternTree.AddPattern(patterns[i], wordPlacement);

                    if (i > 0)
                    {
                        patternTree.AddPatternLink(patterns[i - 1], patterns[i]);
                    }
                }

                progressBar.Value++;

                Application.DoEvents();
            }
        }

        private string GenerateRandomWord()
        {
            Random rand = new Random();

            int randomIndex = rand.Next(0, wordList.Length - 1);

            int length = wordList[randomIndex].Length;

            if (length < 2) length = 2;

            string[] randomPatterns = new string[length - 1];

            for (int i = 0; i < (length - 1); i++)
            {
                string patternToAdd;

                if (i == 0)
                {
                    patternToAdd = patternTree.GetRandomStartingPattern();
                }
                else if (i == (length - 2))
                {
                    patternToAdd = patternTree.GetRandomEndingPattern(randomPatterns[i - 1]);
                }
                else
                {
                    patternToAdd = patternTree.GetRandomFollowingPattern(randomPatterns[i - 1]);
                }

                if (patternToAdd != null)
                {
                    randomPatterns[i] = patternToAdd;
                }
                else
                {
                    break;
                }
            }

            string randomWord = "";

            for (int i = 0; i < randomPatterns.Length; i++)
            {
                string randomPattern = randomPatterns[i];

                if (randomPattern == null)
                {
                    break;
                }

                if (i == 0)
                {
                    randomWord = randomPattern;
                }
                else
                {
                    randomWord += randomPattern.Substring(randomPattern.Length - 1);
                }
            }

            return randomWord;
        }

        private void randomWordButton_Click(object sender, EventArgs e)
        {
            if (wordList == null)
            {
                MessageBox.Show(@"A dictionary must be opened first.", @"Dictionary required", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            string[] randomWords = new[]
            {
                GenerateRandomWord(),
                GenerateRandomWord(),
                GenerateRandomWord(),
                GenerateRandomWord(),
                GenerateRandomWord()
            };

            MessageBox.Show(@"Your random words are:" + Environment.NewLine + Environment.NewLine + string.Join(Environment.NewLine, randomWords), 
                            @"Random word", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void openDictionaryButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                encodingComboBox.Enabled = false;
                openDictionaryButton.Enabled = false;
                randomWordButton.Enabled = false;

                string dictionaryPath = openFileDialog.FileName;

                if (dictionaryPath == null) return;

                string dictionaryFile = Path.GetFileName(dictionaryPath);

                string patternsFile = "patterns_" + dictionaryFile;

                string patternsPath = Path.Combine(Path.GetDirectoryName(dictionaryPath) ?? throw new InvalidOperationException(), patternsFile);

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
                    wordList = wordList.Where(v => !string.IsNullOrWhiteSpace(v)).ToArray();
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

                            patternTree.AddPatterns(tokens[0], int.Parse(tokens[1]), int.Parse(tokens[2]));

                            Application.DoEvents();
                        }

                        foreach (string line in lines.Skip(firstLinkLine - 1))
                        {
                            string[] tokens = line.Split(new[] {":", "->"}, StringSplitOptions.RemoveEmptyEntries);

                            patternTree.AddPatternLinks(tokens[0], tokens[1], int.Parse(tokens[2]));

                            Application.DoEvents();
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
                        StringBuilder stringBuilder = new StringBuilder();

                        stringBuilder.AppendLine("total:" + patternTree.TotalPatternOccurrences);

                        foreach (KeyValuePair<string, PatternTree.PatternCount> kvp in patternTree.PatternList)
                        {
                            stringBuilder.AppendLine(kvp.Key + ":" + kvp.Value.Start + ":" + kvp.Value.End);
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
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                MessageBox.Show(@"Dictionary was loaded successfully!", @"Loaded successfully", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                encodingComboBox.Enabled = true;
                openDictionaryButton.Enabled = true;
                randomWordButton.Enabled = true;
            }
        }
    }
}
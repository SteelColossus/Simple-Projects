using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Hangman_Solver
{
    public partial class Form1 : Form
    {
        private string actualWord;
        private string guessWord;
        private int wrongGuesses;
        private bool gameRunning;

        private string[] wordList;
        private List<char> remainingLetters;

        private List<char> charOrderList;
        private bool obscureMode = false;

        public Form1()
        {
            InitializeComponent();
        }

        private string[] GetStringsWithPattern(string pattern)
        {
            List<string> stringsFollowingPattern = new List<string>();

            foreach (string strInList in wordList)
            {
                if (strInList.Length != pattern.Length)
                    continue;

                int pos = 0;
                bool validWord = true;

                foreach (char letterInPattern in pattern)
                {
                    pos++;
                    char letterAtPos = strInList.ElementAt(pos - 1);

                    if ((letterInPattern != '_' && letterInPattern != letterAtPos) || (pattern.Contains(letterAtPos) && letterInPattern == '_') || (!remainingLetters.Contains(letterAtPos) && !pattern.Contains(letterAtPos)))
                    {
                        validWord = false;
                        break;
                    }
                }

                if (validWord)
                {
                    stringsFollowingPattern.Add(strInList);
                }
            }

            numPossibleWordsBox.Text = stringsFollowingPattern.Count.ToString();

            return stringsFollowingPattern.ToArray<string>();
        }

        private Dictionary<char, int> GetSortedLetterCount(string[] strList)
        {
            Dictionary<char, int> letterCount = remainingLetters.ToDictionary(v => v, v => 0);

            foreach (string strInList in strList)
            {
                int pos = 0;

                foreach (char letter in strInList)
                {
                    if (letterCount.ContainsKey(letter) && !strInList.Substring(0, pos).Contains(letter))
                    {
                        letterCount[letter]++;
                    }

                    pos++;
                }
            }

            return letterCount.OrderByDescending(v => v.Value).ToDictionary(p => p.Key, p => p.Value);
        }

        private char FindMostLikelyLetter(string pattern)
        {
            return FindMostLikelyLetter(GetStringsWithPattern(pattern));
        }

        private char FindMostLikelyLetter(string[] strList)
        {
            Dictionary<char, int> letterCount = GetSortedLetterCount(strList);

            Dictionary<char, int> highestChars = letterCount.Where(v => v.Value == letterCount.First().Value).ToDictionary(p => p.Key, p => p.Value);

            Func<char, bool> pred = v => highestChars.Keys.Contains(v);

            char bestGuess = (obscureMode) ? charOrderList.Last(pred) : charOrderList.First(pred);

            return bestGuess;
        }

        private char GuessLetter()
        {
            StringBuilder stringBuild = new StringBuilder(guessWord);

            char bestLetter = FindMostLikelyLetter(guessWord);

            int pos = 0;
            bool correctGuess = false;

            foreach (char letter in actualWord)
            {
                if (letter == bestLetter)
                {
                    stringBuild.Replace('_', bestLetter, pos, 1);
                    guessWord = stringBuild.ToString();
                    correctGuess = true;
                }

                pos++;
            }

            if (!correctGuess)
            {
                wrongGuesses++;
                wrongLettersBox.Items.Add(bestLetter);
            }

            remainingLetters.Remove(bestLetter);

            return bestLetter;
        }

        private bool CheckWordValidity(string word)
        {
            if (String.IsNullOrWhiteSpace(word))
            {
                MessageBox.Show(@"Please enter a valid word into the textbox.", @"Invalid word", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            word = word.ToLower();

            if (!Regex.IsMatch(word, @"^[a-z]+$") || (word.Length < 2 || word.Length > 20))
            {
                MessageBox.Show(@"The word must be composed of only letters and must be between 2 and 20 characters long.", @"Invalid word", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            bool wordFound = wordList.Any(wordInList => wordInList == word);

            if (wordFound)
            {
                MessageBox.Show(@"Word was found. Let's begin!", @"Game commence", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show(@"Word was not found. :(" + Environment.NewLine + @"Please think of a different word to use.", "Invalid word", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void RunTurn()
        {
            if (possibleWordsBox.Items.Count > 0)
            {
                MessageBox.Show(@"Computer guesses " + GuessLetter() + @"!", @"Computer guess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wrongGuessCounterBox.Text = wrongGuesses.ToString();
            }

            possibleWordsBox.Items.Clear();
            possibleWordsBox.Items.AddRange(GetStringsWithPattern(guessWord).Cast<object>().ToArray());

            currentGuessBox.Text = guessWord;
        }

        private bool StartGame()
        {
            if (!CheckWordValidity(wordBox.Text))
                return false;

            wordBox.Text = wordBox.Text.ToLower();

            wrongGuessCounterBox.Text = @"0";
            possibleWordsBox.Items.Clear();
            currentGuessBox.Clear();
            wrongLettersBox.Items.Clear();
            numPossibleWordsBox.Text = wordList.Length.ToString();

            actualWord = wordBox.Text;
            guessWord = new string('_', actualWord.Length);
            wrongGuesses = 0;

            if (remainingLetters == null || remainingLetters.Count < 26)
            {
                remainingLetters = new List<char>(26) { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            }

            gameRunning = true;
            return true;
        }

        private void FinishGame()
        {
            MessageBox.Show(@"Computer solved it with " + wrongGuesses + @" wrong guesses.", @"Word solved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            gameRunning = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wordList = System.IO.File.ReadAllLines("dictionary.txt");

            remainingLetters = new List<char>(26) { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            Dictionary<char, int> letterCount = GetSortedLetterCount(wordList);

            charOrderList = letterCount.Keys.ToList();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (!gameRunning)
            {
                if (!StartGame())
                    return;
            }

            if (Int32.Parse(numPossibleWordsBox.Text) == 1)
            {
                while (guessWord.IndexOfAny(new[] { '_' }) != -1)
                {
                    RunTurn();
                }
            }
            else
            {
                RunTurn();
            }

            if (guessWord.IndexOfAny(new[] { '_' }) == -1)
            {
                FinishGame();
            }
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            if (!gameRunning)
            {
                if (!StartGame())
                    return;
            }

            while (guessWord.IndexOfAny(new[] { '_' }) != -1)
            {
                RunTurn();
            }

            FinishGame();
        }
    }
}

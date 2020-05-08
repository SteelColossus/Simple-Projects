using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Word_Generator
{
    public class PatternTree
    {
        public class PatternCount
        {
            public int Start { get; set; }
            public int End { get; set; }

            public PatternCount(int start, int end)
            {
                Start = start;
                End = end;
            }
        }

        public enum WordPlacement
        {
            NONE,
            START,
            END
        }

        // List of patterns and the number of occurrences of that pattern at the start of a word
        private Dictionary<string, PatternCount> patternList;

        private List<PatternLink> patternLinkList;

        private readonly Random random;

        public int TotalPatternOccurrences { get; set; }

        public Dictionary<string, PatternCount> PatternList
        {
            get { return patternList; }
        }

        public List<PatternLink> PatternLinks
        {
            get { return patternLinkList; }
        }

        public PatternTree()
        {
            patternList = new Dictionary<string, PatternCount>();
            patternLinkList = new List<PatternLink>();
            random = new Random();
        }

        public void AddPattern(string pattern, WordPlacement wordPlacement)
        {
            if (!patternList.ContainsKey(pattern))
            {
                patternList.Add(pattern, new PatternCount(0, 0));
            }

            if (wordPlacement == WordPlacement.START)
            {
                patternList[pattern].Start++;
            }
            else if (wordPlacement == WordPlacement.END)
            {
                patternList[pattern].End++;
            }

            TotalPatternOccurrences++;
        }

        public void AddPatterns(string pattern, int numStartOfWord, int numEndOfWord)
        {
            patternList[pattern] = new PatternCount(numStartOfWord, numEndOfWord);
        }

        public void AddPatternLink(string fromPattern, string toPattern)
        {
            PatternLink patternLink =
                patternLinkList.FirstOrDefault(v => (v.FromPattern == fromPattern) && (v.ToPattern == toPattern));

            if (patternLink == null)
            {
                patternLinkList.Add(new PatternLink(fromPattern, toPattern));
            }
            else
            {
                patternLink.Occurrences++;
            }
        }

        public void AddPatternLinks(string fromPattern, string toPattern, int occurrences)
        {
            PatternLink patternLink =
                patternLinkList.FirstOrDefault(v => (v.FromPattern == fromPattern) && (v.ToPattern == toPattern));

            if (patternLink == null)
            {
                patternLinkList.Add(new PatternLink(fromPattern, toPattern, occurrences));
            }
            else
            {
                patternLink.Occurrences = occurrences;
            }
        }

        public void OrderAll()
        {
            patternList = patternList.OrderBy(v => v.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            patternLinkList = patternLinkList.OrderBy(v => v.FromPattern).ThenBy(v => v.ToPattern).ToList();
        }

        public string GetRandomStartingPattern()
        {
            int numStartingPatterns = patternList.Sum(v => v.Value.Start);

            int randomNum = random.Next(1, numStartingPatterns);
            int runningTotal = 0;

            foreach (KeyValuePair<string, PatternCount> kvp in patternList)
            {
                runningTotal += kvp.Value.Start;

                if (runningTotal >= randomNum)
                {
                    return kvp.Key;
                }
            }

            throw new Exception("End of list reached with no pattern found.");
        }

        public string GetRandomFollowingPattern(string fromPattern)
        {
            List<PatternLink> followingList = patternLinkList.Where(v => v.FromPattern == fromPattern).ToList();
            int numFollowingPatterns = followingList.Sum(v => v.Occurrences);

            if (numFollowingPatterns == 0)
            {
                return null;
            }

            int randomNum = random.Next(1, numFollowingPatterns);
            int runningTotal = 0;

            foreach (PatternLink patternLink in followingList)
            {
                runningTotal += patternLink.Occurrences;

                if (runningTotal >= randomNum)
                {
                    return patternLink.ToPattern;
                }
            }

            throw new Exception("End of list reached with no pattern found.");
        }

        public string GetRandomEndingPattern(string fromPattern)
        {
            Dictionary<PatternLink, double> followingDict = patternLinkList.Where(v => v.FromPattern == fromPattern).ToDictionary(v => v, v => 0.0);

            int totalFollowingOccurrences = followingDict.Sum(v => v.Key.Occurrences);

            if (totalFollowingOccurrences == 0)
            {
                return null;
            }

            Dictionary<string, int> endingDict = new Dictionary<string, int>();

            List<PatternLink> keys = followingDict.Keys.ToList();

            foreach (PatternLink patternLink in keys)
            {
                endingDict.Add(patternLink.ToPattern, patternList[patternLink.ToPattern].End);
            }

            int totalEndingOccurrences = endingDict.Sum(v => v.Value);

            if (totalEndingOccurrences == 0)
            {
                return null;
            }

            foreach (PatternLink patternLink in keys)
            {
                followingDict[patternLink] = patternLink.Occurrences * ((double)endingDict[patternLink.ToPattern] / totalEndingOccurrences);
            }

            double totalProbability = followingDict.Sum(v => v.Value);

            double randomNum = random.NextDouble() * totalProbability;
            double runningTotal = 0;

            foreach (PatternLink patternLink in keys)
            {
                runningTotal += followingDict[patternLink];

                if (runningTotal >= randomNum)
                {
                    return patternLink.ToPattern;
                }
            }

            throw new Exception("End of list reached with no pattern found.");
        }
    }
}
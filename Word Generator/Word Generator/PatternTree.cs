using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

public class PatternTree
{
    // List of patterns and the number of occurrences of that pattern at the start of a word
    private Dictionary<string, int> patternList;
    private List<PatternLink> patternLinkList;
    private int totalPatternOccurrences;

    private Random random;
    private int numStartingPatterns;

    public int TotalPatternOccurrences
    {
        get => totalPatternOccurrences;
        set => totalPatternOccurrences = value;
    }

    public Dictionary<string, int> PatternList => patternList;
    public List<PatternLink> PatternLinks => patternLinkList;

    public int TotalPatternOccurrences1 { get => totalPatternOccurrences; set => totalPatternOccurrences = value; }

    public PatternTree()
    {
        patternList = new Dictionary<string, int>();
        patternLinkList = new List<PatternLink>();
        random = new Random();
        numStartingPatterns = 0;
    }

    public void AddPattern(string pattern, bool startOfWord)
    {
        if (!patternList.ContainsKey(pattern))
        {
            patternList.Add(pattern, 0);
        }

        if (startOfWord)
        {
            patternList[pattern]++;
        }

        totalPatternOccurrences++;
    }

    public void AddPatterns(string pattern, int numStartOfWord)
    {
        if (!patternList.ContainsKey(pattern))
        {
            patternList.Add(pattern, numStartOfWord);
        }
        else
        {
            patternList[pattern] = numStartOfWord;
        }
    }

    public void AddPatternLink(string fromPattern, string toPattern)
    {
        PatternLink patternLink = patternLinkList.FirstOrDefault(v => v.FromPattern == fromPattern && v.ToPattern == toPattern);

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
        PatternLink patternLink = patternLinkList.FirstOrDefault(v => v.FromPattern == fromPattern && v.ToPattern == toPattern);

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
        if (numStartingPatterns == 0)
        {
            numStartingPatterns = patternList.Sum(v => v.Value);
        }

        int randomNum = random.Next(1, numStartingPatterns);
        int runningTotal = 0;

        foreach (KeyValuePair<string, int> kvp in patternList)
        {
            runningTotal += kvp.Value;

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
}
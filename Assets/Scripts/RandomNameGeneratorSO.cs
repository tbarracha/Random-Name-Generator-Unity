using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Random Name Generator / Random Name Generator")]
public class RandomNameGeneratorSO : ScriptableObject
{
    public static readonly List<char> alphabet = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'y', 'x', 'z' };
    public static readonly List<char> vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };
    public static readonly List<char> alphabetWithoutVowels = new List<char> { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' };

    [Header("Parameters")]
    public int minLetterCount = 2;
    public int maxLetterCount = 5;

    [Header("Names")]
    public int amountOfNamesToGenerate = 5;
    public bool clickMeToGenerateNames;
    public bool modifyNames = true;
    [Space]
    public List<string> names;



    public void GenerateNames(int amount)
    {
        names = new List<string>();
        string n = "";

        for (int i = 0; i < amount; i++)
        {
            n = GenerateNameBasedOnLetterCount();

            if (modifyNames)
                n = NameModifier(n);

            names.Add(n);
        }
    }


    /// <summary>
    /// Generate A SINGLE name based on amount of letters, in a 'vowel' - 'non vowel' pattern (and vice-versa)
    /// </summary>
    public string GenerateNameBasedOnLetterCount()
    {
        // 1) Random name parts/separations amount
        int letterCount = Random.Range(minLetterCount, maxLetterCount + 1);
        List<char> letters = new List<char>();

        // 2) Letter Generation
        for (int i = 0; i <= letterCount; i++)
        {
            // 2.1) Add first letter
            if (i == 0)
            {
                letters.Add(alphabet.GetRandom());
                continue;
            }

            // 2.2) Check previous letter and apply 'vowel' - 'non-vowel' pattern
            char previous = letters[letters.Count - 1];

            // is a vowel, so we add a non-vowel
            if (vowels.Contains(previous))
                letters.Add(alphabetWithoutVowels.GetRandom());

            // is a 'non-vowel', so we add a vowel
            else
                letters.Add(vowels.GetRandom());
        }

        string genName = new string(letters.ToArray());
        return genName;
    }


    public string NameModifier(string genName)
    {
        string modName = "";

        for (int i = 0; i < genName.Length; i++)
        {
            char letter = genName[i];

            if (letter == 'a')
                modName += new List<string> { "a", "ae", "ea", "an", "ar" }.GetRandom().ToString();
            
            //else if (letter == 'b')
            //    modName += new List<string> { "b", "bh" }.GetRandom().ToString();
            
            else if (letter == 'c')
                modName += new List<string> { "c", "ch", "k" }.GetRandom().ToString();
            
            else if (letter == 'd')
                modName += new List<string> { "d", "d", "dr" }.GetRandom().ToString();
            
            else if (letter == 'e')
                modName += new List<string> { "e", "ea", "ae", "en" }.GetRandom().ToString();
            
            else if (letter == 'f')
                modName += new List<string> { "f", "ph", "fw", "fj" }.GetRandom().ToString();
            
            else if (letter == 'g')
                modName += new List<string> { "g", "gl", "gr", "gw" }.GetRandom().ToString();
            
            //else if (letter == 'h')
            //    modName += new List<string> { "h", "hw" }.GetRandom().ToString();
            
            else if (letter == 'i')
                modName += new List<string> { "i", "y", "in" }.GetRandom().ToString();
            
            //else if (letter == 'j')
            //    modName += new List<string> { "j", "jh", "jw" }.GetRandom().ToString();
            
            else if (letter == 'k')
                modName += new List<string> { "k", "kh", "kr", "kw" }.GetRandom().ToString();
            
            //else if (letter == 'l')
            //    modName += new List<string> { "l", "ll", "lh", "lw" }.GetRandom().ToString();
            
            else if (letter == 'm')
                modName += new List<string> { "m", "m", "mn" }.GetRandom().ToString();
            
            //else if (letter == 'n')
            //    modName += new List<string> { "n", "nn", "nm", "ng", "nh", "nw" }.GetRandom().ToString();

            else if (letter == 'o')
                modName += new List<string> { "o", "ou", "on" }.GetRandom().ToString();

            else if (letter == 'p')
                modName += new List<string> { "p", "ph", "pr" }.GetRandom().ToString();
            
            //else if (letter == 'q')
            //    modName += new List<string> { "q" }.GetRandom().ToString();

            else if (letter == 'r')
                modName += new List<string> { "r", "rr", "rg", "rt" }.GetRandom().ToString();

            else if (letter == 's')
                modName += new List<string> { "s", "ss", "sh", "sw" }.GetRandom().ToString();

            else if (letter == 't')
                modName += new List<string> { "t", "th", "tr", "tw" }.GetRandom().ToString();

            //else if (letter == 'u')
            //    modName += new List<string> { "u", "uu", "uh", "uw" }.GetRandom().ToString();

            //else if (letter == 'v')
            //    modName += new List<string> { "v", "vh", "vw" }.GetRandom().ToString();

            //else if (letter == 'w')
            //    modName += new List<string> { "w", "wh" }.GetRandom().ToString();

            else if (letter == 'x')
                modName += new List<string> { "x", "xh" }.GetRandom().ToString();

            //else if (letter == 'y')
            //    modName += new List<string> { "y", "yh" }.GetRandom().ToString();

            //else if (letter == 'z')
            //    modName += new List<string> { "z", "zh" }.GetRandom().ToString();

            else
                modName += letter.ToString();
        }

        return modName;
    }

    private void OnValidate()
    {
        if (clickMeToGenerateNames)
        {
            GenerateNames(amountOfNamesToGenerate);
            clickMeToGenerateNames = false;
        }
    }
}
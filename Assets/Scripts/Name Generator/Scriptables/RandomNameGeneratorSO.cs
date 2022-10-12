using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Random Name Generator / Random Name Generator")]
public class RandomNameGeneratorSO : ScriptableObject
{
    [SerializeField] AlphabetSO alphabet;

    [Header("Parameters")]
    public int minLetterCount = 2;
    public int maxLetterCount = 5;

    [Space]
    public int minSubdivions = 2;
    public int maxSubdivions = 2;

    [Space]
    public int oneInXToAddAnH = 10;
    public int oneInXToModifyLetter = 8;

    [Header("Names")]
    public int amountOfNamesToGenerate = 5;
    public bool clickMeToGenerateNames;
    [Space]
    public List<string> names;



    public void GenerateNames(int amount)
    {
        names = new List<string>();
        string n = "";

        for (int i = 0; i < amount; i++)
        {
            n = GenerateNameBasedOnLetterCount();
            n = NameModifier(n);

            names.Add(n);
        }
    }

    
    /// <summary>
    /// Generate A SINGLE name based on amount of letters, in a "vowel-non vowel" pattern
    /// </summary>
    public string GenerateNameBasedOnLetterCount()
    {
        // 1) Random name parts/separations amount
        int letterCount = Random.Range(minLetterCount, maxLetterCount + 1);
        List<char> letters = new List<char>();

        // 2) Generate chars / letters
        for (int i = 0; i <= letterCount; i++)
        {
            // Generate name first letter
            if (i == 0)
            {
                letters.Add(alphabet.RandomAlphabetLetter);
                continue;
            }

            char lastLetter = letters[letters.Count - 1];

            // Previous was a vowel, so we need to add a random alphabet letter that isn't a vowel
            if (alphabet.vowel.Contains(lastLetter))
                letters.Add(alphabet.RandomAlphabetLetterWithoutVowels);

            // Previous was not a vowel, so we add a vowel
            else
                letters.Add(alphabet.RandomVowel);
        }

        return new string(letters.ToArray());
    }


    public string NameModifier(string genName)
    {
        string modName = "";
        int rand = 0;

        for (int i = 0; i < genName.Length; i++)
        {
            char letter = genName[i];


            // 1 in "10" chances to ADD an "H" at the end of the char
            rand = Random.Range(0, oneInXToAddAnH);
            if (rand == 0)
                modName += letter.ToString() + "h";


            // modifiy individual letters
            rand = Random.Range(0, oneInXToModifyLetter);

            if (rand == 0)
            {
                // Vowels =============================== Vowels
                if (letter == 'a')
                {
                    if (Utilities.RandomTrueOrFalse())
                        modName += "ae";
                    else
                        modName += "ay";
                }

                if (letter == 'e')
                    modName += "ea";

                if (letter == 'i')
                    modName += "y";

                if (letter == 'o')
                {
                    if (Utilities.RandomTrueOrFalse())
                        modName += "ou";
                    else
                        modName += "ao";
                }

                if (letter == 'u')
                    modName += "uo";


                // Alphabet =============================== Alphabet
                if (letter == 'n')
                {
                    if (Utilities.RandomTrueOrFalse())
                        modName += "ng";
                    else
                        modName += "nh";
                }

                if (letter == 't')
                {
                    if (Utilities.RandomTrueOrFalse())
                        modName += "th";
                    else
                        modName += "tk";
                }

                if (letter == 'p')
                        modName += "ph";

                if (letter == 'f')
                        modName += "ph";
            }

            else
                modName += letter.ToString();
        }

        // Last letter modification
        rand = Random.Range(0, 2);
        if (rand == 0)
        {
            char lastChar = modName[modName.Length - 1];

            if (lastChar == 'k' || lastChar == 'q' || lastChar == 'w' || lastChar == 'y' || lastChar == 'x' || lastChar == 'z')
                modName += alphabet.RandomVowel.ToString() + alphabet.RandomFinisherLetter.ToString();
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
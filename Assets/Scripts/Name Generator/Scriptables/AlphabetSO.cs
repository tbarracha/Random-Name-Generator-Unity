using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Random Name Generator / Alphabet")]
public class AlphabetSO : ScriptableObject
{
    [Header("Basic")]
    public List<char> alphabet = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'y', 'x', 'z' };
    public List<char> vowel = new List<char> { 'a', 'e', 'i', 'y', 'o', 'u' };
    public List<char> alphabetWithoutVowels = new List<char> { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' };
    public List<char> finisherLetters = new List<char>() { 'f', 'h', 'j', 'k', 'm', 'n', 'q', 'r', 't', 'x', 'y', 'z' };
    //public List<char> afterVowelLetters = new List<char>() { 'h', 'l', 'j', 'k', 'm', 'n', 'q', 'r', 't', 'x', 'y', 'z' };
    List<char> fillerAlphabet = new List<char>();

    [Header("Real Words")]
    [SerializeField] TextAsset wordsTxt;
    public bool clickToGenerateWordsFromTextFile;
    public List<string> words;


    [Header("Rules")]
    public List<char> asFirstLetterVogalsAreNext = new List<char> { 'l', 'm', 'n', 'q', 'w', 'x', 'y', 'z', };
    public List<char> asCurrentLetterVogalsAreNext = new List<char> { 'm', 'n', 'q', 'w', 'x', 'z', };
    public List<char> canRepeatThemselves = new List<char> { 'a', 'e', 'i', 'o', 'u', 'd', 'k', 'l', 'm', 'n', 's', 'z' };

    public char RandomAlphabetLetter => alphabet.GetRandom();
    public char RandomAlphabetLetterWithoutVowels => alphabetWithoutVowels.GetRandom();
    public char RandomVowel => vowel.GetRandom();
    public char RandomFiller => fillerAlphabet.GetRandom();
    public char RandomFinisherLetter => finisherLetters.GetRandom();
    public string RandomWord => words.GetRandom();



    //[Button("Generate Filler Alphabet")]
    public void GenerateFillerAlphabet()
    {
        fillerAlphabet = new List<char>();

        for (int i = 0; i < alphabet.Count; i++)
        {
            char c = alphabet[i];

            // don't add if any of these conditions are true
            //if (vogals.Contains(c))
            //    continue;

            if (asCurrentLetterVogalsAreNext.Contains(c))
                continue;

            // no conditions are true, so we add
            fillerAlphabet.Add(c);
        }
    }



    public string GetFirstLetterAndNextIfVogalIsRequired()
    {
        string firstLetters = GetPossibleChangesInASingleLetter(RandomAlphabetLetter);

        // next letter has to be a vogal, if last char of "firstLetters" is inside the "asFirstLetterVogalsAreNext" array
        if (firstLetters.Length == 1 && asFirstLetterVogalsAreNext.Contains(firstLetters[firstLetters.Length - 1]))
            return firstLetters + RandomVowel;

        else
            return firstLetters;
    }


    public string GetPossibleChangesInASingleLetter(char letter)
    {
        int rand = Random.Range(0, 11);

        // check if can repeat
        if (rand == 0)
        {
            bool canRepeat = canRepeatThemselves.Contains(letter);

            if (canRepeat)
                return letter.ToString() + letter.ToString();

            else
                return letter.ToString();
        }

        // randomize if we want to add an 'H' after
        else
        {
            if (letter == 'h')
                return letter.ToString();

            rand = Random.Range(0, 9);

            // add an 'H'
            if (rand == 0)
            {
                if (letter == 'a')
                    return "ae";

                else if (letter == 'e')
                    return "ea";

                if (letter == 'f')
                    return "ph";
                
                else if (letter == 't')
                    return "th";
                
                else if (letter == 'x')
                    return "qh";
                
                else
                    return letter.ToString() + "h";
            }

            else
                return letter.ToString();
        }
    }

    public bool CheckIfNextHasToBeVogal(string stringName)
    {
        char lastChar = stringName[stringName.Length - 1];
        return asCurrentLetterVogalsAreNext.Contains(lastChar);
    }

    public void GenerateWordsFromTextFile()
    {
        words = new List<string>();

        string[] array = wordsTxt.text.Split('\n');
        words.AddArrayToList(array);
    }

    private void OnValidate()
    {
        if (clickToGenerateWordsFromTextFile)
        {
            GenerateWordsFromTextFile();
            clickToGenerateWordsFromTextFile = false;
        }
    }
}
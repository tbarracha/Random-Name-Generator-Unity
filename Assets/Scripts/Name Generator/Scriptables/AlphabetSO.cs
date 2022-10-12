using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Random Name Generator / Alphabet")]
public class AlphabetSO : ScriptableObject
{
    [Header("Basic")]
    public char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'y', 'x', 'z' };
    public char[] vogals = { 'a', 'e', 'i', 'y', 'o', 'u' };
    public string[] specials = { "ae", "ea", "ah", "kh", "ph", "qh", "sh", "ez" };
    [Space]
    public bool clickMeToGenerateVogals;
    public List<string> vogalStart;
    public List<string> vogalEnd;

    [Header("Rules")]
    public char[] asFirstLetterVogalsAreNext = { 'l', 'm', 'n', 'q', 'w', 'x', 'y', 'z', };
    public char[] asCurrentLetterVogalsAreNext = { 'l', 'm', 'n', 'q', 'w', 'x', 'z', };
    public char[] canRepeatThemselves = { 'a', 'e', 'i', 'o', 'u', 'd', 'k', 'l', 'm', 'n', 's', 'z' };

    public char RandomAlphabetLetter => alphabet.GetRandom();
    public char RandomVogal => vogals.GetRandom();
    public string RandomSpecial => specials.GetRandom();
    public string RandomVogalStart => vogalStart.GetRandom();
    public string RandomVogalEnd => vogalEnd.GetRandom();

    public string GetRandomVogalString()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
            return RandomVogalStart;
        else
            return RandomVogalEnd;
    }


    public string GetFirstLetterAndNextIfVogalIsRequired()
    {
        string firstLetters = GetPossibleChangesInASingleLetter(RandomAlphabetLetter);

        // next letter has to be a vogal, if last char of "firstLetters" is inside the "asFirstLetterVogalsAreNext" array
        if (firstLetters.Length == 1 && UtilsArray.ArrayToList(asFirstLetterVogalsAreNext).Contains(firstLetters[firstLetters.Length - 1]))
            return firstLetters + RandomVogal;

        else
            return firstLetters;
    }


    public string GetPossibleChangesInASingleLetter(char letter)
    {
        int rand = Random.Range(0, 11);

        // check if can repeat
        if (rand == 0)
        {
            bool canRepeat = UtilsArray.ArrayToList(canRepeatThemselves).Contains(letter);

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
        return UtilsArray.ArrayToList(asCurrentLetterVogalsAreNext).Contains(lastChar);
    }

    void GenerateVogals()
    {
        string str = "";
        vogalStart = new List<string>();
        vogalEnd = new List<string>();

        for (int v = 0; v < vogals.Length; v++)
        {
            for (int a = 0; a < alphabet.Length; a++)
            {
                str = vogals[v].ToString() + alphabet[a].ToString();
                vogalStart.Add(str);

                str = alphabet[a].ToString() + vogals[v].ToString();
                vogalEnd.Add(str);
            }
        }

        vogalStart.RemoveDuplicates();
        vogalEnd.RemoveDuplicates();
    }

    private void OnValidate()
    {
        if (clickMeToGenerateVogals)
        {
            GenerateVogals();
            clickMeToGenerateVogals = false;
        }
    }
}
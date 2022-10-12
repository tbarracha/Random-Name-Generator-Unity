using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Random Name Generator / Random Name Generator")]
public class RandomNameGeneratorSO : ScriptableObject
{
    [SerializeField] AlphabetSO alphabet;
    [Space]
    public int amountOfNamesToGenerate = 5;
    public bool clickMeToGenerateNames;
    [Space]
    public List<string> names;

    [Header("Name Components")]
    public List<string> vogals;

    public void GenerateNames(int amount)
    {
        names = new List<string>();

        for (int i = 0; i < amount; i++)
        {
            string n = GenerateSingleName();
            names.Add(n);
        }
    }

    // Start with single alphabet letter
    // Check possible changes
    // Check proceding letters
    public string GenerateSingleName()
    {
        // Random alphabet letter with Possible Start Changes
        string genName = alphabet.GetFirstLetterAndNextIfVogalIsRequired();

        // Check possible letter changes & procedings
        if (alphabet.CheckIfNextHasToBeVogal(genName))
            genName += alphabet.GetPossibleChangesInASingleLetter(alphabet.RandomVogal);
        else
        {
            int rand = Random.Range(0, 3);

            if (rand == 0)
                genName += alphabet.GetPossibleChangesInASingleLetter(alphabet.RandomAlphabetLetter);

            if (rand == 1)
                genName += alphabet.RandomSpecial;

            if (rand == 2)
                genName += alphabet.GetRandomVogalString();
        }

        return genName;
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
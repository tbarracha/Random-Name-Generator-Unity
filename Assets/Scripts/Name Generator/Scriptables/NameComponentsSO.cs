using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Random Name Generator / Name Components")]
public class NameComponentsSO : ScriptableObject
{
    [SerializeField] AlphabetSO alphabet;

    public List<string> specialsFirst;
    public List<string> specialsLast;
    public List<string> specialNameComponents;

    [Button("Create Vogals")]
    public void GenerateVogalsAndSpecials()
    {
        string str = "";
        specialsFirst = new List<string>();
        specialsLast = new List<string>();
        specialNameComponents = new List<string>();

        //for (int v = 0; v < alphabet.vogalsAndSpecials.Length; v++)
        //{
        //    for (int a = 0; a < alphabet.alphabet.Length; a++)
        //    {
        //        str = alphabet.vogalsAndSpecials[v] + alphabet.alphabet[a];
        //        specialsFirst.Add(str);
        //
        //        str = alphabet.alphabet[a] + alphabet.vogalsAndSpecials[v];
        //        specialsLast.Add(str);
        //    }
        //}
        //
        //for (int a = 0; a < alphabet.cleanAlphabet.Length; a++)
        //{
        //    for (int v = 0; v < alphabet.vogalsAndSpecials.Length; v++)
        //    {
        //        for (int aa = 0; aa < alphabet.cleanAlphabet.Length; aa++)
        //        {
        //            str = alphabet.cleanAlphabet[a] + alphabet.vogalsAndSpecials[v] + alphabet.cleanAlphabet[aa];
        //            specialNameComponents.Add(str);
        //        }
        //    }
        //}

        specialsLast = specialsLast.RemoveDuplicates();
        //specialNameComponents = specialNameComponents.RemoveDuplicates();
    }
}
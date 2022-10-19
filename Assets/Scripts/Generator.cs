using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StardropTools;
using StardropTools.Tween;

public class Generator : Singleton<Generator>
{
    [SerializeField] RandomNameGeneratorSO generatorSO;

    [Header("Generation")]
    [SerializeField] Button generateButton;
    [SerializeField] Button variationButton;
    [Space]
    [SerializeField] TextMeshProUGUI generatedText;
    [SerializeField] TextMeshProUGUI variationText;
    [SerializeField] Color[] activeInnactiveColors;

    [Header("Logs & Lists")]
    [SerializeField] List<string> history;
    [SerializeField] List<string> saved;


    protected override void Awake()
    {
        base.Awake();

        generateButton.onClick.AddListener(GenerateName);
        variationButton.onClick.AddListener(GenerateVariation);

        GenerateName();

        history = new List<string>();
        saved = new List<string>();
    }

    public void GenerateName()
    {
        string genName = generatorSO.GenerateNameBasedOnLetterCount();
        generatedText.text = genName.FirstLetterUppercase();
        variationText.text = "Variation";

        TweenTextColors(1, 0);

        history.Add(genName);
    }

    public void GenerateVariation()
    {
        string variant = generatorSO.NameModifier(generatedText.text);

        while (variant == generatedText.text || variant == variationText.text)
            variant = generatorSO.NameModifier(generatedText.text);

        variationText.text = variant.FirstLetterUppercase();

        TweenTextColors(1, 1);

        history.Add(variant);
    }

    public void SetGeneratedText(string text)
    {
        generatedText.text = text;
        variationText.text = "Variation";

        TweenTextColors(1, 0);
    }

    /// <summary>
    /// TextID: 0-generated, 1-variant | ColorID: 0-innactive, 1-active
    /// </summary>
    /// <param name="textID"> TextID: 0-generated, 1-variant </param>
    /// <param name="colorID"> ColorID: 0-innactive, 1-active </param>
    void TweenTextColors(int textID, int colorID)
    {
        TextMeshProUGUI textMesh = null;
        Color color = activeInnactiveColors[colorID];

        if (textID == 0)
            textMesh = generatedText;
        else
            textMesh = variationText;

        Tween colorTween = new TweenTextMeshColor(variationText, color)
            .SetEaseType(EaseType.EaseOutSine)
            .SetDuration(.2f)
            .SetID(textMesh.GetHashCode())
            .Initialize();
    }
}

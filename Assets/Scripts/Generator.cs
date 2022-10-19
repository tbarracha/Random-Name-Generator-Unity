
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StardropTools.Tween;
using StardropTools.Pool;

public class Generator : Singleton<Generator>
{
    [SerializeField] RandomNameGeneratorSO generatorSO;
    [SerializeField] Pool<ListItem> poolListItems;

    [Header("Generation")]
    [SerializeField] Button generateButton;
    [SerializeField] Button variationButton;
    [Space]
    [SerializeField] TMP_InputField generatedInputField;
    [SerializeField] TMP_InputField variationInputField;
    [SerializeField] TextMeshProUGUI generatedText;
    [SerializeField] TextMeshProUGUI variationText;
    [SerializeField] Color[] activeInnactiveColors;

    [Header("Logs & Lists")]
    [SerializeField] Transform parentHistory;
    [SerializeField] Transform parentSaved;
    [SerializeField] List<string> history;
    [SerializeField] List<string> saved;
    [Space]
    [SerializeField] List<ListItem> savedItems;
    [SerializeField] List<ListItem> historyItems;


    protected override void Awake()
    {
        base.Awake();

        poolListItems.Populate();

        generateButton.onClick.AddListener(GenerateName);
        variationButton.onClick.AddListener(GenerateVariation);

        generatedInputField.onSubmit.AddListener(AddToHistory);
        variationInputField.onSubmit.AddListener(AddToHistory);

        GenerateName();

        history = new List<string>();

        LoadSaved();
    }

    public void GenerateName()
    {
        string genName = generatorSO.GenerateNameBasedOnLetterCount().FirstLetterUppercase();

        generatedInputField.text = genName;
        variationInputField.text = "Variation";

        TweenTextColors(1, 0);

        AddToHistory(genName);
    }

    public void GenerateVariation()
    {
        string variant = generatorSO.NameModifier(generatedText.text).FirstLetterUppercase();

        while (variant == generatedText.text || variant == variationText.text)
            variant = generatorSO.NameModifier(generatedText.text);

        variationInputField.text = variant;

        TweenTextColors(1, 1);

        AddToHistory(variant);
    }

    public void SetGeneratedText(string text)
    {
        generatedInputField.text = text;
        variationInputField.text = "Variation";

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


    public void AddToSaved(string text)
    {
        saved.AddSafe(text);

        ListItem item = poolListItems.Spawn(Vector3.zero, Quaternion.identity, parentSaved);
        item.ToggleSaved(true);
        item.SetText(text);
        item.Initialize();

        savedItems.Add(item);
    }

    public void AddToSaved(ListItem item)
    {
        saved.AddSafe(item.text);
        savedItems.Add(item);
        item.transform.SetParent(parentSaved);
    }

    public void RemoveFromSaved(ListItem item)
    {
        saved.RemoveSafe(item.text);
        item.Despawn();
    }

    public void AddToHistory(string text)
    {
        history.Add(text);

        ListItem item = poolListItems.Spawn(Vector3.zero, Quaternion.identity, parentHistory);
        item.ToggleSaved(false);
        item.SetText(text);
        item.Initialize();
        item.SetAsFirstSibling();

        savedItems.Add(item);
    }

    public void RemoveFromHistory(ListItem item)
    {
        history.RemoveSafe(item.text);
        item.Despawn();
    }

    void LoadSaved()
    {
        saved = SaveManager.BinaryLoad<List<string>>("saved.dat");

        if (saved.Exists() == false)
            saved = new List<string>();

        if (saved.Count > 0)
            for (int i = 0; i < saved.Count; i++)
            {
                ListItem item = poolListItems.Spawn(Vector3.zero, Quaternion.identity, parentSaved);
                item.ToggleSaved(true);
                item.SetText(saved[i]);
                item.Initialize();

                savedItems.Add(item);
            }
                
    }

    private void OnApplicationQuit()
    {
        SaveManager.BinarySave(saved, "saved.dat");
    }
}

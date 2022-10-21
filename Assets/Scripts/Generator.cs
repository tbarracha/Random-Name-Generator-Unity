
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StardropTools.Tween;
using StardropTools.Pool;
using SFB;

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
    [Space]
    [SerializeField] CopyToClipboardNotification copyToClipboardNotification;

    [Header("Logs & Lists")]
    [SerializeField] Transform parentSaved;
    [SerializeField] Transform parentHistory;
    [Space]
    [SerializeField] Button clearSavedButton;
    [SerializeField] Button clearHistoryButton;
    [SerializeField] Button downloadSavedButton;
    [SerializeField] Button downloadHistoryButton;
    [Space]
    [SerializeField] bool clearSaved;
    [Space]
    [SerializeField] List<string> savedNames;
    [SerializeField] List<string> nameHistory;
    [Space]
    [SerializeField] List<ListItem> savedItems;
    [SerializeField] List<ListItem> historyItems;


    protected override void Awake()
    {
        base.Awake();

        nameHistory = new List<string>();
        poolListItems.Populate();

        generateButton.onClick.AddListener(GenerateName);
        variationButton.onClick.AddListener(GenerateVariation);

        generatedInputField.onSubmit.AddListener(AddToHistory);
        generatedInputField.onDeselect.AddListener(AddToHistory);
        variationInputField.onSubmit.AddListener(AddToHistory);
        variationInputField.onDeselect.AddListener(AddToHistory);

        clearSavedButton.onClick.AddListener(ClearSaved);
        clearHistoryButton.onClick.AddListener(ClearHistory);

        downloadSavedButton.onClick.AddListener(DownloadSavedNames);
        downloadHistoryButton.onClick.AddListener(DownloadHistoryNames);

        downloadSavedButton.onClick.AddListener(() => copyToClipboardNotification.Animate());
        downloadHistoryButton.onClick.AddListener(() => copyToClipboardNotification.Animate());
    }

    private void Start()
    {
        GenerateName();
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
        savedNames.AddSafe(text);

        ListItem item = poolListItems.Spawn(Vector3.zero, Quaternion.identity, parentSaved);
        item.Initialize();
        item.SetText(text);
        item.ToggleSaved(true);

        savedItems.AddSafe(item);
    }

    public void AddToSaved(ListItem item)
    {
        savedNames.AddSafe(item.text);
        item.SetParent(parentSaved);

        historyItems.Remove(item);
        savedItems.AddSafe(item);
    }

    public void RemoveFromSaved(ListItem item)
    {
        savedNames.RemoveSafe(item.text);
        item.SetParent(parentHistory);
        item.SetAsFirstSibling();

        savedItems.Remove(item);
        historyItems.Add(item);
    }

    public void ClearSaved()
    {
        savedNames = new List<string>();
        for (int i = 0; i < savedItems.Count; i++)
            savedItems[i].Despawn();

        SaveList();
    }

    public void AddToHistory(string text)
    {
        nameHistory.Add(text);

        ListItem item = poolListItems.Spawn(Vector3.zero, Quaternion.identity, parentHistory);
        item.Initialize();
        item.ToggleSaved(false);
        item.SetText(text);
        item.SetAsFirstSibling();

        historyItems.Add(item);
    }

    public void RemoveFromHistory(ListItem item)
    {
        nameHistory.RemoveSafe(item.text);
        item.Despawn();
    }

    public void ClearHistory()
    {
        nameHistory = new List<string>();

        for (int i = 0; i < historyItems.Count; i++)
            historyItems[i].Despawn();
    }

    void LoadSaved()
    {
        savedNames = SaveManager.BinaryLoad<List<string>>("saved.dat");
        
        if (savedNames.Exists() == false)
            savedNames = new List<string>();
        
        if (savedNames.Count > 0)
            for (int i = 0; i < savedNames.Count; i++)
            {
                ListItem item = poolListItems.Spawn(Vector3.zero, Quaternion.identity, parentSaved);
                item.Initialize();
                item.SetText(savedNames[i]);
                item.ToggleSaved(true);
        
                savedItems.Add(item);
            }
    }

    public void SaveList() => SaveManager.BinarySave(savedNames, "saved.dat");

    /// <summary>
    /// 1) get path to save
    /// 2) save file on path
    /// </summary>
    public void DownloadSavedNames()
    {
        //string path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "Saved Names", "txt");
        //
        //if (path.Length == 0)
        //    return;

        string contents = "";
        for (int i = 0; i < savedNames.Count; i++)
            contents += savedNames[i] + "\n";

        UniClipboard.SetText(contents);
        
        //Utilities.CreateOrAddTextToFile(path, contents);
    }


    /// <summary>
    /// 1) get path to save
    /// 2) save file on path
    /// </summary>
    public void DownloadHistoryNames()
    {
        //string path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "Name Generation History", "txt");
        //
        //if (path.Length == 0)
        //    return;
        //
        string contents = "";
        for (int i = 0; i < nameHistory.Count; i++)
            contents += nameHistory[i] + "\n";

        UniClipboard.SetText(contents);

        //Utilities.CreateOrAddTextToFile(path, contents);
    }

    private void OnApplicationQuit()
    {
        SaveList();
    }

    private void OnValidate()
    {
        if (clearSaved)
        {
            ClearSaved();
            clearSaved = false;
        }
    }
}

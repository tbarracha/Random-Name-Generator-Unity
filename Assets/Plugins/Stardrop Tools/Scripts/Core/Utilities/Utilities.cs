
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using StardropTools;

/// <summary>
/// Class that contains miscellanious static utilities
/// </summary>
public static class Utilities
{
    static Camera camera;

    public static bool RandomTrueOrFalse() => ConvertIntToBool(Random.Range(0, 2));

    /// <summary>
    /// Invokes the InitializeManager() method on an array of IManager
    /// </summary>
    public static void InitializeManagers(IManager[] managers)
    {
        for (int i = 0; i < managers.Length; i++)
            managers[i].InitializeManager();
    }

    /// <summary>
    /// Invokes the LateInitializeManager() method on an array of IManager
    /// </summary>
    public static void LateInitializeManagers(IManager[] managers)
    {
        for (int i = 0; i < managers.Length; i++)
            managers[i].LateInitializeManager();
    }

    /// <summary>
    /// Invokes the Initialize() method on an array of BaseComponents
    /// </summary>
    public static void InitializeBaseComponents(BaseComponent[] baseComponents)
    {
        for (int i = 0; i < baseComponents.Length; i++)
            baseComponents[i].Initialize();
    }

    /// <summary>
    /// Invokes the LateInitialize() method on an array of BaseComponents
    /// </summary>
    public static void LateInitializeBaseComponents(BaseComponent[] baseComponents)
    {
        for (int i = 0; i < baseComponents.Length; i++)
            baseComponents[i].LateInitialize();
    }

    /// <summary>
    /// Returns a list of components found under parent transform
    /// </summary>
    public static List<T> GetItems<T>(Transform parent)
    {
        if (parent != null && parent.childCount > 0)
        {
            // list to store all detected components
            List<T> componentList = new List<T>();

            // loop to find components
            for (int i = 0; i < parent.childCount; i++)
            {
                var component = parent.GetChild(i).GetComponent<T>();
                if (component != null && componentList.Contains(component) == false)
                    componentList.Add(component);
            }

            // return array of found components
            return componentList;
        }

        else
        {
            Debug.Log("Parent has no children");
            return null;
        }
    }

    public static Transform CreateEmpty(string name, Vector3 position, Transform parent)
    {
        Transform point = new GameObject(name).transform;
        point.position = position;
        point.parent = parent;
        return point;
    }

    /// <summary>
    /// 0 - False, 1 - True
    /// </summary>
    public static bool ConvertIntToBool(int id)
    {
        if (id == 0)
            return false;
        else
            return true;
    }

    /// <summary>
    /// 0 - False, 1 - True
    /// </summary>
    public static int ConvertBoolToInt(bool value)
    {
        if (value == false)
            return 0;
        else
            return 1;
    }

    public static void SetLineWidth(this LineRenderer line, float width)
    {
        line.startWidth = width;
        line.endWidth = width;
    }

    public static string FirstLetterUppercase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        str.ToLower();
        char[] characters = str.ToCharArray();
        characters[0] = char.ToUpper(characters[0]);

        return new string(characters);
    }

    public static Vector3 ViewportRaycast(LayerMask layerMask)
    {
        if (camera == null)
            camera = Camera.main;

        Ray ray = camera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 1000, layerMask);

        return hit.point;
    }

    public static List<Collider> HorizontalEightDirectionRaycast(Vector3 origin, float rayLength, LayerMask mask)
    {
        RaycastHit hit;
        Ray ray = new Ray();
        ray.origin = origin;

        List<Collider> colliders = new List<Collider>();

        // loop through directions
        // start at top and go clockwise
        for (int i = 0; i < 8; i++)
        {
            if (i == 0) // top
                ray.direction = Vector3.forward;

            else if (i == 1) // top Right
                ray.direction = Vector3.forward + Vector3.right;

            else if (i == 2) // right
                ray.direction = Vector3.right;

            else if (i == 3) // bottom Right
                ray.direction = Vector3.back + Vector3.right;

            else if (i == 4) // bottom
                ray.direction = Vector3.back;

            else if (i == 5) // bottom Left
                ray.direction = Vector3.back + Vector3.left;

            else if (i == 6) // left
                ray.direction = Vector3.left;

            else if (i == 7) // top Left
                ray.direction = Vector3.forward + Vector3.left;

            ray.direction *= rayLength;

            if (Physics.Raycast(ray, out hit, mask) && hit.collider != null)
                colliders.Add(hit.collider);
        }

        return colliders;
    }

    public static void StopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    /// <summary>
    /// Puts the string into the Clipboard.
    /// </summary>
    public static void CopyStringToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }

    /// <summary>
    /// Creates new file if there isn't one or adds contents to an existing one.
    /// File name ex: 'logs.txt' (extensions can be whatever ex: .bnb, .cro, etc,.)
    /// </summary>
    /// <param name="fileName"></param>
    public static void CreateOrAddTextToFile(string path, string fileName, string content, int newLineAmount = 0)
    {
        // path to file
        string filePath = path + fileName;

        // add new lines
        if (newLineAmount > 0)
            for (int i = 0; i < newLineAmount; i++)
                content += "\n";

        // create file it if doesnt exist
        if (File.Exists(filePath) == false)
            File.WriteAllText(filePath, content);

        // add content to file
        else
            File.AppendAllText(filePath, content);

    }

    public static void SetImageArrayColor(UnityEngine.UI.Image[] images, Color color)
    {
        for (int i = 0; i < images.Length; i++)
            images[i].color = color;
    }

    public static void SetImageArrayAlpha(UnityEngine.UI.Image[] images, float alpha)
    {
        Color color = Color.black;

        for (int i = 0; i < images.Length; i++)
        {
            color = images[i].color;
            color.a = alpha;

            images[i].color = color;
        }
    }

    public static void SetImagePixelsPerUnit(UnityEngine.UI.Image[] images, float pixelsPerUnit)
    {
        for (int i = 0; i < images.Length; i++)
            images[i].pixelsPerUnitMultiplier = pixelsPerUnit;
    }

    #region Get Random

    public static T GetRandom<T>(T[] array) => array.GetRandom(); // kinda redundant, isnt it?

    public static T GetRandom<T>(T option1, T option2)
    {
        List<T> list = new List<T>();
        list.Add(option1);
        list.Add(option2);

        return list.GetRandom();
    }

    public static T GetRandom<T>(T option1, T option2, T option3)
    {
        List<T> list = new List<T>();
        list.Add(option1);
        list.Add(option2);
        list.Add(option3);

        return list.GetRandom();
    }

    public static T GetRandom<T>(T option1, T option2, T option3, T option4)
    {
        List<T> list = new List<T>();
        list.Add(option1);
        list.Add(option2);
        list.Add(option3);
        list.Add(option4);

        return list.GetRandom();
    }

    public static T GetRandom<T>(T option1, T option2, T option3, T option4, T option5)
    {
        List<T> list = new List<T>();
        list.Add(option1);
        list.Add(option2);
        list.Add(option3);
        list.Add(option4);
        list.Add(option5);

        return list.GetRandom();
    }

    #endregion // Get Random

#if UNITY_EDITOR
    public static void ClearLog() //you can copy/paste this code to the bottom of your script
    {
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    #region Gizmos

    public static void DrawCube(Vector3 position, Vector3 scale, Quaternion rotation)
    {
        Matrix4x4 cubeTransform = Matrix4x4.TRS(position, rotation, scale);
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

        Gizmos.matrix *= cubeTransform;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = oldGizmosMatrix;
    }

    public static void DrawString(string text, Vector3 worldPos, Color? color = null)
    {
        Handles.BeginGUI();
        if (color.HasValue) GUI.color = color.Value;
        var view = UnityEditor.SceneView.currentDrawingSceneView;
        Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
        Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
        GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
        Handles.EndGUI();
    }
    #endregion // gizmos

    #region Instantiate Prefabs
#if UNITY_EDITOR
    public static GameObject CreatePrefab(GameObject prefab)
        => PrefabUtility.InstantiatePrefab(prefab) as GameObject;

    public static GameObject CreatePrefab(GameObject prefab, Transform parent)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        obj.transform.parent = parent;
        return obj;
    }

    public static T CreatePrefab<T>(GameObject prefab)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        return obj.GetComponent<T>();
    }

    public static T CreatePrefab<T>(Object prefab)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        return obj.GetComponent<T>();
    }

    /// <summary>
    /// Path to save ex: "Assets/Resources/SO" 
    /// </summary>
    /// <param name="className">Name of scriptable object class</param>
    /// <param name="path"> Path to save ex: "Assets/Resources/SO" </param>
    public static ScriptableObject CreateScriptableObject(string scriptableClassName, string path, string name)
    {
        ScriptableObject so = ScriptableObject.CreateInstance(scriptableClassName);
        so.name = name;

        AssetDatabase.CreateAsset(so, path);
        AssetDatabase.SaveAssets();
        Selection.activeObject = so;

        return so;
    }

    public static T CreatePrefab<T>(GameObject prefab, Transform parent)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        obj.transform.parent = parent;
        return obj.GetComponent<T>();
    }
#endif
    #endregion // instantiate prefabs
#endif
}
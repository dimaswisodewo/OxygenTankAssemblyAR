using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class JsonSerializer : MonoBehaviour
{
    public static JsonSerializer Instance;
    public Data data = new Data();
    public string fileName = "OxygenTankAR.json";

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(this);
        StartCoroutine(Initialize());
    }

    public string GetDescriptionData(ACTION_TYPE actionType, int index)
    {
        string outputString = string.Empty;
        switch (actionType)
        {
            case ACTION_TYPE.ASSEMBLY:
                outputString = data.assemble[index];
                break;

            case ACTION_TYPE.DIASSEMBLY:
                outputString = data.diassemble[index];
                break;
        }

        return outputString;
    }

    public int GetAssembleDataCount()
    {
        return data.assemble.Length;
    }

    public int GetDiassembleDataCount()
    {
        return data.diassemble.Length;
    }

    private IEnumerator Initialize()
    {
        yield return StartCoroutine(GetTextData(fileName));
        yield return StartCoroutine(LoadARScene());
    }

    private IEnumerator LoadARScene()
    {
        yield return new WaitForEndOfFrame();
        SceneLoader.LoadScene(Config.AR_SCENE);
    }

    private IEnumerator GetTextData(string file)
    {
        string path = Application.streamingAssetsPath;
        string fullPath = Path.Combine(path, file);
        Debug.Log("fullPath: " + fullPath);

        UnityWebRequest request = UnityWebRequest.Get(fullPath);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ProtocolError)
        {
            string downloadedText = request.downloadHandler.text;
            data = JsonUtility.FromJson<Data>(downloadedText);
        }
        else
        {
            Debug.LogError("Protocol Error");
        }
    }
}

[System.Serializable]
public class Data
{
    public string[] assemble;
    public string[] diassemble;
    public string[] safety;
}

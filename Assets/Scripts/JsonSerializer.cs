using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System.IO;

public class JsonSerializer : MonoBehaviour
{
    public static JsonSerializer Instance;
    public Data data = new Data();
    public string fileName = "OxygenTankAR.json";

    public UnityEvent onDataLoaded;

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
                outputString = data.assemble[index].step;
                break;

            case ACTION_TYPE.DIASSEMBLY:
                outputString = data.diassemble[index].step;
                break;
        }

        return outputString;
    }

    public string GetWarningData(ACTION_TYPE actionType, int index)
    {
        string outputString = string.Empty;
        switch (actionType)
        {
            case ACTION_TYPE.ASSEMBLY:
                outputString = data.assemble[index].warning;
                break;

            case ACTION_TYPE.DIASSEMBLY:
                outputString = data.diassemble[index].warning;
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
        yield return StartCoroutine(OnDataLoadedCoroutine());
    }

    private IEnumerator OnDataLoadedCoroutine()
    {
        yield return new WaitForEndOfFrame();
        onDataLoaded?.Invoke();
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
    public Step[] assemble;
    public Step[] diassemble;
}

[System.Serializable]
public class Step
{
    public string step;
    public string warning;
    public string note;
    public string caution;
}

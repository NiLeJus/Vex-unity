using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static SaveData _saveData = new SaveData();

    [System.Serializable]

    public struct SaveData
    {
        public GameManagerSaveData GameData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

    private static void HandleSaveData()
    {

        string saveContent = File.ReadAllText(SaveFileName());

        _saveData = JsonUtility.FromJson<SaveData>(saveContent);


    }

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());

        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();

    }

    private static void HandleLoadData()
    {
        GameManager.I.Load(_saveData.GameData);
    }
}

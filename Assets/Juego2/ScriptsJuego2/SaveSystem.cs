using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static string saveFileName = "savefile.json";


    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);


        File.WriteAllText(filePath, json);
        Debug.Log("Juego guardado en: " + filePath);
    }


    public static SaveData LoadGame()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);


        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Juego cargado desde: " + filePath);
            return data;
        }
        else
        {
            Debug.LogWarning("No se encontró un archivo de guardado.");
            return new SaveData();
        }
    }
}

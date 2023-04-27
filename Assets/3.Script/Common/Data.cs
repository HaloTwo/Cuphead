using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Data : MonoBehaviour
{
    public Vector3 pos;
    public bool stage1;
    public bool stage2;

    public void SaveData(Vector3 position, bool stage1, bool stage2)
    {
        Data data = new Data();
        data.pos = position;
        data.stage1 = stage1;
        data.stage2 = stage2;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public Data LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Data data = JsonUtility.FromJson<Data>(json);
            return data;
        }
        return null;
    }
}

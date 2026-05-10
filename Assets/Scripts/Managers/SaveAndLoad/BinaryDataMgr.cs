using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryDataMgr : MonoBehaviour
{
    private static BinaryDataMgr instance ;
    public static BinaryDataMgr Instance => instance;
    
    private static string SAVE_PATH;
    private void Awake()
    {
        SAVE_PATH = Application.persistentDataPath + "/Data/";
        
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void SaveData(object obj, string fileName)
    {
        if(!Directory.Exists(SAVE_PATH))
            Directory.CreateDirectory(SAVE_PATH);
        using (FileStream fs = new FileStream(SAVE_PATH + fileName, FileMode.OpenOrCreate, FileAccess.Write))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, obj);
            fs.Close();
        }
        Debug.Log("已存储");
        Debug.Log(Application.persistentDataPath);
    }

    public T LoadData<T>(string fileName) where T : class
    {
        if(!File.Exists(SAVE_PATH + fileName)){return default(T);}

        T obj;
        using (FileStream fs = File.Open(SAVE_PATH + fileName, FileMode.Open, FileAccess.Read))
        {
            BinaryFormatter bf=new BinaryFormatter();
            obj =bf.Deserialize(fs) as T ;
            fs.Close();
        }
        return obj;
    }
    // 清空所有存档
    public void ClearAllSaveData(string fileName)
    {
        string path = SAVE_PATH + fileName;
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("存档已清空！");
        }
        else
        {
            Debug.Log("暂无存档可删");
        }
    }
}

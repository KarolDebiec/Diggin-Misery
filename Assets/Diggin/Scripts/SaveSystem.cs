using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SavePlayer(PlayerController player,string Save_Dir,string WorldName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + Save_Dir + '/'+ WorldName + '/' + "save.jddd";
        FileStream stream = new FileStream(path,FileMode.Create);

        SaveData data = new SaveData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData LoadPlayer( string Save_Dir, string WorldName)
    {
        string path = Application.persistentDataPath + Save_Dir + '/' + WorldName + '/' + "save.jddd";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in : " + path);
            return null;
        }
    }
}

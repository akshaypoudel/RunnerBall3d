using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class BinarySaving
{
  /*  public static void saveplayer(perlText mytext)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path =Application.persistentDataPath+"/RunnerBall3D.game";
        FileStream stream = new FileStream(path,FileMode.Create);
        PlayerData data = new PlayerData(mytext);
        formatter.Serialize(stream, data);
        stream.Close();

    }
    public static PlayerData LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/RunnerBall3D.game";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data=formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in: "+path);
            return null;
        }
    }
  */
}

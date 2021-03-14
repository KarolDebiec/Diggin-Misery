using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public float Health;
    public float Stamina;
    public float Hunger;
    public float[] position;

    public SaveData(PlayerController player)
    {
        Health = player.health;
        Stamina = player.stamina;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

}
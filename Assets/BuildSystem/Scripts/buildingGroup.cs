using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingGroup : MonoBehaviour
{
    public List<GameObject> structuresInGroup = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    public void AddToList(GameObject value)
    {
        structuresInGroup.Add(value);
    }
    public void RemoveFromList(GameObject value)
    {
        structuresInGroup.Remove(value);
        if(structuresInGroup.Count==0)
        {
            Destroy(gameObject);
        }
    }
}

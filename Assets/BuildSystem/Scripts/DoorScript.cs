using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject toRotate;
    public Collider doorCollider;
    public bool isOpened = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isOpened)
        {
            OpenDoors();
        }
        if (Input.GetKeyDown(KeyCode.X) && isOpened)
        {
            CloseDoors();
        }
    }

    public void OpenDoors()
    {
        toRotate.transform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
        doorCollider.enabled = false;
        isOpened = true;
    }
    public void CloseDoors()
    {
        toRotate.transform.Rotate(0.0f, 90.0f, 0.0f, Space.World);
        doorCollider.enabled = true;
        isOpened = false;
    }

}

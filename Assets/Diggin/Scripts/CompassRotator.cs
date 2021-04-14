using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassRotator : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCamera; //Camera holds X rotation. I.e, up and down. When looking up, it is negative.

    public GameObject vertical_ring;

    public GameObject horizontal_ring;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical_ring.transform.rotation = Quaternion.Euler(0, 90, -playerCamera.transform.localRotation.eulerAngles.x);
        horizontal_ring.transform.rotation = Quaternion.Euler(-90, 0, (player.transform.localRotation.eulerAngles.y));
    }
}

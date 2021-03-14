using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassScript : MonoBehaviour
{
    public RawImage CompassImage;
    public Transform player;
    public Text CompassDiretionText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CompassImage.uvRect = new Rect(player.localEulerAngles.y/360,0,1,1);

        Vector3 forward = player.transform.forward;

        float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;
        int displayAngle = Mathf.RoundToInt(headingAngle);

        CompassDiretionText.text = displayAngle.ToString();
    }
}

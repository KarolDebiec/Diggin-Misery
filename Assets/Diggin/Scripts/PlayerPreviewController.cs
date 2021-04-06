using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreviewController : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;

    public GameObject headWear;
    public GameObject torsoWear;
    public GameObject overTorsoWear;
    public GameObject legsWear;
    public GameObject feetWear;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGear()
    {
        playerController = player.GetComponent<PlayerController>();
        headWear = playerController.headWear;
        torsoWear = playerController.torsoWear;
        overTorsoWear = playerController.overTorsoWear;
        legsWear = playerController.legsWear;
        feetWear = playerController.feetWear;
    }
}

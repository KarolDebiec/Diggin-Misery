using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UIController UIcontroller;
    public GameController gameController;

    public bool alive; // is player alive

    public bool canMove=true;

    public float health;
    public float stamina;
    public float hunger;
    public float thirst;
    public float hungerLossMultiplier;
    public float hungerHPLossMultiplier;
    public float thirstLossMultiplier;
    public float thirstHPLossMultiplier;
    public bool gettingHungry;
    public bool gettingThirsty;
    public GameObject respawnPoint = null;
    //holding users active clothes
    public GameObject headWear;
    public GameObject torsoWear;
    public GameObject overTorsoWear;
    public GameObject legsWear;
    public GameObject feetWear;
    public bool test;

    [SerializeField]
    Transform character;
    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;
    public float sensitivity = 1;
    public float smoothing = 2;
    public Camera cam;
    public GameObject pointer;

    void Start()
    {
        character = gameObject.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(alive && gettingHungry && hunger>0)
        {
            hunger -= hungerLossMultiplier*Time.deltaTime*(float)0.01;
        }
        else if(alive && gettingHungry)
        {
            DamagePlayer(hungerHPLossMultiplier * Time.deltaTime * (float)0.01);
        }
        if (alive && gettingThirsty && thirst > 0)
        {
            thirst -= thirstLossMultiplier * Time.deltaTime * (float)0.01;
        }
        else if (alive && gettingThirsty)
        {
            DamagePlayer(thirstHPLossMultiplier * Time.deltaTime * (float)0.01);
        }
        if (test)
        {
            DamagePlayer(10);
            test = false;
        }
        #region Mouse Look
        if(canMove)
        {
            // Get smooth mouse look.
            Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
            appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
            currentMouseLook += appliedMouseDelta;
            currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

            // Rotate camera and controller.
            cam.transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);


            Ray ray = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));//raycast stuff
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))//notice the layer
            {
                pointer.transform.position = hit.point;
            }
        }
        #endregion
    }
    public void DamagePlayer(float damageValue)
    {
        health -= damageValue;
        if (health <= 0)
        {
            health = 0;
            //UIcontroller.SetDisplayHealth(0);
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        alive = false;
        UIcontroller.displayDeathPanel();
        Debug.Log("Player Died");
    }
    public void RespawnPlayer()
    {
        if(respawnPoint!=null)
        {
            gameObject.transform.position = respawnPoint.transform.position;
        }
        else
        {
            gameObject.transform.position = new Vector3(0,150,0);
        }
    }
    public void AddThirst(float value)
    {
        thirst += value;
    }
    public void AddFood(float value)
    {
        hunger += value;
    }
}

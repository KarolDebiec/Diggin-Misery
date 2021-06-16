using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public float maxHealth;
    public float maxStamina;
    public float maxHunger;
    public float maxThirst;
    public float hungerLossMultiplier;
    public float hungerHPLossMultiplier;
    public float thirstLossMultiplier;
    public float thirstHPLossMultiplier;
    public bool gettingHungry;
    public bool gettingThirsty;
    public GameObject respawnPoint = null;

    public Item objectInHand = null;// object that is picked at the time


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

    public LayerMask layer;//the layer that the raycast of dropping item will hit on
    public float maxDistanceToDrop;

    public GameObject leftLegTarget; //leg is attracted to this object
    public GameObject rightLegTarget;
    public GameObject leftLegTargetTargets; //legs target is attracted to this object if distance is too big
    public GameObject rightLegTargetTargets;
    public GameObject leftLegTargetRayOrigin;
    public GameObject rightLegTargetRayOrigin;
    public float maxLegsDistance;
    public float legMovementSpeed;

    public float GrabRange; // distance in which player can interact with objects
    public GameObject aimedAt;
    public GameObject canInteractIndicator;
    void Start()
    {
        character = gameObject.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(objectInHand!=null)
            {
                UseObjectInHand();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (aimedAt != null)
            {
                //tu wykonaj cos co ma sie stac
            }
        }
        if (alive && gettingHungry && hunger>0)
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
        #region Legs Animation
        RaycastHit hit2;
        if (Physics.Raycast(rightLegTargetRayOrigin.transform.position, (Vector3.down - rightLegTargetRayOrigin.transform.position).normalized, out hit2, Mathf.Infinity, layer))
        {
            Debug.DrawRay(rightLegTargetRayOrigin.transform.position, (Vector3.down - rightLegTargetRayOrigin.transform.position).normalized * hit2.distance, Color.white);
        }
        if (Physics.Raycast(leftLegTargetRayOrigin.transform.position, (Vector3.down - leftLegTargetRayOrigin.transform.position).normalized, out hit2, Mathf.Infinity, layer))
        {
            Debug.DrawRay(leftLegTargetRayOrigin.transform.position, (Vector3.down - leftLegTargetRayOrigin.transform.position).normalized * hit2.distance, Color.blue);
        }
        #endregion
        #region Camera Interactions
        Ray ray3 = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));//raycast stuff
        RaycastHit hit3;
        if (Physics.Raycast(ray3, out hit3,GrabRange))//notice the layer
        {
            Debug.Log(hit3.transform.name);
            if(hit3.rigidbody!=null)
            {
                if (hit3.rigidbody.gameObject.CompareTag("Interactable"))
                {
                    canInteractIndicator.SetActive(true);
                    aimedAt = hit3.rigidbody.gameObject;
                }
                else
                {
                    aimedAt = null;
                    canInteractIndicator.SetActive(false);
                }
            }
            else
            {
                aimedAt = null;
                canInteractIndicator.SetActive(false);
            }
        }
        else
        {
            aimedAt = null;
            canInteractIndicator.SetActive(false);
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
    public void AddHealth(float value)
    {
        if(health+value>=maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += value;
        }
    }
    public void AddStamina(float value)
    {
        if (stamina + value >= maxStamina)
        {
            stamina = maxStamina;
        }
        else
        {
            stamina += value;
        }
    }
    public void ReduceThirst(float value)
    {
        if (thirst + value >= maxThirst)
        {
            thirst = maxThirst;
        }
        else
        {
            thirst += value;
        }
    }
    public void ReduceHunger(float value)
    {
        if (hunger + value >= maxHunger)
        {
            hunger = maxHunger;
        }
        else
        {
            hunger += value;
        }
    }
    public void ChangeObjectInHand(Item item)
    {
        objectInHand = item;
        // zmiana przedmiotu w rece (fizyczna)
    }
    public void UseObjectInHand()
    {
        objectInHand.Use();
    }
    public void SpawnDiggedMaterial(Item digged)
    {
        Ray ray = cam.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));//raycast stuff
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layer))//notice the layer
        {
            if (hit.collider != null)
            {
                if(maxDistanceToDrop>hit.distance)
                {
                    Instantiate(digged.prefab, hit.point, Quaternion.identity);
                    Debug.Log("created at hit.point");
                }
                else
                {
                    Instantiate(digged.prefab, hit.point, Quaternion.identity);
                }
            }
        }
    }
}

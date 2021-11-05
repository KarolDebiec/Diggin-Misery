using BioIK;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PickableMaterialController : MonoBehaviour
{
    private GameObject player;
    public Item item; //item of this prefab(used in eq system)
    public int amount = 1; // amount of items in this items (when items stack on ground it will be over 1)
    public int maxStack = 64;
    public bool attract;
    public bool bounced; //if hitted player and bounced off
    public float attractionSpeed;
    private Inventory inventorySystem;
    private GameController gameController;

    private GameObject attractTo = null;
    public float attractionToMaterialsDistance = 5;
    public float sizeMultiplierOverStack = 1.1f;
    //private Vector3 startingSize;
    //public Vector3 targetSize; // size at which it will be absorbed into equipment
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.AddToListOfMaterials(gameObject);
    }

    void Update()
    {
        checkMaterialsInDistance();
        /*
        if(attract && gameObject.GetComponent<Rigidbody>().useGravity)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        if (!attract && !gameObject.GetComponent<Rigidbody>().useGravity)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        if (attract)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position+(Vector3.up), Time.deltaTime* attractionSpeed);
        }*/
    }
    private void LateUpdate()
    {
      /*  if (bounced)
        {
            for (int i = 0;i<gameController.materialsOnGround.Count; i++)
            {
                if(Vector3.Distance(gameController.materialsOnGround[i].transform.position,gameObject.transform.position)< attractionToMaterialsDistance && gameController.materialsOnGround[i].GetComponent<PickableMaterialController>().amount < maxStack)
                {
                    transform.position = Vector3.MoveTowards(transform.position, gameController.materialsOnGround[i].transform.position, Time.deltaTime * attractionSpeed);
                    gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
            }
        }*/

    }
    private void checkMaterialsInDistance()
    {
        foreach (GameObject materialObject in gameController.materialsOnGround)
        {
            if(materialObject != gameObject)
            {
                if (Vector3.Distance(materialObject.transform.position, gameObject.transform.position) < attractionToMaterialsDistance && !materialObject.GetComponent<PickableMaterialController>().isFull() && !isFull())
                {
                    Debug.Log("Attract to : " + materialObject.name);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Material"))
        {
            if (other.GetComponent<PickableMaterialController>().amount < maxStack && amount < maxStack)
            {
                if (other.GetInstanceID() > gameObject.GetInstanceID())
                {
                    if (amount + other.GetComponent<PickableMaterialController>().amount <= maxStack)
                    {
                        other.GetComponent<PickableMaterialController>().amount+=amount;
                        gameController.RemoveFromListOfMaterials(gameObject);
                        for (int i = 0; i < amount; i++)
                        {
                            other.transform.localScale = other.transform.localScale * sizeMultiplierOverStack;
                        }
                        Destroy(gameObject);
                    }
                    else
                    {
                        other.GetComponent<PickableMaterialController>().amount+= (maxStack- other.GetComponent<PickableMaterialController>().amount);
                        amount -= (maxStack - other.GetComponent<PickableMaterialController>().amount);
                        for (int i = 0; i < (maxStack - other.GetComponent<PickableMaterialController>().amount); i++)
                        {
                            other.transform.localScale = other.transform.localScale * sizeMultiplierOverStack;
                            gameObject.transform.localScale *= 1f / sizeMultiplierOverStack;
                        }
                    }
                }
            }
        }
        
        if(other.CompareTag("Player"))
        {
            inventorySystem = (GameObject.FindGameObjectWithTag("GameController")).GetComponent<Inventory>();
            for(int i = 0;i<amount ;i++)
            {
                if (inventorySystem.IsSpaceForItem(item))
                {
                    inventorySystem.Add(item);
                    if(amount > 1)
                    {
                        amount--;
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Debug.Log("no space");
                    //bounced = true;
                    //gameController.AddToListOfMaterials(gameObject);
                    //attract = false;
                    break;
                }
            }
        }
        /*
        if (other.CompareTag("Material") && bounced)
        {
            if (other.GetComponent<PickableMaterialController>().amount<maxStack && other.GetComponent<PickableMaterialController>().amount>amount)
            {
                other.GetComponent<PickableMaterialController>().amount++;
                gameController.RemoveFromListOfMaterials(gameObject);
                Destroy(gameObject);
            }
            else if(other.GetComponent<PickableMaterialController>().amount < maxStack && other.GetComponent<PickableMaterialController>().amount == amount)
            {
                if(other.GetInstanceID()>gameObject.GetInstanceID())
                {
                    other.GetComponent<PickableMaterialController>().amount++;
                    gameController.RemoveFromListOfMaterials(gameObject);
                    Destroy(gameObject);
                }
            }
            else if (other.GetComponent<PickableMaterialController>().amount >= maxStack || amount >= maxStack)
            {
                bounced = false;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                bounced = false;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }*/
    }
    public bool isFull()
    {
        if(amount >= maxStack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isAttracted()
    {
        return attract;
    }
    public void attractToTarget(GameObject attractingObject)
    {
        if(attractingObject!=null)
        {
            attractTo = attractingObject;
            attract = true;
        }
    }
    public int getAmount()
    {
        return amount;
    }
}

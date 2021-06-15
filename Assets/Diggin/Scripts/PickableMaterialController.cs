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
    public int maxStack = 5;
    public bool attract;
    public bool bounced; //if hitted player and bounced off
    public float attractionSpeed;
    private Inventory inventorySystem;
    private GameController gameController;

    public float attractionToMaterialsDistance=5;

    private Vector3 startingSize;
    public Vector3 targetSize; // size at which it will be absorbed into equipment
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
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
        }
    }
    private void LateUpdate()
    {
        if (bounced)
        {
            for (int i = 0;i<gameController.materialsOnGround.Count; i++)
            {
                if(Vector3.Distance(gameController.materialsOnGround[i].transform.position,gameObject.transform.position)< attractionToMaterialsDistance && gameController.materialsOnGround[i].GetComponent<PickableMaterialController>().amount < maxStack)
                {
                    transform.position = Vector3.MoveTowards(transform.position, gameController.materialsOnGround[i].transform.position, Time.deltaTime * attractionSpeed);
                    gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inventorySystem = (GameObject.FindGameObjectWithTag("GameController")).GetComponent<Inventory>();
            if (inventorySystem.IsSpaceForItem(item))
            {
                inventorySystem.Add(item);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("no space");
                bounced = true;
                gameController.AddToListOfMaterials(gameObject);
                attract = false;
            }
        }
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
        }
    }
}

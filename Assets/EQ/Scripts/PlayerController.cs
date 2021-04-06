using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
  
  private CharacterController _characterController;

  public float Speed = 5.0f;

  public float RotationSpeed = 240.0f;
  
  private float Gravity = 20.0f;

  private Vector3 _moveDir = Vector3.zero;

  public Inventory inventory;

  public GameObject Hand;
  
  public HUD Hud; 

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        //inventory.ItemUsed += Inventory_ItemUsed;

    }

    /*private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;

        //...

        gameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent = Hand.transform;
        goItem.transform.position = Hand.transform.position;
    }*/

    void Update()
    {
        if(mItemToPickup != null && Input.GetKeyDown(KeyCode.X))
        {
            inventory.AddItem(mItemToPickup);
            mItemToPickup.OnPickup();
            Hud.CloseMessagePanel();
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (v<0)
            v=0;
        
        transform.Rotate(0, h * RotationSpeed * Time.deltaTime, 0);
        _moveDir.y -= Gravity * Time.deltaTime;

        _characterController.Move(_moveDir * Time.deltaTime);

        if(_characterController.isGrounded)
        {
            bool move = (v > 0) || (h != 0);

            _moveDir = Vector3.forward * v;

            _moveDir = transform.TransformDirection(_moveDir);
            _moveDir *= Speed;
        }
        _moveDir.y -= Gravity * Time.deltaTime;
        
        _characterController.Move(_moveDir * Time.deltaTime);

    }

    private IInventoryItem mItemToPickup = null;

    private void OnTriggerEnter(Collider other) {
        

        IInventoryItem item = other.GetComponent<IInventoryItem>();

       
        if (item != null && other.gameObject.activeSelf)
        {
            
            mItemToPickup = item;
            //inventory.AddItem(item);
            //item.OnPickup();
            Hud.OpenMessagePanel("");
        }
    }

    private void OnTriggerExit(Collider other) {
        IInventoryItem item = other.GetComponent<IInventoryItem>();

       
        if (item != null)
        {
            Hud.CloseMessagePanel();
            mItemToPickup = null;
        }
        
    }


    
}

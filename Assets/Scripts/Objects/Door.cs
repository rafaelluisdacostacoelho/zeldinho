using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    Key,
    Enemy,
    Button,
}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType doorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public Sprite doorOpen;
    public Sprite doorClose;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerInRange && doorType == DoorType.Key)
            {
                if (playerInventory.numberOfKeys > 0)
                {
                    playerInventory.numberOfKeys--;
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        open = true;
        physicsCollider.enabled = false;
        doorSprite.sprite = doorOpen;
    }

    public void Close()
    {
        open = false;
        physicsCollider.enabled = true;
        doorSprite.sprite = doorClose;
    }
}

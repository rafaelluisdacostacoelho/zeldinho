using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public GameSignal raiseItem;
    
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isOpen)
            {
                StartCoroutine(OpenChestCo());
            }
            else
            {
                ChestAlreadyOpen();
            }
        }
        if (isOpen)
        {
            contextOff.Raise();
        }
    }

    public IEnumerator OpenChestCo()
    {
        isOpen = true;
        contextOff.Raise();
        playerInteract.Raise();
        anim.SetBool("Opened", true);
        dialogBox.SetActive(true);
        dialogText.text = ".";
        yield return new WaitForSeconds(0.5f);
        dialogText.text = "..";
        yield return new WaitForSeconds(0.5f);
        dialogText.text = "...";
        yield return new WaitForSeconds(0.5f);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        playerIdle.Raise();
        raiseItem.Raise();
    }

    public void ChestAlreadyOpen()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            contextOn.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            contextOff.Raise();
            playerInRange = false;
        }
    }
}

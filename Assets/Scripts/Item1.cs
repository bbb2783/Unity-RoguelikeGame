using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item1 : MonoBehaviour
{
    [SerializeField] Text pickupText;
    bool isPickUp;
    
    public Item item;
    public SpriteRenderer image;

    void Start()
    {
        pickupText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPickUp && Input.GetKeyDown(KeyCode.Space))
        {
            DestroyItem();
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Player"))
        {
            pickupText.gameObject.SetActive(true);
            isPickUp = true;

        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Player"))
        {
            pickupText.gameObject.SetActive(false);
            isPickUp = false;
        }
    }
    
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;

        image.sprite = item.itemImage;
    }
    public Item GetItem()
    {
        return item;
    }
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}

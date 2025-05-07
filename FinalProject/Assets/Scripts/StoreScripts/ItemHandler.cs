using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [Header("Item Attributes")]
    [SerializeField] string itemName;
    [SerializeField] int itemCost;
    [SerializeField] bool isPurchased = false;
    [SerializeField] bool showItem = false;

    [Header("Item Components To Hide")]
    MeshRenderer[] meshRenderers;
    MeshRenderer parentMeshRenderer;
    SphereCollider col;

    void Awake()
    {
        parentMeshRenderer = GetComponent<MeshRenderer>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        col = GetComponent<SphereCollider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItemPurchase(bool purchased)
    {
        isPurchased = purchased;
    }

    public bool GetItemPurchased()
    {
        return isPurchased;
    }

    public void SetShowItem(bool show)
    {
        showItem = show;
    }

    public bool GetShowItem()
    {
        return showItem;
    }

    public void HideItem()
    {
        //Hide Collider so player cannot interact with item
        col.enabled = false;

        //Hide MeshRenderer so items are not visible to player
        parentMeshRenderer.enabled = false;
        if(meshRenderers.Length > 0)
        {
            foreach(MeshRenderer mr in meshRenderers)
            {
                mr.enabled = false;
            }
        }
    }

    public void ShowItem()
    {
        //Enable Collider so player can interact with item
        col.enabled = true;

        //Enable MeshRenderer so items are visible to player
        parentMeshRenderer.enabled = true;
        if(meshRenderers.Length > 0)
        {
            foreach(MeshRenderer mr in meshRenderers)
            {
                mr.enabled = true;
            }
        }
    }

    public List<string> GetItemDescription()
    {
        List<string> description = new List<string>();

        description.Add(itemName + "\nCost: " + itemCost.ToString() + " coins");
        return description;
    }
}

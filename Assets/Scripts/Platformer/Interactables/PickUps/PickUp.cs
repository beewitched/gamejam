using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class PickUp : MonoBehaviour, IInteractWithPlayer
{
    [Header("Settings")]
    [SerializeField]
    private string name = "Pickup";
    public string Name
    {
        get
        {
            return name;
        }
    }
    [SerializeField]
    private string group = "Default";
    public string Group
    {
        get
        {
            return group;
        }
    }
    [SerializeField]
    private bool isInteractable = true;
    public bool IsInteractable
    {
        get
        {
            return isInteractable;
        }
    }

    private SpriteRenderer renderer;

    public static Dictionary<string, List<PickUp>> all = new Dictionary<string, List<PickUp>>();
    public static Dictionary<string, List<PickUp>> InScene
    {
        get
        {
            return all;
        }
    }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        if (all.ContainsKey(group))
        {
            all[group].Add(this);
        }
        else
        {
            all.Add(group, new List<PickUp> { this });
        }
    }

    public void Interact()
    {
        if (!isInteractable) { return; }
        if (Inventory.Instance.AddPickUp(new PickUpInfo(name, group, renderer.sprite)))
        {
            for (int i = 0; i < InScene[group].Count; i++)
            {
                InScene[group][i].DisableInteraction();
            }
            Destroy(gameObject);
        }
    }

    public void EnableInteraction()
    {
        isInteractable = true;
    }

    public void DisableInteraction()
    {
        isInteractable = false;
    }
}

[System.Serializable]
public class PickUpInfo
{
    [SerializeField]
    private string name;
    public string Name
    {
        get
        {
            return name;
        }
    }

    [SerializeField]
    private string group;
    public string Group
    {
        get
        {
            return group;
        }
    }

    [SerializeField]
    private Sprite sprite;
    public Sprite Icon
    {
        get
        {
            return sprite;
        }
    }

    public PickUpInfo(string name, string group, Sprite sprite)
    {
        this.name = name;
        this.group = group;
        this.sprite = sprite;
    }
}
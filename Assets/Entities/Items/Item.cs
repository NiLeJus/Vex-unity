using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] public _BaseItem Base { get; set; }
    public string Name => Base.itemName;
    public string Description => Base.description;
    public Sprite Visual => Base.visual;

    [SerializeField] private List<Effect> appliedEffectsOnTarget;
    // Update is called once per frame
    void Update()
    {

    }
}

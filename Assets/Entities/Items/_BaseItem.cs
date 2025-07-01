using UnityEngine;

[CreateAssetMenu(fileName = "Vexplorer", menuName = "Vexplorer/ Create new Items")]
[System.Serializable]

public class _BaseItem : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public string description;
    [SerializeField] public int worth;
    [SerializeField] public _BaseEffect effect;
    [SerializeField] public Sprite visual;

}

using UnityEngine;
[CreateAssetMenu(fileName = "Vexplorer", menuName = "Vexplorer/Create New NPC")]

public class _BaseNPC : _BaseCharacter
{
    [Header("Player Specific")]
    [SerializeField] public int tamerLevel;

}

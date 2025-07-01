using UnityEngine;
[CreateAssetMenu(fileName = "Vexplorer", menuName = "Character/Create New Player")]
[ES3Serializable]
public class _BasePlayer : _BaseCharacter
{
    [Header("Player Specific")]
    [SerializeField] public int tamerLevel;

}

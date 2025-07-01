using UnityEngine;
[CreateAssetMenu(fileName = "Vexplorer", menuName = "Vexplorer/Effect/Create new Effects")]
[System.Serializable]
public abstract class _BaseEffect : ScriptableObject
{
    [SerializeField] public string EffectName;
    [SerializeField] public string Description;
    [SerializeField] public Sprite Visual;
    [SerializeField] public int Level;
    [SerializeField] public int TimeDuration;
    [SerializeField] public bool IsInstant;

    public abstract void TriggerEffect(EffectContext context);
}

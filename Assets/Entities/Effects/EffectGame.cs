using UnityEngine;

public interface IEffect
{
    public void Trigger(
        Creature target
        );
}

public class EffectContext
{
    public Creature Target;

    public EffectContext(Creature target)
    {
        Target = target;
    }

    public static EffectContext Make(Creature creatureTarget)
    {
        return new EffectContext(creatureTarget);
    }

}

[CreateAssetMenu(fileName = "New Effect", menuName = "Vexplorer/New Effect")]
public class Effect : ScriptableObject, IEffect
{
    [SerializeField] public _BaseEffect Base { get; set; }
    public string Name => Base.EffectName;
    public string Description => Base.Description;
    public int Level => Base.Level;
    public Sprite Visual => Base.Visual;

    public void Trigger(Creature target)
    {
        //Base.TriggerEffect();
    }
}

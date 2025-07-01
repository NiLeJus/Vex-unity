using UnityEngine;

[CreateAssetMenu(fileName = "BurnEffect", menuName = "Effect/Burn")]
public class BurnEffect : _BaseEffect
{
    public override void TriggerEffect(EffectContext context)
    {
        context.Target.Damage(1);
    }
}




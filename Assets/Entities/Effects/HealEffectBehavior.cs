using UnityEngine;

[CreateAssetMenu(fileName = "Vexplorer", menuName = "Vexplorer/Effect/Heal")]
public class HealEffect : _BaseEffect
{

    [SerializeField]
    private int healthAmount;

    public override void TriggerEffect(EffectContext context)
    {
        context.Target.Resources.Health.AddAmount(healthAmount);
    }
}
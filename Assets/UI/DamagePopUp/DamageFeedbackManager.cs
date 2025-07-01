using UnityEngine;

public class DamageFeedbackManager : MonoBehaviour
{
    [SerializeField] private GameObject pfDamagePopUp;
    [SerializeField] private Camera mainCamera;

    void Update()
    {

    }

    public void TriggerDamageFeedback(Transform spawnPosition, float value)
    {
        DamagePopUp.Create(spawnPosition, (int)value, pfDamagePopUp, mainCamera);
    }
}

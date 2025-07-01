using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Camera targetCamera; // Référence à la caméra
    private Color textColor;
    private float disapearTimer;

    public static DamagePopUp Create(Transform position, int damageAmount, GameObject pfDamagePopUp, Camera camera)
    {
        GameObject damagePopupTransform = Instantiate(pfDamagePopUp, position.position, Quaternion.identity);
        DamagePopUp popUpDamage = damagePopupTransform.GetComponent<DamagePopUp>();
        popUpDamage.Setup(damageAmount, camera); // Passer la caméra à Setup
        return popUpDamage;
    }

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, Camera camera)
    {
        textMesh.SetText(damageAmount.ToString());
        targetCamera = camera;
        textColor = textMesh.color;
        disapearTimer = 1f;
    }

    private void Update()
    {
        // Orienter le texte pour qu'il face toujours face à la caméra
        transform.rotation = targetCamera.transform.rotation;
        float moveYSpeed = 2f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        HandleTiming();
    }

    private void HandleTiming()
    {
        disapearTimer -= Time.deltaTime;
        if (disapearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

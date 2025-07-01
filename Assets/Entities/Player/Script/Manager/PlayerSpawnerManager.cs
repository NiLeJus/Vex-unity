using KinematicCharacterController;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(WaitForGameManager());
    }

    void Start()
    {
        transform.GetComponent<KinematicCharacterMotor>().SetPosition(GameManager.I.playerSpawnPosition);

        transform.position = GameManager.I.playerSpawnPosition;
        Debug.LogWarning($"{GameManager.I.playerSpawnPosition}");
    }
    private System.Collections.IEnumerator WaitForGameManager()
    {
        while (GameManager.I == null)
            yield return null;

        transform.position = GameManager.I.playerSpawnPosition;
        Debug.LogWarning($"Player spawned at {GameManager.I.playerSpawnPosition}");
    }
}

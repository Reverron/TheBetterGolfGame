using UnityEngine;

public class EndPortalBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) LevelManager.Instance.LoadNextLevel();
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Level Attributes")]
    [Tooltip("Add the name of the scenes in the order you want them to be loaded")]
    [SerializeField] List<string> scene = new List<string>();

    // Local Attributes
    int levelProgression = 0;
    int totalLevels;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        for (int i = 0; i < scene.Count; i++) {
            totalLevels++;
        }
    }

    public void LoadNextLevel() {
            SceneManager.LoadScene("Level1");
            levelProgression++;
    }

}

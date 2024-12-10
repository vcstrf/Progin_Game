using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private Vector2 spawnPosition;
    private string previousScene;

    [SerializeField]
    private Vector2 defaultSpawnPosition = new Vector2(-7.58f, -1.44f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            spawnPosition = defaultSpawnPosition;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector2 GetSpawnPosition()
    {
        return spawnPosition;
    }

    public void SetPreviousScene(string sceneName)
    {
        previousScene = sceneName;
    }

    public string GetPreviousScene()
    {
        return previousScene;
    }
}
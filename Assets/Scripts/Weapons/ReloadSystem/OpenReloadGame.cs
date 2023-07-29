using UnityEngine;

public class OpenReloadGame : MonoBehaviour
{
    [SerializeField] private GameObject reloadMiniGame;

    void Update()
    {
        if (DifferentStatic.openReloadGame)
        {
            reloadMiniGame.SetActive(true);
            DifferentStatic.openReloadGame = false;
        }
    }
}

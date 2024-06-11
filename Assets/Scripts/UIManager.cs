using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    // Start is called before the first frame update
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
}

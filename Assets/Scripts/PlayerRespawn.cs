using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Health playerHealth;
    private UIManager uiManager;

    // Start is called before the first frame update
    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    public void Respawn()
    {

    }
}

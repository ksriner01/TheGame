using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public PauseMenu pause;
    void OnTriggerEnter2D(Collider2D other)
    {
        pause.GameOver();
    }

}

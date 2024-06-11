using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip footstepssfx, attacksfx, playerhurtsfx, enemyhurtsfx;

    public void Moving()
    {
        src.clip = footstepssfx;
        src.Play();
    }

    public void Attacking()
    {
        src.clip = attacksfx;
        src.Play();
    }

    public void PlayerHurt()
    {
        src.clip = playerhurtsfx;
        src.Play();
    }

    public void EnemyHurt()
    {
        src.clip = enemyhurtsfx;
        src.Play();
    }
}

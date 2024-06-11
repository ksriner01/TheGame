using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    private int attackCount;
    private float waitTime = 0.1f;
    public PauseMenu menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            } else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
                popUpIndex++;            
            }
        } else if (popUpIndex == 1) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
            }
        } else if (popUpIndex == 2)
        {
            if (waitTime <= 0)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    attackCount++;
                    if (attackCount == 3)
                    {
                        popUpIndex++;
                    }
                }
            } else
            {
                waitTime -= Time.deltaTime;
            }
        } else if (popUpIndex == 3)
        {

        }
    }
}

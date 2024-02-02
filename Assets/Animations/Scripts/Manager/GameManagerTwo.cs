using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTwo : MonoBehaviour
{
    public int sceneIndex;

    public static GameManagerTwo instance;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}

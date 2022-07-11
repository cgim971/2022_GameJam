using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public SaveManager _saveManager { get; private set; }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject); 

        _saveManager = FindObjectOfType<SaveManager>();
    }
}

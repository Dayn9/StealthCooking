﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => LoadLevel());
    }

    public void LoadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Slice");
    }
}

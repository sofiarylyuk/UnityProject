﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{


    public static LevelController current;


    void Awake()
    {
        current = this;

    }

}


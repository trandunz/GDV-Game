using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : Script_CharacterMotor2D
{
    void Start()
    {
        InitMotor();
    }

    void LateUpdate()
    {
        Motor();
    }
}

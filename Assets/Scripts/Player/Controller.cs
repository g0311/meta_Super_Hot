using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public bool _isAlive;
    public abstract void PawnDeath();

    protected void Move()
    {

    }
    protected void Rotate()
    {

    }
}

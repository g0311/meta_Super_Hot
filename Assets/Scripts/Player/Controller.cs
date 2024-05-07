using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public bool _isAlive = true;
    public abstract void PawnDeath();
}

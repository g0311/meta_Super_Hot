using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    private Vector3 _prevPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = Vector3.Distance(_prevPos, transform.position);
        GameMode._instance._deltaTime = moveDistance * 0.1f;
        _prevPos = transform.position;
    }

    public override void PawnDeath()
    {
        //ui Ãâ·Â
        //
    }
}

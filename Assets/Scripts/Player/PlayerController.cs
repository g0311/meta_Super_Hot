using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    private Vector3 _prevPos;
    public GameObject _centerEye;
    // Start is called before the first frame update
    void Start()
    {
        _prevPos = _centerEye.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = Vector3.Distance(_prevPos, _centerEye.transform.position);
        GameMode.Instance.SetDeltaTime(moveDistance);
        _prevPos = _centerEye.transform.position;
    }

    public override void PawnDeath()
    {
        
    }
}

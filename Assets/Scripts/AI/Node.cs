using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Failure,
    Running,
    Success
}

public class Node : MonoBehaviour
{
    public NodeState state;
    // Start is called before the first frame update
    void Start()
    {
        state = NodeState.Running;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

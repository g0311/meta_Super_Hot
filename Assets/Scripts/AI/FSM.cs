using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FSM : MonoBehaviour
{
    private enum State
    {
        Idle,
        Move,
        Attack
    }

    private State _state;
    private Vector3 _curPlayerPos = new Vector3(0,0,0);
    private EnemyController _enemyController;
    private Gun _enemyWeapon;

    public float _attackRange = 10f; //attack range
    public LayerMask _playerLayer; //player layer
    public Animator _animator;
    private bool _isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        _state = State.Idle;
        _enemyController = GetComponent<EnemyController>();
        _enemyWeapon = _enemyController._weapon.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("State : " + _state);
        switch (_state)
        {
            case State.Idle:
                {
                    _animator.SetBool("isMoving", false);

                    break;
                }
            case State.Move:
                {
                    _animator.SetBool("isMoving", true);
                    
                    //Error space
                    Vector3 directionToPlayer = _curPlayerPos - _enemyWeapon.transform.position;
                    directionToPlayer.y = 0; //with out y axis rotation
                    Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

                    _enemyController.Rotate(lookRotation);
                    _enemyController.Move();
                    break;
                }
            case State.Attack:
                {
                    if (!_isAttacking)
                    {
                        StartCoroutine(AttackRoutine());
                    }
                    break;
                }
        }

        UpdateState();
    }
    
    private void UpdateState()
    {
        //if find Player
        if(CanSeePlayer()) 
        { 
            if(CanAttackPlayer())
            {
                _state = State.Attack;
            }
            else
            {
                _state = State.Move;
            }
        }
        else
        {
            _state = State.Idle;
        }
    }

    private bool CanSeePlayer()
    {
        GameObject player = GameMode._instance.GetPlayer();
        if (player != null)
        {
            _curPlayerPos = player.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _curPlayerPos - transform.position, out hit, Mathf.Infinity, _playerLayer))
            {
                if (hit.collider.tag == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool CanAttackPlayer()
    {
        float dist = Vector3.Distance(_curPlayerPos, transform.position);
        if(dist < 300)
        {
            RaycastHit hit;
            if (Physics.Raycast(_curPlayerPos, _enemyWeapon.transform.forward, out hit, Mathf.Infinity, _playerLayer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        _animator.SetBool("isMoving", false);
        _enemyWeapon.Fire();

        yield return new WaitForSeconds(1.5f); // 1초 동안 대기, 필요에 따라 조정

        _isAttacking = false;
    }
}

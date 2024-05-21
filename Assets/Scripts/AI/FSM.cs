using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class FSM : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Attack
    }

    public State _state;
    private State _preState;
    private Vector3 _curPlayerPos = new Vector3(0,0,0);
    private EnemyController _enemyController;
    private Gun _enemyWeapon;

    public float _attackRange = 10f; //attack range
    public Animator _animator;
    private bool _isAttacking = false;

    private NavMeshAgent _nav;

    // Start is called before the first frame update
    void Start()
    {
        _state = State.Idle;
        _preState = _state;
        _enemyController = GetComponentInChildren<EnemyController>();
        _enemyWeapon = _enemyController._weapon.GetComponent<Gun>();
        _nav = _enemyController.GetComponentInChildren<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.Idle:
                {
                    _animator.SetBool("isMoving", false);
                    break;
                }
            case State.Move:
                {
                    Rotate();
                    Move();
                    break;
                }
            case State.Attack:
                {
                    Rotate();
                    _animator.SetBool("isMoving", false);
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
        _preState = _state;

        //if find Player
        if (CanSeePlayer())
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

        // 상태가 변경되었을 경우 Move 상태 해제 시 동작
        if (_preState != _state && _preState == State.Move)
        {
            _nav.ResetPath();
        }
    }

    private bool CanSeePlayer()
    {
        GameObject player = GameMode._instance.GetPlayer();
        if (player != null)
        {
            _curPlayerPos = player.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _curPlayerPos - transform.position, out hit, Mathf.Infinity))
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _attackRange))
        {
            if (hit.collider.tag == "Player")
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

    private void Move()
    {
        float dist = Vector3.Distance(_curPlayerPos, transform.position);
        if (dist > _attackRange || _state != State.Attack)
        {
            _animator.SetBool("isMoving", true);
            //speed error
            _nav.SetDestination(_curPlayerPos);
            
            _nav.speed = 30 * GameMode.Instance._deltaTime;
            //animation speed
            GetComponentInChildren<Animator>().speed = 15 * GameMode.Instance._deltaTime;
        }
    }

    private void Rotate()
    {
        _curPlayerPos.y = 0; //char rotate error >> x axis rotation
        transform.LookAt(_curPlayerPos);
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        _enemyWeapon.Fire();

        yield return new WaitForSeconds(3);

        _isAttacking = false;
    }
}

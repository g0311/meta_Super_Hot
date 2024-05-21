using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
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

    private NavMeshAgent _nav;

    // Start is called before the first frame update
    void Start()
    {
        _state = State.Idle;
        _enemyController = GetComponentInChildren<EnemyController>();
        _enemyWeapon = _enemyController._weapon.GetComponent<Gun>();
        _nav = _enemyController.GetComponentInChildren<NavMeshAgent>();
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
                    Move();
                    break;
                }
            case State.Attack:
                {
                    Move();
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, _enemyWeapon.transform.forward, out hit, Mathf.Infinity, _playerLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Move()
    {
        _animator.SetBool("isMoving", true);

        //Vector3 directionToPlayer = _curPlayerPos - _enemyWeapon.transform.position;
        //directionToPlayer.y = 0; //with out y axis rotation
        //Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        //_enemyController.Rotate(lookRotation);

        _curPlayerPos.y = 0; //char rotate error >> x axis rotation
        transform.LookAt(_curPlayerPos);
        float dist = Vector3.Distance(_curPlayerPos, transform.position);
        if (dist > _attackRange)
        {
            //speed error
            float speed = 0.1f;
            _nav.SetDestination(_curPlayerPos);
            
            _nav.speed = speed * GameMode.Instance._deltaTime;
            //animation speed
            GetComponentInChildren<Animator>().speed = speed * GameMode.Instance._deltaTime * 90;
        }
        else
        {
            _nav.SetDestination(transform.position);
        }
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        _animator.SetBool("isMoving", false);
        _enemyWeapon.Fire();

        yield return new WaitForSeconds(10);

        _isAttacking = false;
    }
}

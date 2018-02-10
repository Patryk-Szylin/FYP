using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_Brain : MonoBehaviour
{
    public StackFSM m_brain = new StackFSM();
    public float m_speed = 50f;
    public bool m_moveForward = true;
    public float m_outOfRangeDistance = 25f;

    private Player _playerTarget;
    private Vector3 _initialPosition;

    private void Start()
    {
        _playerTarget = null;
        m_brain.PushState(idleWalk);
        _initialPosition = transform.position;
    }


    public void idleWalk()
    {
        print("MOVE");

        // The position needs to be transformed from World Space to Local Space
        var worldToLocalPos = transform.InverseTransformPoint(_initialPosition);
        print(worldToLocalPos);

        if (m_moveForward)
        {
            transform.Translate(m_speed * Vector3.forward * Time.deltaTime);
            if (worldToLocalPos.z < -10f)
                m_moveForward = false;
        }
        else
        {
            transform.Translate(m_speed * Vector3.back * Time.deltaTime);
            if (worldToLocalPos.z > 10f)
                m_moveForward = true;
        }

        // check for nearby players
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);

        // iF THERE'S nearby player then transition to state Attack and pass the variable of the player that is nearby
        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                var p = collider.GetComponent<Player>();
                if (p != null)
                {
                    print("FOUND A TARGET");
                    _playerTarget = p;
                    m_brain.PushState(Attack);
                }
            }
        }
    }

    public void Attack()
    {
        if (_playerTarget)
        {
            print("ATTACK");
            var step = m_speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _playerTarget.transform.position, step);

            // If out of range, move back to idle
            if (Vector3.Distance(transform.position, _playerTarget.transform.position) > m_outOfRangeDistance)
            {
                m_brain.PopState();
                m_brain.PushState(MoveToInitialPosition);
            }

        }
    }

    public void MoveToInitialPosition()
    {
        var step = m_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _initialPosition, step);

        if (transform.position == _initialPosition)
        {
            m_brain.PopState();
        }
    }



}

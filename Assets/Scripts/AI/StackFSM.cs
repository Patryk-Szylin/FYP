using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StackFSM : MonoBehaviour
{
    private Stack<Action> m_stack;

    public StackFSM()
    {
        m_stack = new Stack<Action>();
    }

    public void Update()
    {
        var currentStateFunction = getCurrentState();

        if (currentStateFunction != null)
        {
            if (currentStateFunction.GetType() == typeof(System.Action))
            {
                currentStateFunction();
            }
        }
    }

    public Action PopState()
    {
        return m_stack.Pop();
    }

    public void PushState(Action state, object paramas = null)
    {
        if (getCurrentState() != state)
        {
            m_stack.Push(state);
        }
    }

    public Action getCurrentState()
    {
        return m_stack.Count > 0 ? m_stack.Peek() : null;
    }




}

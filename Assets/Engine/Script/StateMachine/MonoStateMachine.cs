using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace RakaEngine.Statemachine
{
    public class MonoStateMachine : MonoBehaviour
    {

        [SerializeField]
        protected AMonoState m_initialState;

        public AMonoState currentState { get; private set; }

        protected bool IsStateMachineFrozen = false;

        /// <summary>
        /// Called before the enter() of the new state
        /// </summary>
        public Action<MonoStateMachine, AMonoState> SystemEvent_OnstateChange;

        /// <summary>
        /// Called before the enter() of the new state
        /// </summary>
        public UnityEvent<MonoStateMachine, AMonoState> UnityEvent_OnstateChange;


        #region LifeCycle
        public virtual void Update()
        {
            if (!IsStateMachineFrozen)
            {
                currentState?.UpdateState();
            }
        }

        public virtual void FixedUpdate()
        {
            if (!IsStateMachineFrozen)
            {
                currentState?.FixedUpdateState();
            }
        }

        public virtual void LateUpdate()
        {
            if (!IsStateMachineFrozen)
            {
                currentState?.LateUpdateState();
            }
        }
        #endregion


        [Button("Init")]
        public void Initialize()
        {
            currentState = m_initialState;

            //register Callback to change the current state
            currentState.SystemEvent_OnChangeState += ChangeCurrentState;

            //Notify to subscriber this stamachine has changed his curent State
            CallsEventsOnStateChange(currentState);

            //Enter in new state 
            currentState?.OnEnter();
        }

        public virtual void ChangeCurrentState(AMonoState a_state)
        {
            if (IsStateMachineFrozen) return;

            // Leave old state
            currentState?.OnLeave();


            //unregister Callback to oldState
            currentState.SystemEvent_OnChangeState -= ChangeCurrentState;

            //Assign new State
            currentState = a_state;

            //register Callback to change the current state
            currentState.SystemEvent_OnChangeState += ChangeCurrentState;

            //Notify to subscriber this stamachine has changed his curent State
            CallsEventsOnStateChange(currentState);

            //Enter in new state 
            currentState?.OnEnter();
        }

        protected virtual void CallsEventsOnStateChange(AMonoState a_newState)
        {
            SystemEvent_OnstateChange?.Invoke(this, a_newState);
            UnityEvent_OnstateChange?.Invoke(this, a_newState);
        }

        public void SetFreezeStateMachine(bool a_value)
        {
            IsStateMachineFrozen = a_value;
        }

    }
}

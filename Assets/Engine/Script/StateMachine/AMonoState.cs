using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace RakaEngine.Statemachine
{
    public abstract class AMonoState : MonoBehaviour
    {
        public Action EventSystem_OnEnter;
        public UnityEvent UnityEvent_OnEnter;

        public Action EventSystem_OnLeave;
        public UnityEvent UnityEvent_OnLeave;

        public Action<AMonoState> SystemEvent_OnChangeState;
        public UnityEvent<AMonoState> UnityEvent_OnChangeState;


        private Coroutine m_delayedChangeState_routine;

        public virtual void OnEnter()
        {
            EventSystem_OnEnter?.Invoke();
            UnityEvent_OnEnter?.Invoke();
        }
        public virtual void OnLeave()
        {
            EventSystem_OnLeave?.Invoke();
            UnityEvent_OnLeave?.Invoke();
        }

        public virtual void UpdateState() { }

        public virtual void FixedUpdateState() { }

        public virtual void LateUpdateState() { }

        protected void RequestChangeState(AMonoState a_newState)
        {
            SystemEvent_OnChangeState?.Invoke(a_newState);
            UnityEvent_OnChangeState?.Invoke(a_newState);
        }

        protected void RequestDelayedChangeState(AMonoState a_newState, float a_durationToWait)
        {
            //Cancel coroutine if antoher exist
            CancelChangeState();

            //start coroutine
            m_delayedChangeState_routine = StartCoroutine(DelayedChangeState_Routine(a_newState, a_durationToWait));
        }

        private IEnumerator DelayedChangeState_Routine(AMonoState a_newState, float a_durationToWait)
        {
            yield return new WaitForSeconds(a_durationToWait);
            RequestChangeState(a_newState);
        }

        protected void CancelChangeState()
        {
            //Check if it's not null toi avoid nullref
            if (m_delayedChangeState_routine != null)
            {
                StopCoroutine(m_delayedChangeState_routine);
                m_delayedChangeState_routine = null; //set value to null to logic loop
            }
        }


    }
}

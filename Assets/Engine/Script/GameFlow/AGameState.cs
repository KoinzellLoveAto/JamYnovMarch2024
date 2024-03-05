using RakaEngine.Statemachine.Gameflow.GameStateComponent;
using System.Collections.Generic;
using UnityEngine;

namespace RakaEngine.Statemachine.Gameflow
{
    public abstract class AGameState : AMonoState
    {
        [SerializeField]
        protected AGameState m_nextState;

        [SerializeField]
        protected List<AGameStateComponent> m_stateComponents = new();

        /// <summary>
        /// When true, the function called every X frame, should not be execute
        /// </summary>
        public bool IsStateFrozen { get; protected set; } = false;

        public void SetFreezeState(bool a_value)
        {
            IsStateFrozen = a_value;
        }

        /// <summary>
        /// return the component wanted if it's exist in the state
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetStateComponent<T>() where T : AGameStateComponent
        {
            foreach (AGameStateComponent component in m_stateComponents)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }
            return null;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (m_stateComponents.Count > 0)
            {
                foreach(AGameStateComponent component in m_stateComponents)
                {
                    component.OnStateBegin();
                }
            }
        }

        public override void OnLeave()
        {
            base.OnLeave();
            if (m_stateComponents.Count > 0)
            {
                foreach (AGameStateComponent component in m_stateComponents)
                {
                    component.OnStateEnd();
                }
            }

        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void LateUpdateState()
        {
            base.LateUpdateState();
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
        }
    }
}
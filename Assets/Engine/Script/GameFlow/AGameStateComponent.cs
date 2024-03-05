using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RakaEngine.Statemachine.Gameflow.GameStateComponent
{
    public class AGameStateComponent : MonoBehaviour
    {
        private bool isComponentActif;

        public virtual void OnStateBegin() { }
        public virtual void OnStateEnd() { }
    }

}

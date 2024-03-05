using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.DefaultInputActions;

namespace RakaEngine.Character
{

    public abstract class ARakaController : MonoBehaviour
    {
        /// <summary>
        /// Pocesss and Init controller
        /// </summary>
        /// <param name="a_linkedCharacter"></param>
        public abstract void PocessCharacter(ARakaCharacter a_linkedCharacter);

        /// <summary>
        /// Unpocess the current character
        /// </summary>
        /// <param name="a_linkedCharacter"></param>
        public abstract void UnpocessCharacter(ARakaCharacter a_linkedCharacter);

        public abstract void RegisterEvents();

        public abstract void UnregisterEvents();

    }

}
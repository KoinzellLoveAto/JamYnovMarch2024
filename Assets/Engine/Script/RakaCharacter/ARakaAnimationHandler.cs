using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RakaEngine.Character
{
    public abstract class ARakaAnimationHandler 
    {
        private ARakaCharacter m_linkedCharacter;
        public ARakaAnimationHandler(ARakaCharacter a_linkedcharacter)
        {
            m_linkedCharacter = a_linkedcharacter;
        }
    }
}
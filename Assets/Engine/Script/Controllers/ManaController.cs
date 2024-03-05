using RakaTools.Lock;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace RakaEngine.Controllers.Mana
{

    public class ManaController : MonoBehaviour
    {
        #region Events
        public Action<ManaController> EventSystem_onManaChange;
        public Action<ManaController, float> EventSystem_onManaConsumed;
        public Action<ManaController, float> EventSystem_onManaAdded;
        public Action<ManaController> EventSystem_onOutOfMana;
        public Action<ManaController> EventSystem_onReachedFullMana;


        public UnityEvent<ManaController> UnityEvent_onManaChange;
        public UnityEvent<ManaController, float> UnityEvent_onManaConsumed;
        public UnityEvent<ManaController, float> UnityEvent_onManaAdded;
        public UnityEvent<ManaController> UnityEvent_onOutOfMana;
        public UnityEvent<ManaController> UnityEvent_onReachedFullMana;
        #endregion

        #region Variables
        /// <summary>
        /// Used to control if mana should regen
        /// </summary>
        public bool IsRegenFrozen { get; private set; } = false;

        /// <summary>
        ///  Used to use free mana
        /// </summary>
        public bool FreeUseMana { get; private set; } = false;

        public float MinMana { get; private set; } = 0;
        public float MaxMana { get; private set; } = 100;
        public float currentMana { get; private set; }

        protected LockerController m_lockerCanRegenMana = new LockerController();
        protected LockerController m_lockerCanConsumeMana = new LockerController();

        public bool CanRegenMana => m_lockerCanRegenMana.IsLock;
        public bool CanConsumeMana => m_lockerCanConsumeMana.IsLock;


        /// <summary>
        /// Used to avoid spamming event full mana  if alreadyFull mana
        /// </summary>
        protected bool m_haveAlreadyReachedMaxMana = true;

        /// <summary>
        /// Used to avoid spamming event Min mana  if mana
        /// </summary>
        protected bool m_haveAlreadyReachedMinMana = true;

        /// <summary>
        /// the value to regen each tick
        /// </summary>
        protected float m_regenValueByTick = 5;

        /// <summary>
        /// The time between each Tick
        /// </summary>
        protected float m_regenTickRate = .5f;


        protected Coroutine ManaRegenRoutine;
        #endregion

        #region Setters
        /// <summary>
        /// Used to control if mana should regen
        /// </summary>
        /// <param name="a_value"></param>
        public virtual void FreezeManaRegen(bool a_value = false) => IsRegenFrozen = a_value;

        /// <summary>
        /// Used to control if mana should be free
        /// </summary>
        /// <param name="a_value"></param>
        public virtual void SetFreeUseMana(bool a_value) => FreeUseMana = a_value;

        #endregion

        #region Methods



        public virtual void Initialize()
        {
            ManaRegenRoutine = StartCoroutine(ManaRegen_Routine());
        }

        public void OnDestroy()
        {
            StopCoroutine(ManaRegenRoutine);
        }

        /// <summary>
        /// Add locker count to can regen Mana
        /// </summary>
        /// <param name="a_value"></param>
        public void SetCanRegenMana(bool a_value)
        {
            if (!a_value)
                m_lockerCanRegenMana.AddLocker();
            else
                m_lockerCanRegenMana.RemoveLocker();
        }

        /// <summary>
        /// Add locker count to consume mana
        /// </summary>
        /// <param name="a_value"></param>
        public void SetCanBeConsumed(bool a_value)
        {
            if (!a_value)
                m_lockerCanConsumeMana.AddLocker();
            else
                m_lockerCanConsumeMana.RemoveLocker();
        }

        public bool HaveEnoughtMana(float a_amount) => currentMana >= a_amount;


        /// <summary>
        /// Use to consume  Mana (Mana will be clamped)
        /// </summary>
        /// <param name="a_amount"></param>
        public virtual void ConsumeMana(float a_amount)
        {
            if (FreeUseMana) return;

            if (!CanConsumeMana) return;

            currentMana -= a_amount;

            //If reached minMana -> callback out ressources
            if (currentMana <= MinMana && !m_haveAlreadyReachedMinMana)
            {
                m_haveAlreadyReachedMinMana = true;

                EventSystem_onOutOfMana?.Invoke(this);
                UnityEvent_onOutOfMana?.Invoke(this);
            }

            // mana is not full, set true to call the next full mana callback
            if (currentMana < MaxMana) m_haveAlreadyReachedMaxMana = false;

            //Clamp Mana to not go out of range
            currentMana = Mathf.Clamp(currentMana, MinMana, MaxMana);

            //Call event due to Mana Consumed
            EventSystem_onManaChange?.Invoke(this);
            EventSystem_onManaConsumed?.Invoke(this, a_amount);

            //Call UnityEvent
            UnityEvent_onManaChange?.Invoke(this);
            UnityEvent_onManaConsumed?.Invoke(this, a_amount);
        }


        /// <summary>
        /// Use to add Mana (Mana will be clamped)
        /// </summary>
        /// <param name="a_amount"></param>
        public virtual void AddMana(float a_amount)
        {
            if (!CanRegenMana) return;

            currentMana += a_amount;

            if (currentMana >= MaxMana && !m_haveAlreadyReachedMaxMana)
            {
                m_haveAlreadyReachedMaxMana = true;

                EventSystem_onReachedFullMana?.Invoke(this);
                UnityEvent_onReachedFullMana?.Invoke(this);
            }

            if(currentMana > MaxMana) m_haveAlreadyReachedMinMana = false;  

            //Clamp Mana to not go out of range
            currentMana = Mathf.Clamp(currentMana, MinMana, MaxMana);

            //Call event due to Mana added
            EventSystem_onManaChange?.Invoke(this);
            EventSystem_onManaConsumed?.Invoke(this, a_amount);

            //Call UnityEvent
            UnityEvent_onManaChange?.Invoke(this);
            UnityEvent_onManaAdded?.Invoke(this, a_amount);
        }

        protected virtual IEnumerator ManaRegen_Routine()
        {
            while (true)
            {
                yield return new WaitForSeconds(m_regenTickRate);
                if (!IsRegenFrozen)
                {
                    AddMana(m_regenValueByTick);
                }
            }
        }
        #endregion
    }
}


using RakaTools.Lock;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace RakaEngine.Controllers.Stamina
{

    public class StaminaController : MonoBehaviour
    {
        #region Events
        public Action<StaminaController> EventSystem_onStaminaChange;
        public Action<StaminaController, float> EventSystem_onStaminaConsumed;
        public Action<StaminaController, float> EventSystem_onStaminaAdded;
        public Action<StaminaController> EventSystem_onOutStamina;
        public Action<StaminaController> EventSystem_onReachedFullStamina;


        public UnityEvent<StaminaController> UnityEvent_onStaminaChange;
        public UnityEvent<StaminaController, float> UnityEvent_onStaminaConsumed;
        public UnityEvent<StaminaController, float> UnityEvent_onStaminaAdded;
        public UnityEvent<StaminaController> UnityEvent_onOutStamina;
        public UnityEvent<StaminaController> UnityEvent_onReachedFullStamina;

        #endregion

        #region Variables
        /// <summary>
        /// Used to control if stamina should regen
        /// </summary>
        public bool IsRegenFrozen { get; private set; } = false;

        /// <summary>
        ///  Used to control if stamina should be free
        /// </summary>
        public bool FreeUseStamina { get; private set; } = false;

        public float MinStamina { get; private set; } = 0;
        public float MaxStamina { get; private set; } = 100;
        public float currentStamina { get; private set; }


        protected LockerController m_locker_CanRegenStamina = new LockerController();
        protected LockerController m_locker_canConsumeStamina = new LockerController();

        public bool CanRegenStamina => m_locker_CanRegenStamina.IsLock;
        public bool CanConsumeStamina => m_locker_canConsumeStamina.IsLock;

        /// <summary>
        /// Used to avoid spamming event full stamina  if alreadyFull Stamina
        /// </summary>
        protected bool m_haveAlreadyReachedMaxStamina = true;

        /// <summary>
        /// Used to avoid spamming event Min stamina  if alreadyMinStamina
        /// </summary>
        protected bool m_haveAlreadyReachedMinStamina = true;

        /// <summary>
        /// the value to regen each tick
        /// </summary>
        protected float m_regenValueByTick = 5;

        /// <summary>
        /// The time between each Tick
        /// </summary>
        protected float m_regenTickRate = .5f;




        protected Coroutine StaminaRegenRoutine;
        #endregion

        #region Setters
        /// <summary>
        /// Used to control if stamina should regen
        /// </summary>
        /// <param name="a_value"></param>
        public virtual void FreezeStaminaRegen(bool a_value = false) => IsRegenFrozen = a_value;

        /// <summary>
        /// Used to control if stamina should be free
        /// </summary>
        /// <param name="a_value"></param>
        public virtual void SetFreeUseStamina(bool a_value) => FreeUseStamina = a_value;

        #endregion

        #region Methods
        public virtual void Initialize()
        {
            StaminaRegenRoutine = StartCoroutine(StaminaRegen_Routine());
        }

        public void OnDestroy()
        {
            if (StaminaRegenRoutine != null)
            {
                StopCoroutine(StaminaRegenRoutine);
            }
        }


        /// <summary>
        /// Add locker count to can regen stamina
        /// </summary>
        /// <param name="a_value"></param>
        public void SetCanRegenStamina(bool a_value)
        {
            if (!a_value)
                m_locker_CanRegenStamina.AddLocker();
            else
                m_locker_CanRegenStamina.RemoveLocker();
        }

        /// <summary>
        /// Add locker count to consume Stamina
        /// </summary>
        /// <param name="a_value"></param>
        public void SetCanBeConsumed(bool a_value)
        {
            if (!a_value)
                m_locker_canConsumeStamina.AddLocker();
            else
                m_locker_canConsumeStamina.RemoveLocker();
        }
        public bool HaveEnoughtStamina(float a_amount) => currentStamina >= a_amount;


        /// <summary>
        /// Use to Consume an ammount Stamina (Stamina will be clamped)
        /// </summary>
        /// <param name="a_amount"></param>
        public virtual void ConsumeStamina(float a_amount)
        {
            if (FreeUseStamina) return;

            if (!CanConsumeStamina) return;

            currentStamina -= a_amount;

            //if reached minStamina  -> callback ou stamina
            if (currentStamina <= MinStamina && !m_haveAlreadyReachedMinStamina)
            {
                m_haveAlreadyReachedMinStamina = true;

                EventSystem_onOutStamina?.Invoke(this);
                UnityEvent_onOutStamina?.Invoke(this);
            }

            // stamina is not full, set true to call the next full stamina callback
            if (currentStamina < MaxStamina) m_haveAlreadyReachedMaxStamina = false;

            //Clamp stamina to not go out of range
            currentStamina = Mathf.Clamp(currentStamina, MinStamina, MaxStamina);

            //Call event due to Stamina Consumed
            EventSystem_onStaminaChange?.Invoke(this);
            EventSystem_onStaminaConsumed?.Invoke(this, a_amount);

            //Call UnityEvent
            UnityEvent_onStaminaChange?.Invoke(this);
            UnityEvent_onStaminaConsumed?.Invoke(this, a_amount);
        }



        /// <summary>
        /// Use to Add an ammount Stamina (Stamina will be clamped)
        /// </summary>
        /// <param name="a_amount"></param>
        public virtual void AddStamina(float a_amount)
        {
            if (!CanRegenStamina) return;

            currentStamina += a_amount;

            //prent to spamming event full stamina
            if (currentStamina >= MaxStamina && !m_haveAlreadyReachedMaxStamina)
            {
                m_haveAlreadyReachedMaxStamina = true;

                EventSystem_onReachedFullStamina?.Invoke(this);
                UnityEvent_onReachedFullStamina?.Invoke(this);
            }

            //if stamina is superior to min stamina => could call event for outstamina
            if (currentStamina > MinStamina) m_haveAlreadyReachedMinStamina = false;

            //Clamp stamina to not go out of range
            currentStamina = Mathf.Clamp(currentStamina, MinStamina, MaxStamina);

            //Call event due to Stamina added
            EventSystem_onStaminaChange?.Invoke(this);
            EventSystem_onStaminaAdded?.Invoke(this, a_amount);

            //Call UnityEvent
            UnityEvent_onStaminaChange?.Invoke(this);
            UnityEvent_onStaminaAdded?.Invoke(this, a_amount);
        }

        protected virtual IEnumerator StaminaRegen_Routine()
        {
            while (true)
            {
                yield return new WaitForSeconds(m_regenTickRate);
                if (!IsRegenFrozen)
                {
                    AddStamina(m_regenValueByTick);
                }
            }
        }
        #endregion
    }
}

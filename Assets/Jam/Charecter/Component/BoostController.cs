using RakaTools.Lock;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BoostController : MonoBehaviour
{
    #region Events
    public Action<BoostController> EventSystem_onBoostChange;
    public Action<BoostController, float> EventSystem_onBoostConsumed;
    public Action<BoostController, float> EventSystem_onBoostAdded;
    public Action<BoostController> EventSystem_onOutBoost;
    public Action<BoostController> EventSystem_onReachedFullBoost;

    public UnityEvent<BoostController> UnityEvent_onBoostChange;
    public UnityEvent<BoostController, float> UnityEvent_onBoostConsumed;
    public UnityEvent<BoostController, float> UnityEvent_onBoostAdded;
    public UnityEvent<BoostController> UnityEvent_onOutBoost;
    public UnityEvent<BoostController> UnityEvent_onReachedFullBoost;
    #endregion

    #region Variables
    /// <summary>
    /// Used to control if boost should regen
    /// </summary>
    public bool IsRegenFrozen { get; private set; } = false;

    /// <summary>
    ///  Used to control if boost should be free
    /// </summary>
    public bool FreeUseBoost { get; private set; } = false;

    public float MinBoost { get; private set; } = 0;
    public float MaxBoost { get; private set; } = 100;
    public float currentBoost { get; private set; }

    protected LockerController m_locker_CanRegenBoost = new LockerController();
    protected LockerController m_locker_canConsumeBoost = new LockerController();

    public bool CanRegenBoost => m_locker_CanRegenBoost.IsLock;
    public bool CanConsumeBoost => m_locker_canConsumeBoost.IsLock;

    /// <summary>
    /// Used to avoid spamming event full boost  if alreadyFull Boost
    /// </summary>
    protected bool m_haveAlreadyReachedMaxBoost = true;

    /// <summary>
    /// Used to avoid spamming event Min boost  if alreadyMinBoost
    /// </summary>
    protected bool m_haveAlreadyReachedMinBoost = true;

    /// <summary>
    /// the value to regen each tick
    /// </summary>
    protected float m_regenValueByTick = 5;

    /// <summary>
    /// The time between each Tick
    /// </summary>
    protected float m_regenTickRate = .5f;

    protected Coroutine BoostRegenRoutine;
    #endregion

    #region Setters
    /// <summary>
    /// Used to control if boost should regen
    /// </summary>
    /// <param name="a_value"></param>
    public virtual void FreezeBoostRegen(bool a_value = false) => IsRegenFrozen = a_value;

    /// <summary>
    /// Used to control if boost should be free
    /// </summary>
    /// <param name="a_value"></param>
    public virtual void SetFreeUseBoost(bool a_value) => FreeUseBoost = a_value;

    #endregion

    #region Methods
    public virtual void Initialize()
    {
        BoostRegenRoutine = StartCoroutine(BoostRegen_Routine());
    }

    public void OnDestroy()
    {
        if (BoostRegenRoutine != null)
        {
            StopCoroutine(BoostRegenRoutine);
        }
    }

    /// <summary>
    /// Add locker count to can regen boost
    /// </summary>
    /// <param name="a_value"></param>
    public void SetCanRegenBoost(bool a_value)
    {
        if (!a_value)
            m_locker_CanRegenBoost.AddLocker();
        else
            m_locker_CanRegenBoost.RemoveLocker();
    }

    /// <summary>
    /// Add locker count to consume Boost
    /// </summary>
    /// <param name="a_value"></param>
    public void SetCanBeConsumed(bool a_value)
    {
        if (!a_value)
            m_locker_canConsumeBoost.AddLocker();
        else
            m_locker_canConsumeBoost.RemoveLocker();
    }
    public bool HaveEnoughtBoost(float a_amount) => currentBoost >= a_amount;

    /// <summary>
    /// Use to Consume an ammount Boost (Boost will be clamped)
    /// </summary>
    /// <param name="a_amount"></param>
    public virtual void ConsumeBoost(float a_amount)
    {
        if (FreeUseBoost) return;

        if (!CanConsumeBoost) return;

        currentBoost -= a_amount;

        //if reached minBoost  -> callback ou Boost
        if (currentBoost <= MinBoost && !m_haveAlreadyReachedMinBoost)
        {
            m_haveAlreadyReachedMinBoost = true;

            EventSystem_onOutBoost?.Invoke(this);
            UnityEvent_onOutBoost?.Invoke(this);
        }

        // Boost is not full, set true to call the next full Boost callback
        if (currentBoost < MaxBoost) m_haveAlreadyReachedMaxBoost = false;

        //Clamp Boost to not go out of range
        currentBoost = Mathf.Clamp(currentBoost, MinBoost, MaxBoost);

        //Call event due to Boost Consumed
        EventSystem_onBoostChange?.Invoke(this);
        EventSystem_onBoostConsumed?.Invoke(this, a_amount);

        //Call UnityEvent
        UnityEvent_onBoostChange?.Invoke(this);
        UnityEvent_onBoostConsumed?.Invoke(this, a_amount);
    }

    /// <summary>
    /// Use to Add an ammount Boost (Boost will be clamped)
    /// </summary>
    /// <param name="a_amount"></param>
    public virtual void AddBoost(float a_amount)
    {
        if (!CanRegenBoost) return;

        currentBoost += a_amount;

        //prent to spamming event full Boost
        if (currentBoost >= MaxBoost && !m_haveAlreadyReachedMaxBoost)
        {
            m_haveAlreadyReachedMaxBoost = true;

            EventSystem_onReachedFullBoost?.Invoke(this);
            UnityEvent_onReachedFullBoost?.Invoke(this);
        }

        //if Boost is superior to min Boost => could call event for outBoost
        if (currentBoost > MinBoost) m_haveAlreadyReachedMinBoost = false;

        //Clamp Boost to not go out of range
        currentBoost = Mathf.Clamp(currentBoost, MinBoost, MaxBoost);

        //Call event due to Boost added
        EventSystem_onBoostChange?.Invoke(this);
        EventSystem_onBoostAdded?.Invoke(this, a_amount);

        //Call UnityEvent
        UnityEvent_onBoostChange?.Invoke(this);
        UnityEvent_onBoostAdded?.Invoke(this, a_amount);
    }

    protected virtual IEnumerator BoostRegen_Routine()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_regenTickRate);
            if (!IsRegenFrozen)
            {
                AddBoost(m_regenValueByTick);
            }
        }
    }
    #endregion
}

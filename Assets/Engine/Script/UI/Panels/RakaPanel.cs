using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

namespace RakaEngine.UI.Panel
{

    [RequireComponent(typeof(CanvasGroup))]
    public class RakaPanel : MonoBehaviour
    {
        #region Events
        public Action EventSystem_OnHide;
        public Action EventSystem_OnShow;
        public Action EventSystem_OnStartFadeIn;
        public Action EventSystem_OnStartFadeOut;

        public UnityEvent UnityEvent_OnHide;
        public UnityEvent UnityEvent_OnShow;
        public UnityEvent UnityEvent_OnStartFadeIn;
        public UnityEvent UnityEvent_OnStartFadeOut;
        #endregion

        #region Variables
        protected CanvasGroup m_canvasGroup;

        [SerializeField]
        protected Image m_imgPanel;

        private Coroutine m_currentCoutine;

        [SerializeField]
        [Tooltip("In seconde")]
        private float m_fadeInDuration = 1;

        [SerializeField]
        [Tooltip("In seconde")]
        private float m_fadeOutDuration = 1;

        internal E_UI_Visbility visibilityState { get; private set; }

        [field: SerializeField]
        internal E_UI_Visbility InitialState { get; private set; } = E_UI_Visbility.IsHide;


        private bool m_isFading;
        #endregion

        #region Methods
        private void OnValidate()
        {
            if (!m_canvasGroup)
                m_canvasGroup = GetComponent<CanvasGroup>();

            if (InitialState == E_UI_Visbility.IsShow)
                ShowImmediate();
            else
                HideImmediate();
        }


        public void SetActif(bool a_value)
        {
            gameObject.SetActive(a_value);
        }

        /// <summary>
        /// Show panel without delay
        /// </summary>
        internal void ShowImmediate()
        {
            // cannot be show if it's already visible
            if (visibilityState == E_UI_Visbility.IsShow) return;

            if (m_currentCoutine != null)
                StopCoroutine(m_currentCoutine);

            m_isFading = false;
            m_canvasGroup.alpha = 1f;
            m_canvasGroup.interactable = true;
            visibilityState = E_UI_Visbility.IsShow;
        }

        /// <summary>
        /// Hide panel without delay
        /// </summary>
        internal void HideImmediate()
        {
            if (visibilityState == E_UI_Visbility.IsHide) return;

            if (m_currentCoutine != null)
                StopCoroutine(m_currentCoutine);

            m_isFading = false;
            m_canvasGroup.alpha = 0f;
            m_canvasGroup.interactable = false;
            visibilityState = E_UI_Visbility.IsHide;
        }


        /// <summary>
        /// Show panel with default fade duration ( prefab variable)
        /// </summary>
        internal void Show(Action a_callBack = null)
        {
            if (m_isFading || visibilityState == E_UI_Visbility.IsShow) return;


            SetActif(true);
            if (m_currentCoutine != null)
                StopCoroutine(m_currentCoutine);

            m_currentCoutine = StartCoroutine(FadeIn(m_fadeOutDuration, a_callBack));
        }

        /// <summary>
        /// Hide panel with default fade duration ( prefab variable)
        /// </summary>
        internal void Hide(Action a_callBack = null)
        {
            if (m_isFading || visibilityState == E_UI_Visbility.IsHide) return;

            if (m_currentCoutine != null)
                StopCoroutine(m_currentCoutine);

            m_currentCoutine = StartCoroutine(FadeOut(m_fadeOutDuration, a_callBack));
        }

        /// <summary>
        /// Show panel with fade , can add delay before starting
        /// </summary>
        public virtual void Show(float a_duration, float a_delay = 0, Action a_callback = null)
        {
            if (m_isFading || visibilityState == E_UI_Visbility.IsShow) return;

            // actif gameobject
            SetActif(true);
            if (m_currentCoutine != null)
                StopCoroutine(m_currentCoutine);

            m_currentCoutine = StartCoroutine(FadeIn(a_duration, a_callback, a_delay));
        }


        /// <summary>
        /// Hide panel with fade , can add delay before starting
        /// </summary>
        public virtual void Hide(float a_duration, float a_delay = 0, Action a_callback = null)
        {
            if (m_isFading && visibilityState == E_UI_Visbility.IsHide) return;

            if (m_currentCoutine != null)
                StopCoroutine(m_currentCoutine);
            m_currentCoutine = StartCoroutine(FadeOut(a_duration, a_callback, a_delay));
        }

        /// <summary>
        /// Fade In  then wait "a_durationBetweenFade" then fade out (can take callback fadeIN/OUT)
        /// (Fading duration are  m_fadeInDuration and m_fadeOutDuration)
        /// </summary>
        /// <param name="a_durationBetweenFade"></param>
        /// <param name="a_callbackFadeIn"></param>
        /// <param name="a_callbackFadeOut"></param>
        public virtual void FadeInOut(float a_durationBetweenFade, Action a_callbackFadeIn = null, Action a_callbackFadeOut = null)
        {
            if (m_currentCoutine != null)
                StopCoroutine(m_currentCoutine);

            // actif gameobject
            SetActif(true);
            m_currentCoutine = StartCoroutine(FadeIn(m_fadeInDuration, () =>
            {
                a_callbackFadeIn?.Invoke();
                StartCoroutine(FadeOut(m_fadeOutDuration, a_callbackFadeOut, a_durationBetweenFade));
            }));
        }

        /// <summary>
        /// Fade in with a specific duration -> callback to fadeIn, wait a_durationBetweenFade -> fade out with a specific duration -> callback fade out
        /// </summary>
        /// <param name="a_durationFadeIn"></param>
        /// <param name="a_durationFadeOut"></param>
        /// <param name="a_durationBetweenFade"></param>
        /// <param name="a_callbackFadeIn"></param>
        /// <param name="a_callbackFadeOut"></param>
        public virtual void FadeInOut(float a_durationFadeIn, float a_durationFadeOut, float a_durationBetweenFade = .3f, Action a_callbackFadeIn = null, Action a_callbackFadeOut = null)
        {
            if (m_currentCoutine != null)
                StopCoroutine(m_currentCoutine);

            // actif gameobject
            SetActif(true);
            m_currentCoutine = StartCoroutine(FadeIn(a_durationFadeIn, () =>
            {
                a_callbackFadeIn?.Invoke();
                StartCoroutine(FadeOut(a_durationFadeOut, a_callbackFadeOut, a_durationBetweenFade));
            }));
        }



        protected virtual IEnumerator FadeIn(float a_durationToFade, Action a_callback = null, float a_delay = 0)
        {
            EventSystem_OnStartFadeIn?.Invoke();
            UnityEvent_OnStartFadeIn?.Invoke();

            yield return new WaitForSeconds(a_delay);

            float currentTime = Time.time;
            float targetTime = currentTime + a_durationToFade;
            m_isFading = true;
            while (currentTime <= targetTime)
            {
                float alpha = 1 - (targetTime - Time.time) / a_durationToFade;
                m_canvasGroup.alpha = alpha;
                currentTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
            m_isFading = false;
            m_currentCoutine = null;
            visibilityState = E_UI_Visbility.IsShow;
            EventSystem_OnShow?.Invoke();
            UnityEvent_OnShow?.Invoke();
            a_callback?.Invoke();
        }

        protected virtual IEnumerator FadeOut(float a_durationToFade, Action a_callback = null, float a_delay = 0)
        {
            EventSystem_OnStartFadeOut?.Invoke();
            UnityEvent_OnStartFadeOut?.Invoke();

            yield return new WaitForSeconds(a_delay);

            float currentTime = Time.time;
            float targetTime = currentTime + a_durationToFade;
            m_isFading = true;
            while (currentTime <= targetTime)
            {
                yield return new WaitForEndOfFrame();
                float alpha = (targetTime - Time.time) / a_durationToFade;
                m_canvasGroup.alpha = alpha;
                currentTime += Time.deltaTime;
            }
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            m_isFading = false;
            m_currentCoutine = null;
            visibilityState = E_UI_Visbility.IsHide;

            EventSystem_OnHide?.Invoke();
            UnityEvent_OnHide?.Invoke();

            a_callback?.Invoke();
            SetActif(false);
        }
        #endregion
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using GTools.Singletons;

namespace GTools
{
    public class SceneLoader : PersistentSingleton<SceneLoader>
    {
        private enum FadeType
        {
            FadeIn,
            FadeOut
        }

        
        [SerializeField] private float fadeDuration = 1.0f;
        [SerializeField] private CanvasGroup canvasGroup;

        private float alpha = 0f;
        private float timer = 0;
        private int fadeValue = -1;
        private bool fading = false;
        private string sceneName;


        override protected void Awake()
        {
            base.Awake();

            canvasGroup.alpha = 0;
        }

        public void StartSceneLoad(string sceneName)
        {
            this.sceneName = sceneName;
            FadeOut();
            Invoke("LoadScene", 1);
        }

        [ContextMenu("Fade In")]
        private void FadeIn()
        {
            BeginFade(FadeType.FadeIn);
        }

        private void FadeOut()
        {
            BeginFade(FadeType.FadeOut);
        }

        private void BeginFade(FadeType fadeType)
        {
            timer = (fadeType == FadeType.FadeIn) ? fadeDuration : 0;
            fadeValue = (fadeType == FadeType.FadeIn) ? -1 : 1;

            fading = true;

            ToggleUIEvents(fadeType);
        }

        private void ToggleUIEvents(FadeType fadeType)
        {
            if (!EventSystem.current)
                return;

            if (fadeType == FadeType.FadeOut)
                EventSystem.current.sendNavigationEvents = false;
            else
                EventSystem.current.sendNavigationEvents = true;
        }

        private void LoadScene() //called by invoke
        {
            SceneManager.LoadScene(sceneName);
        }

        private void Update()
        {
            if (!fading)
                return;

            timer += Time.unscaledDeltaTime * fadeValue;
            timer = Mathf.Clamp(timer, 0, fadeDuration);

            if (!ReachedTargetAlpha())
            {
                alpha = timer / fadeDuration;
                alpha = Mathf.Clamp01(alpha);

                canvasGroup.alpha = alpha;
            }
            else
            {
                fading = false;
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneWasLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneWasLoaded;
        }

        private void SceneWasLoaded(Scene scene, LoadSceneMode mode)
        {
            FadeIn();
        }

        public Scene CurrentScene()
        {
            return SceneManager.GetActiveScene();
        }

        private bool ReachedTargetAlpha()
        {
            if (fadeValue == -1)
                return alpha <= 0;
            else
                return alpha >= 1;
        }
    }
}

using System.Collections;
using OregoBlink.core;
using UnityEngine;

namespace OregoBlink.presenter
{
    public class OregoPresenter : MonoBehaviour
    {
        /**
         * Application.
         */

        protected OregoApplication application;

        /**
         * Back pressed callback.
         */

        protected OnBackPressed BackPressedPressed;

        #region Init 
        
        protected virtual void Start()
        {
            this.application = this.GetApplication();
        }

        protected virtual OregoApplication GetApplication() => FindObjectOfType<OregoApplication>();
        
        #endregion
        
        #region Visibility

        protected void Show()
        {
            this.gameObject.SetActive(true);
        }
        
        protected void Hide()
        {
            this.gameObject.SetActive(false);
        }

        #endregion
       
        #region Build widget

        /**
         * Loads this window async through resources.
         */

        protected IEnumerator BuildWidget(string path, Transform parent, OnBackPressed pressed = null)
        {
            var resourceRequest = Resources.LoadAsync(path);
            yield return resourceRequest;
            var prefab = resourceRequest.asset;
            var otherObject = Instantiate(prefab, parent) as GameObject;
            if (otherObject != null)
            {
                var rectTransform = otherObject.GetComponent<RectTransform>();
                var widget = otherObject.GetComponent<OregoPresenter>();
                rectTransform.localPosition = new Vector3();
                otherObject.transform.SetParent(parent);
                widget.BackPressedPressed = pressed;
            }
        }

        #endregion
        
        #region BackPressed

        /**
         * Invokes when back button has clicked.
         */

        public virtual void OnBackClicked()
        {
            this.BackPressedPressed?.Invoke();
        }

        /**
         * Back callback delegate.
         */

        protected delegate void OnBackPressed();

        #endregion
    }
}
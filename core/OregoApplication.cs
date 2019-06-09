using System;
using System.Collections.Generic;
using OregoBlink.interactor;
using UnityEngine;

namespace OregoBlink.core
{
    /**
     * Do only this class as root singleton.
     */
    
    public abstract class OregoApplication : MonoBehaviour
    {
        private readonly Dictionary<Type, OregoInteractor> interactors = new Dictionary<Type, OregoInteractor>();

        #region Start

        private void Start()
        {
            this.OnCreateDomain();
            this.CreateInteractors();
        }

        private void CreateInteractors()
        {
            var presenterSet = this.OnCreateInteractors();
            foreach (var presenter in presenterSet)
            {
                var presenterType = presenter.GetType();
                this.interactors.Add(presenterType, presenter);
            }
        }

        protected virtual void OnCreateDomain()
        {
        }

        protected virtual IEnumerable<OregoInteractor> OnCreateInteractors() =>
            new HashSet<OregoInteractor>();

        #endregion

        public T GetInteractor<T>() where T : OregoInteractor
        {
            var presenter = this.interactors[typeof(T)];
            return presenter as T;
        }

        #region Inflate

        protected static T Inflate<T>(string objectName) where T : Component
        {
            //Create new game object:
            var newGameObject = new GameObject(objectName);
            var component = newGameObject.AddComponent<T>();

            //Save game object:
            DontDestroyOnLoad(component);

            //Return component:
            return component;
        }
        
        #endregion
    }
}
using ObjectPooling;
using System;
using UnityEngine;

namespace Gameplay.UI.Windows
{
    public abstract class UIWindowBase : MonoBehaviour, IPoolable
    {
        #region ACTIONS

        public event Action OnCloseWindow;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        public PoolObject Poolable { get; set; }
        public bool Initialized { get; set; }

        #endregion

        #region UNITY_METHODS

        protected virtual void OnEnable()
        {
            if (Initialized == false)
                return;

            AttachEvents();
            Refresh();
        }

        protected virtual void OnDisable()
        {
            if (Initialized == false)
                return;

            DetachEvents();
        }

        #endregion

        #region METHODS

        public virtual void Initialize()
        {
            Initialized = true;
            InitializingAttachEvents();
        }

        public virtual void CleanUp()
        {
            Initialized = false;
            InitializingDetachEvents();
        }

        public void CloseFromUI()
        {
            OnCloseWindow?.Invoke();
        }

        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        /// <summary>
        /// Refreshing window. Called every on enable
        /// </summary>
        protected virtual void Refresh()
        {

        }

        /// <summary>
        /// Attaching events on enable
        /// </summary>
        protected virtual void AttachEvents()
        {

        }

        /// <summary>
        /// Detaching events on enable
        /// </summary>
        protected virtual void DetachEvents()
        {

        }

        /// <summary>
        /// Attaching events in initialize
        /// </summary>
        protected virtual void InitializingAttachEvents()
        {

        }

        /// <summary>
        /// Detaching events from initialize. Calleon in CleanUp
        /// </summary>
        protected virtual void InitializingDetachEvents()
        {

        }

        #endregion
    }
}

using EventSystem;
using Gameplay;
using Gameplay.Managment;
using Gameplay.UI.Windows;
using ObjectPooling;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UI
{
    public class UIManager : GameplayManager<UIManager>
    { 
        #region VARIABLES

        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private string defaultUIPoolCategory;

        private List<UIWindowBase> openedWindows;

        #endregion

        #region PROPERTIES
        public string DefaultUIPoolCategory => defaultUIPoolCategory;
        public bool AnyWindowIsOpened => openedWindows.Count > 0;

        #endregion

        #region METHODS

        public override void Initialzie()
        {
            base.Initialzie();
            openedWindows = new();
        }

        public override void CleanUp()
        {
            CloseAllWindow();
        }

        public T OpenWindow<T>(string poolWindownName) where T : UIWindowBase
        {
            return OpenWindow<T>(defaultUIPoolCategory, poolWindownName);
        }

        public T OpenWindow<T>(string poolCategory, string poolWindowName) where T : UIWindowBase
        {
            PoolObject poolObject = GetPoolWindow(poolCategory, poolWindowName);

            if (poolObject == null)
                return null;

            return OpenWindow<T>(poolObject);
        }

        public T OpenWindow<T>(PoolObject poolObject) where T : UIWindowBase
        {
            T window = poolObject.GetComponent<T>();

            if (window == null)
            {
                Debug.LogError($"[{GetType().Name}] Missing window type from pool {poolObject.Category} - {poolObject.Name} - {typeof(T).Name}");
                return null;
            }

            window.Initialize();
            window.OnCloseWindow += CloseWindow<T>;

            //If is opened but for firstly
            if (openedWindows.Contains(window) && openedWindows.GetLastElement() != window)
            {
                openedWindows.Remove(window);
                openedWindows.SetActiveOptimizeLastElement(false);

                openedWindows.Add(window);
            }
            else
            {
                openedWindows.SetActiveOptimizeLastElement(false);
                openedWindows.Add(window);
                openedWindows.SetActiveOptimizeLastElement(true);

                Events.UI.WindowsEv.OnWindowOpened?.Invoke(window);
            }


            if (window.TryGetComponent<Canvas>(out _) == false)
                window.transform.SetParent(mainCanvas.transform, false);
            else
                window.transform.SetParentWithReset(transform);

            return window;
        }

        public void CloseWindow<T>() where T : UIWindowBase
        {
            Type windowType = typeof(T);
            UIWindowBase window = openedWindows.Find(x => x.GetType() == windowType);

            if (window != null)
            {
                if (openedWindows.GetLastElement() == window)
                {
                    openedWindows.SetActiveOptimizeLastElement(false);
                    openedWindows.Remove(window);
                }
                else
                    openedWindows.Remove(window);

                window.OnCloseWindow -= CloseWindow<T>;

                Events.UI.WindowsEv.OnWindowClosed?.Invoke(window);

                window.CleanUp();
                ObjectPool.Instance.ReturnToPool(window);
            }
        }

        public void TryCloseLastOpenedWindow()
        {
            UIWindowBase window = openedWindows.GetLastElement();
            if (window != null)
                window.CloseFromUI();
        }

        public bool IsOpenened<T>() where T : UIWindowBase
        {
            Type windowType = typeof(T);
            UIWindowBase window = openedWindows.Find(x => x.GetType() == windowType);

            return window != null;
        }

        public void CloseAllWindow()
        {
            for (int i = openedWindows.Count - 1; i >= 0; i--)
            {
                openedWindows[i].CleanUp();
                ObjectPool.Instance.ReturnToPool(openedWindows[i]);
            }

            openedWindows.Clear();
        }

        private PoolObject GetPoolWindow(string poolCategory, string poolWindowName)
        {
            PoolObject poolObject = ObjectPool.Instance.GetFromPool(poolWindowName, poolCategory, false);

            if (poolObject == null)
            {
                Debug.LogError($"[{GetType().Name}] Missing window in pool {poolCategory} - {poolWindowName}");
                return null;
            }

            return poolObject;
        }

        #endregion
    }
}

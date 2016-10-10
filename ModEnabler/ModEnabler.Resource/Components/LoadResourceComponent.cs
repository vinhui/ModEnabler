using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace ModEnabler.Resource.Components
{
    /// <summary>
    /// The base class for a component that needs to load something and then set that on another component
    /// </summary>
    /// <typeparam name="T">Type of the component you want to set something on</typeparam>
    public abstract class LoadResourceComponent<T> : MonoBehaviour
    {
        /// <summary>
        /// Name of the file you should use
        /// </summary>
        [SerializeField]
        [Tooltip("Name of the file you want to load")]
        public string fileName;

        /// <summary>
        /// The component you should set some field of
        /// </summary>
        protected T componentToSet;

        /// <summary>
        /// Is this object initialized?
        /// </summary>
        private bool initialized;

        /// <summary>
        /// The action for setting
        /// </summary>
        private UnityAction<Mod> setAction;

        private void Start()
        {
            // We don't need to initialize it more than once
            if (initialized)
                return;

            initialized = true;
            setAction = (m) => { Set(); };

            componentToSet = GetComponent<T>();

            RuntimeHelpers.RunClassConstructor(typeof(ResourceManager).TypeHandle);

            if (ModsManager.modsLoaded)
                Set();
            else
                ModsManager.onModsLoaded.AddListener(Set);
            ModsManager.onModActivate.AddListener(setAction);
            ModsManager.onModDeactivate.AddListener(setAction);
        }

        private void OnEnable()
        {
            if (!initialized)
                Start();
        }

        private void OnDestroy()
        {
            // Some cleanup here
            if (ModsManager.onModsLoaded != null)
                ModsManager.onModsLoaded.RemoveListener(Set);
            if (ModsManager.onModActivate != null && setAction != null)
                ModsManager.onModActivate.RemoveListener(setAction);
            if (ModsManager.onModDeactivate != null && setAction != null)
                ModsManager.onModDeactivate.RemoveListener(setAction);
        }

        public abstract void Set();
    }
}
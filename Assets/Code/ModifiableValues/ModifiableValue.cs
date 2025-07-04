using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Values
{
    public class ModifiableValue : ValueBase
    {
        #region ACTIONS

        public event Action OnValueChanged;

        #endregion

        #region VARIABLES

        [SerializeField] private float currentRawValue;
        [SerializeField] private float currentValue;
        [SerializeField] private List<ModifiableValue> additionalComponents;
        [SerializeField] private List<Modifier> modifiers;
        [SerializeField] private bool needRefresh;

        #endregion

        #region PROPERTIES

        public float CurrentValue
        {
            get
            {
                if (needRefresh)
                    RecalculateCurrentValue();
                return currentValue;
            }
        }

        public float CurrentRawValue
        {
            get
            {
                if (needRefresh)
                    RecalculateCurrentValue();
                return currentRawValue;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public ModifiableValue() { }
        public ModifiableValue(float startingValue, ValueType valueType = ValueType.OVERALL) : base(startingValue, valueType)
        {
            additionalComponents = new();
            modifiers = new();
            SetRawValue(startingValue);
        }

        #endregion

        #region METHODS

        public override void SetRawValue(float value)
        {
            base.SetRawValue(value);
            ForceRefreshValue();
        }

        public override void AddToRawValue(float value)
        {
            base.AddToRawValue(value);
            if (value != 0)
                ForceRefreshValue();
        }

        public void AddNewComponent(ModifiableValue newComponent)
        {
            additionalComponents.Add(newComponent);
            newComponent.OnValueChanged += HandleComponentValueChanged;

            if (newComponent.CurrentValue != 0)
                ForceRefreshValue();
        }

        public void RemoveComponent(ModifiableValue componentToRemove, bool refreshAfterChanges = true)
        {
            additionalComponents.Remove(componentToRemove);
            componentToRemove.OnValueChanged -= HandleComponentValueChanged;

            if (refreshAfterChanges && componentToRemove.CurrentValue != 0)
                ForceRefreshValue();
        }

        public void AddModifier(Modifier modifier)
        {
            modifiers.Add(modifier);

            if (modifier.BaseValue != 0)
                ForceRefreshValue();
        }

        public void RemoveModifier(Modifier modifier)
        {
            modifiers.Remove(modifier);

            if (modifier.BaseValue != 0)
                ForceRefreshValue();
        }

        public void CleanUp()
        {
            modifiers.Clear();

            for (int i = additionalComponents.Count - 1; i >= 0; i--)
                RemoveComponent(additionalComponents[i]);

            SetRawValue(0);
            ForceRefreshValue();
        }

        public string GetValueToShow()
        {
            string suffix = "";
            switch (ValueType)
            {
                case ValueType.OVERALL:
                    break;
                case ValueType.PERCENTAGE:
                    suffix = "%";
                    break;
                default:
                    break;
            }

            return string.Format("{0}{1}", CurrentRawValue, suffix);
        }

        private void RecalculateCurrentValue()
        {
            float newValue = RawValue;

            newValue = CompleteValue(newValue);
            newValue = GetModifiedValue(newValue);

            currentRawValue = newValue;
            currentValue = ConvertValueToType(currentRawValue);

            needRefresh = false;
        }

        private float CompleteValue(float baseValue)
        {
            float completedValue = baseValue;

            for (int i = 0; i < additionalComponents.Count; i++)
            {
                switch (additionalComponents[i].ValueType)
                {
                    case ValueType.OVERALL:
                        completedValue += additionalComponents[i].CurrentValue;
                        break;
                    case ValueType.PERCENTAGE:
                        completedValue += baseValue * additionalComponents[i].CurrentValue;
                        break;
                    default:
                        break;
                }
            }

            return completedValue;
        }

        private float GetModifiedValue(float valueToModify)
        {
            float modifiedValue = valueToModify;

            for (int i = 0; i < modifiers.Count; i++)
            {
                switch (modifiers[i].ValueType)
                {
                    case ValueType.OVERALL:
                        modifiedValue += modifiers[i].BaseValue;
                        break;
                    case ValueType.PERCENTAGE:
                        modifiedValue += valueToModify * modifiers[i].BaseValue;
                        break;
                    default:
                        break;
                }
            }

            return modifiedValue;
        }

        private void ForceRefreshValue()
        {
            needRefresh = true;
            OnValueChanged?.Invoke();
        }

        #region HANDLERS

        private void HandleComponentValueChanged()
        {
            ForceRefreshValue();
        }

        #endregion

        #endregion
    }
}

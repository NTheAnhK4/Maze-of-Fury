using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Core.Entity
{
    [System.Serializable]
    public class EStateAnim
    {
        private Animator anim;
        [SerializeField] private string stateName;
        private object valueEnter;
        private object valueExit;
        public bool IsTriggerState => valueEnter == null;

        public string StateName => stateName;
        public EStateAnim(Animator anim, string stateName, object valueEnter = null, object valueExit = null)
        {
            this.anim = anim;
            this.stateName = stateName;
            this.valueEnter = valueEnter;
            this.valueExit = valueExit;
        }

        private void SetValue(object value)
        {
            if(value is bool boolValue) anim.SetBool(stateName,boolValue);
            else if(value is int intValue) anim.SetInteger(stateName,intValue);
            else if(value is float floatValue) anim.SetFloat(stateName,floatValue);
            else Debug.LogWarning("Doesn't contain type " + value.ToString());
        }
        /// <summary>
        /// only bool or trigger
        /// </summary>
        public void Enter()
        {
            if (stateName == String.Empty) return;
            if (valueEnter == null)
            {
                anim.SetTrigger(stateName);
                return;
            }
            SetValue(valueEnter);
        }

        public void Exit()
        {
            if (stateName == String.Empty) return;
            if (valueExit == null) return;
            SetValue(valueExit);
        }
        
    }

}

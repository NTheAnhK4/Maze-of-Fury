using System;

using Unity.Mathematics;
using UnityEngine;

namespace Core.Entity
{
    public class EAnim : ChildBehavior
    {
        [SerializeField] protected Transform enity;
        [SerializeField] protected Animator anim;
        [SerializeField] protected Vector3 prePosition;
        [SerializeField] protected Vector3 curPosition;
        [SerializeField] protected EStateAnim curStateAnim;
        [SerializeField] protected EStateAnim preStateAnim;
        protected override void LoadComponentInIt()
        {
            base.LoadComponentInIt();
            if (anim == null) anim = GetComponent<Animator>(); 
        }

        protected override void LoadComponentInParent()
        {
            base.LoadComponentInParent();
            if (enity == null) enity = transform.parent;
        }

        protected virtual void OnEnable()
        {
            prePosition = enity.position;
            curStateAnim = new EStateAnim(anim, String.Empty);
            preStateAnim = curStateAnim;
        }

        protected void RotateAnim()
        {
            curPosition = enity.position;
            if(curPosition.x - prePosition.x < -0.001) transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
            if(curPosition.x - prePosition.x > 0.001) transform.rotation = quaternion.Euler(new Vector3(0,0,0));
            prePosition = curPosition;

        }

        public void ChangeState(string stateName, object valueEnter = null, object valueExit = null)
        {
            if (curStateAnim.StateName == stateName) return;
            EStateAnim newStateAnim = new EStateAnim(anim, stateName, valueEnter, valueExit);
            if(!curStateAnim.IsTriggerState) preStateAnim = curStateAnim;
            curStateAnim?.Exit();
            curStateAnim = newStateAnim;
            curStateAnim?.Enter();
        }

        public void TurnPreState()
        {
            if(curStateAnim.StateName == preStateAnim.StateName) return;
            curStateAnim?.Exit();
            curStateAnim = preStateAnim;
            curStateAnim?.Enter();
        }
        protected virtual void Update()
        {
            RotateAnim();
        }
    }

}


using UnityEngine;

namespace Core.Entity
{
    public class EMove : ChildBehavior
    {
        [SerializeField] protected Transform entity;
        protected IMoveState curMoveState;

        public Transform Entity => entity;
        protected override void ResetValue()
        {
            base.ResetValue();
            if(entity == null) entity = transform.parent;
        }

        public void ChangeState(IMoveState newState)
        {
            curMoveState?.Exit(this);
            curMoveState = newState;
            curMoveState?.Enter(this);
        }

        public void UpdateMovement()
        {
            curMoveState?.Update(this);
        }
        
    }

}

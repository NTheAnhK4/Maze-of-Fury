
using UnityEngine;

namespace Core.Entity
{
   
    public class EGroundMove : EMove
    {
        [SerializeField] protected Rigidbody2D rigid;
        [SerializeField] protected float moveSpeed = 5f;
        [SerializeField] protected float jumpForce = 4f;
        
        public Rigidbody2D Rigid => rigid;

        public float MoveSpeed => moveSpeed;

        public float JumpForce => jumpForce;
        
        protected override void ResetValue()
        {
            base.ResetValue();
            if (rigid == null) rigid = entity.GetComponent<Rigidbody2D>();
        }

       
    }
}


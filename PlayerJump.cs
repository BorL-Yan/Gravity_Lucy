using System;
using UnityEngine;
namespace Player
{
    public class PlayerJump : Charecter
    {
        
        [SerializeField] private float m_gravityJump;
        [SerializeField] private float m_jumpAmount;
        
        private FlipPlayer _flipPlayer;
        
        protected override void Initialization() => base.Initialization();
    

        private void Start()
        {
            _flipPlayer = GetComponent<FlipPlayer>();
        }

        public void Jump()
        {
            if(!charecter.isDoubleJump && !charecter.isDependsWall) return;
            
            
            if (charecter.isDependsWall)
            {
                if(charecter.Flip == true && charecter.NormalSpeed < 0) return;
                if (charecter.Flip == false && charecter.NormalSpeed > 0) return;
                
                _flipPlayer.FlipObject(!charecter.Flip);
            }

            if (charecter.isDoubleJump)
            {
                if (charecter.GravityDirection == GravityDirection.Down) {
                    rb.velocity = new Vector2(rb.velocity.x, m_jumpAmount);
                }else {
                    rb.velocity = new Vector2(rb.velocity.x, -1 * m_jumpAmount);
                }
            }
            DisableTakeWall();
        }

        void DisableTakeWall()
        {
            if (!charecter.isJump)
            {
                charecter.isDoubleJump = false;
            }
            charecter.isDependsWall = false;
            charecter.isJump = false;
            charecter.isGround = false;
        }
    }
}
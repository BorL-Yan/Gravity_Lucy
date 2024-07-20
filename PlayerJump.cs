using System;
using Unity.VisualScripting;
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
                rb.velocity = Vector2.zero;
                switch ((byte)charecter.GravityDirection)
                {
                    case 0:
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -1 * m_jumpAmount);
                        break;
                    }
                    case 1:
                    {
                        rb.velocity = new Vector2(-1 * m_jumpAmount, rb.velocity.y);
                        break;
                    }
                    case 2:
                    {
                        rb.velocity = new Vector2(rb.velocity.x, m_jumpAmount);
                        break;
                    }
                    case 3:
                    {
                        rb.velocity = new Vector2(m_jumpAmount, rb.velocity.y);
                        break;
                    }
                }
                if (charecter.GravityDirection == GravityDirection.Down) {
                    rb.velocity = new Vector2(rb.velocity.x, m_jumpAmount);
                }else {
                }
            }
            DisableTakeWall();
        }

        private void DisableTakeWall()
        {
            if (!charecter.isJump)
            {
                charecter.isDoubleJump = false;
            }
            charecter.isDependsWall = false;
            charecter.isJump = false;
            charecter.isGround = false;
        }

        private void LaserJump(Vector2 direction, bool jump = true)
        {
            if(!jump) return;
            
            rb.velocity = Vector2.zero;
            
            rb.AddForce(direction, ForceMode2D.Impulse);
        }

        
        private void OnEnable()
        {
            EventBus.OnLaserMove += LaserJump;
        }
        
        private void OnDisable()
        {
            EventBus.OnLaserMove -= LaserJump;
        }
    }
}
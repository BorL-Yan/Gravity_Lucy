using System.Collections;
using UnityEngine;
using Player.Data;
using anim = Player.AnimationName;

namespace Player
{
    public class OllColisionDetectit : Charecter
    {

        [SerializeField] private float m_gravityScale = 1;
        
        
        [Header("Ground Colision")] 
        [SerializeField] private Transform m_rayGround;
        [SerializeField] private Vector2 m_groundBox;
        [SerializeField] private float m_timeDetectedGround;

        [Header("Wall Take Colision")] 
        [SerializeField] private Transform m_rayWallTake;
        [SerializeField] private Vector2 m_wallTakeBox;

        [Header("Detected Front")] 
        [SerializeField] private Transform m_rayEsaySlidedFront;
        [SerializeField] private Vector2 m_esaySlidedFrontBox;
        
        [Header("Detected Back")] 
        [SerializeField] private Transform m_rayEsaySlidedBack;
        [SerializeField] private Vector2 m_esaySlidedBackBox;
        
        [SerializeField] private float m_backSlideSpeed;
        
        [SerializeField] private LayerMask m_groundLayer;
        private int _groundLayer;
        
        [SerializeField] private LayerMask m_takeWallLayer;
        private int _takeWallLayer;
        
        
        protected override void Initialization() => base.Initialization();

        private void Start()
        {
            _groundLayer = m_groundLayer;
            _takeWallLayer = m_takeWallLayer;
            
            rb.gravityScale = m_gravityScale;
        }

        public void GroundCollision()
        {
            if(Physics2D.OverlapBox(m_rayGround.position, m_groundBox, 0, _groundLayer)) {
                charecter.isGround = true;
                charecter.isDoubleJump = true;
                charecter.isDashing = false;
                charecter.isJump = true;
                DisableTakeWall();
            }
            else
            {
                charecter.isGround = false;
                if (!charecter.isDependsWall)
                {
                    StartCoroutine(GroundOffset());
                }
            }
        }

        IEnumerator GroundOffset()
        {
            yield return new WaitForSeconds(m_timeDetectedGround);
            charecter.isJump = false;
        }
        
        public void TakeToWallCollision()
        {
            
            if(!WhaterToContinue()) return;
            
            
            if(Physics2D.OverlapBox(m_rayWallTake.position, m_wallTakeBox, 0, _takeWallLayer)) {
                EnableTakeWall();
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                
            }
            else {
                DisableTakeWall();
            }
        }

        private bool WhaterToContinue()
        {
            switch ((byte)charecter.GravityDirection)
            {
                case 0:
                {
                    if (charecter.isGround || rb.velocity.y < 0) {
                        charecter.isDependsWall = false;
                        rb.gravityScale = m_gravityScale;
                        return false;
                    }
                    break;
                }
                case 1:
                {
                    if (charecter.isGround || rb.velocity.x > 0) {
                        charecter.isDependsWall = false;
                        rb.gravityScale = m_gravityScale;
                        return false;
                    }
                    break;
                }
                case 2:
                {
                    if (charecter.isGround || rb.velocity.y > 0) {
                        charecter.isDependsWall = false;
                        rb.gravityScale = m_gravityScale;
                        return false;
                    }
                    break;
                }
                case 3:
                {
                    if (charecter.isGround || rb.velocity.x < 0) {
                        charecter.isDependsWall = false;
                        rb.gravityScale = m_gravityScale;
                        return false;
                    }
                    break;
                }
            }
            // Dose not MATTER
            return true;
        }
        
        public void CharacterBackDetectid()
        {
            if (Physics2D.OverlapBox(m_rayEsaySlidedBack.position, m_esaySlidedBackBox, 0, _groundLayer))
            {
                int direction = (charecter.Flip) ? -1 : 1;
                if (charecter.GravityDirection == GravityDirection.Down) {
                    rb.velocity = new Vector2(m_backSlideSpeed * direction, rb.velocity.y);
                }
                else {
                    rb.velocity = new Vector2(m_backSlideSpeed * -1 * direction, rb.velocity.y);
                }
            }
        }

        public void CharacterFrontDetectid()
        {
            if(charecter.isGround) return;
            
            if (Physics2D.OverlapBox(m_rayEsaySlidedFront.position, m_esaySlidedFrontBox, 0, _groundLayer))
            {
                if (charecter.NormalSpeed < 0 && charecter.Flip) {
                    charecter.isMove = false;
                }else if (charecter.NormalSpeed > 0 && !charecter.Flip){
                    charecter.isMove = false;
                }else {
                    charecter.isMove = true;
                }
            } else {
                charecter.isMove = true;
            }
        }



        private void EnableTakeWall()
        {
            charecter.isDependsWall = true;
            charecter.isMove = false;
            charecter.isGround = false;
            charecter.isJump = true;
            charecter.isDoubleJump = true;
            charecter.isDashing = false;
            charecter.CurrentAnimationState = anim.PLAYER_DEPENDS_WALL;
        }

        private void DisableTakeWall()
        {
            charecter.isDependsWall = false;
            charecter.isMove = true;
            charecter.isTakeWall = false;
        }
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(m_rayGround.position, m_groundBox);
        
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(m_rayWallTake.position, m_wallTakeBox);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(m_rayEsaySlidedFront.position, m_esaySlidedFrontBox);
            
            Gizmos.color = Color.green;
            Gizmos.DrawCube(m_rayEsaySlidedBack.position, m_esaySlidedBackBox);
        }
        
        
    }
}
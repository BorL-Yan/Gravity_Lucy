using UnityEngine;
using anim = Player.AnimationName;

namespace Player
{
    [RequireComponent(typeof(FlipPlayer))]
    public class PlayerMove : Charecter
    {
        [SerializeField] private float m_speed;
        
        private FlipPlayer _flip;
        
        private void Start()
        {
            _flip = GetComponent<FlipPlayer>();
        }

        protected override void Initialization() => base.Initialization();
        
        
        private void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            if (charecter.isDashing || charecter.isDependsWall || !charecter.isMove) {
                return;
            }
            
            if (charecter.NormalSpeed > 0) {
                _flip.FlipObject(false);
            }
            else if (charecter.NormalSpeed < 0) {
                _flip.FlipObject(true);
            }

            Vector3 playerPos = this.transform.position;
            if (charecter.GravityDirection == GravityDirection.Up || charecter.GravityDirection == GravityDirection.Down)
            {
                playerPos += new Vector3( charecter.NormalSpeed * m_speed * Time.fixedDeltaTime, 0);
            }
            else
            {
                playerPos += new Vector3( 0,charecter.NormalSpeed * m_speed * Time.fixedDeltaTime, 0);
            } 
            this.transform.position = playerPos;
            
            if (charecter.isGround) {
                if (Mathf.Abs(charecter.NormalSpeed) >= 0.15f) {
                    charecter.CurrentAnimationState = anim.PLAYER_RUN;
                }
                else {
                    charecter.CurrentAnimationState = anim.PLAYER_IDLE;
                }
            }
            
        }
    }
}
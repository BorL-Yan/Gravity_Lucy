using System;
using UnityEngine;
using anim = Player.AnimationName;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Charecter : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected Animator animator;
        protected Charecter charecter;

        #region Data
        
        public GravityDirection GravityDirection { get; set; }
        public float NormalSpeed { get; set; }
        
        public bool isMove { get; set; }
        public bool isGround { get; set; }
        public bool isJump { get; set; }
        public bool isDoubleJump { get; set; }
        public bool DoubleJumpAnim { get; set; }
        
        public bool isTakeWall { get; set; }
        public bool isDashing { get; set; }
        
        public bool isDependsWall { get; set; }
        
        public string CurrentAnimationState { get; set; }

        public bool Flip { get; set; }

        #endregion
        
        private string _currentState;
        protected void Awake()
        {
            Initialization();
        }

        protected virtual void Initialization()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            charecter = GetComponent<Charecter>();
        }

       // protected virtual bool CollisionDetected(Vector2 position, Vector2 Box, int layerMask)
        //{
       //     return Physics2D.OverlapBox(position, Box, 0, layerMask);
        //}
        
        public void SetAnimation(string newState)
        {
            
            if ((charecter._currentState== anim.PLAYER_FLYING_DOWN || charecter._currentState == anim.PLAYER_FLYING_DOWN_IDLE) && newState == anim.PLAYER_IDLE) {
                newState = anim.PLAYER_FLYING_DOWN_IDLE;
            }

            if ((charecter._currentState == anim.PLAYER_RUN || charecter._currentState == anim.PLAYER_RUN_IDLE) && newState == anim.PLAYER_IDLE)
            {
                newState = anim.PLAYER_RUN_IDLE;
            }

            if ((charecter._currentState == anim.PLAYER_TAKE_WALL) && newState == anim.PLAYER_IDLE)
            {
                newState = anim.PLAYER_FLYING_DOWN_IDLE;
            }

            if (charecter._currentState == anim.PLAYER_SHIFT && newState == anim.PLAYER_DEPENDS_WALL)
            {
                newState = anim.PLAYER_DASH_DEPENDS_WALL;
            }
            
            if (newState == anim.PLAYER_DEPENDS_WALL)
            {
                if ((charecter.NormalSpeed > 0 && Flip) || (charecter.NormalSpeed < 0 && !Flip))
                {
                    newState = anim.PLAYER_DEPENDS_LOOK;
                }
            }
            
            if (charecter._currentState == newState)
                return;
            
            charecter._currentState = newState;
            
            
            animator.Play(newState);
        }

        public void AnimationFlying()
        {
            if(charecter.isDashing || charecter.isGround || charecter.isDependsWall) return;

            switch ((byte)GravityDirection) {
                case 0: {
                    if (rb.velocity.y > 0.1) {
                        if (charecter.DoubleJumpAnim) {
                            //Debug.Log("Double Jump Animation");
                        }
                        CurrentAnimationState = anim.PLAYER_FLYING_UP;
                    }
                    else if (rb.velocity.y < -0.1) 
                    {
                        if (charecter.DoubleJumpAnim) {
                            charecter.DoubleJumpAnim = false;
                        }
                        CurrentAnimationState = anim.PLAYER_FLYING_DOWN; ;
                    }
                    break;
                }
                case 1: {
                    if (rb.velocity.x < -0.1) {
                        if (charecter.DoubleJumpAnim) {
                            //Debug.Log("Double Jump Animation");
                        }
                        CurrentAnimationState = anim.PLAYER_FLYING_UP;
                    }
                    else if (rb.velocity.x > 0.1)
                    {
                        if (charecter.DoubleJumpAnim) {
                            charecter.DoubleJumpAnim = false;
                        }
                        CurrentAnimationState = anim.PLAYER_FLYING_DOWN; ;
                    }
                    break;
                }
                case 2: {
                    if (rb.velocity.y < -0.1) {
                        if (charecter.DoubleJumpAnim) {
                            //Debug.Log("Double Jump Animation");
                        }
                        CurrentAnimationState = anim.PLAYER_FLYING_UP;
                    }
                    else if (rb.velocity.y > 0.1)
                    {
                        if (charecter.DoubleJumpAnim) {
                            charecter.DoubleJumpAnim = false;
                        }
                        CurrentAnimationState = anim.PLAYER_FLYING_DOWN; ;
                    }
                    break;
                }
                case 3: {
                    if (rb.velocity.x > 0.1) {
                        if (charecter.DoubleJumpAnim) {
                            //Debug.Log("Double Jump Animation");
                        }
                        CurrentAnimationState = anim.PLAYER_FLYING_UP;
                    }
                    else if (rb.velocity.x < -0.1) 
                    {
                        if (charecter.DoubleJumpAnim) {
                            charecter.DoubleJumpAnim = false;
                        }
                        CurrentAnimationState = anim.PLAYER_FLYING_DOWN; ;
                    }
                    break;
                }
            }
        }

        private void GravityDirectionChange(byte direction)
        {
            switch (direction) {
                case 0: {
                    GravityDirection = GravityDirection.Up;
                    break;
                }
                case 1: {
                    GravityDirection = GravityDirection.Right;
                    break;
                }
                case 2: {
                    GravityDirection = GravityDirection.Down;
                    break;
                }
                case 3: {
                    GravityDirection = GravityDirection.Left;
                    break;
                }
            }
        }
            
        private void OnEnable() {
            EventBus.GravityDirection += GravityDirectionChange;
        }

        private void OnDisable() {
            EventBus.GravityDirection -= GravityDirectionChange;
        }
    }
}
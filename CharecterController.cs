using UnityEngine;
using Player.Data;

namespace Player
{
    public class CharecterController : Charecter
    {
        private PlayerData _playerData;
        private OllColisionDetectit _playerColisionDetectit;
        
        protected override void Initialization() => base.Initialization();

        private void Start()
        {
            _playerColisionDetectit = GetComponent<OllColisionDetectit>();
        }

        private void FixedUpdate()
        {
            _playerColisionDetectit.GroundCollision();
            _playerColisionDetectit.TakeToWallCollision();
            _playerColisionDetectit.CharacterBackDetectid();
            _playerColisionDetectit.CharacterFrontDetectid();
            VelocityDownDetectid();
            
            charecter.AnimationFlying();
            charecter.SetAnimation(charecter.CurrentAnimationState);
        }

        private void VelocityDownDetectid()
        {
            switch ((byte)charecter.GravityDirection)
            {
                case 0:
                {
                    if (rb.velocity.y > 30)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 30f);
                    }
                    break;
                }
                case 1:
                {
                    if (rb.velocity.x > 30)
                    {
                        rb.velocity = new Vector2(30f, rb.velocity.x);
                    }
                    break;
                }
                case 2:
                {
                    if (rb.velocity.y < -30)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -30f);
                    }
                    break;
                }
                case 3:
                {
                    if (rb.velocity.x < -30)
                    {
                        rb.velocity = new Vector2(-30f, rb.velocity.x);
                    }
                    break;
                }
            }
        }
        
        
    }
}
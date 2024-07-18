using UnityEngine;
using System.Collections;

namespace Player
{
    public class PlayerDash : Charecter
    {
        [SerializeField] private float m_timeDash = 1;
        
        [SerializeField] private float m_dashDistance;
        [SerializeField] private float _transform;


        private bool _dash = true;
        protected override void Initialization() => base.Initialization();

        public void StartDash()
        {
            StartCoroutine(Dashing());
        }
        
        IEnumerator Dashing()
        {
            if (charecter.isGround || !_dash) yield break;
        
            float currnetTime = 0;
            int rotate = 1;
            
            if (charecter.Flip) {
                rotate = -1;
            }else {
                rotate = 1;
            }

            charecter.CurrentAnimationState = "Player_Shift";
            charecter.isDashing = true;
            StartCoroutine(TrueDash());
            while (currnetTime <= m_dashDistance && charecter.isDashing && _dash)
            {
                currnetTime += _transform;
            
                Vector3 movement = transform.position;
                if ((byte)charecter.GravityDirection % 2 == 0) {
                    movement.x += _transform * rotate;
                }else {
                    movement.y += _transform * rotate;
                }
                transform.position = movement;
                
                yield return new WaitForFixedUpdate();
            }

            _dash = false;
            charecter.isDashing = false;
        }

        IEnumerator TrueDash()
        {
            yield return new WaitForSeconds(m_timeDash);
            _dash = true;
        }
    }
}
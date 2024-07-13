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
            
            charecter.AnimationFlying();
            charecter.SetAnimation(charecter.CurrentAnimationState);
        }
    }
}
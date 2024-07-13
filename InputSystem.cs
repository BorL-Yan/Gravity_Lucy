using System;
using UnityEngine;
using Player;
public class InputSystem : Charecter
{
    public bool isMobile;
    
    private PlayerJump _playerJump;
    private PlayerDash _playerDash;

    private void Start()
    {
        _playerJump = GetComponent<PlayerJump>();
        _playerDash = GetComponent<PlayerDash>();
    }

    protected override void Initialization() => base.Initialization();
    
    

    void Update()
    {
        if(isMobile) return;
        
        if (Input.GetKey(KeyCode.A))
        {
            charecter.NormalSpeed = -1;
        }
        else if (Input.GetKey(KeyCode.D)){ 
            charecter.NormalSpeed = 1;
        }
        else
        {
            charecter.NormalSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerJump.Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(charecter.isGround || charecter.isDashing) {return;}
            
            _playerDash.StartDash();
        }
    }

    public void Move(bool Direction_left)
    {
        if (Direction_left) {
            charecter.NormalSpeed = -1;
        }
        else {
            charecter.NormalSpeed = 1;
        }
    }

    public void MoveUp() => charecter.NormalSpeed = 0;

    public void Jump()
    {
            _playerJump.Jump();
    }

    public void Dash()
    {
        if (charecter.isGround || charecter.isDashing)
        {
            return;
        }
        
        _playerDash.StartDash();
    }


}

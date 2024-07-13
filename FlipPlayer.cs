using System;
using UnityEngine;
using Player;

public class FlipPlayer : Charecter
{

    [SerializeField] private GravityDirection _gravityDirection;

    private bool curentRotate;
    
    protected override void Initialization()
    {
        base.Initialization();
        charecter.GravityDirection = _gravityDirection;
    }

    public void FlipObject(bool Y)
    {
        if(curentRotate == Y)return;
        
        if (Y) {
            if (charecter.GravityDirection == GravityDirection.Down) {
                //Debug.Log("Left");
                transform.rotation = Quaternion.Euler(0,180, 0);
            }
            else {
                transform.rotation = Quaternion.Euler(180,180, 0);
            }

            charecter.Flip = true;
        }
        else {
            if (charecter.GravityDirection == GravityDirection.Down){
                //Debug.Log("Right");
                transform.rotation =  Quaternion.Euler(0,0, 0);
            }
            else{
                transform.rotation =  Quaternion.Euler(180,0, 0);
            }
            charecter.Flip = false;
        }

        curentRotate = Y;
    }
}

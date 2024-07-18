using System;
using UnityEngine;
using Player;
using Unity.VisualScripting;

public class FlipPlayer : Charecter
{

    [SerializeField] private GravityDirection _gravityDirection;
    
    protected override void Initialization()
    {
        base.Initialization();
        charecter.GravityDirection = _gravityDirection;
    }

    public void FlipObject(bool rotatin)  // true <-  // false ->
    {
        if (rotatin) {
            switch ((byte)charecter.GravityDirection)
            {
                case 0:
                {
                    transform.rotation = Quaternion.Euler(180,180, 0);
                    break;
                }
                case 1:
                {
                    transform.rotation = Quaternion.Euler(180,0, 90);
                    break;
                }
                case 2:
                {
                    transform.rotation = Quaternion.Euler(0,180, 0);
                    break;
                }
                case 3:
                {
                    transform.rotation = Quaternion.Euler(180, 0, -90);
                    break;
                }
            }
            /*if (charecter.GravityDirection == 0) {
                //Debug.Log("Left");
                transform.rotation = Quaternion.Euler(0,180, 0);
            }
            else {
                transform.rotation = Quaternion.Euler(180,180, 0);
            }*/

            charecter.Flip = true;
        }
        else {
            switch ((byte)charecter.GravityDirection)
            {
                case 0:
                {
                    transform.rotation = Quaternion.Euler(180,0, 0);
                    break;
                }
                case 1:
                {
                    transform.rotation = Quaternion.Euler(0,0, 90);
                    break;
                }
                case 2:
                {
                    transform.rotation = Quaternion.Euler(0,0, 0);
                    break;
                }
                case 3:
                {
                    transform.rotation = Quaternion.Euler(0,0, -90);
                    break;
                }
            }
            /*if (charecter.GravityDirection == GravityDirection.Down){
                //Debug.Log("Right");
                transform.rotation =  Quaternion.Euler(0,0, 0);
            }
            else{
                transform.rotation =  Quaternion.Euler(180,0, 0);
            }*/
            charecter.Flip = false;
        }
    }

    private void Orintation(byte direction)
    {
        switch (direction)
        {
            case 0:
            {
                transform.rotation =  Quaternion.Euler(0,0, 180);
                break;
            }
            case 1:
            {
                transform.rotation =  Quaternion.Euler(0,0, 90);
                break;
            }
            case 2:
            {
                transform.rotation =  Quaternion.Euler(0,0, 0);
                break;
            }
            case 3:
            {
                transform.rotation =  Quaternion.Euler(0,0, -90);
                break;
            }
        }
    }

    private void OnEnable()
    {
        EventBus.GravityDirection += Orintation;
    }

    private void OnDisable()
    {
        EventBus.GravityDirection -= Orintation;
    }
}

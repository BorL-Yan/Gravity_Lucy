using Cinemachine;
using UnityEngine;
using Player;
using UnityEngine.UI;
using UltimateJoystickExample;

[RequireComponent(typeof(LookUporDown))]
public class InputSystem : Charecter
{
    public bool isMobile;
    
    private PlayerJump _playerJump;
    private PlayerDash _playerDash;
    private LookUporDown _lookUporDown;

    [SerializeField] private Button m_jump;
    [SerializeField] private Button m_dash;
    [SerializeField] private Button m_moveLeft;
    [SerializeField] private Button m_moveRight;
    
    [Range(0,1)]
    [SerializeField] private float m_josticInputDistance;
    


    
    [SerializeField] private UltimateJoystick _joystick;


    
    private void Start()
    {
        _playerJump = GetComponent<PlayerJump>();
        _playerDash = GetComponent<PlayerDash>();
        _lookUporDown = GetComponent<LookUporDown>();
        DisabledMobileButton(isMobile);
    }
    
    protected override void Initialization() => base.Initialization();
    
    private void DisabledMobileButton(bool isMobile)
    {
        m_moveLeft.gameObject.SetActive(isMobile);
        m_moveRight.gameObject.SetActive(isMobile);
        m_dash.gameObject.SetActive(isMobile);
        m_jump.gameObject.SetActive(isMobile);
        _joystick.gameObject.SetActive(isMobile);
    }
    

    private void Update()
    {
        #region Mobile

        if (isMobile) {
            Vector3 moveJosticPostion = new Vector3(_joystick.GetHorizontalAxis(), _joystick.GetVerticalAxis());
            
            if ((byte)charecter.GravityDirection % 2 == 0) {
                // Up or Down
                if (moveJosticPostion.x > m_josticInputDistance) {
                    charecter.NormalSpeed = 1;    
                }
                else if (moveJosticPostion.x < -m_josticInputDistance) {
                    charecter.NormalSpeed = -1;
                }
                else {
                    charecter.NormalSpeed = 0;
                }
            }else {
                //Left or Right
                if (moveJosticPostion.y > m_josticInputDistance) {
                    charecter.NormalSpeed = 1;    
                }
                else if (moveJosticPostion.y < -m_josticInputDistance) {
                    charecter.NormalSpeed = -1;
                }
                else {
                    charecter.NormalSpeed = 0;
                }
            }

            if (charecter.isGround) {
                _lookUporDown.CameraLook(moveJosticPostion,  ref charecter);
            }
            
            return;
        }

        #endregion
        
        #region PC
        
        if (Input.GetKey(KeyCode.A)) {
            charecter.NormalSpeed = -1;
        }
        else if (Input.GetKey(KeyCode.D)){ 
            charecter.NormalSpeed = 1;
        }else {
            charecter.NormalSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            _playerJump.Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if(charecter.isGround || charecter.isDashing) {return;}
            
            _playerDash.StartDash();
        }
        
        if(Input.GetKeyDown(KeyCode.W))_lookUporDown.LookUp(true);
        else if(Input.GetKeyUp(KeyCode.W))_lookUporDown.LookUp(false);
        else if(Input.GetKeyDown(KeyCode.S))_lookUporDown.LookDown(true);
        else if(Input.GetKeyUp(KeyCode.S))_lookUporDown.LookDown(false);

        #endregion
    }
    
    #region Mobile Inpute

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
    

    #endregion
}


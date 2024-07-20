using System.Collections;
using Cinemachine;
using UnityEngine;

public class LookUporDown : MonoBehaviour
{

    [SerializeField] private Vector3 m_lookUpDistance;
    [SerializeField] private Vector3 m_lookDownDistance;

    [SerializeField] private float m_time = 1;
    
    [Range(0,1)]
    [SerializeField] private float m_josticLook;
    private bool isPointerDown;
    
    [SerializeField] private Vector2 m_deadZoneUD;
    [SerializeField] private Vector2 m_deadZoneLR;
    
    [SerializeField] private CinemachineVirtualCamera _cinemachine;
    private CinemachineFramingTransposer _framingTransposer;
    
    private void Awake()
    {
        _framingTransposer = _cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    
    public void LookUp(bool value)
    {
        StopAllCoroutines();
        StartCoroutine(ResizeCoroutine(value, m_lookUpDistance));
    } 
    public void LookDown(bool value)
    {
        StopAllCoroutines();
        StartCoroutine(ResizeCoroutine(value, m_lookDownDistance));
    }
    private IEnumerator ResizeCoroutine (bool value, Vector3 distance)
    {
        Vector3 Base =_framingTransposer.m_TrackedObjectOffset;
        Vector3 target = Vector3.zero;
        float time = m_time / 2;
        if (value)
        {
            target = distance;
            time = m_time;
        }
        float Timer = 0;
        
        while (Timer < time) {
            _framingTransposer.m_TrackedObjectOffset = Vector3.Lerp(Base, target, Timer/time);
            yield return null; 
            Timer += Time.deltaTime;
        }

        _framingTransposer.m_TrackedObjectOffset = target;
    }
    
    public void CameraLook(Vector3 direction,  ref Player.Charecter charecter)
    {
        switch ((byte)charecter.GravityDirection)
        {
            case 0:
            {
                if (direction.y > m_josticLook && direction.y > 0 && !isPointerDown) {
                    isPointerDown = true;
                    LookDown(true);
                }
                else if(direction.y < m_josticLook && direction.y > 0 && isPointerDown) {
                    isPointerDown = false;
                    LookDown(false);
                }
                else if (direction.y < -m_josticLook && direction.y < 0 && !isPointerDown) {
                    LookUp(true);
                    isPointerDown = true;
                }
                else if(direction.y > -m_josticLook && direction.y < 0 && isPointerDown) {
                    LookUp(false);
                    isPointerDown = false;
                }
                break;
            }
            case 1:
            {
                if (direction.x > m_josticLook && direction.x > 0 && !isPointerDown)
                {
                    LookDown(true);
                    isPointerDown = true; 
                }
                else if(direction.x < m_josticLook &&  direction.x > 0 && isPointerDown)
                {
                    LookDown(false);
                    isPointerDown = false;
                }
                if (direction.x < -m_josticLook && direction.x < 0 && !isPointerDown)
                {
                    LookUp(true);
                    isPointerDown = true;
                }
                else if(direction.x > -m_josticLook && direction.x < 0 && isPointerDown)
                {
                    LookUp(false);
                    isPointerDown = false;
                }
                break;
            }
            case 2:
            {
                if (direction.y > m_josticLook && direction.y > 0 && !isPointerDown) {
                    isPointerDown = true;
                    LookUp(true);
                }
                else if(direction.y < m_josticLook && direction.y > 0 && isPointerDown) {
                    isPointerDown = false;
                    LookUp(false);
                }
                else if (direction.y < -m_josticLook && direction.y < 0 && !isPointerDown) {
                    LookDown(true);
                    isPointerDown = true;
                }
                else if(direction.y > -m_josticLook && direction.y < 0 && isPointerDown) {
                    LookDown(false);
                    isPointerDown = false;
                }

                break;
            }
            case 3:
            {
                if (direction.x > m_josticLook && direction.x > 0 && !isPointerDown)
                {
                    LookUp(true);
                    isPointerDown = true; 
                }
                else if(direction.x < m_josticLook &&  direction.x > 0 && isPointerDown)
                {
                    LookUp(false);
                    isPointerDown = false;
                }
                if (direction.x < -m_josticLook && direction.x < 0 && !isPointerDown)
                {
                    LookDown(true);
                    isPointerDown = true;
                }
                else if(direction.x > -m_josticLook && direction.x < 0 && isPointerDown)
                {
                    LookDown(false);
                    isPointerDown = false;
                }
                break;
            }
        }
    }
     
    
    
    private void SetGravityDirection(byte direction)
    {
        if (direction % 2 == 0) {
            _framingTransposer.m_DeadZoneWidth = m_deadZoneUD.x;
            _framingTransposer.m_DeadZoneHeight = m_deadZoneUD.y;
        }
        else {
            _framingTransposer.m_DeadZoneWidth = m_deadZoneLR.x;
            _framingTransposer.m_DeadZoneHeight = m_deadZoneLR.y;
        }
    }

    private void OnEnable()
    {
        EventBus.GravityDirection += SetGravityDirection;
    }

    private void OnDisable()
    {
        EventBus.GravityDirection -= SetGravityDirection;
    }
}

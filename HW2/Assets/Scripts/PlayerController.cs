using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private UIManager _uiManager;
    
    //move
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _jumpForce = 60f;
    private float _moveHorizontal;
    private bool _jump;
    private bool _isJumping = false;
    
    //charater
    private float _blood = 100f;
    private bool _isShadowPlayer;
    [SerializeField] private bool _isShadow = true; // is Shadow or Light
    [SerializeField] private bool _isGun = false; // use gun or blade


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isShadowPlayer = _uiManager._isShadowPlayer;
    }
    private void Update()
    {
        //dead
        if(_blood <= 0f)
        {
            //TODO: effect
            
            gameObject.SetActive(false);
            //TODO: LOSE

        }
        // use arrow to move
        if(!_uiManager._useJoystick && _uiManager._useArrow) 
        {
            _moveHorizontal = Input.GetAxis("HorizontalArrow");
            _jump = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space);
        }
        // use WASD to move
        if(!_uiManager._useJoystick && !_uiManager._useArrow) 
        {
            _moveHorizontal = Input.GetAxis("HorizontalWASD");
            _jump = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space);
        }
        // use joystick to move
        if(_uiManager._useJoystick) 
        {
            //TODO: use joystick to move
        }

        // ShadowPlayer / WeaponPlayer Switch Element / Weapon
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(_isShadowPlayer)
            {
                SwitchElement();
            }
            else
            {
                SwitchWeapon();
            }
        }
        // WeaponPlayer Attack
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(!_isShadowPlayer)
            {
                if(_isGun)
                {
                    //TODO: use gun to attack
                }
                else
                {
                    //TODO: use blade to attack
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if(_moveHorizontal != 0 || _jump) Move();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            _isJumping = false;
        }

        // eat item
        if(other.gameObject.tag == "CommonItem")
        {
            //TODO: hiden
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "ShadowItem")
        {
            if(_isShadowPlayer)
            {
                //TODO: can see all of element ground
            }
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "WeaponItem")
        {
            if(!_isShadowPlayer)
            {
                //TODO: Buff
            }
            Destroy(other.gameObject);
        }

        //get hurt
        if(other.gameObject.tag == "Enemy")
        {
            float damage = 25f;
            _blood = Mathf.Clamp(_blood - damage, 0f, 100f); 
            //TODO: blood bar fill change

            //TODO: injure effect
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            _isJumping = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // go to the next level
        if(other.gameObject.tag == "Finish")
        {
            bool arrowIn = _uiManager._useArrow && Input.GetKeyDown(KeyCode.UpArrow);
            bool WASDIn = !_uiManager._useArrow && Input.GetKeyDown(KeyCode.W);
            if(arrowIn || WASDIn)
            {
                string currentScene = SceneManager.GetActiveScene().name;
                if(name == "level_1")
                {
                    if(_isShadowPlayer)
                        _uiManager.LoadScene(2);
                    else
                        _uiManager.LoadScene(3);
                }
                if(name == "level_2_1")
                {
                    // Win
                }
                if(name == "level_2_2")
                {
                    // Win
                }
            }
        }
    }
    private void Move()
    {
        //TODO: animation
        if(Mathf.Abs(_moveHorizontal) > 0.1f)
        {
            _rb.AddForce(new Vector2(_moveHorizontal * _moveSpeed, 0f), ForceMode2D.Impulse);
        }
        if(!_isJumping && _jump)
        {
            _rb.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
        }
    }
    private void SwitchElement()
    {
        if(_isShadow)
        {
            _isShadow = false;
            //TODO: effect
        }
        else
        {
            _isShadow = true;
            //TODO: effect
        }
    }
    private void SwitchWeapon()
    {
        if(_isGun)
        {
            _isGun = false;
            //TODO: animation
        }
        else
        {
            _isGun = true;
            //TODO: animation
        }
    }
}

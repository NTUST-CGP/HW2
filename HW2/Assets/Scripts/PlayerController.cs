using System.Collections.Generic;
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
    public PlayerData _playerData;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerData.isShadowPlayer = _uiManager._isShadowPlayer;
    }
    private void Update()
    {
        //dead
        if(_playerData.blood <= 0f)
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
            if(_playerData.isShadowPlayer)
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
            if(!_playerData.isShadowPlayer)
            {
                if(_playerData.isGun)
                {
                    //TODO: use gun to attack
                }
                else
                {
                    //TODO: use blade to attack
                }
            }
        }
        //TEST: save data
        if(Input.GetKeyDown(KeyCode.K))
            _uiManager.SaveData();
        if(Input.GetKeyDown(KeyCode.X))
            _uiManager.LoadData();
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
            if(_playerData.isShadowPlayer)
            {
                //TODO: can see all of element ground
            }
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "WeaponItem")
        {
            if(!_playerData.isShadowPlayer)
            {
                //TODO: Buff
            }
            Destroy(other.gameObject);
        }

        //get hurt
        if(other.gameObject.tag == "Enemy")
        {
            float damage = 25f;
            _playerData.blood = Mathf.Clamp(_playerData.blood - damage, 0f, 100f); 
            //TODO: blood bar fill change

            //TODO: injure effect
        }

        // //save data
        // if(other.gameObject.tag == "SavePoint")
        // {
        //     SaveData();
        //     //TODO: show the information of savedata
        // }
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
                    if(_playerData.isShadowPlayer)
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
        if(_playerData.isShadow)
        {
            _playerData.isShadow = false;
            //TODO: effect
        }
        else
        {
            _playerData.isShadow = true;
            //TODO: effect
        }
    }
    private void SwitchWeapon()
    {
        if(_playerData.isGun)
        {
            _playerData.isGun = false;
            //TODO: animation
        }
        else
        {
            _playerData.isGun = true;
            //TODO: animation
        }
    }
    [System.Serializable]
    public class PlayerData
    {
        public float blood;
        public bool isShadowPlayer; // is ShadowPlayer or WeaponPlayer
        public bool isShadow; // is Shadow or Light
        public bool isGun; // use gun or blade
        public List<float> playerTransform = new List<float>();
    }
}

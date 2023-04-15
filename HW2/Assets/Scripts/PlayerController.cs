using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]private UIManager _uiManager;
    [SerializeField]private float _moveSpeed = 3f;
    [SerializeField]private float _jumpForce = 60f;
    private float _moveHorizontal;
    private bool _jump;
    [SerializeField]private bool _isJumping = false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Move()
    {
        if(Mathf.Abs(_moveHorizontal) > 0.1f)
        {
            _rb.AddForce(new Vector2(_moveHorizontal * _moveSpeed, 0f), ForceMode2D.Impulse);
        }
        if(!_isJumping && _jump)
        {
            _rb.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
        }
    }
    private void Update()
    {
        if(!_uiManager._useJoystick && _uiManager._useArrow) // use arrow to move
        {
            _moveHorizontal = Input.GetAxis("HorizontalArrow");
            _jump = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space);
        }
        if(!_uiManager._useJoystick && !_uiManager._useArrow) // use WASD to move
        {
            _moveHorizontal = Input.GetAxis("HorizontalWASD");
            _jump = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space);
        }
        if(_uiManager._useJoystick) // use joystick to move
        {
            //TODO: use joystick to move
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
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            _isJumping = true;
        }
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private UIManager _uiManager = new UIManager();
    private void Move()
    {
        if(_uiManager._useJoystick)
        {
            //TODO: play with joystick 
        }
        else
        {
            //TODO: play with keyboard
        }
    }
}

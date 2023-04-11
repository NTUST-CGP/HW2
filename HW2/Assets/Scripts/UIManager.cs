using UnityEngine;

public class UIManager : MonoBehaviour
{
//    [SerializeField]private GameObject _startMenu;
//    [SerializeField]private GameObject _settingMenu;
//    [SerializeField]private GameObject _saveMenu;
//    [SerializeField]private GameObject _exitMenu;
//    [SerializeField]private GameObject _winMenu;
//    [SerializeField]private GameObject _loseMenu;
    [SerializeField]private GameObject[] _menu;
    [SerializeField]private GameObject _joystickUI;
//    _startMenu = 0
//    _settingMenu = 1
//    _saveMenu = 2
//    _exitMenu = 3
//    _winMenu = 4
//    _loseMenu = 5
    public bool _useJoystick = false;
    public void ChangeMenu(int index)
    {
        for(int i = 0; i < _menu.Length; i++)
        {
            if(i == index)
                _menu[i].SetActive(true);
            else   
                _menu[i].SetActive(false);
        }
    }
    public void UseJoystick() 
    {
        _useJoystick = true;    
        //TOEDIT
        _joystickUI.SetActive(true);
    }
    public void UseKeyboard()
    {
        _useJoystick = false;
        //TOEDIT
        _joystickUI.SetActive(false);
    }
}

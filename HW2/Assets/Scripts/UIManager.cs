using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
//    [SerializeField] private GameObject _startMenu;
//    [SerializeField] private GameObject _settingMenu;
//    [SerializeField] private GameObject _saveMenu;
//    [SerializeField] private GameObject _exitMenu;
//    [SerializeField] private GameObject _winMenu;
//    [SerializeField] private GameObject _loseMenu;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject[] _menu;
    [SerializeField] private GameObject _joystickUI;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Image _loadingBarFill;
//    _startMenu = 0
//    _settingMenu = 1
//    _saveMenu = 2
//    _exitMenu = 3
//    _winMenu = 4
//    _loseMenu = 5
    public bool _useJoystick = false;
    public bool _useArrow = true;
    public bool _isShadowPlayer = true;

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
    public void ChooseShadowPlayer()
    {
        _isShadowPlayer = true;
        LoadScene(1);
    }
    public void ChooseWeaponPlayer()
    {
        _isShadowPlayer = false;
        LoadScene(1);
    }
    
    public void LoadScene (int sceneId) 
    {   //0 = start menu, 1 = level 1, 2 = level 2 for shadow, 3 = level 2 for weapon
        StartCoroutine(LoadSceneAsync(sceneId));
    }
    IEnumerator LoadSceneAsync(int sceneId) 
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        _loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            _loadingBarFill.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
    }
    public void SaveData()
    {
        _playerController._playerData.playerTransform.Add(_playerController.gameObject.transform.position.x);
        _playerController._playerData.playerTransform.Add(_playerController.gameObject.transform.position.y);
        // _playerController._playerData.playerTransform.Add(_playerController.gameObject.transform.rotation.x);
        // _playerController._playerData.playerTransform.Add(_playerController.gameObject.transform.rotation.y);
        // _playerController._playerData.playerTransform.Add(_playerController.gameObject.transform.rotation.z);
        XmlSerializer xmlPlayerData = new XmlSerializer(_playerController._playerData.GetType());
        Stream s = File.Open(Application.dataPath + "/PlayerData.xml", FileMode.Create);
        xmlPlayerData.Serialize(s, _playerController._playerData);
        s.Close();
    }

    public void LoadData()
    {
        XmlSerializer xmlPlayerData = new XmlSerializer(_playerController._playerData.GetType());
        Stream s = File.Open(Application.dataPath + "/PlayerData.xml", FileMode.Open);
        _playerController._playerData = (PlayerController.PlayerData)xmlPlayerData.Deserialize(s);
        _playerController.transform.position = new Vector3(_playerController._playerData.playerTransform[0], _playerController._playerData.playerTransform[1], _playerController.gameObject.transform.position.z);
        s.Close();
    }
    
   
    
}

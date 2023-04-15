using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField]private Transform _target;
    private void LateUpdate()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, -10);
        // transform.LookAt(_target);
    }
    
}

using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(BoxCollider2D))]
public class EnemyAIController : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private int _nextID = 1;
    [SerializeField] private int _IDChangeValue = 1;
    [SerializeField] private float _speed = 2f;
    private void Reset()
    {
        Init();
    }
    private void Init()
    {
        // GetComponent<BoxCollider2D>().isTrigger = true;
        
        GameObject root = new GameObject(name + " Root");
        root.transform.position = transform.position;
        transform.SetParent(root.transform);

        // Set Waypoints
        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.position = transform.position;
        waypoints.transform.SetParent(root.transform);
        // Create Waypoints
        _points = new List<Transform>();
        GameObject p1 = new GameObject("Point 1");
        p1.transform.position = waypoints.transform.position;
        p1.transform.SetParent(waypoints.transform); 
         _points.Add(p1.transform);
        GameObject p2 = new GameObject("Point 2");
        p2.transform.position = waypoints.transform.position;
        p2.transform.SetParent(waypoints.transform); 
         _points.Add(p2.transform);
    }
    private void Update()
    {
        MoveToNextPoint();
    }
    private void MoveToNextPoint()
    {
        Transform goalPoint = _points[_nextID];

        // Flip
        if(goalPoint.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
            
        // Move toward
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, _speed * Time.deltaTime);

        //set next point
        if(Vector2.Distance(transform.position, goalPoint.position) < 1f)
        {
            // move backward
            if(_nextID == _points.Count - 1)
                _IDChangeValue = -1;
            // move toward
            if(_nextID == 0)
                _IDChangeValue = 1;
            _nextID += _IDChangeValue;
        }
                
    }
}

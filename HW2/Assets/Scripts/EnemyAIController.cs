using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(BoxCollider2D))]
public class EnemyAIController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    // To store waypoints
    [SerializeField] private List<Transform> _points;
    // Which point to move
    [SerializeField] private int _nextID = 1;
    // To update _nextID with increment or decrement
    [SerializeField] private int _IDChangeValue = 1;
    // The speed of Moving to the waypoint
    [SerializeField] private float _speed = 2f;

    [SerializeField] private float _attckDistance = 2f;
    [SerializeField] private float _timeBetweenAttack = 3f;
    private void Reset()
    {
        Init();
    }
    private void Init()
    {
        _player = GameObject.FindWithTag("Player");

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
        if(Vector2.Distance(transform.position, _player.transform.position) < _attckDistance)
        {
            // check if facing player
            bool faceRight =  _player.transform.position.x >= transform.position.x && transform.localScale.x > 0;
            bool faceLeft =  _player.transform.position.x <= transform.position.x && transform.localScale.x < 0;
            if(faceLeft || faceRight)
            {
                Attack();
            }
        }
        else
            Patrol();
    }
    private void Patrol()
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
        if(Vector2.Distance(transform.position, goalPoint.position) < 0.3f)
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
    private void Attack()
    {
        //TODO: attck player

        // time between attcking
        float timer = 0f;
        while(timer < _timeBetweenAttack)
        {
            timer += Time.deltaTime;
        }
    }
}

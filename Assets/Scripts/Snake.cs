using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector2 _startPos;

    [SerializeField] private List<Transform> _tails;
    [SerializeField] private float _bonesDistance;
    [SerializeField] private GameObject _bonePrefab;
    [SerializeField] private GameObject _traceCamera;
    [SerializeField] private Text _humansText;
    [SerializeField] private Text _cristalsText;
    [SerializeField] private GameObject _restart;

    private int _humans;
    private int _cristals;
    private bool _fever = false;
    private string _nowMaterial = "Snake (Instance)";

    private Ray _ray;
    private RaycastHit _hit;
    private Vector3 _playerVr, _cursor;

    void UpdatePosition()
    {
        _playerVr = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _cursor = new Vector3(_hit.point.x, 0, _hit.point.z);
        
#if (UNITY_ANDROID)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch touch = Input.GetTouch(0);
            _ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(_ray, out _hit))
            {
                if (_cursor.x > 4) _cursor.x = 4;
                else if (_cursor.x < -4) _cursor.x = -4;
                transform.position = new Vector3(_cursor.x, transform.position.y, transform.position.z);
            }
        }
#endif
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        if (Input.GetMouseButton(0))
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit))
            {
                if (_cursor.x > 4) _cursor.x = 4;
                else if (_cursor.x < -4) _cursor.x = -4;
                transform.position = new Vector3(_cursor.x, transform.position.y, transform.position.z);
            }
        }
#endif
    }

    void FixedUpdate()
    {
        if (transform.position.z < 642)
        {
            transform.position += new Vector3(0, 0, 0.7f * _speed);
            _traceCamera.transform.position += new Vector3(0, 0, 0.7f * _speed);
        }
        if(_fever == false)
        {
            if (Input.GetKey(KeyCode.A) && transform.position.x > -4) transform.position = transform.position + new Vector3(-0.7f * _speed, 0, 0);
            else if (Input.GetKey(KeyCode.D) && transform.position.x < 4) transform.position = transform.position + new Vector3(0.7f * _speed, 0, 0);
            UpdatePosition();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(_fever == false)
        {
            if (col.tag == "human")
            {
                if (col.gameObject.GetComponent<MeshRenderer>().material.name != _nowMaterial)
                {
                    Debug.Log(col.gameObject.GetComponent<MeshRenderer>().material.name);
                    SceneManager.LoadScene(0);
                }
                else
                {
                    _humans++;
                    _humansText.text = "Humans: " + _humans;
                    Destroy(col.gameObject);
                }
            }
            else if (col.tag == "lvl")
            {
                gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<MeshRenderer>().material;
                _tails[0].gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<MeshRenderer>().material;
                _tails[1].gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<MeshRenderer>().material;
                _nowMaterial = col.gameObject.GetComponent<MeshRenderer>().material.name;
                Debug.Log(_nowMaterial);
            }
            else if (col.tag == "cristal")
            {
                _cristals++;
                _cristalsText.text = "Cristals: " + _cristals;
                Destroy(col.gameObject);
                if (_cristals >= 3)
                {
                    StartCoroutine(Fever());
                }
            }
            else if (col.tag == "block")
            {
                SceneManager.LoadScene(0);
            }
            else if (col.tag == "restart")
            {
                _restart.SetActive(true);
            }
        }
        else
        {
            if(col.tag == "human")
            {
                _humans++;
                _humansText.text = "Humans: " + _humans;
                Destroy(col.gameObject);
            }
            else if (col.tag != "lvl")
            {
                Destroy(col.gameObject);
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<MeshRenderer>().material;
                _tails[0].gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<MeshRenderer>().material;
                _tails[1].gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<MeshRenderer>().material;
            }
        }
    }

    private IEnumerator Fever()
    {
        _fever = true;
        _speed *= 3;
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(5);
        _cristals = 0;
        _cristalsText.text = "Cristals: " + _cristals;
        _speed /= 3;
        _fever = false;
    }

    public void Restart()
    {
        _restart.SetActive(false);
        SceneManager.LoadScene(0);
    }
}

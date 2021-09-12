using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 startPos;

    [SerializeField] private List<Transform> _tails;
    [SerializeField] private float _bonesDistance;
    [SerializeField] private GameObject _bonePrefab;
    [SerializeField] private GameObject traceCamera;
    [SerializeField] private Text humansText;
    [SerializeField] private Text cristalsText;
    [SerializeField] private GameObject restart;

    private int humans;
    private int cristals;
    private bool fever = false;
    private string nowMaterial = "Snake (Instance)";

    Ray ray;
    RaycastHit hit;
    Vector3 PlayerVr, Cursor;

    void UpdatePosition()
    {
        PlayerVr = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Cursor = new Vector3(hit.point.x, 0, hit.point.z);
        
#if (UNITY_ANDROID)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit))
            {
                if (Cursor.x > 4) Cursor.x = 4;
                else if (Cursor.x < -4) Cursor.x = -4;
                transform.position = new Vector3(Cursor.x, transform.position.y, transform.position.z);
            }
        }
#endif
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (Cursor.x > 4) Cursor.x = 4;
                else if (Cursor.x < -4) Cursor.x = -4;
                transform.position = new Vector3(Cursor.x, transform.position.y, transform.position.z);
            }
        }
#endif
    }

    void FixedUpdate()
    {
        if (transform.position.z < 642)
        {
            transform.position += new Vector3(0, 0, 0.7f * speed);
            traceCamera.transform.position += new Vector3(0, 0, 0.7f * speed);
        }
        if(fever == false)
        {
            if (Input.GetKey(KeyCode.A) && transform.position.x > -4) transform.position = transform.position + new Vector3(-0.7f * speed, 0, 0);
            else if (Input.GetKey(KeyCode.D) && transform.position.x < 4) transform.position = transform.position + new Vector3(0.7f * speed, 0, 0);
            UpdatePosition();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(fever == false)
        {
            if (col.tag == "human")
            {
                if (col.gameObject.GetComponent<MeshRenderer>().material.name != nowMaterial)
                {
                    Debug.Log(col.gameObject.GetComponent<MeshRenderer>().material.name);
                    SceneManager.LoadScene(0);
                }
                else
                {
                    humans++;
                    humansText.text = "Humans: " + humans;
                    Destroy(col.gameObject);
                }
            }
            else if (col.tag == "lvl")
            {
                gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<MeshRenderer>().material;
                _tails[0].gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<MeshRenderer>().material;
                _tails[1].gameObject.GetComponent<MeshRenderer>().material = col.gameObject.GetComponent<MeshRenderer>().material;
                nowMaterial = col.gameObject.GetComponent<MeshRenderer>().material.name;
                Debug.Log(nowMaterial);
            }
            else if (col.tag == "cristal")
            {
                cristals++;
                cristalsText.text = "Cristals: " + cristals;
                Destroy(col.gameObject);
                if (cristals >= 3)
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
                restart.SetActive(true);
            }
        }
        else
        {
            if(col.tag == "human")
            {
                humans++;
                humansText.text = "Humans: " + humans;
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
        fever = true;
        speed *= 3;
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(5);
        cristals = 0;
        cristalsText.text = "Cristals: " + cristals;
        speed /= 3;
        fever = false;
    }

    public void Restart()
    {
        restart.SetActive(false);
        SceneManager.LoadScene(0);
    }
}

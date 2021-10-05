using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private List<GameObject> _objects;
    [SerializeField] private List<Material> _materials;

    public void Variants(int num, int mater)
    {
        if (num == 0)
        {
            _objects[num].GetComponent<MeshRenderer>().material = _materials[mater];
            Instantiate(_objects[num], transform.position + new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(2.6f, 6.9f)), transform.rotation);
            Instantiate(_objects[num], transform.position + new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(-2.5f, 2.5f)), transform.rotation);
            _objects[num].GetComponent<MeshRenderer>().material = _materials[mater + 1];
            Instantiate(_objects[num], transform.position + new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(-2.6f, -6.9f)), transform.rotation);
            
        }
        else if(num == 1)
        {
            Instantiate(_objects[num], transform.position + new Vector3(-3f, 0.5f, -5f), transform.rotation);
            Instantiate(_objects[num], transform.position + new Vector3(-3f, 0.5f, 5f), transform.rotation);
            Instantiate(_objects[num], transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            Instantiate(_objects[num], transform.position + new Vector3(3f, 0.5f, -5f), transform.rotation);
            Instantiate(_objects[num], transform.position + new Vector3(3f, 0.5f, 5f), transform.rotation);

            Instantiate(_objects[num + 1], transform.position + new Vector3(3f, 0.5f, 0), transform.rotation);
            Instantiate(_objects[num + 1], transform.position + new Vector3(-3f, 0.5f, 0), transform.rotation);
            Instantiate(_objects[num + 1], transform.position + new Vector3(0, 0.5f, -5f), transform.rotation);
            Instantiate(_objects[num + 1], transform.position + new Vector3(0, 0.5f, 5f), transform.rotation);
        }
        else if(num == 2)
        {
            _objects[num + 1].GetComponent<MeshRenderer>().material = _materials[mater];
            Instantiate(_objects[num + 1], transform.position + new Vector3(0, 0.01f, 2.5f), _objects[num + 1].transform.rotation);

        }
        else if(num == 4)
        {
            Instantiate(_objects[num], transform.position + new Vector3(0, 0.01f, 7.5f), _objects[num].transform.rotation);

        }
    }
}

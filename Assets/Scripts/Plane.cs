using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private List<Material> materials;

    public void Variants(int num, int mater)
    {
        if (num == 0)
        {
            objects[num].GetComponent<MeshRenderer>().material = materials[mater];
            Instantiate(objects[num], transform.position + new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(2.6f, 6.9f)), transform.rotation);
            Instantiate(objects[num], transform.position + new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(-2.5f, 2.5f)), transform.rotation);
            objects[num].GetComponent<MeshRenderer>().material = materials[mater + 1];
            Instantiate(objects[num], transform.position + new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(-2.6f, -6.9f)), transform.rotation);
            
        }
        else if(num == 1)
        {
            Instantiate(objects[num], transform.position + new Vector3(-3f, 0.5f, -5f), transform.rotation);
            Instantiate(objects[num], transform.position + new Vector3(-3f, 0.5f, 5f), transform.rotation);
            Instantiate(objects[num], transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            Instantiate(objects[num], transform.position + new Vector3(3f, 0.5f, -5f), transform.rotation);
            Instantiate(objects[num], transform.position + new Vector3(3f, 0.5f, 5f), transform.rotation);

            Instantiate(objects[num + 1], transform.position + new Vector3(3f, 0.5f, 0), transform.rotation);
            Instantiate(objects[num + 1], transform.position + new Vector3(-3f, 0.5f, 0), transform.rotation);
            Instantiate(objects[num + 1], transform.position + new Vector3(0, 0.5f, -5f), transform.rotation);
            Instantiate(objects[num + 1], transform.position + new Vector3(0, 0.5f, 5f), transform.rotation);
        }
        else if(num == 2)
        {
            objects[num + 1].GetComponent<MeshRenderer>().material = materials[mater];
            Instantiate(objects[num + 1], transform.position + new Vector3(0, 0.01f, 2.5f), objects[num + 1].transform.rotation);

        }
        else if(num == 4)
        {
            Instantiate(objects[num], transform.position + new Vector3(0, 0.01f, 7.5f), objects[num].transform.rotation);

        }
    }
}

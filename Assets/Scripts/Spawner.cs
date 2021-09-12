using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private GameObject prefab;
    private int zcore = 5;

    void Start()
    {
        for(int i = 0; i < 42; i++)
        {
            zcore += 15;
            var obj = Instantiate(prefab, new Vector3(0, -0.5f, zcore), transform.rotation);
            if (i < 3 || (i > 3 && i < 14))
            {
                obj.GetComponent<Plane>().Variants(0,0);
            }
            else if(i == 3)
            {
                obj.GetComponent<Plane>().Variants(1,0);
            }
            else if (i == 14)
            {
                obj.GetComponent<Plane>().Variants(2,1);
            }
            else if(i > 14 && i < 21)
            {
                obj.GetComponent<Plane>().Variants(0,1);
            }
            else if (i == 21)
            {
                obj.GetComponent<Plane>().Variants(2, 3);
            }
            else if((i < 24 && i > 21) || (i > 24 && i < 35))
            {
                obj.GetComponent<Plane>().Variants(0, 3);
            }
            else if (i == 24)
            {
                obj.GetComponent<Plane>().Variants(1, 0);
            }
            else if (i == 35)
            {
                obj.GetComponent<Plane>().Variants(2, 0);
            }
            else if (i == 41)
            {
                obj.GetComponent<Plane>().Variants(4, 0);
            }
            else
            {
                obj.GetComponent<Plane>().Variants(0, 0);
            }
        }
    }
}

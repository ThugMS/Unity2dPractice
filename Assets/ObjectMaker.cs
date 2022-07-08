using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMaker : MonoBehaviour
{

    [SerializeField]
    private GameObject[] prefabArray;

    private void Awake()
    {
        for(int i = 0; i < 10; i++)
        {
            int index = Random.Range(0, prefabArray.Length);
            Vector3 position = new Vector3(-4.5f + i, 0, 0);

            Instantiate(prefabArray[index], position, Quaternion.identity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

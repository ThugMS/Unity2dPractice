using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private GameObject boxPrefab;

    private void Awake()
    {
        //원본
        //Instantiate(boxPrefab);

        //복제하면서 위치와 회전
        //Instantiate(boxPrefab, new Vector3(3,3,0), Quaternion.identity);

        Quaternion rotation = Quaternion.Euler(0, 0, 45);
        Instantiate(boxPrefab, new Vector3(2, 1, 0), rotation);

        //방금 생성된 복제 정보를 받아서 설정
        GameObject clone = Instantiate(boxPrefab, Vector3.zero, rotation);

        clone.name = "Box001";

        clone.GetComponent<SpriteRenderer>().color = Color.black;

        clone.transform.position = new Vector3(2, 1, 0);

        clone.transform.localScale = new Vector3(3, 2, 1);
    }

        
}

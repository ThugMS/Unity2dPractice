using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private GameObject boxPrefab;

    private void Awake()
    {
        //����
        //Instantiate(boxPrefab);

        //�����ϸ鼭 ��ġ�� ȸ��
        //Instantiate(boxPrefab, new Vector3(3,3,0), Quaternion.identity);

        Quaternion rotation = Quaternion.Euler(0, 0, 45);
        Instantiate(boxPrefab, new Vector3(2, 1, 0), rotation);

        //��� ������ ���� ������ �޾Ƽ� ����
        GameObject clone = Instantiate(boxPrefab, Vector3.zero, rotation);

        clone.name = "Box001";

        clone.GetComponent<SpriteRenderer>().color = Color.black;

        clone.transform.position = new Vector3(2, 1, 0);

        clone.transform.localScale = new Vector3(3, 2, 1);
    }

        
}

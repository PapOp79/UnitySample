using UnityEngine;
public class Item : MonoBehaviour
{
    public float rotateSpeed;
    void Awake()
    {
        rotateSpeed = 100;
    }
    void Update()
    {
        //매게 변수 기준으로 회전시키는 함수, up사용시 Y축, right X축으로 회전함
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime, Space.World); //Time.deltaTime -> 어떤 환경이든 동일한 움직임을 위해서 설정
    }
}
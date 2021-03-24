using UnityEngine;
public class PlayerBall : MonoBehaviour
{
    public GameManagerLogic manager;
    // Jump Step1 - 변수 선언
    public bool isJump = false; //처음 시작은 점프가 안된 상태로 간주
    public int jumpPower = 20;
    public int itemCount; //아이템 +점수 -점수 변수
    Rigidbody rigid; //물리현상을 표현할 수 있는 컴포넌트 선언
    AudioSource audio;
    // initialize(초기화)
    // Awake Function : 게임 오브젝트를 생성할 때 최초 1번만 실행되는 함수
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();//초기화
        audio = GetComponent<AudioSource>();//초기화
    }
    // FixedUpdate Function : 유니티 엔진이 물리 연산을 하기 전에 실행되는 업데이트 함수, 컴퓨터 사양과는 영향없이 고정적으로 호출됨
    // CPU 가 많이 사용되므로 물리 연산과 관련된 것만 넗음
    void FixedUpdate()
    {
        // GetAxisRaw : 0, 1 ,-1로 떨어짐 
        float h = Input.GetAxisRaw("Horizontal");  //Default0, 좌우, 왼쪽방향키 -1, 오른쪽방향키 +1 
        float v = Input.GetAxisRaw("Vertical"); //Default0, 상하, 아래방향키 -1, 위방향키 +1
        // 주어진 x, y, z의 컴포넌트를 가진 새로운 벡터를 생성합니다.
        // y 좌표는 점프 기능이므로 제외
        // ForceMode.Impulse - 정지상태에서 이동시적합, 뒤에서 누가 밀듯이 순간적으로 속도를 붙임, 무게 적용
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }
    // Update Function : 게임 로직 업데이트 물리 연산에 관련된 로직을 제외하고 나머지 계속해서 변화하는 로직을 사용, 환경에 따라 실행주기가 변할 수 있음
    // 초당 60번반복(60프레임)
    void Update()
    {
        // isJump가 false일 때만(1회만 가능)
        if (!isJump && Input.GetButtonDown("Jump"))
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse); // Jump Step2 - 점프 실행
        }
    }
    // 바닥과 닿으면 일어나는 이벤트
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false; // Jump Step3 - Player가 바닥과 닿으면 jump상태 해제 
        }
    }
    //물체와 닿으면 일어나는 이벤트
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            itemCount++; // 점수 올리기 
            audio.Play(); // 사운드 재생 
            other.gameObject.SetActive(false); //아이템 비활성화 
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            if (itemCount == manager.TotalItemCount)
            {
                Debug.Log("next stage");
                //게임 끝 (다음 레벨로)
            }
            else
            {
                Debug.Log("state restart");
                //재시작 
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    UIManager MyUIManager;

    public GameObject BallPrefab;   // prefab of Ball

    // Constants for SetupBalls
    public static Vector3 StartPosition = new Vector3(0, 0, -6.35f);
    public static Quaternion StartRotation = Quaternion.Euler(0, 90, 90);
    const float BallRadius = 0.286f;
    const float RowSpacing = 0.02f;

    GameObject PlayerBall;
    GameObject CamObj;

    const float CamSpeed = 3f;

    const float MinPower = 15f;
    const float PowerCoef = 1f;

    void Awake()
    {
        // PlayerBall, CamObj, MyUIManager를 얻어온다.
        // ---------- TODO ---------- 
        PlayerBall = GameObject.Find("PlayerBall");
        CamObj = GameObject.Find("Main Camera");
        MyUIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        // -------------------- 
    }

    void Start()
    {
        SetupBalls();
    }

    // Update is called once per frame
    void Update()
    {
        // 좌클릭시 raycast하여 클릭 위치로 ShootBallTo 한다.
        // ---------- TODO ---------- 
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPos = hit.point;
                ShootBallTo(targetPos);
            }
        }
        // -------------------- 
    }

    void LateUpdate()
    {
        CamMove();
    }

    void SetupBalls()
    {
        // 15개의 공을 삼각형 형태로 배치한다.
        // 가장 앞쪽 공의 위치는 StartPosition이며, 공의 Rotation은 StartRotation이다.
        // 각 공은 RowSpacing만큼의 간격을 가진다.
        // 각 공의 이름은 {index}이며, 아래 함수로 index에 맞는 Material을 적용시킨다.
        // Obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ball_1");
        // ---------- TODO ---------- 
        int ballIndex = 0;

        for(int i = 1; i <= 5; i++)
        {
            for(int j = 1; j <= i; j++)
            {
                ballIndex++;
                Vector3 pos = StartPosition;
                pos.z -= i * (BallRadius * 2 + RowSpacing);

                float xOffset = (i - 1) * (BallRadius + RowSpacing * 0.5f);
                pos.x += -xOffset + (j - 1) * (BallRadius * 2 + RowSpacing);

                GameObject obj = Instantiate(BallPrefab, pos, StartRotation);
                obj.name = ballIndex.ToString();
                obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ball_" + (ballIndex));
            }
        }

        // -------------------- 
    }
    void CamMove()
    {
        // ---------- TODO ---------- 
        Vector3 currentPos = CamObj.transform.position;
        Vector3 targetPos = PlayerBall.transform.position;

        targetPos.y = currentPos.y;

        CamObj.transform.position = Vector3.MoveTowards(currentPos, targetPos, CamSpeed * Time.deltaTime);
        // -------------------- 
    }

    float CalcPower(Vector3 displacement)
    {
        return MinPower + displacement.magnitude * PowerCoef;
    }

    void ShootBallTo(Vector3 targetPos)
    {
        // targetPos의 위치로 공을 발사한다.
        // 힘은 CalcPower 함수로 계산하고, y축 방향 힘은 0으로 한다.
        // ForceMode.Impulse를 사용한다.
        // ---------- TODO ---------- 
        Vector3 dir = targetPos - PlayerBall.transform.position;
        dir.y = 0f;

        float power = CalcPower(dir);
        PlayerBall.GetComponent<Rigidbody>().AddForce(dir.normalized * power, ForceMode.Impulse); // 크기 1인 벡터에 힘을 곱했음)
        // -------------------- 
    }
    
    // When ball falls
    public void Fall(string ballName)
    {
        // "{ballName} falls"을 1초간 띄운다.
        // ---------- TODO ---------- 
        MyUIManager.DisplayText(ballName + " falls", 1f);
        // -------------------- 
    }
}

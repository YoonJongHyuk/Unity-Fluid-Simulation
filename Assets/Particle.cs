// Particle.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using vector2 = UnityEngine.Vector2;
using list = System.Collections.Generic.List<Particle>;

using static Config;

public class Particle : MonoBehaviour
{
    // 하나의 유체 입자 객체

    // Config.cs에서 불러온 설정값
    public static int N = Config.N;
    public static float SIM_W = Config.SIM_W;
    public static float BOTTOM = Config.BOTTOM;
    public static float DAM = Config.DAM;
    public static int DAM_BREAK = Config.DAM_BREAK;
    public static float G = Config.G;
    public static float SPACING = Config.SPACING;
    public static float K = Config.K;
    public static float K_NEAR = Config.K_NEAR;
    public static float REST_DENSITY = Config.REST_DENSITY;
    public static float R = Config.R;
    public static float SIGMA = Config.SIGMA;
    public static float MAX_VEL = Config.MAX_VEL;
    public static float WALL_DAMP = Config.WALL_DAMP;
    public static float VEL_DAMP = Config.VEL_DAMP;
    public static float DT = Config.DT;
    public static float WALL_POS = Config.WALL_POS;

    // 물리 변수들
    public vector2 pos;
    public vector2 previous_pos;
    public vector2 visual_pos;
    public float rho = 0.0f; // 밀도
    public float rho_near = 0.0f; // 근접 밀도
    public float press = 0.0f; // 압력
    public float press_near = 0.0f; // 근접 압력
    public list neighbours = new list(); // 이웃 입자들
    public vector2 vel = vector2.zero; // 속도
    public vector2 force = new vector2(0f, -G); // 힘
    public float velocity = 0.0f; // 속도 크기

    // 격자 위치
    public int grid_x;
    public int grid_y;

    void Start()
    {
        // 초기 위치 설정
        pos = transform.position;
        previous_pos = pos;
        visual_pos = pos;
    }

    // 프레임마다 호출됨
    public void UpdateState()
    {
        previous_pos = pos;

        // 힘 적용 (Euler integration)
        vel += force * Time.deltaTime * DT;

        // 속도에 따라 위치 이동
        pos += vel * Time.deltaTime * DT;

        // 화면 표시 위치 갱신
        visual_pos = pos;
        transform.position = visual_pos;

        // 힘 초기화 (중력만 적용)
        force = new vector2(0, -G);

        // 속도 재계산
        vel = (pos - previous_pos) / Time.deltaTime / DT;
        velocity = vel.magnitude;

        // 속도 제한
        if (velocity > MAX_VEL)
        {
            vel = vel.normalized * MAX_VEL;
        }

        // 밀도 초기화
        rho = 0.0f;
        rho_near = 0.0f;

        // 이웃 초기화
        neighbours = new list();

        // 바닥 아래로 내려갔으면 제거
        if (pos.y < BOTTOM)
        {
            if (name != "Base_Particle")
            {
                Destroy(gameObject);
            }
        }
    }

    // 압력 계산 함수
    public void CalculatePressure()
    {
        press = K * (rho - REST_DENSITY);
        press_near = K_NEAR * rho_near;
    }

    // 벽 충돌 처리
    void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌 방향 벡터
        vector2 normal = collision.contacts[0].normal;

        // 충돌 방향 속도
        float vel_normal = Vector2.Dot(vel, normal);

        // 벽에서 떨어지는 중이면 무시
        if (vel_normal > 0)
        {
            return;
        }

        // 접선 방향 속도 계산
        vector2 vel_tangent = vel - normal * vel_normal;

        // 감쇠 반사 적용
        vel = vel_tangent - normal * vel_normal * WALL_DAMP;

        // 벽에서 튀어나오게 위치 조정
        pos = collision.contacts[0].point + normal * WALL_POS;
    }
}

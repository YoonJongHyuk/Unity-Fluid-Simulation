// Config.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    // 시뮬레이션 파라미터
    public static int N = 20;                    // 입자 수
    public static float SIM_W = 0.5f;            // 시뮬레이션 공간의 너비
    public static float BOTTOM = -2f;            // 바닥 위치
    public static float DAM = -0.3f;             // 댐 위치 (시작 시 입자 차단용)
    public static int DAM_BREAK = 200;           // 댐이 열리는 프레임 수
    public static float DT = 20f;                // 시간 흐름 배율 (시뮬레이션 속도)
    public static float WALL_POS = 0.08f;        // 벽에서 밀어낼 최소 거리

    // 물리 파라미터
    public static float G = 0.02f * 0.25f;        // 중력 가속도
    public static float SPACING = 0.08f;         // 입자 간 기본 거리
    public static float K = SPACING / 1000.0f;   // 압력 상수
    public static float K_NEAR = K * 10f;        // 근접 압력 상수
    public static float REST_DENSITY = 3.0f;     // 기준 밀도
    public static float R = SPACING * 1.25f;     // 이웃으로 인식할 거리
    public static float SIGMA = 0.2f;            // 점성 계수 (높을수록 끈적함)
    public static float MAX_VEL = 0.25f;         // 입자 최대 속도
    public static float WALL_DAMP = 0.2f;        // 벽 반사 감쇠 계수
    public static float VEL_DAMP = 0.5f;         // 과속 시 속도 감쇠 계수
}

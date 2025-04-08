// Shower.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using vector2 = UnityEngine.Vector2;

public class Shower : MonoBehaviour
{
    // 시뮬레이션 오브젝트 참조
    public GameObject Simulation;

    // Base_Particle 프리팹 참조
    public GameObject Base_Particle;

    // 초기 속도 설정
    public Vector2 init_speed = new Vector2(1.0f, 0.0f);

    // 생성 속도 (초당 몇 개 생성할지)
    public float spawn_rate = 1f;

    private float time;

    // 시작 시 호출
    void Start()
    {
        Simulation = GameObject.Find("Simulation");
        Base_Particle = GameObject.Find("Base_Particle");
    }

    // 매 프레임마다 호출
    void Update()
    {
        // 입자 최대 개수 제한
        if (Simulation.transform.childCount < 1000)
        {
            // 일정 시간 간격으로 입자 생성
            time += Time.deltaTime;
            if (time < 1.0f / spawn_rate)
            {
                return;
            }

            // 현재 위치에서 입자 생성
            GameObject new_particle = Instantiate(Base_Particle, transform.position, Quaternion.identity);

            // 입자의 초기 위치 및 속도 설정
            new_particle.GetComponent<Particle>().pos = transform.position;
            new_particle.GetComponent<Particle>().previous_pos = transform.position;
            new_particle.GetComponent<Particle>().visual_pos = transform.position;
            new_particle.GetComponent<Particle>().vel = init_speed;

            // 시뮬레이션 오브젝트의 자식으로 설정
            new_particle.transform.parent = Simulation.transform;

            // 시간 초기화
            time = 0.0f;
        }
    }
}

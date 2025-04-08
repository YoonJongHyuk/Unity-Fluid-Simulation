// Shower.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using vector2 = UnityEngine.Vector2;

public class Shower : MonoBehaviour
{
    // �ùķ��̼� ������Ʈ ����
    public GameObject Simulation;

    // Base_Particle ������ ����
    public GameObject Base_Particle;

    // �ʱ� �ӵ� ����
    public Vector2 init_speed = new Vector2(1.0f, 0.0f);

    // ���� �ӵ� (�ʴ� �� �� ��������)
    public float spawn_rate = 1f;

    private float time;

    // ���� �� ȣ��
    void Start()
    {
        Simulation = GameObject.Find("Simulation");
        Base_Particle = GameObject.Find("Base_Particle");
    }

    // �� �����Ӹ��� ȣ��
    void Update()
    {
        // ���� �ִ� ���� ����
        if (Simulation.transform.childCount < 1000)
        {
            // ���� �ð� �������� ���� ����
            time += Time.deltaTime;
            if (time < 1.0f / spawn_rate)
            {
                return;
            }

            // ���� ��ġ���� ���� ����
            GameObject new_particle = Instantiate(Base_Particle, transform.position, Quaternion.identity);

            // ������ �ʱ� ��ġ �� �ӵ� ����
            new_particle.GetComponent<Particle>().pos = transform.position;
            new_particle.GetComponent<Particle>().previous_pos = transform.position;
            new_particle.GetComponent<Particle>().visual_pos = transform.position;
            new_particle.GetComponent<Particle>().vel = init_speed;

            // �ùķ��̼� ������Ʈ�� �ڽ����� ����
            new_particle.transform.parent = Simulation.transform;

            // �ð� �ʱ�ȭ
            time = 0.0f;
        }
    }
}

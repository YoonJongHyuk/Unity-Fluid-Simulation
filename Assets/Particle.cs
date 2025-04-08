// Particle.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using vector2 = UnityEngine.Vector2;
using list = System.Collections.Generic.List<Particle>;

using static Config;

public class Particle : MonoBehaviour
{
    // �ϳ��� ��ü ���� ��ü

    // Config.cs���� �ҷ��� ������
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

    // ���� ������
    public vector2 pos;
    public vector2 previous_pos;
    public vector2 visual_pos;
    public float rho = 0.0f; // �е�
    public float rho_near = 0.0f; // ���� �е�
    public float press = 0.0f; // �з�
    public float press_near = 0.0f; // ���� �з�
    public list neighbours = new list(); // �̿� ���ڵ�
    public vector2 vel = vector2.zero; // �ӵ�
    public vector2 force = new vector2(0f, -G); // ��
    public float velocity = 0.0f; // �ӵ� ũ��

    // ���� ��ġ
    public int grid_x;
    public int grid_y;

    void Start()
    {
        // �ʱ� ��ġ ����
        pos = transform.position;
        previous_pos = pos;
        visual_pos = pos;
    }

    // �����Ӹ��� ȣ���
    public void UpdateState()
    {
        previous_pos = pos;

        // �� ���� (Euler integration)
        vel += force * Time.deltaTime * DT;

        // �ӵ��� ���� ��ġ �̵�
        pos += vel * Time.deltaTime * DT;

        // ȭ�� ǥ�� ��ġ ����
        visual_pos = pos;
        transform.position = visual_pos;

        // �� �ʱ�ȭ (�߷¸� ����)
        force = new vector2(0, -G);

        // �ӵ� ����
        vel = (pos - previous_pos) / Time.deltaTime / DT;
        velocity = vel.magnitude;

        // �ӵ� ����
        if (velocity > MAX_VEL)
        {
            vel = vel.normalized * MAX_VEL;
        }

        // �е� �ʱ�ȭ
        rho = 0.0f;
        rho_near = 0.0f;

        // �̿� �ʱ�ȭ
        neighbours = new list();

        // �ٴ� �Ʒ��� ���������� ����
        if (pos.y < BOTTOM)
        {
            if (name != "Base_Particle")
            {
                Destroy(gameObject);
            }
        }
    }

    // �з� ��� �Լ�
    public void CalculatePressure()
    {
        press = K * (rho - REST_DENSITY);
        press_near = K_NEAR * rho_near;
    }

    // �� �浹 ó��
    void OnCollisionStay2D(Collision2D collision)
    {
        // �浹 ���� ����
        vector2 normal = collision.contacts[0].normal;

        // �浹 ���� �ӵ�
        float vel_normal = Vector2.Dot(vel, normal);

        // ������ �������� ���̸� ����
        if (vel_normal > 0)
        {
            return;
        }

        // ���� ���� �ӵ� ���
        vector2 vel_tangent = vel - normal * vel_normal;

        // ���� �ݻ� ����
        vel = vel_tangent - normal * vel_normal * WALL_DAMP;

        // ������ Ƣ����� ��ġ ����
        pos = collision.contacts[0].point + normal * WALL_POS;
    }
}

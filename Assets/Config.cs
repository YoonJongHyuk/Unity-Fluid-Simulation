// Config.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    // �ùķ��̼� �Ķ����
    public static int N = 20;                    // ���� ��
    public static float SIM_W = 0.5f;            // �ùķ��̼� ������ �ʺ�
    public static float BOTTOM = -2f;            // �ٴ� ��ġ
    public static float DAM = -0.3f;             // �� ��ġ (���� �� ���� ���ܿ�)
    public static int DAM_BREAK = 200;           // ���� ������ ������ ��
    public static float DT = 20f;                // �ð� �帧 ���� (�ùķ��̼� �ӵ�)
    public static float WALL_POS = 0.08f;        // ������ �о �ּ� �Ÿ�

    // ���� �Ķ����
    public static float G = 0.02f * 0.25f;        // �߷� ���ӵ�
    public static float SPACING = 0.08f;         // ���� �� �⺻ �Ÿ�
    public static float K = SPACING / 1000.0f;   // �з� ���
    public static float K_NEAR = K * 10f;        // ���� �з� ���
    public static float REST_DENSITY = 3.0f;     // ���� �е�
    public static float R = SPACING * 1.25f;     // �̿����� �ν��� �Ÿ�
    public static float SIGMA = 0.2f;            // ���� ��� (�������� ������)
    public static float MAX_VEL = 0.25f;         // ���� �ִ� �ӵ�
    public static float WALL_DAMP = 0.2f;        // �� �ݻ� ���� ���
    public static float VEL_DAMP = 0.5f;         // ���� �� �ӵ� ���� ���
}

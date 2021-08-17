using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    public float speed = 5.0f;
    public float accelSpeed = 1.0f;
    Animator PlayerAnim;

    void Awake()
    {
        PlayerAnim = GetComponent<Animator>();
    }

    public bool Die()
    {
        PlayerAnim.SetBool("isDie", true);
        return true;
    }

    public bool Idle()
    {
        PlayerAnim.SetBool("isIdle", true);
        return true;
    }

    public bool Run(Vector3 targetPos, bool isRun = false)
    {
        // �̵��ϰ����ϴ� ��ǥ ���� ���� �� ��ġ�� ���̸� ���Ѵ�.
        float dis = Vector3.Distance(transform.position, targetPos);
        if (dis >= 0.5f) // ���̰� ���� �ִٸ�
        {
            // ĳ���͸� �̵���Ų��.
            transform.localPosition = Vector3.MoveTowards(transform.position, targetPos, speed * accelSpeed * Time.deltaTime);
            PlayerAnim.SetBool("isRun", true); // �ȱ� �ִϸ��̼� ó��
            return true;
        }
        else
        {
            PlayerAnim.SetBool("isRun", false);
            return false;
        }
    }

    public void Turn(Vector3 targetPos)
    {
        // ĳ���͸� �̵��ϰ��� �ϴ� ��ǥ�� �������� ȸ����Ų��
        Vector3 dir = targetPos - transform.position;
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        Quaternion targetRot = Quaternion.LookRotation(dirXZ);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 550.0f * Time.deltaTime);
    }

    public bool Attack1(bool isAttack1)
    {
        PlayerAnim.SetBool("isAttack1", isAttack1);
        return isAttack1;
    }

    public bool Attack2(bool isAttack2)
    {
        PlayerAnim.SetBool("isAttack2", isAttack2);
        return isAttack2;
    }

    public bool Defend(bool isDefend)
    {
        PlayerAnim.SetBool("isDefend", isDefend);
        return isDefend;
    }
}

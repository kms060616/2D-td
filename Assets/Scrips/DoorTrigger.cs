using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform targetPosition;

    private static bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTeleporting) return;

        if (other.CompareTag("Player") && targetPosition != null)
        {
            RoomController room = GetComponentInParent<RoomController>();
            bool isBackDoor = targetPosition.GetComponentInParent<RoomController>() != room;

            if (room == null || room.isCleared || isBackDoor)
            {
                Vector3 offset = Vector3.zero;
                if (gameObject.name.Contains("Up")) offset = Vector3.down * -2f;
                else if (gameObject.name.Contains("Down")) offset = Vector3.up * -3f;

                Vector3 targetPos = targetPosition.position + offset;
                targetPos.z = other.transform.position.z;

                isTeleporting = true; // �̵� ����
                other.transform.position = targetPos;
                Debug.Log("�÷��̾ ���� ���� �̵��߽��ϴ�.");

                // ���� �ð� �� �ٽ� �̵� ���
                other.GetComponent<Player>().StartCoroutine(ResetTeleportCooldown());
            }
        }
    }

    private IEnumerator ResetTeleportCooldown()
    {
        yield return new WaitForSeconds(0.3f); // ��Ÿ�� (�ʿ�� ���� ����)
        isTeleporting = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapePanelController : MonoBehaviour
{
    public string titleSceneName = "title";

    public void GoToTitle()
    {
        // �ʿ� �� ���� ó��
        SceneManager.LoadScene(titleSceneName);
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}

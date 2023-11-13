using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject ui;
    private bool isUIVisible = false;

    public void MoveToNextScene()
    {
        // 필드 아이템이 모두 수집되었는지 확인
        bool allItemsCollected = true;
        FieldItem[] fieldItems = FindObjectsOfType<FieldItem>();
        foreach (FieldItem item in fieldItems)
        {
            if (!item.isCollected)
            {
                allItemsCollected = false;
                break;
            }
        }

        if (allItemsCollected)
        {
            SceneManager.LoadScene("MainLab");
        }
        else
        {
            SceneManager.LoadScene("MainLab");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isUIVisible)
            {
                ui.SetActive(false);
                isUIVisible = false;
            }
            else
            {
                ui.SetActive(true);
                isUIVisible = true;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject ui;

    public void MoveToNextScene()
    {
        SceneManager.LoadScene("MonsterScene");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ui.activeSelf)
            {
                ui.SetActive(false);
            }
            else
            {
                // Check if all the field items have been collected
                bool allItemsCollected = true;
                foreach (FieldItem item in FindObjectsOfType<FieldItem>())
                {
                    if (!item.isCollected)
                    {
                        allItemsCollected = false;
                        break;
                    }
                }

                if (allItemsCollected)
                {
                    ui.SetActive(true);
                }
            }
        }
    }
}

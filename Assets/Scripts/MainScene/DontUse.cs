using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontUse : MonoBehaviour
{
    public Image DontUseYet;//나레이션용 패널
    // Start is called before the first frame update
    public void Arret()
    {
        DontUseYet.gameObject.SetActive(true);
        
    }

    public void quitArret()
    {
        DontUseYet.gameObject.SetActive(false);
    }
}

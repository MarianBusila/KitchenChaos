using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputUI : MonoBehaviour
{
    private void Start()
    {
        Show();
    }
    public void Show()
    {
        Debug.Log("Platform: " + Application.platform);
        this.gameObject.SetActive(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.ToggleInstructions();
        Destroy(this.gameObject);
    }
}

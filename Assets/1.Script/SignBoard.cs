using UnityEngine;

public class SignBoard : MonoBehaviour
{   
    public GameObject signBoardUI;
    private void Start()
    {
        signBoardUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            signBoardUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            signBoardUI.SetActive(false);
        }
    }
}


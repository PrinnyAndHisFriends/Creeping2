using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClicktoStart : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //Button btnMount = this.GetComponent<Button>();
        //btnMount.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
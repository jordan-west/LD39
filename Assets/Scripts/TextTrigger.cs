using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : ATriggerable {

    [SerializeField]
    private string text;

    private GameObject TextPanel;
    private Text TextBox;

    public override void Activate()
    {
        throw new NotImplementedException();
    }

    public override void Deactivate()
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        TextPanel = GameObject.Find("TextPanel");
        TextBox = TextPanel.transform.GetChild(0).GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TextBox.text = text;
        TextPanel.SetActive(true);

        LevelController.Instance.RespawnPoint = transform.position;

        activated = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TextBox.text = "";
        TextPanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ReadyButton : MonoBehaviourPunCallbacks
{
    public GameObject currentPlayer;

    private GameObject unready, ready;

    public bool readyFlag = true;
    public void SetCurrentPlayer(GameObject playerListContent) 
    {
        Text[] tempPlayerInfoList = playerListContent.GetComponentsInChildren<Text>();
        foreach (Text temp in tempPlayerInfoList)
        {
            Debug.Log("temp text: " + temp.text);
            Debug.Log("Photon Nickname: " + PhotonNetwork.NickName);
            if (temp.text == PhotonNetwork.NickName)
                currentPlayer = temp.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        }
        unready = currentPlayer.transform.GetChild(2).gameObject;
        ready = currentPlayer.transform.GetChild(3).gameObject;

        readyFlag = !readyFlag;

        if (readyFlag)
        {
            unready.SetActive(false);
            ready.SetActive(true);
        }
        else
        {
            unready.SetActive(true);
            ready.SetActive(false);
        }
    }

    public void OnClickReady()
    {
        Debug.Log("currentPlayer: " + currentPlayer.name);
        unready = currentPlayer.transform.GetChild(2).gameObject;
        ready = currentPlayer.transform.GetChild(3).gameObject;

        readyFlag = !readyFlag;

        if (readyFlag)
        {
            unready.SetActive(false);
            ready.SetActive(true);
        }
        else
        {
            unready.SetActive(true);
            ready.SetActive(false);
        }
    }
}

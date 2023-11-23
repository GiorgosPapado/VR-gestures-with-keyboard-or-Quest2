using Assets.Scripts.Evaluation.Input.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using Assets.Scripts.Evaluation.Actions;

public class TeleportHome : MonoBehaviour
{
    public Transform m_HomeLocation;
    public Transform m_Player;

    public IGestureAction m_GestureAction;

    [Inject]
    public void Init(IGestureAction gestureAction)
    {
        m_GestureAction = gestureAction;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_GestureAction.Home())
        {
            m_Player.position = m_HomeLocation.position;
            m_Player.rotation = m_HomeLocation.rotation;
            MessageBroker.Default.Publish(new TeleportHomeAction());
        }
    }
}

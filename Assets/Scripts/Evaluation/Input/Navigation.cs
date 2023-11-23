using Assets.Scripts.Evaluation.Input.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class Navigation : MonoBehaviour
{
    public float TranslationSpeed = 5.0f;
    public float RotationSpeed = 500.0f;

    private INavigation m_Navigation;

    [Inject]
    public void Init(INavigation navigation)
    {
        m_Navigation = navigation;
    }

    // Update is called once per frame
    void Update()
    {
        float ver = m_Navigation.GetMoveVertical();
        float hor = m_Navigation.GetMoveHorizontal();
        float height = m_Navigation.GetMoveHeight();

        Vector3 fw = transform.rotation * Vector3.forward;
        Vector3 xzdir = fw;
        xzdir.y = 0;
        transform.position += xzdir * ver * TranslationSpeed * Time.deltaTime;
        //transform.Translate(xydir * ver * TranslationSpeed);
        transform.Translate(Vector3.right * hor * TranslationSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * height * TranslationSpeed * Time.deltaTime);

        float rotHor = m_Navigation.GetRotateX();
        float rotVer = m_Navigation.GetRotateY();
        transform.Rotate(Vector3.right, -rotVer * RotationSpeed * Time.deltaTime);
        transform.RotateAround(transform.position, Vector3.up, rotHor * RotationSpeed * Time.deltaTime);       
    }
}

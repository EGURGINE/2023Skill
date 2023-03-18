using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    // ������ ���
    void ResisterObserver(IObserver observer);
    // ������ ����
    void RemoveObserver(IObserver observer);
    //�������鿡�� ���� ����
    void NotifyObservers();
}

public interface IObserver
{
    void DestroyObj();
    Vector3 ThisTransform();
}

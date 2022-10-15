using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTime = JC.DateTime.DateTime;

[ExecuteAlways]
public class DateTimeManager : MonoBehaviour
{
    public DTime _DateTime = new DTime();

    void Start()
    {
        _DateTime.Initialize();
    }

    void Update()
    {
        _DateTime.OnUpdate();
    }
}

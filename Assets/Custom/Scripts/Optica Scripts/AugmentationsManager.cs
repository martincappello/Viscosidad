﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentationsManager : MonoBehaviour
{

    public GameObject PlaneMirror;

    public void ActivatePlaneMirror()
    {
        DeactivateAll();
        PlaneMirror.SetActive(true);
    }

    private void DeactivateAll()
    {
        PlaneMirror.SetActive(false);
    }
}

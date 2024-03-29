﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidInteraction : Interaction
{
    List<ElementInstance> absorbedElements = new List<ElementInstance>();


    public void Update()
    {
        for (int i = absorbedElements.Count - 1; i >= 0; i--)
        {
            var element = absorbedElements[i];
            if (element == null)
            {
                absorbedElements.RemoveAt(i);
                continue;
            }
            element.transform.position = Vector3.Lerp(element.transform.position, transform.position, .05f);
            element.transform.localScale *= .95f;
            if (element.transform.localScale.magnitude <= .05f)
            {
                Destroy(element.gameObject);
                absorbedElements.RemoveAt(i);
            }
        }
    }

    public override void Kill()
    {
        base.Kill();
        foreach (var element in absorbedElements)
        {
            Destroy(element.gameObject);
        }
    }

    public override void Interact(ElementInstance element)
    {
        if (!absorbedElements.Contains(element))
        {
            element.Lock();
            absorbedElements.Add(element);
        }
    }


}


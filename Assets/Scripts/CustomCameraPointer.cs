//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CustomCameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;
    private GameObject newObject = null;
    private GameObject oldObject = null;
    private EventTrigger trigger;
    float timer = 0.0f;

    public float maxTime = 3.0f;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            newObject = hit.transform.gameObject;
            if (oldObject == null) // null reference error fix
            {
                oldObject = newObject;
            }
            if (newObject.transform.name == oldObject.transform.name)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                oldObject = newObject;
            }
            // GameObject detected in front of the camera for certain seconds
            if (hit.transform.gameObject && timer > maxTime)
            {
                trigger?.OnPointerExit(null);
                trigger = hit.transform.GetComponent<EventTrigger>();
                trigger?.OnPointerEnter(null);
            }
        }
        else
        {
            trigger?.OnPointerExit(null);
            trigger = null;
        }

    }
}

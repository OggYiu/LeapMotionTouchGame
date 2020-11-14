using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMgr : MonoBehaviour
{
    private Ray ray;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool justTouched = false;
        if(Input.touchCount > 0)
        {
            var touch = Input.touches[0];
            if(touch.phase == TouchPhase.Began)
            {
                ray = Camera.main.ScreenPointToRay(touch.position);
                justTouched = true;
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            justTouched = true;
        }

        if(justTouched)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                //Debug.Log("hitinfo: " + hitInfo.collider.gameObject.name);

                GameObject targetGameObj = hitInfo.collider.gameObject;
                if(targetGameObj != null)
                {
                    InteractionButton button = null;
                    button = targetGameObj.GetComponent<InteractionButton>();


                    if (targetGameObj.transform.parent != null && button == null)
                    {
                        button = targetGameObj.transform.parent.GetComponent<InteractionButton>();
                    }

                    if(button != null && button.enabled)
                    {
                        if (button.onPressSound != null) button.onPressSound.Play();
                        button.OnPress();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(ray);
    }
}

using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveSurface : InteractionBehaviour
{
    private InteractionBehaviour _intObj;
    public GameObject touchAnimSprite;

    protected override void Start()
    {
        base.Start();

        _intObj = GetComponent<InteractionBehaviour>();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    PlayTouchAnimation();
        //}


        if (_intObj != null && _intObj.isPrimaryHovered && false)
        {
            Vector3 hoverPoint = primaryHoveringControllerPoint;


            if (closestHoveringController != null)
            {
                //Vector3 dir = (hoverPoint - closestHoveringController.position).normalized;
                //RaycastHit hitInfo;
                //int layerMask = 1 << LayerMask.NameToLayer("UI");
                //layerMask = ~layerMask;

                //if (Physics.Raycast(closestHoveringController.position, dir, out hitInfo, 10f, layerMask))
                {
                    //float scale = 1f + (1f - closestHoveringControllerDistance) * 5f;
                    //hoverSprite.transform.localScale = new Vector3(scale, scale, scale);
                    //Debug.Log("closestHoveringControllerDistance: " + closestHoveringControllerDistance);
                    //hoverSprite.transform.position = hitInfo.point;
                }
                //Debug.Log("distance: " + closestHoveringControllerDistance);
            }
        }
        //Debug.Log("this.primaryHoverDistance: " + this.primaryHoverDistance);
    }

    //public void PlayTouchAnimation()
    //{
    //    touchAnimSprite.SetActive(true);
    //    touchAnimSprite.transform.position = hoverSprite.transform.position;
    //    touchAnimSprite.transform.localScale = Vector3.one;

    //    touchAnimSprite.transform
    //        .DOScale(0f, 0.1f)
    //        .SetLoops(4, LoopType.Yoyo)
    //        .OnComplete(()=>touchAnimSprite.SetActive(false));
    //}



    private void OnDrawGizmos()
    {
        if (_intObj != null && _intObj.isPrimaryHovered)
        {
            Vector3 hoverPoint = primaryHoveringControllerPoint;
            Gizmos.color = Color.red;
            //Gizmos.DrawSphere(hoverPoint, 0.05f);
            

            if(closestHoveringController != null)
            {
                Gizmos.color = Color.blue;
            }
        }
    }
}

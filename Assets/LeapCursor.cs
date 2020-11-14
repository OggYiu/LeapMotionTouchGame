using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeapCursor : MonoBehaviour
{
    public GameObject hoverSprite;
    public Color hoverColor = Color.white;
    InteractionBehaviour interactionBehaviour;

    private void Awake()
    {
        interactionBehaviour = GetComponent<InteractionBehaviour>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        hoverSprite.SetActive(interactionBehaviour != null && interactionBehaviour.isPrimaryHovered);
        if (interactionBehaviour != null && interactionBehaviour.isPrimaryHovered)
        {   
            float hoverDistance = interactionBehaviour.closestHoveringControllerDistance;
            if (hoverDistance > 0 && hoverDistance <= 0.3f)
            {
                hoverSprite.transform.localScale = Vector3.one * hoverDistance * 10f;
                var closestPos = interactionBehaviour.closestHoveringController.position;
                hoverSprite.transform.position = interactionBehaviour.primaryHoveringControllerPoint;
                hoverSprite.GetComponent<Image>().color = hoverColor;
            }
        }
    }
}

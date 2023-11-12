using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfferBoard : MonoBehaviour
{
    public List<Upgrades> upgrades; // Assuming Upgrade is a class you've already defined

    public RectTransform hide;
    public RectTransform show;
    public RectTransform container;
    public RectTransform offerLabalContainer;
    public Image timerIcon;
    private GameObject currentOfferUI;

    public AudioSource audioSource;
    public AudioClip offerShowUpClip, takeOfferClip, cannotAffordClip;

    public bool isPlayer1 = true;
    private Upgrades currentOffer;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float moveDuration;
    private float moveTimeElapsed;
    private bool isMoving;

    private bool _offerReady;
    public float offerDuration = 3.5f; // Duration for how long the offer is available
    public float offerCooldownMin = 3f, offerCooldownMax = 7f;  // Time before the next offer becomes available
    public float offerContainerMoveSpeed = 0.5f;
    private float offerTimer = 0f;

    public bool offerReady
    {
        get { return _offerReady; }
        set
        {
            _offerReady = value;
            offerTimer = _offerReady ? offerDuration : Random.Range(offerCooldownMin, offerCooldownMax);;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            moveTimeElapsed += Time.deltaTime;
            float normalizedTime = moveTimeElapsed / moveDuration;
            if (normalizedTime >= 1f)
            {
                normalizedTime = 1f;
                isMoving = false; // Stop the movement
            }
            container.position = Vector3.Slerp(startPosition, endPosition, normalizedTime);
        }

        // Handle the offer timer
        if (offerTimer > 0)
        {
            offerTimer -= Time.deltaTime;
            if (offerTimer <= 0)
            {
                if (_offerReady)
                {
                    // Offer time expired, hide the offer and start cooldown for the next offer
                    StartMove(show, hide, offerContainerMoveSpeed);
                    offerReady = false;
                }
                else
                {
                    // Cooldown expired, ready for next offer
                    if (FindNextUpgrade()) {
                        StartMove(hide, show, offerContainerMoveSpeed);
                        if (offerShowUpClip) { audioSource.PlayOneShot(offerShowUpClip); }
                        // instanciate the UI element currentOffer.UIPrefab inside of the container with exactly same position
                        InstantiateCurrentOffer();
                        offerReady = true;
                    }
                }
            }
        } else if (FindNextUpgrade()) {
            // as there is offer Timer, whenever an offer is ready, just push it.
            StartMove(hide, show, offerContainerMoveSpeed);
            if (offerShowUpClip) { audioSource.PlayOneShot(offerShowUpClip); }
            InstantiateCurrentOffer();
            offerReady = true;
        }

        if (offerReady) {
            timerIcon.fillAmount = offerTimer / offerDuration; // percentage...
            if (isPlayer1? Input.GetKeyDown(KeyCode.LeftShift) : Input.GetKeyDown(KeyCode.RightShift)) {
                TakeTheOffer();
            }
        }
    }

    private void InstantiateCurrentOffer()
    {
        if (currentOffer != null && currentOffer.UIPrefab != null)
        {
            // Destroy existing offer UI if any
            foreach (Transform child in offerLabalContainer)
            {
                Destroy(child.gameObject);
            }

            // Instantiate new offer UI as a child of the container
            currentOfferUI = Instantiate(currentOffer.UIPrefab, offerLabalContainer);
            currentOfferUI.transform.localPosition = Vector3.zero; // Set position to center of offerLabalContainer
            currentOfferUI.transform.localScale = Vector3.one; // Ensure scale is set correctly
            currentOffer.SetCurrentCost(currentOfferUI);
        }
    }

    public void TakeTheOffer() {
        if (currentOffer.PurchasedUpgrade()) {
            if (takeOfferClip) { audioSource.PlayOneShot(takeOfferClip); }
            StartMove(show, hide, offerContainerMoveSpeed);
            offerReady = false; // took the offer. this is enough to generate next.
            // Buy algorithm goes here.
        } else {
            if (cannotAffordClip) { audioSource.PlayOneShot(cannotAffordClip); }
            Debug.Log("GO CHOP MORE TREES YOU BROKEN LITTLE FIRM");
        }
    }

    public void StartMove(RectTransform from, RectTransform to, float duration)
    {
        startPosition = from.position;
        endPosition = to.position;
        moveDuration = duration;
        moveTimeElapsed = 0;
        isMoving = true;
    }

    public bool FindNextUpgrade()
    {
        if (upgrades.Count > 0)
        {
            int randomIndex = Random.Range(0, upgrades.Count);
            currentOffer = upgrades[randomIndex];
            return true;
        }
        return false;
    }
}
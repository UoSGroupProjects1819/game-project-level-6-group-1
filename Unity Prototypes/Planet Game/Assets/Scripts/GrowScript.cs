using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowScript : MonoBehaviour {

    [SerializeField]
    private Vector3 desiredSize;
    [SerializeField]
    private float growthTime;
    [SerializeField]
    private Sprite smallSprite;
    [SerializeField]
    private Sprite bigSprite;
    [SerializeField]
    private SpriteRenderer sprRenderer;

	void Start () {
        StartCoroutine(GrowPlant(growthTime));
	}

    IEnumerator GrowPlant(float time)
    {
        Vector3 originalScale = gameObject.transform.localScale;

        float currentTime = 0.0f;

        do
        {
            gameObject.transform.localScale = Vector3.Lerp(originalScale, desiredSize, currentTime / growthTime);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= growthTime);

        sprRenderer.sprite = bigSprite;
        StopAllCoroutines();
    }
}

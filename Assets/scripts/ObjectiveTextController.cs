using UnityEngine;

public class ObjectiveTextController : MonoBehaviour
{
    public GameObject objectiveText; // Assign your TMP text GameObject
    public float displayTime = 3f;

    void Start()
    {
        if (objectiveText != null)
        {
            objectiveText.SetActive(true);
            Invoke(nameof(HideObjective), displayTime);
        }
    }

    void HideObjective()
    {
        if (objectiveText != null)
            objectiveText.SetActive(false);
    }
}
using UnityEngine;

public class DevMode : MonoBehaviour
{
    [SerializeField]
    private Health health;

    [SerializeField]
    private Hint hint;

    private int healthSave;
    private int hintSave;

    private enum DevModeType
    {
        DEV,
        USER
    }

    private DevModeType devMode;

    private void Awake()
    {
        devMode = DevModeType.USER;
    }

    public void ButtonClicked()
    {
        if (devMode == DevModeType.USER)
        {
            healthSave = health.health;
            hintSave = hint.counter;
            health.setHealth(999);
            hint.setHintCount(999);
            devMode = DevModeType.DEV;
        }
        else
        {
            health.setHealth(healthSave);
            hint.setHintCount(hintSave);
            devMode = DevModeType.USER;
        }
    }
}

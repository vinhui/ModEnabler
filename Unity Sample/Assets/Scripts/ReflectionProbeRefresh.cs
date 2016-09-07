using ModEnabler;
using UnityEngine;

[RequireComponent(typeof(ReflectionProbe))]
public class ReflectionProbeRefresh : MonoBehaviour
{
    private ReflectionProbe reflectionProbe;

    private void Awake()
    {
        this.reflectionProbe = this.GetComponent<ReflectionProbe>();

        ModsManager.onModActivate.AddListener((mod) => { this.Refresh(); });
        ModsManager.onModDeactivate.AddListener((mod) => { this.Refresh(); });

        this.Refresh();
    }

    private void Refresh()
    {
        if (this.reflectionProbe != null)
            this.reflectionProbe.RenderProbe();
    }
}
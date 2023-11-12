using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Blur : MonoBehaviour
{

    [SerializeField] playerHealth ph;

    PostProcessVolume volume;
    DepthOfField dof;
    // Start is called before the first frame update
    void Start()
    {
       dof = ScriptableObject.CreateInstance<DepthOfField>();
       dof.enabled.Override(true);
       dof.focusDistance.Override(10f);
       volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, dof);
       volume = GetComponent<PostProcessVolume>();
       volume.profile.settings.Add(dof);

    }

    // Update is called once per frame
    void Update()
    {
        dof.focusDistance.value = (float)ph.health/10f;
        //Debug.Log(dof.focusDistance.value);
    }
}

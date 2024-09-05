using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSound : MonoBehaviour
{

    private playerscript player;
    private float footStepTimer;
    private float footstepTimerMax = .1f;

    private void Awake()
    {
        player = GetComponent<playerscript>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if(footStepTimer < 0f)
        {
            footStepTimer = footstepTimerMax;
            if (player.IsWalking())
            {
                float volume = 1f;
                SoundManager.instance.PlayFootstepSound(player.transform.position, volume);
            }
        }
    }
}

using UnityEngine;

namespace Managers
{
    public interface ISoundManager
    {
        void PlaySound(SoundManager.Type soundType = SoundManager.Type.Lobby);
    }
}
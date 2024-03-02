using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // 사운드에는 3개 객체가 필요
    // 1. MP3 Player -> AudioSource
    // 2. MP3 음원   -> AudioClip
    // 3. 관객(귀)   -> AudioListener : default로 카메라에 달려있음

    public void Init()
    {
        

        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i< soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                // go에 컴포넌트 객체 추가하고 배열에도 넣어주기
                _audioSources[i] = go.AddComponent<AudioSource>(); 
                // UI의 LecTransform의 경우에는 setParent
                // 일반적인 경우에는 아래처럼 연결해주면됨
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;

        }
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;    
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
                                           //선택적 매개변수들은 반드시 마지막에 위치할 것!!
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)
        {
            //TODO
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip; // Play()는 클립을 같이 안 넣어주기 때문에 여기서 넣어줌
            audioSource.Play(); //그냥 재생
        }
        else
        {
            // 효과음만 가져오기
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip); //해당 클립 1번만 재생
        }
    }


    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }
        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing : {path}");
        }

        return audioClip;
    }
}

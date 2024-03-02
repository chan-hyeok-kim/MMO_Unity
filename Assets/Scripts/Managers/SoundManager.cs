using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // ���忡�� 3�� ��ü�� �ʿ�
    // 1. MP3 Player -> AudioSource
    // 2. MP3 ����   -> AudioClip
    // 3. ����(��)   -> AudioListener : default�� ī�޶� �޷�����

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
                // go�� ������Ʈ ��ü �߰��ϰ� �迭���� �־��ֱ�
                _audioSources[i] = go.AddComponent<AudioSource>(); 
                // UI�� LecTransform�� ��쿡�� setParent
                // �Ϲ����� ��쿡�� �Ʒ�ó�� �������ָ��
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
                                           //������ �Ű��������� �ݵ�� �������� ��ġ�� ��!!
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
            audioSource.clip = audioClip; // Play()�� Ŭ���� ���� �� �־��ֱ� ������ ���⼭ �־���
            audioSource.Play(); //�׳� ���
        }
        else
        {
            // ȿ������ ��������
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip); //�ش� Ŭ�� 1���� ���
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

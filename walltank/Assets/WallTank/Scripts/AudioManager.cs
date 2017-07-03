using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Audio鳴らす基底クラス
/// </summary>
public class AudioManager : SingletonMonoBehavior<AudioManager>
{

	/// <summary>
	/// BGMリスト
	/// </summary>
	private List<AudioClip> audioList;

	/// <summary>
	/// Audiosourceリスト
	/// </summary>
	private List<AudioSource> audioSources;

	/// <summary>
	/// AudioClipDictionary
	/// </summary>
	private Dictionary<string, AudioClip> audioDict = null;

	/// <summary>
	/// 
	/// </summary>
	public enum PlayMode { Normal, Repeat }

	protected override void Awake()
	{
		base.Awake();
		if (this != I) { Destroy(this); return; }
		DontDestroyOnLoad(this.gameObject);

		if (FindObjectsOfType(typeof(AudioListener)).All(o => !((AudioListener)o).enabled)) { this.gameObject.AddComponent<AudioListener>(); }
		audioList = new List<AudioClip>();
		audioSources = new List<AudioSource>();
		audioDict = new Dictionary<string, AudioClip>();
		Action<Dictionary<string, AudioClip>, AudioClip> addClipdict = (dict, c) =>
		{
			if (!dict.ContainsKey(c.name)) { dict.Add(c.name, c); }
		};

		// Resources/Audiosに保存している音源を再生
		audioList.Add(Resources.Load("Audios/se_maoudamashii_battle12") as AudioClip);
		audioList.Add(Resources.Load("Audios/se_maoudamashii_system37") as AudioClip);
		audioList.Add(Resources.Load("Audios/se_maoudamashii_system27") as AudioClip);
		audioList.Add(Resources.Load("Audios/se_maoudamashii_se_whistle01") as AudioClip);
		audioList.Add(Resources.Load("Audios/game_maoudamashii_5_town26") as AudioClip);
		audioList.Add(Resources.Load("Audios/attack") as AudioClip);
		audioList.Add(Resources.Load("Audios/announce") as AudioClip);
		audioList.Add(Resources.Load("Audios/get") as AudioClip);
		audioList.Add(Resources.Load("Audios/heri") as AudioClip);
		audioList.Add(Resources.Load("Audios/shippu") as AudioClip);
		audioList.Add(Resources.Load("Audios/loop_47") as AudioClip);
		audioList.Add(Resources.Load("Audios/loop_89") as AudioClip);
		audioList.Add(Resources.Load("Audios/loop_111") as AudioClip);
		audioList.Add(Resources.Load("Audios/loop_131") as AudioClip);
		audioList.Add(Resources.Load("Audios/loop_144") as AudioClip);

		audioList.ForEach(audio => addClipdict(audioDict, audio));
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Audio名に対応した音声を鳴らす
	/// </summary>
	/// <param name="audioName"></param>
	public void PlayAudio(string audioName, float volume = 1.0f, PlayMode playMode = PlayMode.Normal)
	{
		// 使われていないAudioSourceがあれば使うし，なければ追加して鳴らす
		if (!audioDict.ContainsKey(audioName)) { return; }
		AudioSource source = audioSources.FirstOrDefault(s => !s.isPlaying);
		if (source == null)
		{
			source = gameObject.AddComponent<AudioSource>();
			audioSources.Add(source);
		}
		source.clip = audioDict[audioName];
		source.volume = volume;
		source.Play();
		if (playMode == PlayMode.Repeat) { source.loop = true; }
		else { source.loop = false; }
	}

	/// <summary>
	/// Audioを止める
	/// </summary>
	public void StopAudio() { audioSources.ForEach(s => s.Stop()); }

	/// <summary>
	/// 特定のAudioを止める
	/// </summary>
	public void StopAudio(string audioName) { audioSources.FirstOrDefault(s => s.clip.name == audioName).Stop(); }

	/// <summary>
	/// Audio名に対応した音声が再生されているかどうか
	/// </summary>
	/// <param name="audioName"></param>
	public bool IsPlaying(string audioName)
	{
		return audioSources.FirstOrDefault(source => source.clip.name == audioName) == null ? false : audioSources.First(source => source.clip.name == audioName).isPlaying;
	}

	public void SetLooping(string audioName, bool isLooping)
	{
		audioSources.FirstOrDefault(source => source.clip.name == audioName).loop = isLooping;
	}
}

using Godot;
using System;

public class AudioVisualizer : Node3D
{
	private float[] spectrum = new float[256];
	private const string AUDIO_BUS = "Music";

	public override void _Process(float delta)
	{
		var spectrumAnalyzer = AudioServer.GetBusEffectInstance(AudioServer.GetBusIndex(AUDIO_BUS), 0) as AudioEffectSpectrumAnalyzerInstance;

		if (spectrumAnalyzer != null)
		{
			spectrumAnalyzer.GetMagnitudesForFrequencyRange(20, 20000, spectrum, 256);
		}
	}
}

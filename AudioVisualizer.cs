using Godot;
using System;

public partial class AudioVisualizer : Node2D
{
	private AudioStreamPlayer _audioPlayer;
	private Line2D _lineVisualizer;
	private AudioEffectSpectrumAnalyzer _spectrumAnalyzer;
	private float[] _spectrum = new float[128];  // Array to store spectrum data
	private int _bandCount = 128;  // Default band count, can be adjusted

	public override void _Ready()
	{
		// Initialize AudioStreamPlayer
		_audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

		// Initialize Line2D for visualizing the spectrum
		_lineVisualizer = new Line2D();
		AddChild(_lineVisualizer);
		_lineVisualizer.Width = 2;  // Width of the lines
		_lineVisualizer.DefaultColor = new Color(0, 1, 0);  // Set line color to green

		// Set the band count (number of frequency bands)
		_bandCount = 128;

		// Start audio playback
		_audioPlayer.Play();
	}

	public override void _Process(double delta)
	{
		// Update the spectrum visualization each frame
		UpdateVisualizer();
	}

	private void UpdateVisualizer()
	{
		// Retrieve the audio spectrum data from the audio bus
		var busIndex = AudioServer.GetBusIndex("Master");  // Assuming "Master" bus
		int effectCount = AudioServer.GetBusEffectCount(busIndex);

		// Check if there are any effects on the bus
		if (effectCount > 0)
		{
			// Access the Spectrum Analyzer effect (we assume it's at index 0)
			_spectrumAnalyzer = AudioServer.GetBusEffect(busIndex, 0) as AudioEffectSpectrumAnalyzer;

			// If we found the SpectrumAnalyzer, we can access the spectrum data
			if (_spectrumAnalyzer != null)
			{
				// Retrieve the spectrum data using the correct method
				var spectrumData = _spectrumAnalyzer.GetMagnitudes(); // Correct method to retrieve spectrum data

				// Ensure we have the correct number of bands (matching the length of _spectrum)
				if (spectrumData.Length == _bandCount)
				{
					_spectrum = spectrumData;
				}
			}
		}

		// Update the Line2D visualizer
		_lineVisualizer.ClearPoints();

		// Get the viewport size using GetViewportRect() and access width and height
		var viewportRect = GetViewportRect();
		float width = viewportRect.Size.X;  // Get the width from the Rect2
		float step = width / _spectrum.Length;

		// Loop through the spectrum data and visualize each band
		for (int i = 0; i < _spectrum.Length; i++)
		{
			// Height of the line, scaled for visibility
			float height = _spectrum[i] * 200;  
			// Vector for each point in the visualization
			Vector2 point = new Vector2(i * step, viewportRect.Size.Y - height);  // Use 'Y' for the height
			_lineVisualizer.AddPoint(point);
		}
	}
}

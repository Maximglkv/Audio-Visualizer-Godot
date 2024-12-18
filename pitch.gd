extends Node

@onready 
var audio_player: AudioStreamPlayer = $"../AudioStreamPlayer"
@onready 
var pitch_slider: HSlider = $"."

@export 
var initial_pitch: float = 1.0

func _ready() -> void:
	audio_player.pitch_scale = initial_pitch
	pitch_slider.value = initial_pitch

	pitch_slider.value_changed.connect(_on_slider_value_changed)

func _on_slider_value_changed(value: float) -> void:
	audio_player.pitch_scale = value

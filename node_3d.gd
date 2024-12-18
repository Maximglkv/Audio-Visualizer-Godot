extends Control
 
@onready
var spectrum = AudioServer.get_bus_effect_instance(1,0)
 
@onready
var topRightArray = $AudioStreamPlayer/IDK/Control/topLeft.get_children()
 
@onready
var bottomRightArray = $AudioStreamPlayer/IDK/Control/bottomRight.get_children()
 
@onready
var topLeftArray = $AudioStreamPlayer/IDK/Control/topRight.get_children()
 
@onready
var bottomLeftArray = $AudioStreamPlayer/IDK/Control/bottomLeft.get_children()
 
const VU_COUNT = 19
const HEIGHT = 60
const FREQ_MAX = 11050.0
 
const MIN_DB = 60
 
func _ready():
	bottomLeftArray.reverse()
	topLeftArray.reverse()
 
func _process(delta):
 
	var prev_hz = 0
	for i in range(1,VU_COUNT+1):   
		var hz = i * FREQ_MAX / VU_COUNT;
		var f = spectrum.get_magnitude_for_frequency_range(prev_hz,hz)
		var energy = clamp((MIN_DB + linear_to_db(f.length()))/MIN_DB,0,1)
		var height = energy * (20 + HEIGHT)
 
		prev_hz = hz
 
		var bottomRightRect = bottomRightArray[i - 1]
 
		var topRightRect = topRightArray[i - 1]
 
		var topLeftRect = topLeftArray[i - 1]
 
		var bottomLeftRect = bottomLeftArray[i - 1]
 
		var tween = get_tree().create_tween()
 
		tween.tween_property(topRightRect, "size", Vector2(topRightRect.size.x, height), 0.05)
		tween.tween_property(bottomRightRect, "size", Vector2(bottomRightRect.size.x, height), 0.05)
		tween.tween_property(topLeftRect, "size", Vector2(topLeftRect.size.x, height), 0.05)
		tween.tween_property(bottomLeftRect, "size", Vector2(bottomLeftRect.size.x, height), 0.05)

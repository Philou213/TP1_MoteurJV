extends CharacterBody2D

@onready var animated_sprite_2d = $AnimatedSprite2D
@export var SPEED = 2000

func _ready():
	animated_sprite_2d.play("Idle")

func _physics_process(delta: float) -> void:
	var horizontalDirection := Input.get_axis("left_axis", "right_axis")
	var verticalDirection := Input.get_axis("top_axis", "down_axis")
	var direction := Vector2(horizontalDirection,verticalDirection)
	if (horizontalDirection < 0):
		animated_sprite_2d.flip_h = true
	if (horizontalDirection > 0):
		animated_sprite_2d.flip_h = false
	if (direction):
		animated_sprite_2d.play("Moving")
		velocity = direction * SPEED * delta
	else:
		velocity = velocity.move_toward(Vector2(0,0), SPEED * delta)
		animated_sprite_2d.play("Idle")

	move_and_slide()

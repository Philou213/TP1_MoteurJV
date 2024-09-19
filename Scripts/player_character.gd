extends CharacterBody2D

@onready var animated_sprite_2d = $AnimatedSprite2D
@export var SPEED = 2000

func _physics_process(delta: float) -> void:
	var direction := _getMovementDirection()
	_flipSprite(direction.x)
	_move(delta, direction)
	

func _move(delta: float, movement : Vector2) -> void:
	if (movement):
		velocity = movement * SPEED * delta
		animated_sprite_2d.play("Moving")
	else:
		velocity = velocity.move_toward(Vector2(0,0), SPEED * delta)
		animated_sprite_2d.play("Idle")
	move_and_slide()
	
func _flipSprite(horizontalMovement: float) -> void:
	if horizontalMovement < 0:
		animated_sprite_2d.flip_h = true
	elif horizontalMovement > 0:
		animated_sprite_2d.flip_h = false
	
func _getMovementDirection() -> Vector2:
	var horizontalDirection := Input.get_axis("left_axis", "right_axis")
	var verticalDirection := Input.get_axis("top_axis", "down_axis")
	var direction := Vector2(horizontalDirection,verticalDirection)
	
	# Normalize the direction
	if direction != Vector2.ZERO:
		direction = direction.normalized()
		
	return direction
	

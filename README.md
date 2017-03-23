# asteroid-defense

Known bugs:
* Missiles might hit another target while they are on their way to their primary one, could be fixed by targeting the missile only to a certain asteroid id
* Unity's Update is not quite fast enough to handle fast mouse movements, so asteroids the player has dragged may sometimes get the wrong direction because calculation was too late
* Asteroid will be shot at only once, so if shot at and then player changes direction before hit, turret won't shoot again, could be fixed by checking after expected hit time if asteroid destroyed or not, and then shoot again if not

Requirements:

Earth is being bombarded by asteroids and it’s your job to destroy them before they cause damage. You control a huge turret that can fire missiles at any asteroids that would come within a designated safety zone. 

* The simulation runs in a 2D top-down environment. For the purpose of this exercise, you can completely ignore the effect of gravity.
* Earth (and the turret) are located at the center of the screen.
* The player can “throw" asteroids towards earth (e.g. by dragging with mouse)
* When a new asteroid is spawned, the turret needs to check if the trajectory comes within the safety zone.
* If the asteroid would come too close, the turret needs to fire a missile, which intercepts the asteroid.

Your working implementation should include two methods with the following (or similar) signatures:
* bool TrajectoryWithinSafetyZone(Vector3 asteroidPosition, Vector3 asteroidVelocity)
* Vector3 CalculateMissileVelocity(Vector3 asteroidPosition, Vector3 asteroidVelocity)

Here are some other facts about the scenario:
* The turret can fire immediately into any direction. There is no targeting delay.
* The turret must fire immediately when a new (dangerous) asteroid is spawned.
* As there is no gravity, the velocity of an asteroid stays constant after it has been spawned.
* Missiles are always launched with a constant speed S. You only decide the angle of the velocity vector - not its magnitude.

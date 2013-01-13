#pragma strict

function Start () {

}

function Update () {
	var playerPlane = new Plane(Vector3.up, transform.position);
	
	var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	
	var hitDistance = 0.0;
	
	if(playerPlane.Raycast(ray, hitDistance)) {
		var targetPoint = ray.GetPoint(hitDistance);
		
		var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
		transform.rotation = targetRotation;
	}
}
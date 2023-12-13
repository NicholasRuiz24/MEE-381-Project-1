using Godot;
using System;

public partial class SimBeginScene : Node3D
{
MeshInstance3D Anchor;
MeshInstance3D Ball;
SpringModel spring;
Label keLabel;
Label peLabel;
Label teLabel;

PendSim pend;
double xA, yA, zA; // coords of anchor
double xB, yB, zB; // coords of ball
float length0; // natural length of pendulum
float length; // length of pendulum
double time;

Vector3 endA; // end point of anchor
Vector3 endB; // end point for pendulum bob

// Called when the node enters the scene tree for the first time.
public override void _Ready()
{
GD.Print("Hello MEE 381 in Godot!");
xA = 0.0; yA = 1.2; zA = 0.0;
Anchor = GetNode<MeshInstance3D>("Anchor");
Ball = GetNode<MeshInstance3D>("Ball");
spring = GetNode<SpringModel>("SpringModel");
endA = new Vector3((float)xA, (float)yA, (float)zA);
Anchor.Position = endA;

keLabel = GetNode<Label>("KELabel");
peLabel = GetNode<Label>("PELabel");
teLabel = GetNode<Label>("TELabel");

pend = new PendSim();

length0 = length = 0.9f;
spring.GenMesh(0.05f, 0.015f, length, 6.0f, 62);

endB.X = (float)(xA + pend.X_Position);
endB.Y = (float)(yA + pend.Y_Position);
endB.Z = (float)(zA + pend.Z_Position);
PlacePendulum(endB);

time = 0.0;
}

// Called every frame. 'delta' is the elapsed time since the previous frame.
public override void _Process(double delta)
{

//Shows the kinetic and potential energy inside the simulation
keLabel.Text = "Kinetic Energy: " + pend.KE.ToString("0.00") + " J";
peLabel.Text = "Potential Energy: " + pend.PE.ToString("0.00") + " J";
teLabel.Text = "Total Energy: " + pend.TE.ToString("0.00") + " J";

endB.X = (float)(pend.X_Position);
endB.Y = (float)(pend.Y_Position);
endB.Z = (float)(pend.Z_Position);

PlacePendulum(endB);
time += delta;
}

public override void _PhysicsProcess(double delta)
{
base._PhysicsProcess(delta);

pend.StepRK4(time, delta);
}

private void PlacePendulum(Vector3 endBB)
{

spring.PlaceEndPoints(endA, endB);
Ball.Position = endBB;
}
}

//============================================================================
// PendSim.cs : Defines derived class for simulating a simple pendulum.
//============================================================================
using System;
using Godot;

public class PendSim : Simulator
{
    double L0;     //Natural pendulum length
    double L;      //Pendulum length
    double k;      //Spring Constant
    double m;      //Mass of ball
    double KineticEnergy;
    double PotentialEnergy;
    double TotalEnergy;
    

    public PendSim() : base(6)
    {
        //Given Constants
        L0 = 0.9;   //Natural Length
        k = 90;     //Spring constant
        m = 1.4;    //Mass of ball

        //Springy Pendulum Parameters
        x[0] = -.7;   //x initial position
        x[1] = 0;     //x initial velocity
        x[2] = -.2;   //y initial position
        x[3] = 0;     //y initial velocity
        x[4] = .5;    //z initial position
        x[5] = .9;    //z initial velocity

        SetRHSFunc(RHSFuncPendulum);
    }

    //----------------------------------------------------
    // RHSFuncPendulum
    //----------------------------------------------------
    private void RHSFuncPendulum(double[] xx,double t, double[] ff)
    {
        //Calculates the current length of the spring
        L = Math.Sqrt(xx[0]*xx[0] + xx[2]*xx[2] + xx[4]*xx[4]);

        //Springy pendulum equations
        ff[0] = xx[1];
        ff[1] = ((-k*(L-L0)/L)*xx[0])/m;
        ff[2] = xx[3];
        ff[3] = (((-k*(L-L0)/L)*xx[2])/m)-g;
        ff[4] = xx[5];
        ff[5] = ((-k*(L-L0)/L)*xx[4])/m;

        //Energy Calculations
        KineticEnergy = .5*m*(x[1]*x[1]+x[3]*x[3]+x[5]*x[5]);
        PotentialEnergy = -(m*g*x[2]+ .5*k*(L-L0));
        TotalEnergy = KineticEnergy + PotentialEnergy;
    }

    //--------------------------------------------------------------------
    // Getters and Setters
    //--------------------------------------------------------------------
    public double X_Position
    {
        get{
            return(x[0]);
        }

        set{
            x[0] = value;
        }
    }

    public double Y_Position
    {
        get{
            return(x[2]);
        }

        set{
            x[2] = value;
        }
    }

    public double Z_Position
    {
        get{
            return(x[4]);
        }

        set{
            x[4] = value;
        }
    }
    public double KE
    {
        get{
            return(KineticEnergy);
        }
    }
    public double PE
    {
        get{
            return(PotentialEnergy);
        }
    }
    public double TE
    {
        get{
            return(TotalEnergy);
        }
    }
}
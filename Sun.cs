﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp1
{
    internal class Sun
    {
        public Color color;
        public int centreX, centreY;
        public double x, y, z;
        public DispatcherTimer timer;
        public List<(int x, int y)> trajectory;
        public int trI = 0, direction = 1;
        public Drawer drawer;

        public Sun(Color _color, double _x, double _y, double _z, Drawer _drawer)
        {
            color = _color;
            centreX = (int)_x;
            centreY = (int)_y;
            x = _x;
            y = _y;
            z = _z;
            drawer = _drawer;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += SunTimerEvent;
            trajectory = GetTrajectory(0.05, 15, 10);
        }

        void SunTimerEvent(object sender, EventArgs e)
        {
            x = trajectory[trI].x;
            y = trajectory[trI].y;
            if (trI == trajectory.Count - 1) direction = -1;
            if (trI == 0) direction = 1;
            trI += direction;
            Drawer.Redraw(drawer, this);
        }
        List<(int x, int y)> GetTrajectory(double scale, double delta, double revolutions)
        {
            trajectory = new List<(int x, int y)>();
            double X = centreX;
            double Y = centreY;
            trajectory.Add(((int)X, (int)Y));
            double theta = 0;
            double radius = 0;

            while (X > 0 && X < Drawer.bitmapHeight && Y > 0 && Y < Drawer.bitmapWidth && theta <= (revolutions * 360))
            {
                theta += delta;

                radius = (Math.Pow(theta / 180 * Math.PI, Math.E)) * scale;

                X = (radius * Math.Cos(theta / 180 * Math.PI)) + centreX;
                Y = (radius * Math.Sin(theta / 180 * Math.PI)) + centreY;

                trajectory.Add(((int)X, (int)Y));
            }

            return trajectory;
        }
    }
}
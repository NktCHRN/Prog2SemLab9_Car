﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLib
{
    public delegate void MaxMileageExcessHandler(object sender, CarEventArgs e);
    public class Car
    {
        public event MaxMileageExcessHandler MaxMileageExceeded;
        private const int _maxMileage = 999999;
        public int MaxSpeed { get; private set; }               // max speed, km/h
        public double MaxTankCapacity { get; private set; }
        public double FuelConsumption { get; private set; }     // litres per kilometer
        public bool IsStarted { get; private set; }
        public int Mileage { get; private set; }
        public double TankCapacity { get; private set; }        // in litres
        public Car(int maxSpeed, double maxTankCapacity, double fuelConsumption)
        {
            if (maxSpeed > 0)
                MaxSpeed = maxSpeed;
            else
                throw new ArgumentOutOfRangeException(nameof(maxSpeed), "Max speed cannot be lower than or equal zero");
            if (maxTankCapacity > 0)
                MaxTankCapacity = maxTankCapacity;
            else
                throw new ArgumentOutOfRangeException(nameof(maxTankCapacity), "Max tank capacity cannot be lower than or equal zero");
            if (fuelConsumption > 0)
                FuelConsumption = fuelConsumption;
            else
                throw new ArgumentOutOfRangeException(nameof(fuelConsumption), "Fuel consumption (litres per kilometer) cannot be lower than or equal zero");
            IsStarted = false;
            Mileage = 0;
            TankCapacity = 0;
        }
        public void Refuel(double fuelCapacity)
        {
            if (fuelCapacity >= 0 && TankCapacity + fuelCapacity <= MaxTankCapacity)
                TankCapacity += fuelCapacity;
            else
                throw new ArgumentOutOfRangeException(nameof(fuelCapacity), "Fuel capacity cannot be lower than zero or tank capacity cannot be bigger than max tank capacity");
        }
        public void Start()
        {
            if (TankCapacity > 0)
                IsStarted = true;
            else
                throw new InvalidOperationException("You cannot start an engine because your tank is empty");
        }
        public double Drive(int speed, int minutes)
        {
            if (minutes > 0)
            {
                if (speed > 0 && speed <= MaxSpeed)
                {
                    if (IsStarted)
                    {
                        const double minutesInHour = 60.0;
                        double distance = (double)speed * minutes / minutesInHour;
                        const double consumptionKilometrage = 100.0;
                        double fuelSpended = distance * FuelConsumption / consumptionKilometrage;
                        if (fuelSpended > TankCapacity)
                            throw new InvalidOperationException($"You cannot ride this distance because you do not have enough fuel (required: {fuelSpended:F1} litres you have: {TankCapacity:F1} litres)");
                        TankCapacity -= fuelSpended;
                        if (TankCapacity == 0)
                            Stop();
                        if (Mileage + distance <= _maxMileage)
                        {
                            Mileage += (int)Math.Round(distance);
                        }
                        else
                        {
                            if (MaxMileageExceeded != null)
                                MaxMileageExceeded(this, new CarEventArgs(Mileage, distance, _maxMileage));
                            Mileage = _maxMileage;
                        }
                        return distance;
                    }
                    else
                    {
                        throw new InvalidOperationException("You cannot drive car while it is not started");
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(speed), $"Speed cannot be lower than or equal zero or bigger than max speed ({MaxSpeed}) km/h");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), "Quantity of minutes be lower than or equal zero");
            }
        }
        public void Stop()
        {
            IsStarted = false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Threading;


namespace WpfApp1
{
    public class Model : INotifyPropertyChanged
    {

        //CSV fields
        private float[,] floatProperties;
        private double[,] doubleProperties;
        private SortedDictionary<int, string> data;

        //client fields
        private TcpClient client;
        private bool stop;

        // start method fields
        private int sleep; // = 100/speed
        private int currentRow; // in csv file


        //time and speed propeties
        private int currentTime; // in seconds: hours = currentTime/3600, minutes = currentTime/60, seconds = currentTime%60
        public int CurrentTime
        {
            get { return currentTime; }
            set
            {
                /**
                 * we read from the csv file in a normal speed 10 rows per second
                 * for example, if we jump 5 seconds forward we need to jump 10*5 rows, (value - current time = 5)
                 */
                currentRow = currentRow + 10 * (value - currentTime);
                currentTime = value;
                NotifyPropertyChanged("CurrentTime");
            }
        }
        private float speed;
        public float Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                sleep = (int)(100 / speed);
                NotifyPropertyChanged("Speed");
            }
        }

        //event
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        // CTOR
        public Model()
        {
            speed = 1;
            currentTime = 0;
            currentRow = 0;
            sleep = (int)(100 / speed); //or switch to 100
            stop = false;
            csvParser(); //##
        }

        //other properties and fields
        private float aileron;
        public float Aileron
        {
            get { return aileron; }
        }
        private float elevator;
        public float Elevator
        {
            get { return elevator; }
        }
        private float rudder;
        public float Rudder
        {
            get { return rudder; }
        }
        private float flaps;
        public float Flaps
        {
            get { return flaps; }
        }
        private float slats;
        public float Slats
        {
            get { return slats; }
        }
        private float speedBrake;
        public float SpeedBrake
        {
            get { return speedBrake; }
        }
        private float throttle0;
        public float Throttle0
        {
            get { return throttle0; }
        }
        private float throttle1;
        public float Throttle1
        {
            get { return throttle1; }
        }
        private float enginePump0;
        public float EnginePump0
        {
            get { return enginePump0; }
        }
        private float enginePump1;
        public float EnginePump1
        {
            get { return enginePump1; }
        }
        private float electricPump0;
        public float ElectricPump0
        {
            get { return electricPump0; }
        }
        private float electricPump1;
        public float ElectricPump1
        {
            get { return electricPump1; }
        }
        private float externalPower;
        public float ExternalPower
        {
            get { return externalPower; }
        }
        private float apuGenerator;
        public float ApuGenerator
        {
            get { return apuGenerator; }
        }
        private double latitudeDeg;
        public double LatitudeDeg
        {
            get { return latitudeDeg; }
        }
        private double longitudeDeg;
        public double LongitudeDeg
        {
            get { return longitudeDeg; }
        }
        private float altitudeFd;
        public float AltitudeFd
        {
            get { return altitudeFd; }
        }
        private float rollDeg;
        public float RollDeg
        {
            get { return rollDeg; }
        }
        private float pitchDeg;
        public float PitchDeg
        {
            get { return pitchDeg; }
        }
        private float headingDeg;
        public float HeadingDeg
        {
            get { return headingDeg; }
        }
        private float yaw; //side-slip-deg
        public float Yaw
        {
            get { return yaw; }
        }
        private float airspeedKt;
        public float AirspeedKt
        {
            get { return airspeedKt; }
        }
        private float glideslope;
        public float Glideslope
        {
            get { return glideslope; }
        }
        private float verticalSpeedFps;
        public float VerticalSpeedFps
        {
            get { return verticalSpeedFps; }
        }
        private float airspeedIndicatorIndicatedSpeedKt;
        public float AirspeedIndicatorIndicatedSpeedKt
        {
            get { return airspeedIndicatorIndicatedSpeedKt; }
        }
        private float altimeter_indicated_altitude_ft;
        public float Altimeter_indicated_altitude_ft
        {
            get { return altimeter_indicated_altitude_ft; }
        }
        private float altimeter_pressure_alt_ft;
        public float Altimeter_pressure_alt_ft
        {
            get { return altimeter_pressure_alt_ft; }
        }
        private float attitude_indicator_indicated_pitch_deg;
        public float Attitude_indicator_indicated_pitch_deg
        {
            get { return attitude_indicator_indicated_pitch_deg; }
        }
        private float attitude_indicator_indicated_roll_deg;
        public float Attitude_indicator_indicated_roll_deg
        {
            get { return attitude_indicator_indicated_roll_deg; }
        }
        private float attitude_indicator_internal_pitch_deg;
        public float Attitude_indicator_internal_pitch_deg
        {
            get { return attitude_indicator_internal_pitch_deg; }
        }
        private float attitude_indicator_internal_roll_deg;
        public float Attitude_indicator_internal_roll_deg
        {
            get { return attitude_indicator_internal_roll_deg; }
        }
        private float encoder_indicated_altitude_ft;
        public float Encoder_indicated_altitude_ft
        {
            get { return encoder_indicated_altitude_ft; }
        }
        private float encoder_pressure_alt_ft;
        public float Encoder_pressure_alt_ft
        {
            get { return encoder_pressure_alt_ft; }
        }
        private float gps_indicated_altitude_ft;
        public float Gps_indicated_altitude_ft
        {
            get { return gps_indicated_altitude_ft; }
        }
        private float gps_indicated_ground_speed_kt;
        public float Gps_indicated_ground_speed_kt
        {
            get { return gps_indicated_ground_speed_kt; }
        }
        private float gps_indicated_vertical_speed;
        public float Gps_indicated_vertical_speed
        {
            get { return gps_indicated_vertical_speed; }
        }
        private float indicated_heading_deg;
        public float Indicated_heading_deg
        {
            get { return indicated_heading_deg; }
        }
        private float magnetic_compass_indicated_heading_deg;
        public float Magnetic_compass_indicated_heading_deg
        {
            get { return magnetic_compass_indicated_heading_deg; }
        }
        private float slip_skid_ball_indicated_slip_skid;
        public float Slip_skid_ball_indicated_slip_skid
        {
            get { return slip_skid_ball_indicated_slip_skid; }
        }

        private float turn_indicator_indicated_turn_rate;
        public float Turn_indicator_indicated_turn_rate
        {
            get { return turn_indicator_indicated_turn_rate; }
        }
        private float vertical_speed_indicator_indicated_speed_fpm;
        public float Vertical_speed_indicator_indicated_speed_fpm
        {
            get { return vertical_speed_indicator_indicated_speed_fpm; }
        }
        private float engine_rpm;
        public float Engine_rpm
        {
            get { return engine_rpm; }
        }


        // CSV parser

        private void csvParser()
        {
            /**
             * the key is the number of row in file (currentRow)
             * the value is row itself (the properties's values seperated by ',' as one string)
             */
            data = new SortedDictionary<int, string>();
            StreamReader reader = new StreamReader("reg_flight.csv");
            string line;
            int row = 0;
            while ((line = reader.ReadLine()) != null)
            {
                data[row++] = line + Environment.NewLine;
            }
            reader.Close();
            //42 properties: 40 float, 2 double
            /**
             * save the properties's values in an multidimnesional array
             * to find a property in a certain csv row: properties[row][property-index]
             * properties 15 and 16 are doubles and stored in different array
             */
            floatProperties = new float[row, 40];
            doubleProperties = new double[row, 2];
            for (int i = 0; i < row; i++)
            {
                string[] properties = data[i].Split(',');
                int newj = 0; //index used by float properties, needed because properties 15 and 16 are in different array
                for (int j = 0; j < properties.Length; j++)
                {
                    //Console.WriteLine(properties[j]);
                    //properties 15 and 16 are double
                    if (j != 14)
                    {
                        floatProperties[i, newj++] = float.Parse(properties[j]);
                    }
                    else
                    {
                        doubleProperties[i, 0] = double.Parse(properties[j++]); // j = 14
                        doubleProperties[i, 1] = double.Parse(properties[j]); // j = 15
                    }
                }
            }
        }


        //client-server
        public void connect()
        {
            client = new TcpClient("localhost", 5400);
        }

        public void start()
        {
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            new Thread(delegate ()
            {
                while (!stop)
                {
                    writer.Write(data[currentRow]);
                    writer.Flush();
                    updateProperties();
                    currentRow++;
                    currentTime = currentRow / 10; // one second contains 10 rows
                    Thread.Sleep(sleep);
                }
            }).Start();

        }


        public void disconnect()
        {
            stop = true;
            client.Close();
        }

        private void updateProperties()
        {
            int i = 0;
            aileron = floatProperties[currentRow, i++]; //0
            elevator = floatProperties[currentRow, i++];
            rudder = floatProperties[currentRow, i++];
            flaps = floatProperties[currentRow, i++];
            slats = floatProperties[currentRow, i++];
            speedBrake = floatProperties[currentRow, i++]; //5
            throttle0 = floatProperties[currentRow, i++]; // ######
            throttle1 = floatProperties[currentRow, i++];
            enginePump0 = floatProperties[currentRow, i++];
            enginePump1 = floatProperties[currentRow, i++];
            electricPump0 = floatProperties[currentRow, i++]; //10
            electricPump1 = floatProperties[currentRow, i++];
            externalPower = floatProperties[currentRow, i++];
            apuGenerator = floatProperties[currentRow, i++]; //13
            latitudeDeg = doubleProperties[currentRow, 0];
            longitudeDeg = doubleProperties[currentRow, 1];
            altitudeFd = floatProperties[currentRow, i++]; //14
            rollDeg = floatProperties[currentRow, i++]; //15 #######
            pitchDeg = floatProperties[currentRow, i++];
            headingDeg = floatProperties[currentRow, i++];
            yaw = floatProperties[currentRow, i++];
            airspeedKt = floatProperties[currentRow, i++]; // #######
            glideslope = floatProperties[currentRow, i++]; //20
            verticalSpeedFps = floatProperties[currentRow, i++];
            airspeedIndicatorIndicatedSpeedKt = floatProperties[currentRow, i++];
            altimeter_indicated_altitude_ft = floatProperties[currentRow, i++]; // ????
            altimeter_pressure_alt_ft = floatProperties[currentRow, i++]; // ????
            attitude_indicator_indicated_pitch_deg = floatProperties[currentRow, i++]; //25
            attitude_indicator_indicated_roll_deg = floatProperties[currentRow, i++];
            attitude_indicator_internal_pitch_deg = floatProperties[currentRow, i++];
            attitude_indicator_internal_roll_deg = floatProperties[currentRow, i++];
            encoder_indicated_altitude_ft = floatProperties[currentRow, i++];
            encoder_pressure_alt_ft = floatProperties[currentRow, i++]; //30
            gps_indicated_altitude_ft = floatProperties[currentRow, i++];
            gps_indicated_ground_speed_kt = floatProperties[currentRow, i++];
            gps_indicated_vertical_speed = floatProperties[currentRow, i++];
            indicated_heading_deg = floatProperties[currentRow, i++];
            magnetic_compass_indicated_heading_deg = floatProperties[currentRow, i++]; //35
            slip_skid_ball_indicated_slip_skid = floatProperties[currentRow, i++];
            turn_indicator_indicated_turn_rate = floatProperties[currentRow, i++];
            vertical_speed_indicator_indicated_speed_fpm = floatProperties[currentRow, i++];
            engine_rpm = floatProperties[currentRow, i++]; //39
        }

        /**
         * 
         * Helper Method can be deleted before submitting
         * 
         */
        public void helper()
        {
            new Thread(delegate ()
            {
                //Console.WriteLine("START");
                while (!stop)
                {
                    updateProperties();
                    currentRow++;
                 //   Console.WriteLine(currentRow);
                 //   Console.WriteLine(currentTime);
                    currentTime = currentRow / 10;
                    NotifyPropertyChanged("CurrentTime");
                    if (currentRow == 2000)
                    {
                        stop = true;
                    }
                    Thread.Sleep(10);
                }
                //Console.WriteLine("finish");
            }).Start();
        }
    }
}
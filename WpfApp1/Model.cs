using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Threading;


namespace FIApp
{
    public class Model : INotifyPropertyChanged
    {

        //CSV fields
      //  private string csvPath;
        private double[,] doubleProperties;
        private SortedDictionary<int, string> data;
        private int numberOfRows;

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
                if (speed != 0)
                {
                    sleep = (int)(100 / speed);
                }
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
         //   csvPath = null;
            speed = 1;
            currentTime = 0;
            currentRow = 0;
            sleep = (int)(100 / speed);
            stop = false;
            csvParser(); //##
        }
        public double[,] getProperties()
        {
            return doubleProperties;
        }
        //other properties and fields
        private double aileron;
        public double Aileron
        {
            get { return aileron; }
        }
        private double elevator;
        public double Elevator
        {
            get { return elevator; }
        }
        private double rudder;
        public double Rudder
        {
            get { return rudder; }
        }
        private double flaps;
        public double Flaps
        {
            get { return flaps; }
        }
        private double slats;
        public double Slats
        {
            get { return slats; }
        }
        private double speedBrake;
        public double SpeedBrake
        {
            get { return speedBrake; }
        }
        private double throttle0;
        public double Throttle0
        {
            get { return throttle0; }
        }
        private double throttle1;
        public double Throttle1
        {
            get { return throttle1; }
        }
        private double enginePump0;
        public double EnginePump0
        {
            get { return enginePump0; }
        }
        private double enginePump1;
        public double EnginePump1
        {
            get { return enginePump1; }
        }
        private double electricPump0;
        public double ElectricPump0
        {
            get { return electricPump0; }
        }
        private double electricPump1;
        public double ElectricPump1
        {
            get { return electricPump1; }
        }
        private double externalPower;
        public double ExternalPower
        {
            get { return externalPower; }
        }
        private double apuGenerator;
        public double ApuGenerator
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
        private double altitudeFd;
        public double AltitudeFd
        {
            get { return altitudeFd; }
        }
        private double rollDeg;
        public double RollDeg
        {
            get { return rollDeg; }
        }
        private double pitchDeg;
        public double PitchDeg
        {
            get { return pitchDeg; }
        }
        private double headingDeg;
        public double HeadingDeg
        {
            get { return headingDeg; }
        }
        private double yaw; //side-slip-deg
        public double Yaw
        {
            get { return yaw; }
        }
        private double airspeedKt;
        public double AirspeedKt
        {
            get { return airspeedKt; }
        }
        private double glideslope;
        public double Glideslope
        {
            get { return glideslope; }
        }
        private double verticalSpeedFps;
        public double VerticalSpeedFps
        {
            get { return verticalSpeedFps; }
        }
        private double airspeedIndicatorIndicatedSpeedKt;
        public double AirspeedIndicatorIndicatedSpeedKt
        {
            get { return airspeedIndicatorIndicatedSpeedKt; }
        }
        private double altimeter_indicated_altitude_ft;
        public double Altimeter_indicated_altitude_ft
        {
            get { return altimeter_indicated_altitude_ft; }
        }
        private double altimeter_pressure_alt_ft;
        public double Altimeter_pressure_alt_ft
        {
            get { return altimeter_pressure_alt_ft; }
        }
        private double attitude_indicator_indicated_pitch_deg;
        public double Attitude_indicator_indicated_pitch_deg
        {
            get { return attitude_indicator_indicated_pitch_deg; }
        }
        private double attitude_indicator_indicated_roll_deg;
        public double Attitude_indicator_indicated_roll_deg
        {
            get { return attitude_indicator_indicated_roll_deg; }
        }
        private double attitude_indicator_internal_pitch_deg;
        public double Attitude_indicator_internal_pitch_deg
        {
            get { return attitude_indicator_internal_pitch_deg; }
        }
        private double attitude_indicator_internal_roll_deg;
        public double Attitude_indicator_internal_roll_deg
        {
            get { return attitude_indicator_internal_roll_deg; }
        }
        private double encoder_indicated_altitude_ft;
        public double Encoder_indicated_altitude_ft
        {
            get { return encoder_indicated_altitude_ft; }
        }
        private double encoder_pressure_alt_ft;
        public double Encoder_pressure_alt_ft
        {
            get { return encoder_pressure_alt_ft; }
        }
        private double gps_indicated_altitude_ft;
        public double Gps_indicated_altitude_ft
        {
            get { return gps_indicated_altitude_ft; }
        }
        private double gps_indicated_ground_speed_kt;
        public double Gps_indicated_ground_speed_kt
        {
            get { return gps_indicated_ground_speed_kt; }
        }
        private double gps_indicated_vertical_speed;
        public double Gps_indicated_vertical_speed
        {
            get { return gps_indicated_vertical_speed; }
        }
        private double indicated_heading_deg;
        public double Indicated_heading_deg
        {
            get { return indicated_heading_deg; }
        }
        private double magnetic_compass_indicated_heading_deg;
        public double Magnetic_compass_indicated_heading_deg
        {
            get { return magnetic_compass_indicated_heading_deg; }
        }
        private double slip_skid_ball_indicated_slip_skid;
        public double Slip_skid_ball_indicated_slip_skid
        {
            get { return slip_skid_ball_indicated_slip_skid; }
        }

        private double turn_indicator_indicated_turn_rate;
        public double Turn_indicator_indicated_turn_rate
        {
            get { return turn_indicator_indicated_turn_rate; }
        }
        private double vertical_speed_indicator_indicated_speed_fpm;
        public double Vertical_speed_indicator_indicated_speed_fpm
        {
            get { return vertical_speed_indicator_indicated_speed_fpm; }
        }
        private double engine_rpm;
        public double Engine_rpm
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
            StreamReader reader = new StreamReader("C:\\Users\\taco9\\Desktop\\reg_flight.csv"); // #### change to csvPath or reg_flight.csv!!!!!!!!!!/
            string line;
            int row = 0;
            numberOfRows = 0;
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
            numberOfRows = row - 1;
            doubleProperties = new double[row, 42];
            for (int i = 0; i < row; i++)
            {
                string[] properties = data[i].Split(',');
                int newj = 0; //index used by float properties, needed because properties 15 and 16 are in different array
                for (int j = 0; j < properties.Length; j++)
                {
                    doubleProperties[i, newj++] = double.Parse(properties[j]);
                }
            }
            
        }


        //client-server
        public void connect()
        {
//            csvParser(); //##
            client = new TcpClient("localhost", 5400);
        }

        public void start()
        {
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            new Thread(delegate ()
            {
                while (!stop && currentRow <= numberOfRows)
                {
                    if (speed == 0)
                    {
                        continue;
                    }
                    writer.Write(data[currentRow]);
                    writer.Flush();
                    updateProperties();
                    currentRow++;
                    currentTime = currentRow / 10; // one second contains 10 rows
                    NotifyPropertyChanged("CurrentTime"); //added
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
            aileron = doubleProperties[currentRow, i++]; //0
            elevator = doubleProperties[currentRow, i++];
            rudder = doubleProperties[currentRow, i++];
            flaps = doubleProperties[currentRow, i++];
            slats = doubleProperties[currentRow, i++];
            speedBrake = doubleProperties[currentRow, i++]; //5
            throttle0 = doubleProperties[currentRow, i++];
            throttle1 = doubleProperties[currentRow, i++];
            enginePump0 = doubleProperties[currentRow, i++];
            enginePump1 = doubleProperties[currentRow, i++];
            electricPump0 = doubleProperties[currentRow, i++]; //10
            electricPump1 = doubleProperties[currentRow, i++];
            externalPower = doubleProperties[currentRow, i++];
            apuGenerator = doubleProperties[currentRow, i++];
            latitudeDeg = doubleProperties[currentRow, i++];
            longitudeDeg = doubleProperties[currentRow, i++]; //15
            altitudeFd = doubleProperties[currentRow, i++]; 
            rollDeg = doubleProperties[currentRow, i++];
            pitchDeg = doubleProperties[currentRow, i++];
            headingDeg = doubleProperties[currentRow, i++];
            yaw = doubleProperties[currentRow, i++]; //20
            airspeedKt = doubleProperties[currentRow, i++];
            glideslope = doubleProperties[currentRow, i++]; 
            verticalSpeedFps = doubleProperties[currentRow, i++];
            airspeedIndicatorIndicatedSpeedKt = doubleProperties[currentRow, i++];
            altimeter_indicated_altitude_ft = doubleProperties[currentRow, i++]; // 25
            altimeter_pressure_alt_ft = doubleProperties[currentRow, i++];
            attitude_indicator_indicated_pitch_deg = doubleProperties[currentRow, i++];
            attitude_indicator_indicated_roll_deg = doubleProperties[currentRow, i++];
            attitude_indicator_internal_pitch_deg = doubleProperties[currentRow, i++];
            attitude_indicator_internal_roll_deg = doubleProperties[currentRow, i++]; //30
            encoder_indicated_altitude_ft = doubleProperties[currentRow, i++];
            encoder_pressure_alt_ft = doubleProperties[currentRow, i++];
            gps_indicated_altitude_ft = doubleProperties[currentRow, i++];
            gps_indicated_ground_speed_kt = doubleProperties[currentRow, i++];
            gps_indicated_vertical_speed = doubleProperties[currentRow, i++]; //35
            indicated_heading_deg = doubleProperties[currentRow, i++];
            magnetic_compass_indicated_heading_deg = doubleProperties[currentRow, i++];
            slip_skid_ball_indicated_slip_skid = doubleProperties[currentRow, i++];
            turn_indicator_indicated_turn_rate = doubleProperties[currentRow, i++];
            vertical_speed_indicator_indicated_speed_fpm = doubleProperties[currentRow, i++]; //40
            engine_rpm = doubleProperties[currentRow, i++];
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
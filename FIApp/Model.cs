using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Xml;

namespace FIApp
{
    public class Model : INotifyPropertyChanged
    {
        //CSV fields
        private SortedDictionary<int, string> dataForFlightGear; //save number of row and row's data as string
        private int numberOfRows; //number of rows in csv file
        private string csvPath; //csv path, from the user
        private double[][] featuresData; //save features's data from csv file
        private SortedDictionary<string, List<double>> csvData; //save all csv data

        //xml
        public List<string> features; //save features's names

        //client fields
        private TcpClient client;
        private bool stop;

        // start method fields
        private int sleep; // = 100/speed
        private int currentRow; // in csv file
        public int maxTime_s;

        //time and speed propeties
        private int currentTime; // in seconds; hours = currentTime/3600, minutes = currentTime/60, seconds = currentTime%60
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

        private decimal speed;
        public decimal Speed
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

        public string CsvPath
        {
            set { csvPath = value; }
        }

        //event
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }


        // CTOR
        public Model()
        {
            csvPath = "reg_flight.csv"; //default
            speed = 0; //normal speed
            currentTime = 0;
            currentRow = 0;
            sleep = 0;
            stop = false;
            features = new List<string>();
        }

        //other properties and fields
        string currentFeature;
        public string CurrentFeature
        {
            set
            {
                this.currentFeature = value;
                NotifyPropertyChanged("CurrentFeature");
            }
            get
            {
                return this.currentFeature;
            }
        }

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
        private double throttle0;
        public double Throttle0
        {
            get { return throttle0; }
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
        private double altimeter_indicated_altitude_ft;
        public double Altimeter_indicated_altitude_ft
        {
            get { return altimeter_indicated_altitude_ft; }
        }
        //properties names for csvData and features list
        private string aileronName = null;
        private string rudderName = null;
        private string throttleName = null;
        private string elevatorName = null;
        private string altimeterName = null;
        private string airspeedName = null;
        private string headingName = null;
        private string yawName = null;
        private string rollName = null;
        private string pitchName = null;

        //xml reader
        private void xmlParser()
        {
            features = new List<string>();
            string file = Properties.Resources.playback_small;
            bool input = false;
            int idx = 0;
            using (XmlReader xmlReader = XmlReader.Create(new StringReader(file)))
            {
                while (xmlReader.Read() && !input)
                {
                    if (xmlReader.IsStartElement())
                    {
                        switch (xmlReader.Name.ToString())
                        {
                            case "input":
                                input = true;
                                break;
                            case "name":
                                string name = xmlReader.ReadString();
                                string featureName = String.Concat(Convert.ToString(idx++), "_", name);
                                features.Add(featureName);
                                switch (name)
                                {
                                    case "throttle":
                                        if (throttleName == null)
                                            throttleName = featureName;
                                        break;
                                    case "aileron":
                                        aileronName = featureName;
                                        break;
                                    case "elevator":
                                        elevatorName = featureName;
                                        break;
                                    case "altimeter_indicated-altitude-ft":
                                        altimeterName = featureName;
                                        break;
                                    case "heading-deg":
                                        headingName = featureName;
                                        break;
                                    case "airspeed-kt":
                                        airspeedName = featureName;
                                        break;
                                    case "roll-deg":
                                        rollName = featureName;
                                        break;
                                    case "pitch-deg":
                                        pitchName = featureName;
                                        break;
                                    case "side-slip-deg":
                                        yawName = featureName;
                                        break;
                                    case "rudder":
                                        rudderName = featureName;
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
        }

        // CSV parser
        private void csvParser()
        {
            xmlParser();
            /**
             * dataForFlightGear: the key is the number of row in file (currentRow)
             * the value is row itself (the properties's values seperated by ',' as one string)
             */
            dataForFlightGear = new SortedDictionary<int, string>();
            csvData = new SortedDictionary<string, List<double>>();
            StreamReader reader = new StreamReader(csvPath);
            string line;
            int row = 0;
            numberOfRows = 0;
            while ((line = reader.ReadLine()) != null)
            {
                dataForFlightGear[row++] = line + Environment.NewLine;
            }
            reader.Close();
            numberOfRows = row - 1;
            for (int j = 0; j < features.Count; j++)
            {
                csvData[features[j]] = new List<double>();
            }
            for (int i = 0; i < row; i++)
            {
                string[] properties = dataForFlightGear[i].Split(',');
                for (int j = 0; j < properties.Length; j++)
                {
                    csvData[features[j]].Add(double.Parse(properties[j]));
                }
            }
            maxTime_s = numberOfRows / 10;

            featuresData = new double[10][];
            featuresData[0] = csvData[aileronName].ToArray(); //0 = aileron
            featuresData[1] = csvData[elevatorName].ToArray(); //1 = elevator
            featuresData[2] = csvData[rudderName].ToArray(); // 2 = rudder
            featuresData[3] = csvData[throttleName].ToArray(); // 3 = throttle
            featuresData[4] = csvData[rollName].ToArray(); // 4 = roll
            featuresData[5] = csvData[pitchName].ToArray(); // 5 = pitch
            featuresData[6] = csvData[headingName].ToArray(); // 6 = heading
            featuresData[7] = csvData[yawName].ToArray(); // 7 = yaw
            featuresData[8] = csvData[airspeedName].ToArray(); // 8 = airspeed
            featuresData[9] = csvData[altimeterName].ToArray(); // 9 = altimeter
        }

        //client-server
        public void connect()
        {
            csvParser(); //## HERE
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
                    writer.Write(dataForFlightGear[currentRow]);
                    writer.Flush();
                    updateProperties();
                    currentRow++;
                    currentTime = currentRow / 10; // one second contains 10 rows
                    //check later
                    if (currentTime >= maxTime_s)
                    {
                        currentTime = maxTime_s;
                        NotifyPropertyChanged("CurrentTime");
                        return;
                    }

                    NotifyPropertyChanged("CurrentTime"); //check if only when CurrentTime acctually changes
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
            aileron = featuresData[i++][currentRow];
            elevator = featuresData[i++][currentRow];
            rudder = featuresData[i++][currentRow];
            throttle0 = featuresData[i++][currentRow];
            rollDeg = featuresData[i++][currentRow];
            pitchDeg = featuresData[i++][currentRow];
            headingDeg = featuresData[i++][currentRow];
            yaw = featuresData[i++][currentRow];
            airspeedKt = featuresData[i++][currentRow];
            altimeter_indicated_altitude_ft = featuresData[i++][currentRow];
        }

        public List<double> getDataByFeatureName(string feature)
        {

            //check later
            if (feature == null || !csvData.ContainsKey(feature))
            {
                return new List<double> { };
            }

            return csvData[feature];
        }


        /**
         * 
         * Helper Method can be deleted before submitting
         * 
         */
        public void helper()
        {
            csvParser();
            Console.WriteLine("helper-method");
            new Thread(delegate ()
            {

/**
                while (!stop)
                {
                    updateProperties();
                    currentRow++;
                    currentTime = currentRow / 10;
                    NotifyPropertyChanged("CurrentTime");
                    if (currentRow == 2000)
                    {
                        stop = true;
                    }
                    Thread.Sleep(10);
*/

                while (true)
                {
                    if (Speed == 0)
                    {
                        continue;
                    }

                    updateProperties();

                    currentRow++;
                    currentTime = currentRow / 10; // one second contains 10 rows

                    if (currentTime >= maxTime_s)
                    {
                        currentTime = maxTime_s;
                        NotifyPropertyChanged("CurrentTime");
                        return;
                    }

                    NotifyPropertyChanged("CurrentTime");
                    Console.WriteLine(currentTime);
                    Thread.Sleep(sleep);
                }
            }).Start();
        }
    }
}


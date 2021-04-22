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

        //xml
        public List<string> features; //save features's names

        //CSV fields
        private string csvPath; //csv path, from the user
        private SortedDictionary<int, string> dataForFlightGear; //save number of row and row's data as string
        private SortedDictionary<string, List<double>> csvData; //maps each feature to a list of its values
        private double[][] featuresData; //save some features's data in a different way
        private int numberOfRows; //number of rows in csv file

        //client fields
        private TcpClient client;
        private bool stop;

        // start method fields
        private int sleep; // = 100/speed
        private int currentRow; // in csv file
        public int maxTime_s;

        //time and speed properties
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

        //property changed event
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
            csvPath = "anomaly_flight.csv"; //default
            speed = 0; 
            sleep = 0;
            currentTime = 0;
            currentRow = 0;
            stop = false;
            features = new List<string>();
        }

        
        // xml parser - extracts the propeties's names from the file and saves them in a list. 
        private void xmlParser()
        {
            string file = Properties.Resources.playback_small;
            bool input = false;
            int idx = 0;
            using (XmlReader xmlReader = XmlReader.Create(new StringReader(file)))
            {
                // the features are written twice in the file- one time under the label "input" and the second under "output".
                // we want to read the names only once.
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
                                //save each name with its index
                                string featureName = String.Concat(Convert.ToString(idx++), "_", name);
                                features.Add(featureName);
                                // we save some of the features's "new" names(the original names combined with the indexs),
                                // in order to access them later.
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
             * the value is the row itself (the features's values seperated by ',' as one string)
             */
            dataForFlightGear = new SortedDictionary<int, string>();
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
            csvData = new SortedDictionary<string, List<double>>();
            //adds the features as keys to the dictionary
            for (int j = 0; j < features.Count; j++)
            {
                csvData[features[j]] = new List<double>();
            }
            //insert the features's values
            for (int i = 0; i < row; i++)
            {
                string[] properties = dataForFlightGear[i].Split(',');
                for (int j = 0; j < properties.Length; j++)
                {
                    csvData[features[j]].Add(double.Parse(properties[j]));
                }
            }
            maxTime_s = numberOfRows / 10;
            
            //save the values of some features in a jaggedArray - 
            //helps us to easily access a feature's single value.
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

        //update the properties according to the current row
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
            if (feature == null || !csvData.ContainsKey(feature))
            {
                return new List<double> { };
            }

            return csvData[feature];
        }
        
        //client-server
        public void connect()
        {
            csvParser(); 
            client = new TcpClient("localhost", 5400);
        }

        //start the communication with the flightGear server
        public void start()
        {
            Stream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            new Thread(delegate ()
            {
                while (!stop && currentRow <= numberOfRows)
                {
                    //If the speed is zero we temporarily stop sending information to the server
                    if (speed == 0)
                    {
                        continue;
                    }
                    //send the current csv row to flightGear
                    writer.Write(dataForFlightGear[currentRow]);
                    writer.Flush();
                    updateProperties();
                    currentRow++;
                    currentTime = currentRow / 10; // one second contains 10 rows
                    //if currentTime >= maxTime_s it means we finished sending all of the flight data
                    if (currentTime >= maxTime_s)
                    {
                        currentTime = maxTime_s;
                        NotifyPropertyChanged("CurrentTime");
                        return;
                    }

                    NotifyPropertyChanged("CurrentTime");
                    Thread.Sleep(sleep);
                }
            }).Start();

        }

        public void disconnect()
        {
            stop = true;
            client.Close();
        }

    }
}

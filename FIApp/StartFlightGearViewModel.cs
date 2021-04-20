using System;


namespace FIApp.ViewModels
{
    class StartFlightGearViewModel
    {
        readonly Model model;

        public StartFlightGearViewModel(Model model)
        {
            this.model = model;
        }

        //set the path to the csv file where the data for the flightGear simulator is stored
        public string VM_CsvPath
        {
            set { model.CsvPath = value; }
        }

        //connect to the flightGear server and start the communication
        public void startFlight()
        {
            model.connect();
            model.start();
        }
    }
}
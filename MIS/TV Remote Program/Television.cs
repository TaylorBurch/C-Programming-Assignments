namespace TaylorBurchLab7
{
    class Television
    {
        //Television Blueprint 

        private string MANUFACTURER = ""; //Manufactuer or brand of TV
        private int SCREEN_SIZE = 0; //Size of screen of TV
        private int volume = 0; //Volume set on TV
        private int channel = 0; //Channel TV is set to
        private bool powerOn = false; //Is TV on or off


        public Television(string manu, int size) //Set TV manufacturer and size and initialize power, volume, and channel
        {
            MANUFACTURER = manu;
            SCREEN_SIZE = size;

            powerOn = false;
            volume = 20;
            channel = 2;
        }

        public void setChannel(int station) //Set channel to the station number selected 
        {
            channel = station;
        }

        public void power() //Set TV to On or Off
        {
            if (powerOn == true)
            {
                powerOn = !powerOn;
            }
            else
            {
                powerOn = true;
            }

        }

        public void increaseVolume() //Increases volume by 1 when called
        {
            volume++;
        } 

        public void decreaseVolume() //Decreases volume by 1 when called. 
        {
            volume--;
        }

        public int getChannel()
        {
            return channel;
        }

        public int getVolume()
        {
            return volume;
        }

        public string getManufacturer()
        {
            return MANUFACTURER;
        }

        public int getScreenSize()
        {
            return SCREEN_SIZE;
        }

        
    }
}
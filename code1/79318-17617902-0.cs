            System.IO.StreamReader file = new System.IO.StreamReader(str1);
            line = file.ReadLine();
            Int32 ctn=0;
            try
            {
                while ((line = file.ReadLine()) != null)
                {
                        if (Counter == ctn)
                        {
                            MessageBox.Show("I am here");
                            ctn=ctn+5;
                            continue;
                        }
                        else
                        {
                            Counter++;
                            MessageBox.Show(Counter.ToString());
                        } 
                    }
               
                file.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show("Documentation");
            }

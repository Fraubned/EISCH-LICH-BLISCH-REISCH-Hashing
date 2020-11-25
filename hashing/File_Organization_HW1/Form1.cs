using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Organization_HW1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public int[] Rand_Array = new int[900];

        Random rnd;
        public void Generate900Random()
        {
            rnd = new Random();
            for (int i = 0; i < Rand_Array.Length; i++)
            {
                Rand_Array[i] = rnd.Next(1, 15000);
            }
        }

        //Beginning of EISCH Method
        //Packing factor should be nearly %90. So the dimensions of link and key arrays are 997.
        public static float eischProbe = 0;
        public int[] eischKey = new int[997];
        public int[] eischLink = new int[997];

        public void addToEISCH()
        {
            for (int i = 0; i < 900; i++)
            {
                int location = Rand_Array[i] % 997;
                int last = location;
                if (eischKey[location] == 0)
                {
                    eischProbe++;
                    eischKey[location] = Rand_Array[i];
                }
                else
                {
                    eischProbe++;
                    while (eischKey[last]!= 0 && last < 997)
                    {
                        last = last + 1;
                        if (last == 997)
                        {
                            break;
                        }
                    }
                    if (last == 997)
                    {
                        last = location;
                        while (eischKey[last] != 0)
                        {
                            last = last - 1;
                        }
                    }
                    eischKey[last] = Rand_Array[i];

                    if (eischLink[location] != 0)
                    {
                        int temp = eischLink[location];
                        eischLink[temp] = last;
                    }
                    else
                    {
                        eischProbe++;
                        eischLink[location] = last;
                    }
                }
            }
        }
        // End of Eisch Method

        //Beginning of LICH method
        public static int primary = (int)(997 * 0.86);
        public static int overflow = (int)(997 * 0.14);
        public int[] lichKey = new int[primary+overflow];
        public int[] lichLink = new int[primary+overflow];
        public static float lichProbe = 0;

        public void addToLICH()
        {
            for (int i = 0; i < Rand_Array.Length; i++)
            {
                int location = Rand_Array[i] % primary;
                int last = primary + overflow;

                if (lichKey[location] == 0)
                {
                    lichProbe++;
                    lichKey[location] = Rand_Array[i];
                }

                else
                {
                    lichProbe++;
                    last = last - 1;
                    if (lichKey[last] == 0)
                    {
                        lichProbe++;
                        lichKey[last] = Rand_Array[i];
                        lichLink[location] = last;
                    }

                    else
                    {
                        while (lichKey[last]!=0)
                        {
                            last = last - 1;
                        }
                        lichKey[last] = Rand_Array[i];
                        lichLink[location] = last;
                        lichProbe++;
                    }

                    lichLink[location] = last;
                }
            }
        }
        //End of LICH method



        //Beginning Of BLISCH Method
        public static float blischProbe = 0;
        public int[] blischKey = new int[997];
        public int[] blischLink = new int[997];
        public void addToBLISCH()
        {
            int up = 0;
            int down = 996;
            for (int i = 0; i < Rand_Array.Length; i++)
            {
                int location = Rand_Array[i] % 997;
                if (blischKey[location] == 0)
                {
                    blischKey[location] = Rand_Array[i];
                    blischProbe++;
                }
                else
                {
                    blischProbe++;
                    if (blischLink[location]!=0)
                    {
                        blischProbe++;
                        while (blischLink[location]==0)
                        {
                            location = blischLink[location];
                            
                        }
                    }

                    while (true)
                    {
                        if (blischKey[up]==0)
                        {
                            blischProbe++;
                            blischKey[up] = Rand_Array[i];
                            blischLink[location] = up;
                            break;
                        }
                        else
                        {
                            up=up+1;
                            if (blischKey[down]==0)
                            {
                                blischProbe++;
                                blischKey[down] = Rand_Array[i];
                                blischLink[location] = down;
                                break;
                            }

                            else
                            {
                                down=down-1;
                            }
                        }
                    }
                }
            }
        }
        //End Of BLISCH method

        //Beginning of  REISCH method
        public int[] reischKey = new int[997];
        public int[] reischLink = new int[997];
        public static float reischProbe = 0;
        public void addToREISCH()
        {
            Random reisch = new Random();
            int rand_reisch = reisch.Next(0, 997);
            for (int i = 0; i < Rand_Array.Length; i++)
            {
                int location = Rand_Array[i] % 997;
                if (reischKey[location] == 0)
                {
                    reischKey[location] = Rand_Array[i];
                    reischProbe++;
                }
                else
	            {
                    reischProbe++;
                    if (reischLink[location]!=0)
                    {
                        reischProbe++;
                        while (reischLink[location]==0)
                        {
                            location = reischLink[location];
                        }
                    }

                    while (reischKey[rand_reisch]!=0)
                    {
                        rand_reisch = reisch.Next(0, 997);
                    }
                    reischKey[rand_reisch] = Rand_Array[i];
                    reischLink[location] = rand_reisch;
                    reischProbe++;
                }
            }
        }
        //End of REISCH method

        public static int sEISCH = 0;
        public static bool EISCHfindYes = false;
        public void searchEISCH(int searching)
        {
            int location = searching % 997;

            if (eischKey[location]!=0)
            {
                while (EISCHfindYes == false)
                {
                    sEISCH++;
                    if (eischKey[location] == searching)
                    {
                        EISCHfindYes = true;
                        break;
                    }
                    else
                    {
                        if (eischLink[location]==0)
                        {
                            MessageBox.Show("Aranan değer bulunamadı!!");
                            break;
                        }
                        else
                        {
                            location = eischLink[location];
                        }
                    }
                    }

                }
            else
            {
                MessageBox.Show("Aranan değer bulunamadı!!");
            }

        }


        public static int BLISCH = 0;
        public static bool BLISCHfindYes = false;
        public void searchBLISCH(int searching)
        {
            int location = searching % 997;

            if (blischKey[location] != 0)
            {
                while (BLISCHfindYes == false)
                {
                    BLISCH++;
                    if (blischKey[location] == searching)
                    {
                        BLISCHfindYes = true;
                        break;
                    }
                    else
                    {
                        if (blischLink[location] == 0)
                        {
                            MessageBox.Show("Aranan değer bulunamadı!!");
                            break;
                        }
                        else
                        {
                            location = blischLink[location];
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Aranan değer bulunamadı!!");
            }

        }

        public static int REISCH = 0;
        public static bool REISCHfindYes = false;
        public void searchREISCH(int searching)
        {
            int location = searching % 997;

            if (reischKey[location] != 0)
            {
                while (REISCHfindYes == false)
                {
                    REISCH++;
                    if (reischKey[location] == searching)
                    {
                        REISCHfindYes = true;
                        break;
                    }
                    else
                    {
                        if (reischLink[location] == 0)
                        {
                            MessageBox.Show("Aranan değer bulunamadı!!");
                            break;
                        }
                        else
                        {
                            location = reischLink[location];
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Aranan değer bulunamadı!!");
            }

        }

        public static int LICH = 0;
        public static bool LICHfindYes = false;
        public void searchLICH(int searching)
        {
            int location = searching % primary;

            if (lichKey[location] != 0)
            {
                while (LICHfindYes == false)
                {
                    LICH++;
                    if (lichKey[location] == searching)
                    {
                        LICHfindYes = true;
                        break;
                    }
                    else
                    {
                        if (eischLink[location] == 0)
                        {
                            MessageBox.Show("Aranan değer bulunamadı!!");
                            break;
                        }
                        else
                        {
                            location = eischLink[location];
                            if (lichKey[location]==0)
                            {
                                MessageBox.Show("Aranan değer bulunamadı!!");
                                break;
                            }
                            else
                            {
                                while (lichKey[location]==0 || location ==0)
                                {
                                    location = location - 1;
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Aranan değer bulunamadı!!");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Generate900Random();
            for (int i = 0; i < Rand_Array.Length; i++)
            {
                RandList.Items.Add(Rand_Array[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            eisch_list.Items.Clear();
            lich_list.Items.Clear();
            eisch_link.Items.Clear();
            lich_link.Items.Clear();
            addToEISCH();
            addToLICH();
            addToBLISCH();
            addToREISCH();

            for (int i = 0; i < 900; i++)
            {
                eisch_list.Items.Add(i + ". key = " + eischKey[i]);
                eisch_link.Items.Add(i + ". link = " + eischLink[i]);
                lich_list.Items.Add(i + ". key = " + lichKey[i]);
                lich_link.Items.Add(i + ". link = " + lichLink[i]);
                blisch_list.Items.Add(i + ". key = " + blischKey[i]);
                blisch_link.Items.Add(i + ". link = " + blischLink[i]);
                reisch_list.Items.Add(i + ". key = " + reischKey[i]);
                reisch_link.Items.Add(i + ". link = " + reischLink[i]);
            }
            //We put 900 element in 997 area. So our packing factor is 90.2
            label9.Text = (90.2).ToString();
            label11.Text = (90.2).ToString();
            label13.Text = (90.2).ToString();
            label15.Text = (90.2).ToString();

            //Average Probes
            string av_eisch = Convert.ToString(eischProbe / 900);
            string av_lich = Convert.ToString(lichProbe / 900);
            string av_blisch = Convert.ToString(blischProbe / 900);
            string av_reisch = Convert.ToString(reischProbe / 900);
            label10.Text = av_eisch;
            label12.Text = av_lich;
            label14.Text = av_blisch;
            label16.Text = av_reisch;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int searching = Convert.ToInt32(textBox1.Text);
            searchEISCH(searching);
            label28.Text = Convert.ToString(sEISCH);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int searching = Convert.ToInt32(textBox2.Text);
            searchLICH(searching);
            label29.Text = Convert.ToString(LICH);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int searching = Convert.ToInt32(textBox3.Text);
            searchBLISCH(searching);
            label32.Text = Convert.ToString(BLISCH);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int searching = Convert.ToInt32(textBox4.Text);
            searchREISCH(searching);
            label35.Text = Convert.ToString(REISCH);
        }    }
}

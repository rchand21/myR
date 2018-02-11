using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banyan_Batcher_Network
{
    public partial class Form1 : Form
    {
        static int n = 8;
        Node[] A = new Node[n/2];
        Node[] B = new Node[n/2];
        Node[] C = new Node[n/2];
        int numberofbits = Convert.ToString(n-1, 2).ToCharArray().Length;
        int step;
        List<int> sortedinput;
        List<int> inputTable = new List<int>();
       

        public Form1()
        {
            InitializeComponent();
            refresh();
            textBox1.BackColor = Color.Green;
            tb0.Select();
            CreateTable();
        }
        void refresh()
        {
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = new Node();
                B[i] = new Node();
                C[i] = new Node();
            }
            sortedinput = new List<int>();
            step = 0;
        }
        //function that creates the table with routes from input to output ports
        void CreateTable()
        {
            for(int i=0;i<n;i++)
            {
                char[] binary = (Convert.ToString(i, 2).PadLeft(numberofbits, '0')).ToCharArray();

               Node Ap= InsertValue(binary[0], A, null, i);
                Node Bp = InsertValue(binary[1],B, Ap, -1);
                Node Cp = InsertValue(binary[2],C, Bp, -1);
                Cp.table.Add(i);
            }
           
        }
       Node InsertValue(char binary, Node[] A, Node B, int i)
        {
            
                if ((int)Char.GetNumericValue(binary) == 0)
                {
                    for (int j = 0; j < A.Length; j++)
                    {
                        if (A[j].Input0 == true)
                        {
                            continue;
                        }
                        else
                        {
                            A[j].Input0 = true;
                            if (i != -1)
                            {
                            inputTable.Add(j);
                            }
                            else
                            {
                            B.table.Add(j);
                        }
                        return A[j];
                        }
                    }
                }
                else if ((int)Char.GetNumericValue(binary) == 1)
                {
                    for (int j = 0; j < A.Length; j++)
                    {
                        if (A[j].Input1 == true)
                        {
                            continue;
                        }
                        else
                        {
                            A[j].Input1 = true;
                            if (i != -1)
                            {
                            inputTable.Add(j);
                            }
                        else
                        {
                            B.table.Add(j);
                        }
                        return A[j];
                        }
                    }
                }
            return null;
        }
        //function to run the batcher banyan algorithm to stepsin order to view the paths
        private void button1_Click_1(object sender, EventArgs e)
        {
            bool NoInput = true;
            if(step==0)
            {
                
                foreach (Control c in gbIn.Controls)
                {
                    if(c.Text!="")
                    {
                        NoInput = false;
                        break;
                    }
                }
                if(NoInput==true)
                {
                    MessageBox.Show("Please enter input");
                }
                else
                {
                    Batcher();
                    step++;
                }
            }
            else if(step==1)
            {
                Banyan(A,B,0);
                step++;
            }
            else if(step==2)
            {
                Banyan(B, C,1);
                step++;
            }
            else if(step==3)
            {
                Outputter();
            }
        }
        //function Batcher that sorts and arranges the input values at te=he input port
        void Batcher()
        {

            textBox2.BackColor = Color.Green;
                foreach (Control c in gbIn.Controls)
            {
                if(c.Text!="")
                {
                    sortedinput.Add(Convert.ToInt16(c.Text));
                    //c.Text = "";
                }
            }
            sortedinput.Sort();
            for(int i= 0;i < sortedinput.Count;i++)
            {
                gbInputs.Controls[i].Text = sortedinput[i].ToString();
            }
        }
        //function Banyan that looks up the table for the next node based on 0 and 1 input values 
        void Banyan(Node[] Node1,Node[] Node2,int index)
        {
            textBox3.BackColor = Color.Green;
            if (step == 1)
            {
                for (int i = 0; i < sortedinput.Count; i++)
                {
                    int index1 = inputTable[i];
                    Node1[index1].Value.Add(Convert.ToString(sortedinput[i], 2).PadLeft(numberofbits, '0'));
                    Node1[index1].intnum.Add(sortedinput[i]);
                }
            }
            for (int i = 0; i < Node1.Count(); i++)
            {
                for (int j = 0; j < Node1[i].intnum.Count; j++)
                {
                        {
                            char[] binaryvalue = Node1[i].Value[j].ToCharArray();
                            int indexofBinary = (int)Char.GetNumericValue(binaryvalue[index]);
                            int index2 = Node1[i].table[indexofBinary];
                            Node2[index2].Value.Add(Node1[i].Value[j]);
                            Node2[index2].intnum.Add(Node1[i].intnum[j]);
                            Control[] gbA;
                            if (step == 1)
                            {
                                gbA = this.Controls.Find("A" + i, true);
                            }
                            else
                            {
                                gbA = this.Controls.Find("B" + i, true);
                            }

                            foreach (GroupBox c in gbA)
                            {
                                c.Controls[ indexofBinary].Text = Node1[i].intnum[j].ToString();
                            }
                        }
                       
                    
                }
            }

        }
        
        void Outputter()
        {
            textBox4.BackColor = Color.Green;
            for (int i = 0; i < C.Length; i++)
            {
                for(int j=0;j<C[i].intnum.Count;j++)
                {
                    {
                        int index2;
                        char[] binaryvalue = C[i].Value[j].ToCharArray();
                        {
                            index2 = C[i].table[(int)Char.GetNumericValue(binaryvalue[2])];
                        }
                        Control[] gbA;
                        gbA = this.Controls.Find("Output", true);
                        foreach (GroupBox c in gbA)
                        {
                            c.Controls[index2].Text = C[i].intnum[j].ToString();
                        }
                    }
                    }
                }
            }
        //Function to clear values on button clear click
        private void button2_Click(object sender, EventArgs e)
        {
            sortedinput = new List<int>();
            step = 0;
            
            for (int i=0;i<A.Length;i++)
            {
                
                    A[i].intnum.Clear();
                    A[i].Value.Clear();
                    B[i].Value.Clear();
                    C[i].Value.Clear();
                    B[i].intnum.Clear();
                    C[i].intnum.Clear();
                
            }
            foreach (Control c in groupBox2.Controls)
            {
                c.BackColor = Color.White;
            }
            foreach(Control c in gbIn.Controls)
            {
                c.Text = "";
            }
            foreach(Control gb in groupBox1.Controls)
            {
                if(gb is GroupBox)
                {
                    foreach(Control lb in gb.Controls)
                    {
                        lb.Text = "";
                    }
                }
            }
            textBox1.BackColor = Color.Green;
        }

    }
}

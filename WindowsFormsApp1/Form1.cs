﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private async void button1_Click(object sender, EventArgs e)
        private void button1_Click(object sender, EventArgs e)
        {
            string ssmsgIn = "Hello Event Hubs Async to Sync!";
            string ssmsgOut = "";
            ClassLibraryOSTest.Sender.Send(ssmsgIn, out ssmsgOut);

            //string ssmsgOut = await ssmsgOutTask;



            //string ssmsgIn = "Hello Event Hubs Async to Sync!";
            //Task<string> ssmsgOutTask = ClassLibraryOSTest.Sender.Send2(ssmsgIn);

            ////string ssmsgOut = ssmsgOutTask.Result;
            //string ssmsgOut = await ssmsgOutTask;

        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace run_batch_files_test
{
    public partial class Form1 : Form
    {
        // create new list
        List<string> _items = new List<string>();

        public Form1()
        {
            InitializeComponent();

            // gets the data source for these items
            listBox1.DataSource = _items;
        }

        // run scripts button
        private void button1_Click(object sender, EventArgs e)
        {
            // get string from listbox at selected index
            string text = listBox1.GetItemText(listBox1.SelectedItem);

            label1.Text = text;

            // handles starting up stupid command prompt and stuff
            var processInfo = new ProcessStartInfo("cmd.exe", "/c" + text);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            // reads error to log
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            process.Start();

            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
        }

        // add scripts location button
        private void button2_Click(object sender, EventArgs e)
        {
            // add button was clicked
            button2.Enabled = true;

            _items.Add(textBox1.Text);

            listBox1.DataSource = null;
            listBox1.DataSource = _items;
        }

        // remove scripts location button at selected index
        private void button3_Click(object sender, EventArgs e)
        {
            // check to see if collections exists
            if (listBox1.Items.Count == 0)
            {
                button2.Enabled = false;
            }

            // create new integer object selected index
            int selectedIndex = listBox1.SelectedIndex;

            try
            {
                // remove item in the list
                _items.RemoveAt(selectedIndex);
            }
            catch
            {

            }
            listBox1.DataSource = null;
            listBox1.DataSource = _items;
        }
    }
}

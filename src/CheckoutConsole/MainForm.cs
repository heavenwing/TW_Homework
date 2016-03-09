using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdminConsole.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckoutConsole
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            var output = await GetComputingResultFromServer();

            var writer = new StringWriter();
            if (Printer == null)
                Printer = new RawResultPrinter();
            Printer.Print(output,writer);
            txtOutput.Text = writer.ToString();
        }

        public IResultPrinter Printer { get; set; }

        private async Task<ComputeResultDto> GetComputingResultFromServer()
        {
            var data = JsonConvert.DeserializeObject<string[]>(txtInput.Text);
            var api = new HttpClient();
            api.BaseAddress = new Uri("http://localhost:61852/");
            var response = await api.PostAsJsonAsync("http://localhost:61852/api/ComputeApi", data);
            return await response.Content.ReadAsAsync<ComputeResultDto>();
        }
    }
}

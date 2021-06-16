using CommonLibrary.Business;
using CommonLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interface.UI
{
    public partial class Form1 : Form
    {


        private  List<MassaDadosModel> a_ListReturnFullDados;
        private List<AgrupamentoModel> a_Conta_AgrupamentoModel;
        private List<AgrupamentoModel> a_Ativo_AgrupamentoModel;
        private List<AgrupamentoModel> a_TipoOperacao_AgrupamentoModel;
        private DateTime DatatInicioRequest;
        private DateTime DatatFinalRequest;
        private double a_seconds;
        Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //comboBoxAgrup.DataSource = Enum.GetValues(typeof(AgrupamentoEnum));
        }

        private void carregaGridFull(List<MassaDadosModel> p_massaDadosModels)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("datetime", typeof(string));
                dt.Columns.Add("tipoOperacao", typeof(string));
                dt.Columns.Add("ativo", typeof(string));
                dt.Columns.Add("quantidade", typeof(int));
                dt.Columns.Add("preco", typeof(double));
                dt.Columns.Add("conta", typeof(int));
                DataRow dr = dt.NewRow();

                foreach (var item in p_massaDadosModels)
                {
                    dr = dt.NewRow();
                    dr["id"] = item.id;
                    dr["datetime"] = item.datetime;
                    dr["tipoOperacao"] = item.tipoOperacao;
                    dr["ativo"] = item.ativo;
                    dr["quantidade"] = item.quantidade;
                    dr["preco"] = item.preco;
                    dr["conta"] = item.conta;
                    
                    dt.Rows.Add(dr);

                }
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }

        private string ExecRequestAgrupamento(int p_agrupamentoEnum) 
        {
            try{ 
                RequestBusiness _request = new RequestBusiness();
                var json = _request.RunRequest("https://localhost:44313/Agrupamento/" + p_agrupamentoEnum.ToString());
                return json;
             }catch(Exception ex) 
            {
                MessageBox.Show("Erro ao consultar dados: " + ex.Message);
                return "";
            }
        }

        private void carregaAgrupamento(int p_agrupamentoEnum)
        {
            try
            {
                List<AgrupamentoModel> _genericLIst =new List<AgrupamentoModel>();
                var json = "";
                this.DatatInicioRequest = DateTime.Now;
                switch (p_agrupamentoEnum)
                {
                    case 1: // Conta
                        if (a_Conta_AgrupamentoModel == null ) 
                        {
                            json = ExecRequestAgrupamento(p_agrupamentoEnum);
                            if (string.IsNullOrEmpty(json)) { return;  }
                            a_Conta_AgrupamentoModel = JsonConvert.DeserializeObject<List<AgrupamentoModel>>(json);
                            _genericLIst = a_Conta_AgrupamentoModel;
                        }
                        else 
                        {
                            _genericLIst = a_Conta_AgrupamentoModel;
                        }
                        break;
                    case 2: // Ativo
                        if (a_Ativo_AgrupamentoModel == null)
                        {
                            json = ExecRequestAgrupamento(p_agrupamentoEnum);
                            if (string.IsNullOrEmpty(json)) { return; }
                            a_Ativo_AgrupamentoModel = JsonConvert.DeserializeObject<List<AgrupamentoModel>>(json);
                            _genericLIst = a_Ativo_AgrupamentoModel;
                        }
                        else {_genericLIst = a_Ativo_AgrupamentoModel; }

                        break;
                    case 3: // TipoOperacao
                        if (a_TipoOperacao_AgrupamentoModel == null) 
                        {
                            json = ExecRequestAgrupamento(p_agrupamentoEnum);
                            if (string.IsNullOrEmpty(json)) { return; }
                            a_TipoOperacao_AgrupamentoModel = JsonConvert.DeserializeObject<List<AgrupamentoModel>>(json);
                            _genericLIst = a_TipoOperacao_AgrupamentoModel;
                        }
                        else { _genericLIst = a_TipoOperacao_AgrupamentoModel; }
                        break;
                    default:
                        MessageBox.Show("Erro " + "Tipo de agrupamento inválido");
                        break;
                }
                this.DatatFinalRequest = DateTime.Now;

                if ( _genericLIst== null  ) { return; }
                if ( _genericLIst.Count == 0 ) { return; }


                DataTable dt = new DataTable();
                dt.Columns.Add("descricao", typeof(string));
                dt.Columns.Add("quantidade", typeof(int));
                dt.Columns.Add("precomedio", typeof(double));
                
                DataRow dr = dt.NewRow();

                foreach (var item in _genericLIst)
                {
                    dr = dt.NewRow();
                    dr["descricao"] = item.descricao;
                    dr["quantidade"] = item.quantidade;
                    dr["precomedio"] = item.precomedio;
                    dt.Rows.Add(dr);
                }
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }

        private void ExportarExcel() 
        {
            if (dataGridView1.Rows.Count > 0)
            {
                try
                {
                    XcelApp.Application.Workbooks.Add(Type.Missing);
                    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                    {
                        XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    }
                    
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    
                    XcelApp.Columns.AutoFit();
                    
                    XcelApp.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    XcelApp.Quit();
                }
            }
            else 
            {
                MessageBox.Show("Sem dados a Exportar");
            }
        }

        private void gerarCSV() 
        {
            try 
            { 
                //before your loop
                var csv = new StringBuilder();
                var linha = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    linha = "";
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            if (!string.IsNullOrEmpty(linha))
                                linha += ";";

                            linha += cell.Value.ToString();
                        }
                            
                    }
                    if (!string.IsNullOrEmpty(linha))
                        csv.AppendLine(linha);
                }
                if (csv.Length == 0)
                {
                    MessageBox.Show("Sem dados a Exportar");
                }
                else 
                { 
                    Guid _guid = Guid.NewGuid();
                    string fullPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "");
                    fullPath += @"\" + _guid.ToString() + ".csv";
                    //var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(path);
                    File.WriteAllText(fullPath, csv.ToString());
                    System.Diagnostics.Process.Start(fullPath);
                }
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var _agrup = comboBoxAgrup.SelectedIndex;

            if (_agrup == 0) 
            { 
                if (a_ListReturnFullDados == null )
                { 
                    RequestBusiness _request = new RequestBusiness();
                    this.DatatInicioRequest = DateTime.Now;
                    var json = _request.RunRequest("https://localhost:44313/GetFullDados");
                    this.DatatFinalRequest = DateTime.Now;
                    a_ListReturnFullDados = JsonConvert.DeserializeObject<List<MassaDadosModel>>(json);
                    this.carregaGridFull(a_ListReturnFullDados);
                    a_seconds = Math.Round(((this.DatatFinalRequest - this.DatatInicioRequest).TotalSeconds), 2);
                    labelTempoExec.Text = a_seconds.ToString();
                }
                else
                {
                    this.DatatInicioRequest = DateTime.Now;
                    this.carregaGridFull(a_ListReturnFullDados);
                    this.DatatFinalRequest = DateTime.Now;
                    a_seconds = Math.Round(((this.DatatFinalRequest - this.DatatInicioRequest).TotalSeconds), 2);
                    labelTempoExec.Text = a_seconds.ToString();

                }
            }
            else 
            {
                this.carregaAgrupamento(_agrup);
                a_seconds =  Math.Round( ( (this.DatatFinalRequest- this.DatatInicioRequest).TotalSeconds),2);
                labelTempoExec.Text = a_seconds.ToString();
            }
        }

        private void buttonCSV_Click(object sender, EventArgs e)
        {
            gerarCSV();
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }
    }
}

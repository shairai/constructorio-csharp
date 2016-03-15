using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConstructorIOClient;

namespace Sample_App__Add_Modify_Delete__
{
    public partial class frmSample : Form
    {
        public ConstructorIO m_constructorClient;

        string m_sActionValue;
        string m_sOldName;
        List<string> m_arKeywords = new List<string>();

        public frmSample()
        {
            InitializeComponent();
            this.AlignText();
            pictureBoxLoading.BackColor = Color.Transparent;
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAction.SelectedItem.ToString() == "Add")
            {
                if (dataGridViewData.Rows.Count == 0)
                {
                    dataGridViewData.Rows.Add();
                }
                this.InsertSampleData();
            }

            m_sActionValue = comboBoxAction.SelectedItem.ToString();
        }


        private void InsertSampleData()
        {
            if (chkRequired.Checked)
            {
                dataGridViewData.Rows[0].Cells["item_name"].Value = "item 1";
                dataGridViewData.Rows[0].Cells["autocomplete_section"].Value = comboBoxType.SelectedItem.ToString();

                if (dataGridViewData.Rows[0].Cells["autocomplete_section"].Value.ToString() == "Products")
                    dataGridViewData.Rows[0].Cells["url"].Value = "www.item1.com";
            }

            if (chkOptional.Checked)
            {
                dataGridViewData.Rows[0].Cells["suggested_score"].Value = "1";
                dataGridViewData.Rows[0].Cells["keywords"].Value = "a,b,c";
                dataGridViewData.Rows[0].Cells["url"].Value = "www.item1.com";
                dataGridViewData.Rows[0].Cells["image_url"].Value = "www.item1.com/image";
                dataGridViewData.Rows[0].Cells["description"].Value = "description";
                dataGridViewData.Rows[0].Cells["id"].Value = "id1";
            }            
        }

        private Dictionary<string, object> LoadDataToDictionary()
        {
            Dictionary<string, object> objDic = new Dictionary<string, object>();

            if(dataGridViewData.Rows[0].Cells["suggested_score"].Value != null)
                objDic.Add("suggested_score", Convert.ToInt32(dataGridViewData.Rows[0].Cells["suggested_score"].Value));

            if (dataGridViewData.Rows[0].Cells["keywords"].Value != null)
            {
                this.GenerateListOfKeywords();
                //objDic.Add("keywords:", "[" + m_arKeywords.ToString() + "]");
                objDic.Add("keywords", "[\"a\",\"b\",\"c\"]");
            }
            if (dataGridViewData.Rows[0].Cells["url"].Value != null)
                objDic.Add("url", dataGridViewData.Rows[0].Cells["url"].Value);

            if (dataGridViewData.Rows[0].Cells["image_url"].Value != null)
                objDic.Add("image_url", dataGridViewData.Rows[0].Cells["image_url"].Value.ToString());

            if (dataGridViewData.Rows[0].Cells["description"].Value != null)
                objDic.Add("description", dataGridViewData.Rows[0].Cells["description"].Value.ToString());

            if (dataGridViewData.Rows[0].Cells["id"].Value != null)
                objDic.Add("id", dataGridViewData.Rows[0].Cells["id"].Value.ToString());

            return objDic;
        }

        private void AlignText()
        {
            foreach (DataGridViewColumn col in dataGridViewData.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            pictureBoxLoading.Visible = true;
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bool bResult;

                m_constructorClient = new ConstructorIO(txtAPI.Text, txtKey.Text);

                switch (m_sActionValue)
                {
                    case "Add":
                           bResult = m_constructorClient.Add(dataGridViewData.Rows[0].Cells["item_name"].Value.ToString(), dataGridViewData.Rows[0].Cells["autocomplete_section"].Value.ToString(), this.LoadDataToDictionary());
                        break;
                    case "Modify ( name of item )":
                        if(dataGridViewData.Rows[0].Cells["autocomplete_section"].Value.ToString() == "Products")
                            bResult = m_constructorClient.Modify(m_sOldName, dataGridViewData.Rows[0].Cells["item_name"].Value.ToString(), dataGridViewData.Rows[0].Cells["autocomplete_section"].Value.ToString(), this.LoadDataToDictionary());
                        else
                            bResult = m_constructorClient.Modify(m_sOldName, dataGridViewData.Rows[0].Cells["item_name"].Value.ToString(), dataGridViewData.Rows[0].Cells["autocomplete_section"].Value.ToString());
                        break;
                    case "Delete":
                        bResult = m_constructorClient.Remove(dataGridViewData.Rows[0].Cells["item_name"].Value.ToString(), dataGridViewData.Rows[0].Cells["autocomplete_section"].Value.ToString());
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBoxLoading.Visible = false;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dataGridViewData.Rows.Count > 0)
                dataGridViewData.Rows[0].Cells["autocomplete_section"].Value = comboBoxType.SelectedItem.ToString();

            this.ClearData();
        }

        private void chkRequired_CheckedChanged(object sender, EventArgs e)
        {
            this.ClearData();
        }

        private void ClearData()
        {
            if (dataGridViewData.Rows.Count > 0)
            {
                this.InsertSampleData();

                if (!chkRequired.Checked)
                {
                    dataGridViewData.Rows[0].Cells["item_name"].Value = null;
                    dataGridViewData.Rows[0].Cells["autocomplete_section"].Value = null ;

                    if (comboBoxType.SelectedItem.ToString() == "Products")
                        dataGridViewData.Rows[0].Cells["url"].Value = null;
                }

                if (!chkOptional.Checked)
                {
                    dataGridViewData.Rows[0].Cells["suggested_score"].Value = null;
                    dataGridViewData.Rows[0].Cells["keywords"].Value =null;
                    if (comboBoxType.SelectedItem.ToString() != "Products")
                        dataGridViewData.Rows[0].Cells["url"].Value = null;
                    dataGridViewData.Rows[0].Cells["image_url"].Value = null;
                    dataGridViewData.Rows[0].Cells["description"].Value = null;
                    dataGridViewData.Rows[0].Cells["id"].Value = null;
                }
            }

            dataGridViewData.Refresh();
        }

        private void chkOptional_CheckedChanged(object sender, EventArgs e)
        {
            this.ClearData();
        }

        private void dataGridViewData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
                m_sOldName = dataGridViewData[0, 0].Value.ToString();
        }

        private List<string> GenerateListOfKeywords()
        {
           m_arKeywords = ((string[])(dataGridViewData.Rows[0].Cells["keywords"].Value.ToString().Split(','))).ToList();
           return ((string[])(dataGridViewData.Rows[0].Cells["keywords"].Value.ToString().Split(','))).ToList();
        }
    }
}

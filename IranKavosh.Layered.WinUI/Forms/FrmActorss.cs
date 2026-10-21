using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IranKavosh.Layered.WinUI.Forms
{
    public partial class FrmActorss : DevExpress.XtraEditors.XtraForm
    {
        public List<ModelActors> _actorsList;
        public string _description;

        public class ModelActors
        {
            public Guid Id { get; set; }
            public string En { get; set; }
            public string Fa { get; set; }
        }
        public FrmActorss(List<ModelActors> actorsList, string description)
        {
            _actorsList = actorsList;
            _description = description;
            InitializeComponent();
            this.Shown += FrmActorss_Shown;
        }

        private void FrmActorss_Shown(object? sender, EventArgs e)
        {
            foreach (ModelActors i in _actorsList)
            {
                memoEn.Text += i.En + Environment.NewLine;
                memoFa.Text += i.Fa + Environment.NewLine;
                memoId.Text += i.Id + Environment.NewLine;
            }

            memoDes.Text = _description;
        }

        private void FrmActorss_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < memoFa.Lines.Length; i++)
            {
                if (!string.IsNullOrEmpty(memoFa.Lines[i]))
                {
                    _actorsList[i].En = memoEn.Lines[i];
                    _actorsList[i].Fa = memoFa.Lines[i];
                }
            }
            _description = memoDes.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _actorsList = null;
            Close();

        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmChangePassword : Form
    {
        public NetworkData NetworkEdit;
        public FrmChangePassword()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载
        /// </summary>
        private void FrmChangePassword_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            PasswordKind kind;
            if (cbxKind.SelectedIndex == 0)
                kind = PasswordKind.Manager;
            else
                kind = PasswordKind.User;
            if (!NetworkEdit.CheckPassword(edtOldPassword.Text, kind))
                CommonTools.MessageShow("原密码错误!请先链接网络!", 2, "");
            else
                NetworkEdit.ChangePassword(edtNewPassword.Text, kind);
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ConfigDevice
{
    /// <summary>
    /// 背景指令
    /// </summary>
    public class ViewBackgroundControl : ViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcSoundSource;//音源
        GridColumn dcVolume;//音量
        GridColumn dcPlayOrder;//播放方式
        GridColumn dcPlayNum;//播放曲目
        GridColumn dcPlayTime;//播放时间
        Background background;//背景
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxSoundSource;//选择音源   
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxPlayOrder;//选择播放方式

        public ViewBackgroundControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            background = controlObj as Background;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcSoundSource = ViewSetting.Columns.ColumnByName("parameter1");
            dcVolume = ViewSetting.Columns.ColumnByName("parameter2");
            dcPlayOrder = ViewSetting.Columns.ColumnByName("parameter3");
            dcPlayNum = ViewSetting.Columns.ColumnByName("parameter4");
            dcPlayTime = ViewSetting.Columns.ColumnByName("parameter5");

            cbxSoundSource = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxSoundSource.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            cbxPlayOrder = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxPlayOrder.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            InitViewSetting();
        } 

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            dcSoundSource.Visible = true;
            dcVolume.Visible = true;
            dcPlayOrder.Visible = true;
            dcPlayNum.Visible = true;
            dcPlayTime.Visible = true;

            ViewSetting.Columns.ColumnByName("parameter1").VisibleIndex = 6;
            ViewSetting.Columns.ColumnByName("parameter2").VisibleIndex = 7;
            ViewSetting.Columns.ColumnByName("parameter3").VisibleIndex = 8;
            ViewSetting.Columns.ColumnByName("parameter4").VisibleIndex = 9;
            ViewSetting.Columns.ColumnByName("parameter5").VisibleIndex = 10;

            cbxCommandKind.Items.Add(Background.NAME_CMD_SWIT_ALL_MUSIC);
            cbxCommandKind.Items.Add(Background.NAME_CMD_SWIT_OPEN_MUSIC);
            cbxCommandKind.Items.Add(Background.NAME_CMD_SWIT_CLOSE_MUSIC);

            dcSoundSource.Caption = "音源";
            cbxSoundSource.Items.Add(Background.NAME_SOURCE_MP3);
            cbxSoundSource.Items.Add(Background.NAME_SOURCE_RADIO);
            cbxSoundSource.Items.Add(Background.NAME_SOURCE_AUX1);
            cbxSoundSource.Items.Add(Background.NAME_SOURCE_AUX2);
            dcSoundSource.ColumnEdit = cbxSoundSource;

            dcVolume.Caption = "音量";
            dcVolume.ColumnEdit = edtPercentNum;

            dcPlayOrder.Caption = "播放方式";
            cbxPlayOrder.Items.Add(Background.NAME_PLAY_ORDER_ONE);
            cbxPlayOrder.Items.Add(Background.NAME_PLAY_ORDER_ONE_LOOP);
            cbxPlayOrder.Items.Add(Background.NAME_PLAY_ORDER);
            cbxPlayOrder.Items.Add(Background.NAME_PLAY_ORDER_LOOP);
            cbxPlayOrder.Items.Add(Background.NAME_PLAY_ORDER_RANDOM);
            cbxPlayOrder.Items.Add(Background.NAME_PLAY_INVALID);
            dcPlayOrder.ColumnEdit = cbxPlayOrder;

            dcPlayNum.Caption = "曲目";
            dcPlayNum.ColumnEdit = edtNum;
            dcPlayTime.Caption = "播放时间";
            dcPlayTime.ColumnEdit = tedtTime;

            ViewSetting.SetRowCellValue(0, dcCommand, cbxCommandKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcSoundSource, cbxSoundSource.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcVolume, "50");
            ViewSetting.SetRowCellValue(0, dcPlayOrder, cbxPlayOrder.Items[0].ToString());   
            ViewSetting.SetRowCellValue(0, dcPlayNum, "1");
            ViewSetting.SetRowCellValue(0, dcPlayTime, "00:00:00");

            ViewSetting.BestFitColumns();
        }

        /// <summary>
        /// 重置
        /// </summary>
        public override void ResetSetting()
        {
            
        }

        /// <summary>
        /// 生成指令数据
        /// </summary>
        /// <returns></returns>
        public override CommandData GetCommand()
        {
            return null;
        }



        public override void SetCommandData(CommandData data)
        {
           
        }
    }
}

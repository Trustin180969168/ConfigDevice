using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;

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
            dcCommand.Visible = true;
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
            cbxSoundSource.Items.Add(AudioConfig.NAME_CTRLP_BGMST_MP3);
            cbxSoundSource.Items.Add(AudioConfig.NAME_CTRLP_BGMST_RADIO);
            cbxSoundSource.Items.Add(AudioConfig.NAME_CTRLP_BGMST_AUX1);
            cbxSoundSource.Items.Add(AudioConfig.NAME_CTRLP_BGMST_AUX2);
            dcSoundSource.ColumnEdit = cbxSoundSource;

            dcVolume.Caption = "音量";
            dcVolume.ColumnEdit = edtPercentNum;

            dcPlayOrder.Caption = "播放方式";
            cbxPlayOrder.Items.Add(AudioConfig.NAME_CTRLP_PMD_PLY_ONE);
            cbxPlayOrder.Items.Add(AudioConfig.NAME_CTRLP_PMD_REP_ONE);
            cbxPlayOrder.Items.Add(AudioConfig.NAME_CTRLP_PMD_PLY_ALL);
            cbxPlayOrder.Items.Add(AudioConfig.NAME_CTRLP_PMD_REP_ALL);
            cbxPlayOrder.Items.Add(AudioConfig.NAME_CTRLP_PMD_SHUFFLE);
            cbxPlayOrder.Items.Add(AudioConfig.NAME_CTRLP_PMD_PLY_TOT);
            dcPlayOrder.ColumnEdit = cbxPlayOrder;

            edtNum.Mask.EditMask = "\\d+";
            edtNum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            edtNum.MaxValue = new decimal(new int[] { 255, 255, 0, 0 });
            edtNum.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
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

           // ViewSetting.BestFitColumns();
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
            ViewSetting.PostEditor();
            DataRow dr = ViewSetting.GetDataRow(0);
            byte[] command = Background.NameAndCommand[dr[dcCommand.FieldName].ToString()];//----播放命令--------            
            int volume = Convert.ToInt16(dr[dcVolume.FieldName].ToString());//-----音量----
            //----------音源-----------------
            int sourceIndex = 0;
            string actionName = dr[dcSoundSource.FieldName].ToString();
            sourceIndex = AudioConfig.AudioSourceNameID[actionName];
            //----------播放模式-----------------
            int kindIndex = 0;
            string kindName = dr[dcPlayOrder.FieldName].ToString();
            kindIndex = AudioConfig.AudioPlayModeNameID[kindName];
            int playNum = Convert.ToInt16(dr[dcPlayNum.FieldName]);//----播放曲目-----
            //----------计算时间-------------------
            DateTime dtRunTime = DateTime.Parse(dr[dcPlayTime.FieldName].ToString());
            int runTimeSeconds = dtRunTime.Hour * 60 * 60 + dtRunTime.Minute * 60 + dtRunTime.Second;//运行秒数
            if (runTimeSeconds > 64800)
            { CommonTools.MessageShow("运行时间不能大于18小时!", 2, ""); return null; }

            return background.GetCommandData(command, volume, sourceIndex, kindIndex, playNum, runTimeSeconds);

        }


        /// <summary>
        /// 显示指令数据
        /// </summary>
        /// <param name="data"></param>
        public override void SetCommandData(CommandData data)
        {
            //---找出对应的指令---------
            string cmdName = "";
            foreach (string key in Background.NameAndCommand.Keys)
            {
                if (CommonTools.BytesEuqals(data.Cmd, Background.NameAndCommand[key]))
                { cmdName = key; break; }
            }
            ViewSetting.SetRowCellValue(0, dcCommand, cmdName);//---命令名称---
            ViewSetting.SetRowCellValue(0, dcVolume, (int)data.Data[3]);//---音量---
            //---音源---
            int sourceIndex = (int)data.Data[4];
            ViewSetting.SetRowCellValue(0, dcSoundSource,AudioConfig.AudioSourceIDName[sourceIndex]);
            //---播放方式---
            int kindIndex = (int)data.Data[5];
            ViewSetting.SetRowCellValue(0, dcPlayOrder,AudioConfig.AudioPlayModeIDName[kindIndex]);
            //---曲目-----
            int playNum = ConvertTools.Bytes2ToInt(new byte[] { data.Data[6], data.Data[7] });
            ViewSetting.SetRowCellValue(0, dcPlayNum, playNum.ToString());
            //---播放时间-----
            int playTime = ConvertTools.Bytes2ToInt(new byte[] { data.Data[8], data.Data[9] });
            DataRow dr = ViewSetting.GetDataRow(0);
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcPlayTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(playTime).ToLongTimeString();
        }
    }
}

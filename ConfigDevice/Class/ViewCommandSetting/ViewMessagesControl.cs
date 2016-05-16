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
    /// 消息指令
    /// </summary>
    public class ViewMessagesControl : ViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcSoundSource;//音源
        GridColumn dcVolume;//音量
        GridColumn dcPlayOrder;//播放方式
        GridColumn dcPlayNum;//播放曲目
        GridColumn dcPlayCount;//播放次数
        Messages message;//消息
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxSoundSource;//选择音源   
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxPlayOrder;//选择播放方式

        public ViewMessagesControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            message = controlObj as Messages;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcSoundSource = ViewSetting.Columns.ColumnByName("parameter1");
            dcVolume = ViewSetting.Columns.ColumnByName("parameter2");
            dcPlayOrder = ViewSetting.Columns.ColumnByName("parameter3");
            dcPlayNum = ViewSetting.Columns.ColumnByName("parameter4");
            dcPlayCount = ViewSetting.Columns.ColumnByName("parameter5");

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
            dcPlayCount.Visible = true;
            ViewSetting.Columns.ColumnByName("parameter1").VisibleIndex = 6;
            ViewSetting.Columns.ColumnByName("parameter2").VisibleIndex = 7;
            ViewSetting.Columns.ColumnByName("parameter3").VisibleIndex = 8;
            ViewSetting.Columns.ColumnByName("parameter4").VisibleIndex = 9;
            ViewSetting.Columns.ColumnByName("parameter5").VisibleIndex = 10;

            cbxCommandKind.Items.Add(Messages.NAME_CMD_SWIT_ALL_MUSIC);
            cbxCommandKind.Items.Add(Messages.NAME_CMD_SWIT_OPEN_MUSIC);
            cbxCommandKind.Items.Add(Messages.NAME_CMD_SWIT_CLOSE_MUSIC);

            dcSoundSource.Caption = "音源";
            cbxSoundSource.Items.Add(Messages.CTRLP_MSSAGEST_EMC);
            cbxSoundSource.Items.Add(Messages.CTRLP_MESSAGEST_WINDOWS);
            dcSoundSource.ColumnEdit = cbxSoundSource;

            dcVolume.Caption = "音量";
            dcVolume.ColumnEdit = edtPercentNum;

            dcPlayOrder.Caption = "播放方式";
            cbxPlayOrder.Items.Add(Messages.CTRLP_PMD_SIMPLE_COUNT);
            cbxPlayOrder.Items.Add(Messages.CTRLP_PMD_SIMPLE_TIME);
            cbxPlayOrder.Items.Add(Messages.CTRLP_PMD_MULTI_COUNT);
            cbxPlayOrder.Items.Add(Messages.CTRLP_PMD_MULTI_TIME);
            cbxPlayOrder.Items.Add(Messages.CTRLP_PMD_MULTI_INVALID);
            dcPlayOrder.ColumnEdit = cbxPlayOrder;

            dcPlayNum.Caption = "曲目";
            dcPlayNum.ColumnEdit = edtNum;
            dcPlayCount.Caption = "播放次数";
            dcPlayCount.ColumnEdit = edtNum;

            ViewSetting.SetRowCellValue(0, dcCommand, cbxCommandKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcSoundSource, cbxSoundSource.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcVolume, "50");
            ViewSetting.SetRowCellValue(0, dcPlayOrder, cbxPlayOrder.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcPlayNum, "1");
            ViewSetting.SetRowCellValue(0, dcPlayCount, "1");

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
            ViewSetting.PostEditor();
            DataRow dr = ViewSetting.GetDataRow(0);
            byte[] command = Messages.NameAndCommand[dr[dcCommand.FieldName].ToString()];//----播放命令--------            
            int volume = Convert.ToInt16(dr[dcVolume.FieldName].ToString());//-----音量----
            //----------音源-----------------
            int sourceIndex = 0;
            string actionName = dr[dcSoundSource.FieldName].ToString();
            if (actionName == Messages.CTRLP_MSSAGEST_EMC)
                sourceIndex = 0;
            else if (actionName == Messages.CTRLP_MESSAGEST_WINDOWS)
                sourceIndex = 1;
            //----------播放模式-----------------
            int kindIndex = 0;
            actionName = dr[dcPlayOrder.FieldName].ToString();
            if (actionName == Messages.CTRLP_PMD_SIMPLE_COUNT)
                kindIndex = 0;
            else if (actionName == Messages.CTRLP_PMD_SIMPLE_TIME)
                kindIndex = 1;
            else if (actionName == Messages.CTRLP_PMD_MULTI_COUNT)
                kindIndex = 2;
            else if (actionName == Messages.CTRLP_PMD_MULTI_TIME)
                kindIndex = 3;
            int playNum = Convert.ToInt16(dr[dcPlayNum.FieldName]);//----播放曲目-----
            int playCount = Convert.ToInt16(dr[dcPlayCount.FieldName]);//---------播放次数-------------------
            return message.GetCommandData(command, volume, sourceIndex, kindIndex, playNum, playCount);
        }


        /// <summary>
        /// 显示指令数据
        /// </summary>
        /// <param name="data"></param>
        public override void SetCommandData(CommandData data)
        {
            //---找出对应的指令---------
            string cmdName = "";
            foreach (string key in Messages.NameAndCommand.Keys)
            {
                if (CommonTools.BytesEuqals(data.Cmd, Messages.NameAndCommand[key]))
                { cmdName = key; break; }
            }
            ViewSetting.SetRowCellValue(0, dcCommand, cmdName);//---命令名称---
            //---音源---
            int sourceIndex = (int)data.Data[5];
            ViewSetting.SetRowCellValue(0, dcSoundSource, cbxSoundSource.Items[sourceIndex]);
            //---播放方式---
            int kindIndex = (int)data.Data[6];
            ViewSetting.SetRowCellValue(0, dcPlayOrder, cbxPlayOrder.Items[kindIndex]);
            //---曲目-----
            int playNum = ConvertTools.Bytes2ToInt(new byte[] { data.Data[7], data.Data[8] });
            ViewSetting.SetRowCellValue(0, dcPlayNum, playNum.ToString());
            //---播放次数-----
            int playCount = ConvertTools.Bytes2ToInt(new byte[] { data.Data[9], data.Data[10] });
            ViewSetting.SetRowCellValue(0, dcPlayCount, playCount.ToString());
        }
    }
}
